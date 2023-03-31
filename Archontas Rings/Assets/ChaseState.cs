using KID;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{

    [Header("Configuration Values")]
    [SerializeField] float minStrafeDistance = 1.5f;
    [SerializeField] float minChaseStateTime = .45f;
    private float chaseTime;


    public override void EnterState(StateMachine sentStateMachine)
    {
        checkIfStateMachine(sentStateMachine);
        stateMachine.enemyAnimationManager.PlayTargetAnimation("Locomotion", false, false);
        chaseTime = minChaseStateTime;
    }

    public override void ExitState()
    {
       // Debug.Log("Stopped chasing!");
    }

    public override State UpdateState()
    {

        Vector3 targetDirection = stateMachine.currentTarget.transform.position - transform.position;
        stateMachine.distanceFromTarget = Vector3.Distance(stateMachine.currentTarget.transform.position, transform.position);
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

        // stop removing if performing action
        if (stateMachine.isPerformingAction)
        {
            stateMachine.enemyAnimationManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            stateMachine.navMeshAgent.enabled = false;
        }
        else
        {

            if(chaseTime <= 0)
            {
                int g = Random.Range(1, 200);
                if (g == 1)
                {
                    //print("STATIOATNATATA");
                    return stationaryState;
                }

                if (stateMachine.distanceFromTarget <= minStrafeDistance)
                {
                    int s = Random.Range(1, 250);
                    if (s == 1)
                        return strafeState;

                }
            }
           

            // Addition needs to be here otherwise navmesh will distance from player too much
            if (stateMachine.distanceFromTarget > navMesh.stoppingDistance+.4)
            {
                stateMachine.enemyAnimationManager.anim.SetFloat("Vertical", 1f, 0.1f, Time.deltaTime);

            }
            else  //(stateMachine.distanceFromTarget <= navMesh.stoppingDistance)
            {
                stateMachine.enemyAnimationManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                if (stateMachine.canAttack)
                {
                    int u = Random.Range(1, 5);
                    if (u == 1) { 
                        stateMachine.enemyAnimationManager.PlayTargetAnimation("Light Attack", true, false);
                        stateMachine.canAttack = false;}
                }
                
            }

            HandleRootMotionCorrection();

        }

        chaseTime -= Time.deltaTime;
        HandleRotationTowardsTarget(stateMachine);

        return this;
    }




}