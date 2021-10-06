using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
// General System for placing and getting towers
public class TowerSystem : MonoBehaviour
{
    [SerializeField] 
    private GameObject prefabObject;
    
    [SerializeField]
    private Renderer renderedObject;

    [SerializeField]
    private KeyCode getTower = KeyCode.A;
    
    [SerializeField]
    private KeyCode cancelAction = KeyCode.Escape;
    
    private GameObject _currentPlaceableTower;
    private bool _canPlace;
    void Start()
    {
        // retrieve's the prefab form asset folder
        prefabObject = (GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/Towers/Base_tower.prefab");
        Debug.Log(prefabObject);
        Debug.Log((GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/Towers/Base_tower.prefab"));
        renderedObject = GetComponent<Renderer>();

    }

    public GameObject GetPrefab()
    {
        return prefabObject;
    }
    
    public void SetPrefab(GameObject obj)
    {
        prefabObject = obj;
    }
    
    // Update is called once per frame
    void Update()
    {
        HandleObjectKey();

        if (_currentPlaceableTower != null)
        {
            ObjectToMouse();
            if (_canPlace == true)
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
            _currentPlaceableTower.GetComponent<Collider>().enabled = true;
            _currentPlaceableTower.transform.GetChild(0).gameObject.SetActive(false);
            _currentPlaceableTower = null;
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
            _currentPlaceableTower.transform.position = new Vector3(hit.point.x, hit.point.y + prefabObject.transform.position.y, hit.point.z);
            _currentPlaceableTower.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            // use overlap function here to check colliders in raduis.
            Collider[] hitColliders = Physics.OverlapBox(hit.point,new Vector3(1,1,1), Quaternion.identity);
            int i = 0;
            bool overlap = false;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].gameObject.CompareTag("tower") || hit.collider.gameObject.CompareTag("path"))
                {
                    overlap = true;
                }
                i++;
            }
            if (overlap == true) {    
                renderedObject.material.color = Color.red;
                _canPlace = false;
            }
            else
            {
                renderedObject.material.color = Color.green;
                _canPlace = true;
            }
        }
    }
    
    private void HandleObjectKey()
    {
        if (Input.GetKeyDown(getTower))
        {
            if (_currentPlaceableTower != null)
                return;
            _currentPlaceableTower = Instantiate(prefabObject);
            renderedObject = _currentPlaceableTower.GetComponent<Renderer>();
        }

        if (Input.GetKeyDown(cancelAction))
        {
            Destroy(_currentPlaceableTower);
        }
    }
}
