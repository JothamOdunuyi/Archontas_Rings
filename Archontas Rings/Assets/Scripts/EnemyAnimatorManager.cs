using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KID
{
    public class EnemyAnimatorManager : AnimatorManager
    {
        [SerializeField] Rigidbody enemyRigidBody;
        
        private void Awake()
        {
            anim = GetComponent<Animator>();
            enemyRigidBody = GetComponent<Rigidbody>();
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

    }


}
