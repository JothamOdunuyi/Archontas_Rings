using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KID
{ 
    public class AnimatorManager : MonoBehaviour
    {
        public Animator anim;
        public GameObject enemyWeapon;

        public void PlayTargetAnimation(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }

        public void PlayTargetAnimation(string targetAnim, bool isInteracting, bool setRootMotion)
        {
            anim.applyRootMotion = setRootMotion;
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFade(targetAnim, 0.2f);            
        }

        public void PlayTargetAnimation(string targetAnim, ref bool isInteracting, bool setBool, bool setRootMotion)
        {
            print("Ref called");
            isInteracting = setBool;
            anim.applyRootMotion = setRootMotion;
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }


        public void PlayTargetAnimationWithNoDelay(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("isInteracting", isInteracting);
            anim.Play(targetAnim);
        }

        public void PlayTargetAnimationWithNoDelay(string targetAnim, bool isInteracting, bool setRootMotion)
        {
            anim.applyRootMotion = setRootMotion;
            anim.SetBool("isInteracting", isInteracting);
            anim.Play(targetAnim);
        }
    }
}
