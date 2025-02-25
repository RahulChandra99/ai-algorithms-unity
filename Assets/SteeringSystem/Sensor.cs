using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    private const float sensorLength = 2.5f;
    private const float frontSensorStartingPoint = 1f;
    private const float frontSideSensorStartingPoint = 0.5f;
    private const float frontSensorAngle = 25f;

    public float Check()
    {
        RaycastHit hit;
        float avoidDirection = 0;

        Vector3 frontPosition = transform.position + (transform.forward * frontSensorStartingPoint);

        if (DrawSensors(frontPosition, transform.forward, sensorLength * 2, out hit))
        {
            avoidDirection = (hit.normal.x < 0) ? -0.25f : 0.25f;
        }

        avoidDirection -= FrontSideSensors(frontPosition, out hit, 1);
        avoidDirection += FrontSideSensors(frontPosition, out hit, -1);

        return avoidDirection;
    }

    private bool DrawSensors(Vector3 sensorPosition, Vector3 direction, float length, out RaycastHit hit)
    {
        if (Physics.Raycast(sensorPosition, direction, out hit, length))
        {
            Debug.DrawLine(sensorPosition, hit.point, Color.black);
            return true;
        }
        return false;
    }

    private float FrontSideSensors(Vector3 frontPosition, out RaycastHit hit, float sensorDirection)
    {
        float avoidDirection = 0;
        Vector3 sensorPosition = frontPosition + (transform.right * frontSideSensorStartingPoint * sensorDirection);
        Vector3 sensorAngle = Quaternion.AngleAxis(frontSensorAngle * sensorDirection, transform.up) * transform.forward;

        if (Physics.Raycast(sensorPosition, sensorAngle, out hit, sensorLength))
        {
            avoidDirection = 1f;
            Debug.DrawLine(sensorPosition, hit.point, Color.black);
        }
        else if (Physics.Raycast(sensorPosition, transform.forward, out hit, sensorLength))
        {
            avoidDirection = 0.5f;
            Debug.DrawLine(sensorPosition, hit.point, Color.black);
        }

        return avoidDirection;
    }
}
