using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Transform> WayPoints;

    [ContextMenu("Create Path")]
    void CreatePath()
    {
        WayPoints = new List<Transform>(GetComponentsInChildren<Transform>());
        WayPoints.Remove(this.transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (WayPoints != null && WayPoints.Count > 1)
        {
            for (int i = 1; i < WayPoints.Count; i++)
            {
                Gizmos.DrawLine(WayPoints[i - 1].position, WayPoints[i].position);
            }
            Gizmos.DrawLine(WayPoints[0].position, WayPoints[WayPoints.Count - 1].position);
        }
    }
}