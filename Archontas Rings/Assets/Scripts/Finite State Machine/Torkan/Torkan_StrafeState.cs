using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Torkan_StrafeState : Torkan_AttackBaseState
{
    public override StateBehaviour ThisStateType => StateBehaviour.Strafe;

    [Header("Strafe Settings")]
    [SerializeField] private float miniStrafeTime = .2f, maxStrafeTime = 1f, strafeTime;

    [HideInInspector]
    [Header("Strafe Settings")]
    private Transform player;
    private float lerpAddition;
    private bool strafing;

    public enum StrafeDirection
    {
        Right,
        Left,
        Back
    }

    StrafeDirection strafeDirection;


    public override void EnterState(Torkan_StateMachine sentStateMachine)
    {
        player = stateMachine.currentTarget.transform;
        //HandleRotationTowardsTarget(stateMachine);
        //transform.LookAt(player);
        strafing = false;
        fixedDistance = false;
        strafeTime = Random.Range(miniStrafeTime, maxStrafeTime);
        stateMachine.enemyAnimationManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);



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
                // Unity for some reason minuses maxmum range by 1 when using an INT for random
                strafeDirection = (StrafeDirection)Random.Range(1, 4);

                if (strafeDirection == StrafeDirection.Right)
                {
                    stateMachine.enemyAnimationManager.PlayTargetAnimation("Right Strafe", false, false);
                    //print("Right Strafe!");
                }
                else if (strafeDirection == StrafeDirection.Left)
                {
                    stateMachine.enemyAnimationManager.PlayTargetAnimation("Left Strafe", false, false);
                }
                else
                {
                    stateMachine.enemyAnimationManager.PlayTargetAnimation("Back Strafe", false, false);
                }
                strafing = true;
            }

            HandleRootMotionCorrection();

            strafeTime -= Time.deltaTime;

            // Addition needs to be here otherwise navmesh will distance from player too much
            if (stateMachine.distanceFromTarget > navMesh.stoppingDistance + .4)
            {
                stateMachine.enemyAnimationManager.anim.SetFloat("Vertical", .5f, 0.1f, Time.deltaTime);

            }
            ChanceAttack();



            return ThisStateType;
        }
        else
        {
            //stateMachine.enemyAnimationManager.PlayTargetAnimation("Locomotion", false, false);


            int g = Random.Range(1, 5);
            if (g <= 3)
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
