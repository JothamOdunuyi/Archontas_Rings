using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public bool target;

    public override void EnterState(StateMachine sentStateMachine)
    {
        checkIfStateMachine(sentStateMachine);
    }


    public override void ExitState()
    {

    }


    public override State UpdateState()
    {
        Debug.Log("a");

        if (target)
            return this;
        else
            return null;
    }
}
