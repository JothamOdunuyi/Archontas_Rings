using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


namespace MobStates
{
    namespace Torkan
    {
        public class StrafeState : AttackBaseState
        {
            public override StateBehaviour ThisStateType => StateBehaviour.Strafe;

            [Header("Strafe Settings")]
            [SerializeField] private float miniStrafeTime = .2f, maxStrafeTime = 1f, strafeTime;

            [HideInInspector]
            [Header("Strafe Settings")]
            private Transform player;
            private float lerpAddition;
            private bool strafing;

            public enum StrafeDirection{
                Right,
                Left,
                Back
            }

            int strafeDirection;


            public override void EnterState(StateMachine sentStateMachine)
            {
                player = stateMachine.currentTarget.transform;
                //HandleRotationTowardsTarget(stateMachine);
                //transform.LookAt(player);
                strafing = false;
                fixedDistance = false;
                strafeTime = Random.Range(miniStrafeTime, maxStrafeTime);
                stateMachine.enemyAnimationManager.anim.SetFloat("Vertical", 0, 0.5f, Time.deltaTime);

                strafeDirection = Random.Range(1, 2);

                if (strafeDirection == 1)
                {
                    stateMachine.enemyAnimationManager.PlayTargetAnimation("Right Strafe", false, true);
                    //print("Right Strafe!");
                }
                else
                {
                    stateMachine.enemyAnimationManager.PlayTargetAnimation("Left Strafe", false, true);
                }



                //print("Strafe time is " + strafeTime);
            }

            public void SetStrafeDirection(StrafeDirection strafeDirection)
            {

            }


            public override void ExitState()
            {
       
            }

            public override StateBehaviour UpdateState()
            {
                if (strafeTime > 0)
                {

                    if (!strafing)
                    {
                        strafing = true;
                        // Unity for some reason minuses maxmum range by 1 when using an INT for random
                       
                      
                    }

                    HandleRootMotionCorrection();
       
                    strafeTime -= Time.deltaTime;



                    return ThisStateType;
               } else {
                    //stateMachine.enemyAnimationManager.PlayTargetAnimation("Locomotion", false, false);
                    strafing = false;

                    int g = Random.Range(1, 6);
                    if(g <= 3)
                    {
                        return StateBehaviour.Chase;
                    }
                    else
                    {
                        return StateBehaviour.Stationary;
                    }
           
                }

            }


        }
    }
}
