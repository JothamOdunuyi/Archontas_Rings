using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KID
{
    public class PlayerLocomotion : MonoBehaviour
    {
        [SerializeField]
        PlayerManager playerManager;
        PlayerStats playerStats;
        Animator anim;
        AudioManager audioManager;
        [SerializeField] AudioSource footsteps;
        [SerializeField] AudioSource sprintFootsteps;

        Transform cameraObject;
        private InputHandler inputHandler;

        private Vector3 m_moveDirection;
        public Vector3 moveDirection { get
            {
                m_moveDirection = cameraObject.forward * inputHandler.vertical + cameraObject.right * inputHandler.horizontal;
                m_moveDirection.Normalize();
                m_moveDirection.y = 0;

                return m_moveDirection;
            }

        }
        

        [HideInInspector]
        public Transform myTransform;

        [HideInInspector]
        public AnimationHandler animationHandler;

        public new Rigidbody rigidbody;
        [Tooltip("Mad")]
        public CapsuleCollider capsuleCollider;
        public GameObject normalCamera;

        [Header("Movement Stats")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float sprintSpeed = 7;
        [SerializeField]
        float jumpForce = 45;
        [SerializeField]
        float rotationSpeed = 10;
        [SerializeField]
        float fallingSpeed = 45;

        [Header("Group and Air Detection Stats")]
        [SerializeField]
        Vector3 groundDetectionRayStartPoint = new Vector3 (0,0.5f,0);
        [SerializeField]
        float minimumDistanceNeededToBeginFall = 1f;
        [SerializeField]
        float groundDirectionRayDistance = 0.2f;
        LayerMask ignoreForGroundCheck;
        public float inAirTimer;
      



        void Start()
        {
            playerManager = GetComponent<PlayerManager>();
            rigidbody = GetComponent<Rigidbody>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            animationHandler = GetComponentInChildren<AnimationHandler>();
            inputHandler = GetComponent<InputHandler>();
            playerStats = GetComponent<PlayerStats>();
            anim = GetComponentInChildren<Animator>();

            cameraObject = Camera.main.transform;
            myTransform = transform;
            animationHandler.Initialize();

            playerManager.isGrounded = true;
            ignoreForGroundCheck = ~(1 << 8 | 1 << 11);
            audioManager = GameObject.FindGameObjectWithTag("Audio Manager").GetComponent<AudioManager>();

        }

        private Vector3 GetMoveDirection()
        {
            return cameraObject.forward * inputHandler.vertical + cameraObject.right * inputHandler.horizontal;
        }

        private void FaceMoveDirection()
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            myTransform.rotation = targetRotation;
        }
    
        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;

        public void HandleRotation(float delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputHandler.moveAmount;

            targetDir = cameraObject.forward * inputHandler.vertical;
            targetDir += cameraObject.right * inputHandler.horizontal;
            targetDir.Normalize();
            targetDir.y = 0;

            if(targetDir == Vector3.zero)
                targetDir = myTransform.forward;

            float rs = rotationSpeed;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

            myTransform.rotation = targetRotation;
        }

        public void HandleMovement(float delta)
        {
            if (inputHandler.rollFlag || playerManager.isInteracting)
            {
                footsteps.enabled = false;
                sprintFootsteps.enabled = false;
                return;
            }
               

            
            float speed = movementSpeed;
            if (inputHandler.sprintFlag)
            {
                if(!animationHandler.anim.GetBool("isDrinking"))
                    speed = sprintSpeed;

                if (inputHandler.moveAmount > 0 && !playerManager.isDrinking)
                {
                    playerManager.isSprinting = true;
                }
                else
                {
                    playerManager.isSprinting = false;
                }
               
            }
           
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection * speed, normalVector);
            rigidbody.velocity = projectedVelocity;

            animationHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0, playerManager.isSprinting);

            if (inputHandler.moveAmount > 0 && !inputHandler.rollFlag && !playerManager.isInteracting)
            {
                if (playerManager.isSprinting)
                {
                    sprintFootsteps.enabled = true;
                    footsteps.enabled = false;
                }
                else
                {
                    footsteps.enabled = true;
                    sprintFootsteps.enabled = false;
                }
                
            }
            else
            {
                footsteps.enabled = false;
                sprintFootsteps.enabled = false;
            }
        
           
            if (animationHandler.canRotate)
            {
                HandleRotation(delta);
            }

        
        }
        #endregion

        public void HandleJump(float delta, float colliderCenter)
        {

            if (colliderCenter > 0 )
                capsuleCollider.center = new Vector3(0, colliderCenter, 0);
                

            if (playerManager.isInteracting || animationHandler.anim.GetBool("isDrinking"))
                return;

            if (inputHandler.jumpFlag && playerManager.isGrounded)
            {
                animationHandler.PlayTargetAnimation("Jump", true);
                audioManager.PlaySound("Player Jump", .2f);
                playerManager.isGrounded = false;
                Quaternion jumpRotation = Quaternion.LookRotation(moveDirection);
                myTransform.rotation = jumpRotation;

            }
        }

        // Has nothing to do with sprinting
        public void HandleRollingAndSprinting(float delta)
        {
            if (animationHandler.anim.GetBool("isDrinking") || animationHandler.anim.GetBool("isInteracting") && !playerManager.canDoCombo || playerStats.stamina < 10 || playerManager.isRolling)
                return;

            if (inputHandler.rollFlag)
            {
                inputHandler.rollFlag = false;
                playerManager.canDoCombo = false;
                playerManager.isRolling = true;
                anim.SetBool("isInteracting", true);
                if (inputHandler.moveAmount > 0)
                {
                    //playerStats.UseStamina(20);
                    animationHandler.PlayTargetAnimation("Rolling", true);
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = rollRotation;
                }
                else
                {
                    animationHandler.PlayTargetAnimation("Backstep", true);
                }
            }

        }
        
        public void HandleFalling(float delta, Vector3 moveDirection)
        {
            playerManager.isGrounded = false;
            RaycastHit hit;
            Vector3 origin = myTransform.position + groundDetectionRayStartPoint;

            if(Physics.Raycast(origin, -myTransform.up, out hit, 0.4f))
            {
                moveDirection = Vector3.zero;
            }

            // Gravity + forward force
            if (playerManager.isInAir)
            {
                if (playerManager.isJumping)
                {
                    playerManager.isJumping = false;
                }
                else
                {
                    rigidbody.AddForce(-Vector3.up * fallingSpeed);
                    rigidbody.AddForce(moveDirection.normalized * fallingSpeed / 2f);
                }
               
            }

            // Ground check
            Vector3 dir = moveDirection;
            dir.Normalize();
            origin = origin + dir * groundDirectionRayDistance;

            targetPosition = myTransform.position;

            // If there is ground
            Debug.DrawRay(origin, -Vector3.up * minimumDistanceNeededToBeginFall, Color.red, 0.1f, false);
            if (Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, ignoreForGroundCheck))
            {
                normalVector = hit.normal;
                Vector3 tp = hit.point;

                playerManager.isGrounded = true;
                targetPosition.y = tp.y;

                if (playerManager.isInAir)
                {
                    if(inAirTimer > 0.35f)
                    {
                        Debug.Log("You were in the air for + " + inAirTimer);
                        animationHandler.PlayTargetAnimation("Hard Land", true);
                        audioManager.PlaySound("Player Grunt");
                    }
                    else
                    {
                        // Keep rolling animation
                        if(!animationHandler.isAnimationPlaying("Rolling"))
                            animationHandler.PlayTargetAnimation("Locomotion", false);
                    }

                    inAirTimer = 0;
                    playerManager.isInAir = false;
                }
            }
            else
            {
                footsteps.enabled = false;
                sprintFootsteps.enabled = false;
                if (playerManager.isGrounded)
                {
                    playerManager.isGrounded = false;
                }

                if (!playerManager.isInAir)
                { 
                    if (!animationHandler.isAnimationPlaying("Falling") && !animationHandler.isAnimationPlaying("Jump") && !animationHandler.isAnimationPlaying("Rolling"))
                    {
                        animationHandler.PlayTargetAnimation("Falling", true);
                    }

                    Vector3 vel = rigidbody.velocity;
                    vel.Normalize();
                    rigidbody.velocity = vel * (movementSpeed / 2);
                    playerManager.isInAir = true;
                }

                if(myTransform.position.y < -10)
                {
                    playerStats.TakeDamage(9999999999);
                }
            }
            if (playerManager.isGrounded)
            {
                if (playerManager.isInteracting || inputHandler.moveAmount > 0 )
                {
                    myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime);
                }
                else
                {
                    myTransform.position = targetPosition;
                }
            }
        }

        public void HandleAttack(float delta)
        {
            if (animationHandler.anim.GetBool("isInteracting") || animationHandler.anim.GetBool("isDrinking") || !inputHandler.attackFlag || playerStats.stamina < 5) { 
                return;}

            if(moveDirection != Vector3.zero)
            {
                FaceMoveDirection();
            }

            //playerStats.UseStamina(8);
           
            animationHandler.PlayTargetAnimation("Light Attack", true);
            playerManager.lastAttack = "Light Attack";

      
            /* if (playerManager.lastAttack == "Light Attack" && playerManager.canDoCombo)
             {
                 animationHandler.PlayTargetAnimation("Light Attack 2", true);
                 playerManager.lastAttack = "Light Attack 2";

             }
             else if(!playerManager.canDoCombo)
             {
                 animationHandler.PlayTargetAnimation("Light Attack", true);
                 playerManager.lastAttack = "Light Attack";

             }*/

        }

        public void HandleWeaponCombo()
        {
            if (animationHandler.anim.GetBool("isDrinking"))
                return;

            if (inputHandler.comboFlag)
            {
                footsteps.enabled = false;
                sprintFootsteps.enabled = false;
                if (playerStats.stamina > 5)
                {
                    if (moveDirection != Vector3.zero)
                    {
                        FaceMoveDirection();
                    }

                    //playerStats.UseStamina(8);

                    anim.SetBool("canDoCombo", false);
                    if (playerManager.lastAttack == "Light Attack")
                    {
                        animationHandler.PlayTargetAnimation("Light Attack 2", true);
                        playerManager.lastAttack = "Light Attack 2";
                    } else if(playerManager.lastAttack == "Light Attack 2")
                    {
                        animationHandler.PlayTargetAnimation("Light Attack", true);
                        playerManager.lastAttack = "Light Attack";
                    }

                }

            }
           
        }

        public void HandleFlaskHeal(float delta)
        {

            if (animationHandler.anim.GetBool("isInteracting") || animationHandler.anim.GetBool("isDrinking") || !inputHandler.flaskFlag)
            {
                return;
            }

            // Acts as a debounce
          
            playerManager.isSprinting = false;
            animationHandler.PlayTargetAnimation("Flask Drink", false);
            if(playerStats.flasks > 0)
            {
                playerStats.flasks -= 1;
                playerStats.HealFlask();
                anim.SetBool("isDrinking", true);
                audioManager.PlaySound("Heal Flask", .1f);
            }
           
        }
        
    }
}

