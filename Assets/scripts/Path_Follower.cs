using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/* need to do:
 fix object stuck in ground
 add multiple options of paths
 add damage to player
 */
public class Path_Follower : MonoBehaviour
{
    private float _speed = 3.0f;
    private float _triggerDistance = 0.1f;
    
    [SerializeField] private Path path;
    private Transform _currentWaypoint;

    public float speedOfWalker
    {
        get { return _speed; }
        set { _speed = value; }
    }
    
    
    // Start is called before the first frame update
    private void Start()
    {
        SetupPath();
    }
    private void SetupPath()
    {
        path = FindObjectOfType<Path>();
        _currentWaypoint = path.GetStartPath();
        transform.position = _currentWaypoint.transform.position;
        transform.LookAt(_currentWaypoint);
    }
    
    // Update is called once per frame
    void Update()
    {
        float wayPointDistance = Vector3.Distance(transform.position, _currentWaypoint.transform.position);

        if (wayPointDistance <= _triggerDistance)
        {
            if (_currentWaypoint == path.GetEndPath())
            {
                PathCompleted();
            }
            else
            {
                _currentWaypoint = path.getNextWaypoint(_currentWaypoint);
                transform.LookAt(_currentWaypoint.transform.position);
            }
        }

        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void PathCompleted()
    {
        Debug.Log("yeah end me");
        _speed = 0f;
        
        // player takes damage part here
        
        Destroy(gameObject);
    }
}
