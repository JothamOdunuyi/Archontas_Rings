using KID;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetIsDrinking : StateMachineBehaviour
{

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isDrinking", false);
    }
}
