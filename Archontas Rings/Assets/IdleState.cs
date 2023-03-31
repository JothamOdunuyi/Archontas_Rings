using KID;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class IdleState : State
{


    public override void EnterState (StateMachine sentStateMachine)
    {
        print("Entered state idlee");
        checkIfStateMachine(sentStateMachine);
        stateMachine.navMeshAgent.enabled = true;
    }

    public override void ExitState()
    {
        Debug.Log("Showing HP bar");
    }


    public override State UpdateState()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, stateMachine.detectionRadius, stateMachine.detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterStats characterStats = colliders[i].GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                Vector3 targetDirection = characterStats.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if (viewableAngle > stateMachine.minimumDetectionAngle && viewableAngle < stateMachine.maximumDetectionAngle)
                {
                    stateMachine.currentTarget = characterStats;
                    //print("found character");
                    return chaseState ;
                }
               
            }

        }
        return this;
    }

    
   
}
