using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementScript : MonoBehaviour
{
    public GameObject target;
    NavMeshAgent agent;

    void Awake()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (!agent.hasPath)
        {
            if (Input.GetKey(KeyCode.W))
            {
                SetAgentPosition();
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                transform.Rotate(0, -90, 0);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                transform.Rotate(0, 90, 0);
            }
        }
    }

    void SetAgentPosition()
    {
        // Define the starting point of the ray
        Vector3 rayOrigin = transform.position;

        // Define the direction of the ray (from current position to another position)
        Vector3 rayDirection = (target.transform.position - transform.position).normalized;

        // Define the length of the ray (distance from starting point to target)
        float rayLength = Vector3.Distance(transform.position, target.transform.position);

        // Perform the raycast and get all hits
        RaycastHit[] hits = Physics.RaycastAll(rayOrigin, rayDirection, rayLength);

        if (hits.Length < 2)
        {
            agent.SetDestination(target.transform.position);
        }





    }
}
