using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;


public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Transform targetTransform;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        targetTransform = FindObjectOfType<XRRig>().transform;
        
    }

    
    void Update()
    {
        navMeshAgent.SetDestination(targetTransform.position);
    }
}
