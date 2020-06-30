using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(PlayerControllerData playerData) : base(playerData)
    {

    }

    public override void Tick()
    {
        throw new System.NotImplementedException();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStatExit()
    {
        base.OnStatExit();
    }
}
