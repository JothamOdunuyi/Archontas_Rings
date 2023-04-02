using KID;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

namespace MobStates
{
    namespace Skeletor
    {
        public class IdleState : State
        {

            public override StateBehaviour ThisStateType => StateBehaviour.Idle;

            public override void EnterState(StateMachine sentStateMachine)
            {

            }

            public override void ExitState()
            {

            }


            public override StateBehaviour UpdateState()
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
                            return StateBehaviour.Chase;
                        }

                    }

                }
                if (stateMachine.enemyStats.health < stateMachine.enemyStats.maxHealth)
                {
                    stateMachine.currentTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
                    return StateBehaviour.Chase;
                }
                return ThisStateType;
            }



        }
    }
}
