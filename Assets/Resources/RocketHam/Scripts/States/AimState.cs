using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class AimState : State
{
    public AimState(PlayerControllerData playerData) : base(playerData)
    {

    }

    public override void Tick()
    {
        Debug.Log("Current State: " + this);
        HandleAim();
    }

    public override void OnStateEnter()
    {
        Debug.Log("Now ENTERING the AIM state.");
        PlayerData.CanMove = false;
        PlayerData.CanJump = false;
        PlayerData.Player.GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    public override void OnStateExit()
    {
        Debug.Log("Now EXITING the AIM state.");
        PlayerData.CanMove = true;
        PlayerData.CanJump = true;
        PlayerData.Player.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    private void HandleAim()
    {
        if (Input.GetMouseButton(1))
        {
            PlayerData.BoomerangTarget = PlayerData.PlayerCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            Debug.DrawRay(PlayerData.Player.transform.position, (PlayerData.BoomerangTarget - PlayerData.Player.transform.position).normalized *
                                                                 (PlayerData.BoomerangTarget - PlayerData.Player.transform.position).magnitude, Color.red);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            PlayerData.SetState(PlayerData.Throw);
        }
    }
}
