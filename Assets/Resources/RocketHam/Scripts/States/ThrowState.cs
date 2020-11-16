public class ThrowState : State
{
    public ThrowState(PlayerControllerData playerData) : base(playerData)
    {

    }

    public override void Tick()
    {
        //Debug.Log("Current State: " + this);
    }

    public override void OnStateEnter()
    {
        //Debug.Log("Now ENTERING the " + this);
        PlayerData.BoomerangDeployed = true;
        PlayerData.PlayerBoomerang.CurrentBoomerang.SetMode(Boomerang.BoomerangModes.TRAVEL);
        PlayerData.SetState(PlayerData.Idle);
    }

    public override void OnStateExit()
    {
        //Debug.Log("Now EXITING the " + this);
    }
}
