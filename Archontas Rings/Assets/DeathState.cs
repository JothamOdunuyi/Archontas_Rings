using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    [Header("Configuration Values")]
    [SerializeField] float deathFadeWaitTime = 20f;

    public override void EnterState(StateMachine sentStateMachine)
    {
        checkIfStateMachine(sentStateMachine);
        Debug.Log("Entered Death State");
        SetChildrensGravity(transform.Find("root").transform);
        Invoke("RemoveBody", deathFadeWaitTime);

        //Give souls
    }

    public override void ExitState()
    {
        Debug.Log("Somehow this enemy got resurrected!!");
    }

    private void RemoveBody()
    {
        transform.root.gameObject.SetActive(false);
    }

    public override State UpdateState()
    {
        return this;
    }

    private void SetChildrensGravity(Transform obj)
    {
        if (obj.childCount > 0)
        {
            for (int i = 0; i < obj.childCount; i++)
            {
                if (obj.GetChild(i).GetComponent<Rigidbody>()){
                    obj.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
                }

                SetChildrensGravity(obj.GetChild(i).transform);
            }
        }

    }
}
