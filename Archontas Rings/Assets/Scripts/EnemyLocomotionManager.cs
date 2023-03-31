using KID;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotionManager : MonoBehaviour
{
    EnemyManager enemyManager;
    public EnemyAnimatorManager enemyAnimationManager;
    public Rigidbody enemyRigidBody;
    public LayerMask detectionLayer;
    public CharacterStats currentTarget;

    public NavMeshAgent navMeshAgent;

    public float distanceFromTarget;
    public float stoppingDistance = 1.1f;
    public float meleeDistance = 1.1f;
    public float rotationSpeed  = 25f;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimationManager = GetComponent<EnemyAnimatorManager>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        enemyRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        navMeshAgent.enabled = false;
        enemyRigidBody.isKinematic = false;
    }

    public void HandleDetection()
    {
       
    }

    public void HandleMoveToTarget()
    {
       

    }

    public void HandleRotationTowardsTarget()
    {
        if (enemyManager.isPerformingAction)
        {
            Vector3 direction = currentTarget.transform.position - transform.position;
            direction.y = 0f;
            direction.Normalize();

            if(direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed / Time.deltaTime);
            
        }
        else
        {
            Vector3 relativeDirection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyRigidBody.velocity;

            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(currentTarget.transform.position);
            enemyRigidBody.velocity = targetVelocity;
            transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
        }

    }
}