using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KID
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;
        PlayerStats playerStats;

        [Header("Player Flags")]
        public bool isInteracting;
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool isJumping;
        public bool canDoCombo;
        public bool isDrinking;
        public string lastAttack;

        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
        }


        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            playerStats = GetComponent<PlayerStats>();
        }

        void Update()
        {
            float delta = Time.deltaTime;

            isInteracting = anim.GetBool("isInteracting");
            isDrinking = anim.GetBool("isDrinking");
            canDoCombo = anim.GetBool("canDoCombo");

            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollingAndSprinting(delta);
            playerLocomotion.HandleJump(delta, anim.GetFloat("ColliderHeight"));
            playerLocomotion.HandleFlaskHeal(delta);
            //playerLocomotion.HandleAttack(delta);

            playerStats.RegenStamina(delta);

        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;


            if (cameraHandler != null && playerStats.health > 0)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }

            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);

        }

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            inputHandler.jumpFlag = false;
            inputHandler.attackFlag = false;
            inputHandler.comboFlag = false;
            inputHandler.flaskFlag = false;
            isSprinting = inputHandler.b_input;

            if (isInAir)
            {
                playerLocomotion.inAirTimer += Time.deltaTime;
            }

        }
    }

   

}
