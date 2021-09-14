using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildtowers : MonoBehaviour
{
    [SerializeField] 
    private GameObject prefabObject;
    
    [SerializeField]
    private KeyCode getTower = KeyCode.A;
    [SerializeField]
    private KeyCode cancelAction = KeyCode.Escape;

    private GameObject currentPlaceableTower;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleObjectKey();

        if (currentPlaceableTower != null)
        {
            ObjectToMouse();
            ReleaseByClick();
        }
    }

    private void ReleaseByClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPlaceableTower = null;
        }
    }
    private void ObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            currentPlaceableTower.transform.position = new Vector3(hit.point.x, hit.point.y + prefabObject.transform.position.y, hit.point.z);
            currentPlaceableTower.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }
    
    private void HandleObjectKey()
    {
        if (Input.GetKeyDown(getTower))
        {
            if (currentPlaceableTower != null)
                return;
            currentPlaceableTower = Instantiate(prefabObject);
        }
    }
}
