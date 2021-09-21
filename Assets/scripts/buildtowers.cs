using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class buildtowers : MonoBehaviour
{
    [SerializeField] 
    private GameObject prefabObject;
    
    [SerializeField]
    private Renderer renderedObject;

    [SerializeField]
    private KeyCode getTower = KeyCode.A;
    
    [SerializeField]
    private KeyCode cancelAction = KeyCode.Escape;

    private GameObject currentPlaceableTower;
    private bool canPlace;
    void Start()
    {
        // retrieve's the prefab form asset folder
        prefabObject = (GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/prefab-test.prefab");
        Debug.Log(prefabObject);
        Debug.Log((GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/prefab-test.prefab"));
        renderedObject = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleObjectKey();

        if (currentPlaceableTower != null)
        {
            ObjectToMouse();
            if (canPlace == true)
            {
                ReleaseByClick();
            }
        }
    }

    private void ReleaseByClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            renderedObject.material.color = Color.white;
            currentPlaceableTower.GetComponent<BoxCollider>().enabled = true;
            currentPlaceableTower = null;
        }
    }
    private void ObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask tower = LayerMask.GetMask("tower");
        LayerMask path = LayerMask.GetMask("path");

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            currentPlaceableTower.transform.position = new Vector3(hit.point.x, hit.point.y + prefabObject.transform.position.y, hit.point.z);
            currentPlaceableTower.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            // use overlap function here to check colliders in raduis.
            if (hit.collider.gameObject.CompareTag("tower") || hit.collider.gameObject.CompareTag("path"))
            {
                renderedObject.material.color = Color.red;
                canPlace = false;
            }
            else
            {
                renderedObject.material.color = Color.green;
                canPlace = true;
            }

        }
    }
    
    private void HandleObjectKey()
    {
        if (Input.GetKeyDown(getTower))
        {
            if (currentPlaceableTower != null)
                return;
            currentPlaceableTower = Instantiate(prefabObject);
            renderedObject = currentPlaceableTower.GetComponent<Renderer>();
        }

        if (Input.GetKeyDown(cancelAction))
        {
            Destroy(currentPlaceableTower);
        }
    }
}
