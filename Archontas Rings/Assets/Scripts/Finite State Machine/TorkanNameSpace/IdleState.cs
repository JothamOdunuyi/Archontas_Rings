using KID;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

namespace MobStates
{
    namespace Torkan
    {
        public class IdleState : State
        {

            public override StateBehaviour ThisStateType => StateBehaviour.Idle;

            public override void EnterState(StateMachine sentStateMachine)
            {

            }

            public override void ExitState()
            {
                GameObject.Find("Torkan Boss HP").transform.GetChild(1).GetChild(0).gameObject.SetActive(true) ;
                GameObject.Find("Torkan Boss HP").transform.Find("Nametag").gameObject.SetActive(true);

                audioManager.GetComponent<AudioSource>().Stop();
                audioManager.transform.Find("Boss Music").GetComponent<AudioSource>().Play();
                audioManager.PlaySound("Your Time Has Come", gameObject, .3f);
            }


            public override StateBehaviour UpdateState()
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, stateMachine.detectionRadius, stateMachine.detectionLayer);

                for (int i = 0; i < colliders.Length; i++)
                {
                    CharacterStats characterStats = colliders[i].GetComponent<CharacterStats>();
                    if (characterStats != null)
                    {
                        Vector3 targetDirection = characterStats.transform.position - transform.position;
                        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                        if (viewableAngle > stateMachine.minimumDetectionAngle && viewableAngle < stateMachine.maximumDetectionAngle)
                        {
                            stateMachine.currentTarget = characterStats;
                            //print("found character");
                            return StateBehaviour.Chase;
                        }

                    }

                }
                if (stateMachine.enemyStats.health < stateMachine.enemyStats.maxHealth)
                {
                    stateMachine.currentTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
                    return StateBehaviour.Chase;
                }
                return ThisStateType;
            }



        }
    }
}
