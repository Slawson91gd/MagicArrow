using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowState : State
{
    public ThrowState(PlayerControllerData playerData) : base(playerData)
    {

    }

    public override void Tick()
    {
        Debug.Log("Current State: " + this);
    }

    public override void OnStateEnter()
    {
        Debug.Log("Now ENTERING the " + this);

        Object.Instantiate(Resources.Load("RocketHam/Prefabs/TempBoomerang"),
                           PlayerData.Player.transform.position + (PlayerData.BoomerangTarget - PlayerData.Player.transform.position).normalized * 1.5f,
                           Quaternion.identity);
        PlayerData.BoomerangDeployed = true;
        PlayerData.SetState(PlayerData.Idle);
    }

    public override void OnStateExit()
    {
        Debug.Log("Now EXITING the " + this);
    }
}
