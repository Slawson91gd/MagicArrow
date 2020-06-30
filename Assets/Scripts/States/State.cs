using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected PlayerControllerData PlayerData { get; set; }

    protected State(PlayerControllerData playerData)
    {
        PlayerData = playerData;
    }

    public abstract void Tick();

    public virtual void OnStateEnter()
    {
        Debug.Log("This is the base state entry method.");
    }

    public virtual void OnStatExit()
    {
        Debug.Log("This is the base state exit method.");
    }
}
