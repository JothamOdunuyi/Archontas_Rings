using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace KID
{
    public class AnimationHandler : AnimatorManager
    {
        [SerializeField]
        private PlayerManager playerManager;
        InputHandler inputHandler;
        PlayerLocomotion playerLocomotion;
        PlayerStats playerStats;
        int vertical;
        int horizontal;
        public bool canRotate;

        public void Initialize()
        {
            playerStats = GetComponentInParent<PlayerStats>();
            playerManager = GetComponentInParent<PlayerManager>();
            anim = GetComponent<Animator>();
            inputHandler = GetComponentInParent<InputHandler>();
            playerLocomotion = GetComponentInParent<PlayerLocomotion>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
        {
            #region Vertical
            float v = 0;

            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }
            else if (verticalMovement > 0.55f)
            {
                v = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if (verticalMovement < -0.55f)
            {
                v = -1;
            }
            else
            {
                v = 0;
            }
            #endregion

            #region Horizontal
            float h = 0;

            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                h = -1;
            }
            else
            {
                h = 0;
            }
            #endregion

            if (isSprinting)
            {
                v = 2;
                h = horizontalMovement;
            }

            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
            anim.SetBool("isInAir", playerManager.isInAir);


        }

        public void CanRotate()
        {
            canRotate = true;
        }

        public void StopRotation()
        {
            canRotate = false;
        }
        
        public bool isAnimationPlaying(string targetAnim)
        {
            return anim.GetCurrentAnimatorStateInfo(0).IsName(targetAnim);
        }

        private void OnAnimatorMove()
        {
            if (!playerManager.isInteracting)
                return;

            float delta = Time.deltaTime;
            playerLocomotion.rigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            playerLocomotion.rigidbody.velocity = velocity;

        }

        private GameObject GetPlayerWeapon()
        {
            return GameObject.FindGameObjectWithTag("PlayerWeapon");
        }

        public void EnablePlayerWeaponCollider()
        {
            playerLocomotion.rigidbody.AddForce(playerLocomotion.transform.forward * 10, ForceMode.Impulse);

            GetPlayerWeapon().GetComponent<DamageCollider>().EnableDamageCollider();
        }

        public void DisablePlayerWeaponCollider()
        {
            GetPlayerWeapon().GetComponent<DamageCollider>().DisableDamageCollider();
        }

        public void EnableCombo()
        {
            anim.SetBool("canDoCombo", true);
        }

        public void DisableCombo()
        {
            anim.SetBool("canDoCombo", false);
        }

        public void UseStaminafloat (float staminaUsage)
        {
            playerStats.UseStamina(staminaUsage);
        }

    }
}
