using KID;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.LowLevel;

public enum StateBehaviour {
    None,
    Idle,
    Chase,
    Attack,
    Strafe,
    Stationary,
    Death
}

namespace MobStates
{
    namespace Skeletor
    {
        public class StateMachine : MonoBehaviour
        {
            #region currentState and canAttack Properties
            // currentState property values
            private State m_currentState;
            public State currentState
            {
                get { return m_currentState; }
                set
                {
                    //Swaps the state if the value reiceved is different to the current state
                    if (m_currentState != value)
                    {
                        m_currentState?.ExitState();
                        value?.EnterState(this);
                    }
                    m_currentState = value;
                    CurrentState = value.ThisStateType;
                }
            }

            // canAttack property values
            private static bool m_canAttack = true;
            private bool isTicking;
            public bool canAttack
            {
                // AI can't attack if performing an isPerformingACtion
                get { return isPerformingAction ? false : m_canAttack; }
                set
                {
                    // From false to true
                    if (m_canAttack == true && value == false)
                    {
                        m_canAttack = value;
                        StartCoroutine(ResetCanAttack(Random.Range(.6f, 2f)));
                    }
                    else
                    {
                        m_canAttack = value;
                    }


                }
            }

            #endregion

            public StateBehaviour CurrentState;

            [Header("Class References")]
            public EnemyManager enemyManager;
            public EnemyAnimatorManager enemyAnimationManager;
            public Rigidbody enemyRigidBody;
            public LayerMask detectionLayer;
            public CharacterStats currentTarget;
            public NavMeshAgent navMeshAgent;

            [Header("AI Settings")]
            public float stoppingDistance = 1.1f;
            public float meleeDistance = 1.1f;
            public float rotationSpeed = 25f;
            public float detectionRadius = 20f;
            public float maximumDetectionAngle = 50;
            public float minimumDetectionAngle = -50;
            public float distanceFromTarget;

            public bool isPerformingAction;

            [Header("All States")]
            [HideInInspector] public IdleState idleState;
            [HideInInspector] public ChaseState chaseState;
            [HideInInspector] public StrafeState strafeState;
            [HideInInspector] public AttackState attackState;
            [HideInInspector] public StationaryState stationaryState;
            [HideInInspector] public DeathState deathState;

            public EnemyStats enemyStats;

            [Header("State Values")]
            public static StateMachine stateMachine;
            public float directionProgress;
            public float slerpAddition;

            private void Awake()
            {
                enemyManager = GetComponent<EnemyManager>();
                enemyAnimationManager = GetComponent<EnemyAnimatorManager>();
                navMeshAgent = GetComponentInChildren<NavMeshAgent>();
                enemyRigidBody = GetComponent<Rigidbody>();
                enemyStats = GetComponent<EnemyStats>();

                void SetUpStateVals()
                {
                    idleState = GetComponent<IdleState>();
                    chaseState = GetComponent<ChaseState>();
                    strafeState = GetComponent<StrafeState>();
                    attackState = GetComponent<AttackState>();
                    stationaryState = GetComponent<StationaryState>();
                    deathState = GetComponent<DeathState>();

                    // States no longer have to call this on EnterState
                    // Making it only happen once
                    idleState.checkIfStateMachine(this);
                    chaseState.checkIfStateMachine(this);
                    strafeState.checkIfStateMachine(this);
                    attackState.checkIfStateMachine(this);
                    stationaryState.checkIfStateMachine(this);
                    deathState.checkIfStateMachine(this);

                    chaseState.SetAttackValues(1, 5, "Light Attack");
                    strafeState.SetAttackValues(1, 50, "Light Attack");
                    stationaryState.SetAttackValues(1, 6, "Light Attack");
                }

                SetUpStateVals();

                m_currentState = idleState;
                currentState = m_currentState;
                currentState.EnterState(this);
            }

            private void Update()
            {
                isPerformingAction = enemyAnimationManager.anim.GetBool("isInteracting");
                RunCurrentState();
            }

            private void RunCurrentState()
            {
                State potentialNewState = EnumToState(currentState.UpdateState());

                if (potentialNewState != currentState)
                {
                    currentState = potentialNewState;
                }

                if(enemyStats.health <=0 && currentState != deathState)
                {
                    currentState = deathState;
                    enemyStats.healthUI.transform.GetChild(0).gameObject.SetActive(false);
                }
            }

            private IEnumerator ResetCanAttack(float waitTime)
            {
                if (!isTicking)
                {
                    isTicking = true;
                    yield return new WaitForSeconds(waitTime);
                    canAttack = true;
                    isTicking = false;
                }
            }

            public State EnumToState(StateBehaviour stateEnum)
            {
                switch (stateEnum)
                {
                    case StateBehaviour.None:
                        return null;
                    case StateBehaviour.Idle:
                        return idleState;
                    case StateBehaviour.Chase:
                        return chaseState;
                    case StateBehaviour.Attack:
                        return attackState;
                    case StateBehaviour.Strafe:
                        return strafeState;
                    case StateBehaviour.Stationary:
                        return stationaryState;
                    case StateBehaviour.Death:
                        return deathState;
                    default:
                        return null;
                }
            }


        }
    }
}

