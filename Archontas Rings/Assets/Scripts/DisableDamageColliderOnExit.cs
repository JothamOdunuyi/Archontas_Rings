using KID;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class DisableDamageColliderOnExit : StateMachineBehaviour
{

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject weapon = animator.gameObject.GetComponent<CharacterStats>() ? animator.gameObject.GetComponent<CharacterStats>().weapon : animator.gameObject.GetComponentInParent<CharacterStats>().weapon;
        weapon.GetComponent<DamageCollider>().enabled = false;
        //Debug.Log("Disabled weapon " + weapon.GetComponent<DamageCollider>().enabled);
    }

   
}
