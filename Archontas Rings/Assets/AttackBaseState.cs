using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBaseState : State
{
    public override StateBehaviour ThisStateType => StateBehaviour.None;

    private int m_min, m_max;
    private string m_attack_name;

    public void SetAttackValues(int min, int max, string attack_name)
    {
        m_min = min;
        m_max = max;
        m_attack_name = attack_name;
    }
    public void ChanceAttack()
    {
        if (stateMachine.canAttack)
        {
            int u = Random.Range(m_min, m_max);
            if (u == 1)
            {
                print("Attacking in AttackBaseState");
                stateMachine.enemyAnimationManager.PlayTargetAnimation(m_attack_name, true, false);
                stateMachine.canAttack = false;
            }
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
