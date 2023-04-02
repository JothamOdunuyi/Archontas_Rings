using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KID
{
    public class EnemyAnimatorManager : AnimatorManager
    {
        [SerializeField] Rigidbody enemyRigidBody;
        AudioManager audioManager;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            enemyRigidBody = GetComponent<Rigidbody>();
            audioManager = GameObject.FindGameObjectWithTag("Audio Manager").GetComponent<AudioManager>();

        }

        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyRigidBody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyRigidBody.velocity = velocity;
        }

        public void EnableEnemyWeaponCollider()
        {
            enemyWeapon.GetComponent<DamageCollider>().EnableDamageCollider();
        }

        public void DisableEnemyWeaponCollider()
        {
            enemyWeapon.GetComponent<DamageCollider>().DisableDamageCollider();
        }

        public void PlaySound(string soundName)
        {
            audioManager.PlaySound(soundName, gameObject);
        }

    }


}
