using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace MobStates
{
    namespace Torkan
    {
        public class StationaryState : AttackBaseState
        {
            public override StateBehaviour ThisStateType => StateBehaviour.Stationary;

            [Header("Stationary Settings")]
            [SerializeField] private float miniTime = .2f, maxTime = .65f, stationaryTime;

            public override void EnterState(StateMachine sentStateMachine)
            {
                //stateMachine.navMeshAgent.ResetPath();
                stationaryTime = Random.Range(miniTime, maxTime);
                //print("Stationary time is: " + stationaryTime);
                stateMachine.enemyAnimationManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);

            }

            public override void ExitState()
            {
                //print("Stationary ended!");
            }

            public override StateBehaviour UpdateState()
            {
                //transform.LookAt(stateMachine.currentTarget.transform.position);
                HandleRootMotionCorrection();
                HandleRotationTowardsTarget(stateMachine);
                if (!stateMachine.isPerformingAction)
                {
                    stationaryTime -= Time.deltaTime;
                }
                else
                {
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion") && !anim.GetBool("isInteracting"))
                    {
                        stateMachine.enemyAnimationManager.PlayTargetAnimation("Locomotion", false, false);
                    }
              
                }


                if (stationaryTime <= 0)
                {
                    int g = Random.Range(1, 6);
                    if(g == 1)
                    {
                        return StateBehaviour.Strafe;
                    }
                    else
                    {
                        return StateBehaviour.Chase;
                    }
                }

                if (stateMachine.distanceFromTarget > navMesh.stoppingDistance + .4)
                {
                    stateMachine.enemyAnimationManager.anim.SetFloat("Vertical", .5f, 0.1f, Time.deltaTime);

                }
                else
                {
                    ChanceAttack(1, 125, new string[] { "Poke", "Wegihted Poke", "Double Spin", "Tusk Attack" });
                }

                return ThisStateType;
            }

        }
    }
}
