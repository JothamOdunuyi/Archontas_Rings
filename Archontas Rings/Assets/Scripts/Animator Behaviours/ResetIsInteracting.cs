using KID;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetIsInteracting : StateMachineBehaviour
{
   
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Rolling") && animator.GetComponentInParent<PlayerManager>().isInAir == true)
        {
            return;
        }

        if (stateInfo.IsName("Jump"))
        {
            if (animator.GetComponentInParent<PlayerManager>().isInAir == true)
            {
                animator.GetComponent<AnimationHandler>().PlayTargetAnimation("Falling", true);
                return;
            }
        }

        animator.SetBool("isInteracting", false); 
    }

}
