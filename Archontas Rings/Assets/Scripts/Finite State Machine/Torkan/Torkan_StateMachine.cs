using KID;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.LowLevel;


public class Torkan_StateMachine : MonoBehaviour
{
    #region currentState and canAttack Properties
    // currentState property values
    private Torkan_State m_currentState;
    public Torkan_State currentState
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
    [HideInInspector] public Torkan_IdleState idleState;
    [HideInInspector] public Torkan_ChaseState chaseState;
    [HideInInspector] public Torkan_StrafeState strafeState;
    [HideInInspector] public Torkan_StationaryState stationaryState;
    [HideInInspector] public Torkan_DeathState deathState;

    [Header("State Values")]
    public static Torkan_StateMachine stateMachine;
    public float directionProgress;
    public float slerpAddition;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimationManager = GetComponent<EnemyAnimatorManager>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        enemyRigidBody = GetComponent<Rigidbody>();

        void SetUpStateVals()
        {
            idleState = GetComponent<Torkan_IdleState>();
            chaseState = GetComponent<Torkan_ChaseState>();
            strafeState = GetComponent<Torkan_StrafeState>();
            stationaryState = GetComponent<Torkan_StationaryState>();
            deathState = GetComponent<Torkan_DeathState>();

            // States no longer have to call this on EnterState
            // Making it only happen once
            idleState.checkIfStateMachine(this);
            chaseState.checkIfStateMachine(this);
            strafeState.checkIfStateMachine(this);
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
        Torkan_State potentialNewState = EnumToState(currentState.UpdateState());

        if (potentialNewState != currentState)
        {
            currentState = potentialNewState;
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

    public Torkan_State EnumToState(StateBehaviour stateEnum)
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
                return null;
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

    // Makes sure that while AI is attacking it is not overridden
    /*    public IEnumerator PlayLocomotionAfterPeroforming()
        {
            while (isPerformingAction)
            {
                yield return new WaitForSeconds(.1f);
            }
            enemyAnimationManager.PlayTargetAnimation("Locomotion", false, false);
            print("Playing Locmotion after waiting for performing action!");
        }
    */

    /*private void RunCurrentState()
    {
        State newState = currentState?.UpdateState();

        if(newState != currentState)
        {
            SwitchState(newState);
        }
    }

    public void SwitchState(State newState)
    {
        print("switching states to " + newState);
        currentState?.ExitState();
        newState.EnterState(this);
        //currentState = newState;


    }*/

}
