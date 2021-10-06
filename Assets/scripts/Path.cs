using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/* need to do:
 Add a box collider form each waypoint so that you can't place towers on waypoint path.
 */
public class Path : MonoBehaviour
{
    // Variables of this class
    // future switch waypoints out for vector 3 array
    [SerializeField] private Transform[] waypoints;

    public Transform GetStartPath()
    {
        return waypoints[0];
    }

    public Transform GetEndPath()
    {
        return waypoints[waypoints.Length - 1];
    }

    public Transform getNextWaypoint(Transform _currentWaypoint)
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] == _currentWaypoint)
            {
                Debug.Log("Waypoint reached");
                return waypoints[i + 1];
            }
        }
        return null;
    }
}
