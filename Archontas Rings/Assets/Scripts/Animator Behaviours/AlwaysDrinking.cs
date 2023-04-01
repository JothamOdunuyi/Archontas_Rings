using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysDrinking : StateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isDrinking", true);
    }
}
