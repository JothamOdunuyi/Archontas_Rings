using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public override StateBehaviour ThisStateType => StateBehaviour.Attack;

    public bool target;

    public override void EnterState(StateMachine sentStateMachine)
    {
        //checkIfStateMachine(sentStateMachine);
    }


    public override void ExitState()
    {

    }


    public override StateBehaviour UpdateState()
    {
        Debug.Log("a");

        if (target)
            return StateBehaviour.Attack;
        else
            return StateBehaviour.None;
    }
}
