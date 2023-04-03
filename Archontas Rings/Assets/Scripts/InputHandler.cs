using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

namespace KID
{
    public class InputHandler : MonoBehaviour
    {
        [Header("Movement")]
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        [Header("Inputs")]
        public bool b_input;
        public bool jump_input;
        public bool lightAttack_input;
        public bool flask_input;
        public bool pause_input;

        [Header("Flags")]
        public bool rollFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public bool jumpFlag;
        public bool attackFlag;
        public bool flaskFlag;

        [Header("Others")]
        public float rollInputTImer;
        
        
        PlayerControls inputActions;
        Animator anim;
        PlayerLocomotion locomotion;
        PlayerStats playerStats;

        Vector2 movementInput;
        Vector2 cameraInput;
        GameObject pauseCanvas;


        public void OnEnable()
        {
            anim = GetComponentInChildren<Animator>();
            locomotion = GetComponent<PlayerLocomotion>();
            playerStats = GetComponent<PlayerStats>();
            pauseCanvas = GameObject.Find("Pause Canvas");
            pauseCanvas.SetActive(false);

            {
                if(inputActions == null)
                {
                    inputActions = new PlayerControls();
                    inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                    inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
                }

                inputActions.Enable();

            }
        }

        public void OnDisable()
        {
            inputActions.Disable();
            vertical = 0f;
            horizontal = 0f;
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
            HandleRollInput(delta);
            JumpInput(delta);
            LightAndHeavyAttackInput(delta);
            HandleFlaskInut(delta);
            PauseGame();
        }

        private void PauseGame()
        {
            pause_input = inputActions.PlayerActions.Pause.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
            if (pause_input)
            {
                pauseCanvas.SetActive(true);
                Time.timeScale = 0;
            }
        }

        private void HandleFlaskInut(float delta)
        {
            flask_input = inputActions.PlayerActions.Flask.phase == UnityEngine.InputSystem.InputActionPhase.Performed;

            if (flask_input)
            {
                //print("YES");
                flaskFlag = true;
            }
            else
            {
                //print("NOO");
            }

        }

        private void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;

        }
    
        private void HandleRollInput(float delta)
        {
            b_input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
            if (b_input)
            {
                rollInputTImer += delta;
                sprintFlag = true;
            }
            else
            {
                if(rollInputTImer > 0 && rollInputTImer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }

                rollInputTImer = 0;
            }
        }

        private void JumpInput(float delta)
        {
            jump_input = inputActions.PlayerActions.Jump.phase == UnityEngine.InputSystem.InputActionPhase.Performed;

            if (jump_input)
                jumpFlag = true;
           
        }

        private void LightAndHeavyAttackInput(float delta)
        {
            lightAttack_input = inputActions.PlayerActions.Attack.phase == UnityEngine.InputSystem.InputActionPhase.Performed;

            if (lightAttack_input)
            {
                attackFlag = true;

                if (anim.GetBool("canDoCombo") && !comboFlag)
                {
                    comboFlag = true;
                    locomotion.HandleWeaponCombo();
                    comboFlag = false;
                }
                else
                {
                    locomotion.HandleAttack(delta);
                }
                
             
              
            }
               
        }
    }
}

