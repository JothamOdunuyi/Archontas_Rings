using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MobStates
{
    namespace Torkan
    {
        public class AttackBaseState : State
{
    public override StateBehaviour ThisStateType => StateBehaviour.None;

    private int m_min, m_max;
    private string m_attack_name;

    #region Setting Value Methods

    private void setMinAndMaxVals(int min, int max)
    {
        m_min = min;
        m_max = max;
    }

    public void SetAttackValues(int min, int max)
    {
        setMinAndMaxVals(min, max);
    }

    public void SetAttackValues(int min, int max, string attack_name)
    {
        setMinAndMaxVals(min, max);
        m_attack_name = attack_name;
    }

    #endregion

    private void DoAttack(string attack_name)
    {
        stateMachine.canAttack = false;
        stateMachine.enemyAnimationManager.anim.SetBool("isInteracting", true);
        stateMachine.enemyAnimationManager.PlayTargetAnimation(attack_name, true, false);
    }

    private bool AttackConditions()
    {
        if (stateMachine.canAttack)
        {
            int u = Random.Range(m_min, m_max);
            if (u == 1)
            {
                return true;
            }
        }

        return false;
    }

    public void ChanceAttack()
    {
        if (AttackConditions())
        {
            DoAttack(m_attack_name);
        }
    }

    public void ChanceAttack(string[] listOfAttacks)
    {
        if (AttackConditions())
        {
            DoAttack(listOfAttacks[Random.Range(0, listOfAttacks.Length - 1)]);
        }
    }

    #region State Methods
    public override void EnterState(StateMachine stateMachine)
    {
    }

    public override void ExitState()
    {
       
    }

    public override StateBehaviour UpdateState()
    {
        return StateBehaviour.None;
    }
    #endregion

}
    }
}
