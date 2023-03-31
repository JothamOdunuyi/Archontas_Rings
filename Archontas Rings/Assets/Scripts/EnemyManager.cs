using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KID
{
    public class EnemyManager : MonoBehaviour
    {
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyStats enemyStats;

        public bool isPerformingAction;

        [Header("AI Settings")]
        public float detectionRadius= 20;

        public float maximumDetectionAngle = 50;
        public float minimumDetectionAngle = -50;

        private void Awake()
        {
            enemyStats = GetComponent<EnemyStats>();
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();   
        }


        private void FixedUpdate()
        {

            if(enemyStats.health > 0 )
                HandleCurrentAction();
            
        }

        private void Update()
        {
            
        }

        private void HandleCurrentAction()
        {
            if(enemyLocomotionManager.currentTarget == null)
            {
                enemyLocomotionManager.HandleDetection();
            }
            else
            {
                enemyLocomotionManager.HandleMoveToTarget();
            }
        }


    }

  

}
