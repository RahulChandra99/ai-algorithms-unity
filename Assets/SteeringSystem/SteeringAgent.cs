using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAgent : MonoBehaviour
{
    public float Speed = 5f;
    public float AvoidSpeed = 200f;
    public Transform target;
    public Path path;

    private Rigidbody rb;
    private Sensor sensor;
    private int currentWaypoint = 0;
    private const float distanceToChangeWaypoint = 5f;

    private List<Transform> pathWaypoints => path?.WayPoints ?? new List<Transform>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sensor = GetComponent<Sensor>();
    }

    private void FixedUpdate()
    {
        float avoid = sensor.Check();
        if (avoid == 0)
        {
            StandardSteer();
        }
        else
        {
            AvoidSteer(avoid);
        }

        Move();
        CheckWaypoint();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + (transform.forward * Speed * Time.deltaTime));
    }

    private void StandardSteer()
    {
        if (pathWaypoints.Count == 0) return;

        Vector3 targetDirection = pathWaypoints[currentWaypoint].position - rb.position;
        targetDirection.y = 0; // Keep movement horizontal
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Time.fixedDeltaTime * Speed, 0);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void AvoidSteer(float avoid)
    {
        transform.Rotate(Vector3.up * AvoidSpeed * Time.fixedDeltaTime * avoid);
    }

    private void CheckWaypoint()
    {
        if (pathWaypoints.Count == 0) return;

        if (Vector3.Distance(rb.position, pathWaypoints[currentWaypoint].position) < distanceToChangeWaypoint)
        {
            currentWaypoint++;
            if (currentWaypoint >= pathWaypoints.Count)
            {
                currentWaypoint = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * 5));
    }
}
