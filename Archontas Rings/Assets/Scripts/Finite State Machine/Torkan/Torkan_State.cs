using KID;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public abstract class Torkan_State : MonoBehaviour
{

    [HideInInspector] public Torkan_StateMachine stateMachine;
    [HideInInspector] public NavMeshAgent navMesh;
    [HideInInspector] public Animator anim;
    // better to be here, saves performance than in statemachine
    [HideInInspector] public bool fixedDistance;


    public abstract StateBehaviour ThisStateType { get; }

    public void checkIfStateMachine(Torkan_StateMachine sentStateMachine)
    {
        if (!stateMachine)
        {
            stateMachine = sentStateMachine;
            anim = stateMachine.enemyAnimationManager.anim;
            navMesh = stateMachine.navMeshAgent;
        }

    }

    private IEnumerator ResetFixedDistance(float waiTime)
    {
        yield return new WaitForSeconds(waiTime);
        fixedDistance = false;
    }

    // If the distance between the navmesh object and the transform of the mob is too far, its likely the mob is going off the map, this resets its transform
    public void HandleRootMotionCorrection()
    {
        float distance = Vector3.Distance(stateMachine.transform.position, navMesh.transform.position);
        if (distance > 0.082 && !fixedDistance)
        {

            fixedDistance = true;

            stateMachine.enemyAnimationManager.PlayTargetAnimation("Locomotion", false, false);

            transform.position = navMesh.transform.position;

            stateMachine.navMeshAgent.transform.localPosition = Vector3.zero;
            stateMachine.navMeshAgent.transform.localRotation = Quaternion.identity;

            StartCoroutine(ResetFixedDistance(0f));
        }
    }
    public abstract StateBehaviour UpdateState();
    public abstract void ExitState();
    public abstract void EnterState(Torkan_StateMachine stateMachine);

    public void HandleRotationTowardsTarget(Torkan_StateMachine stateMachine)
    {
        {
            if (stateMachine.isPerformingAction)
            {
                Vector3 direction = stateMachine.currentTarget.transform.position - transform.position;
                direction.y = 0f;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                stateMachine.directionProgress += stateMachine.rotationSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, stateMachine.directionProgress);

            }
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(stateMachine.navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = stateMachine.enemyRigidBody.velocity;

                stateMachine.navMeshAgent.enabled = true;
                stateMachine.navMeshAgent.SetDestination(stateMachine.currentTarget.transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, stateMachine.navMeshAgent.transform.rotation, stateMachine.slerpAddition * Time.deltaTime);
                stateMachine.slerpAddition += stateMachine.navMeshAgent.angularSpeed;
            }

        }

        stateMachine.navMeshAgent.transform.localPosition = Vector3.zero;
        stateMachine.navMeshAgent.transform.localRotation = Quaternion.identity;
    }



}
