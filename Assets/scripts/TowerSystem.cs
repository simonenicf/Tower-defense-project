using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

// General System for placing and getting towers
public class TowerSystem : MonoBehaviour
{
    // variable's in script
    [SerializeField] private Renderer renderedObject;
    //[SerializeField] private KeyCode getTower = KeyCode.A;
    [SerializeField] private KeyCode cancelAction = KeyCode.Escape;
    private bool _canPlace;
    private GameObject _currentPlaceableTower;

    private bool hasObj;
    // Is used by an other script
    [SerializeField] private GameObject prefabObject;
    
    // Access to other script to use getter's and setter's
    private LoadTower loadTowerS; // uses _buildMode of this script

    private void Awake()
    {
        prefabObject = GetComponent<GameObject>();
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
        if (Input.GetKeyDown(cancelAction) && _currentPlaceableTower != null)
        {
            Destroy(_currentPlaceableTower);
            prefabObject = null;
            hasObj = false;
        }
        if (prefabObject == null) return;
        if (!hasObj)
        {
            CreateTower();
        }
        if (_currentPlaceableTower == null) return;
        ObjectToMouse();
        if (_canPlace == true)
        {
            ReleaseByClick();
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
            prefabObject = null;
            hasObj = false;
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

    private void CreateTower()
    {
        _currentPlaceableTower = Instantiate(prefabObject);
        renderedObject = _currentPlaceableTower.GetComponent<Renderer>();
        hasObj = true;
    }
    
}
