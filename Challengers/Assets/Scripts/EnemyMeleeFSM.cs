using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeFSM : EnemyBase
{
    public enum State
    {
        Idle,
        Move,
        Attack,
    };

    public State CurrentState = State.Idle;

    WaitForSeconds Delay500 = new WaitForSeconds(0.5f);
    WaitForSeconds Delay250 = new WaitForSeconds(0.25f);

    protected void Start()
    {
        StartCoroutine(FSM());
    }

    protected virtual IEnumerator FSM()
    {
        yield return null;
        //while (!parentRoom.GetComponent<RoomCondition>().playerInThisRoom)
        //{
        //    yield return Delay500;
        //}

        while(true)
        {
            yield return StartCoroutine(CurrentState.ToString());
        }
    }

    protected virtual IEnumerator Idle()
    {
        yield return null;

        CurrentState = State.Attack;

    }


    protected virtual IEnumerator Move()
    {
        yield return null;
    }
    protected virtual IEnumerator Attack()
    {
        yield return null;
    }
}
