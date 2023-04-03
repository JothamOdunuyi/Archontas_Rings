using KID;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class WeaponTrailHandle : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    public bool isEnemy;
    public bool OnEnter = true;
    public bool OnExit = true;

    GameObject trail;

    private void SetTrailObj(Animator animator)
    {

        if (isEnemy)
        {
            trail = animator.GetComponentInParent<EnemyStats>().weapon.transform.GetChild(0).gameObject;
        }
        else
        {
            trail = animator.GetComponentInParent<PlayerLocomotion>().trailEffect.gameObject;
        }
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!trail)
            SetTrailObj(animator);

        if (trail.active)
            return;
        

        if (OnEnter)
        {
            if (isEnemy)
            {
                trail.SetActive(true);
            }
            else
            {
                trail.SetActive(true);
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (OnExit)
        {
            if (isEnemy)
            {
                trail.SetActive(false);
            }
            else
            {
                trail.SetActive(false);
            }
        }
    }




    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
