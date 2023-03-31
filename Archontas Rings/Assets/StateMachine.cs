using KID;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.LowLevel;

public class StateMachine : MonoBehaviour
{

    // "Fake" current state
    [SerializeField]
    private State m_currentState;

    //Swaps the state if the value reiceved is different to the current state
    public State currentState
    {
        get { return m_currentState; }
        set
        {
            if(m_currentState != value)
            {
                m_currentState?.ExitState();
                value?.EnterState(this);
            }
            m_currentState = value;
        }
    }

    private static bool m_canAttack = true;
    private bool isTicking;
    public bool canAttack
    {
        get { return m_canAttack; }
        set
        {
            // From false to true
            if (m_canAttack == true && value == false)
            {
                m_canAttack = value;
                StartCoroutine(ResetCanAttack(Random.Range(.5f, 2f)));
            }
            else
            {
                m_canAttack = value;
                print("Can attack set to false from setter");
            }


        }
    }

    public enum Behaviours
    {
        Idle,
        Chase,
        Strafe,
        Attack
    }

    public Behaviours COOLSTUFF;



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


    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimationManager = GetComponent<EnemyAnimatorManager>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        enemyRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        navMeshAgent.enabled = false;
        enemyRigidBody.isKinematic = false;
        m_currentState = GetComponent<IdleState>();
        currentState = m_currentState;
        //print(currentState);
        currentState.EnterState(this);
    }

    private void Update()
    {
        isPerformingAction = enemyAnimationManager.anim.GetBool("isInteracting");
        RunCurrentState();
    }

    private void RunCurrentState()
    {
        currentState = currentState?.UpdateState();
    }

    private IEnumerator ResetCanAttack(float waiTime)
    {
        print("Waiting for canAttack");
        yield return new WaitForSeconds(waiTime);
        canAttack = true;
            
        
    }


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
