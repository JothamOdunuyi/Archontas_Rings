using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class StationaryState : State
{
    [Header("Stationary Settings")]
    [SerializeField] private float miniTime = .2f, maxTime = .65f, stationaryTime;

    public override void EnterState(StateMachine sentStateMachine)
    {
        checkIfStateMachine(sentStateMachine);
        stateMachine.enemyAnimationManager.PlayTargetAnimationWithNoDelay("Locomotion", false, false);
        //stateMachine.navMeshAgent.ResetPath();
        stationaryTime = Random.Range(miniTime, maxTime);
        //print("Stationary time is: " + stationaryTime);
        stateMachine.enemyAnimationManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);

    }

    public override void ExitState()
    {
        //print("Stationary ended!");
    }

    public override State UpdateState()
    {
        //transform.LookAt(stateMachine.currentTarget.transform.position);
        HandleRootMotionCorrection();
        HandleRotationTowardsTarget(stateMachine);
        stationaryTime -= Time.deltaTime;

        if(stationaryTime <= 0)
        {
            int g = Random.Range(1, 6);
            if(g == 1)
            {
                return strafeState;
            }
            else
            {
                return chaseState;
            }
        }

        if (stateMachine.distanceFromTarget > navMesh.stoppingDistance + .4)
        {
            stateMachine.enemyAnimationManager.anim.SetFloat("Vertical", .5f, 0.1f, Time.deltaTime);
   
        }
        else if (stateMachine.canAttack)
        {
            int u = Random.Range(1, 6);
            if (u == 1) { 
                stateMachine.enemyAnimationManager.PlayTargetAnimation("Light Attack", true, false);
                stateMachine.canAttack = false;}
        }

        return this;
    }

}
