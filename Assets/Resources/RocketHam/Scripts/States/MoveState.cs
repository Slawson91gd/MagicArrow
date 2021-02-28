using UnityEngine;

public class MoveState : State
{
    public MoveState(PlayerControllerData playerData) : base(playerData)
    {

    }

    public override void Tick()
    {
        //Debug.Log("Current State: " + this);
        base.HandleMovement(PlayerData.MoveInputX);
        base.TransitionToJump();
        TransitionToAim();
    }

    public override void OnStateEnter()
    {
        //Debug.Log("Now entering the MOVE state.");
    }

    public override void OnStateExit()
    {
        //Debug.Log("Now exiting the MOVE state.");
    }

    protected override void TransitionToAim()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (PlayerData.CanThrow && !PlayerData.BoomerangDeployed)
            {
                PlayerData.SetState(PlayerData.Aim);
            }
            else
            {
                Debug.Log("Cannot currently throw boomerang.");
            }
        }
    }
}
