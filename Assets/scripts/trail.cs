using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class trail : MonoBehaviour
{
    private float _speed = 10.0f;
    private float _triggerDistance = 0.1f;
    [SerializeField] private Path path;
    private Transform _currentWaypoint;
    private TrailRenderer trailRend;

    private void Start()
    {
        SetupTrail();
        trailRend = GetComponent<TrailRenderer>();
    }
    
    private void SetupTrail()
    {
        path = FindObjectOfType<Path>();
        _currentWaypoint = path.GetStartPath();
        transform.position = _currentWaypoint.transform.position;
        transform.LookAt(_currentWaypoint);
    }
    
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
        _speed = 0;
        //_currentWaypoint = path.GetStartPath();
        //transform.position = _currentWaypoint.transform.position;
        //transform.LookAt(_currentWaypoint);
        //trailRend.Clear();
    }
}
