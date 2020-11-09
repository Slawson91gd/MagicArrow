using UnityEngine;

public abstract class Boomerang
{
    protected PlayerControllerData PlayerData { get; set; }
    protected Rigidbody2D PlayerRB { get; set; }

    protected float TravelSpeed { get; private set; }
    protected float ReturnSpeed { get; private set; }
    protected bool HasCollided { get; private set; }

    public Vector3 Direction { get; set; }

    public enum BoomerangModes
    {
        TRAVEL,
        RETURN
    }
    [SerializeField] private BoomerangModes Mode;

    public enum BoomerangTypes
    {
        NORMAL,
        FIRE,
        SHOCK,
        ICE,
        WIND
    }
    [SerializeField] private BoomerangTypes Type;

    protected Boomerang()
    {
        PlayerData = Object.FindObjectOfType<PlayerController>().PlayerData;
        TravelSpeed = 10.0f;
        ReturnSpeed = TravelSpeed * 2.0f;
        HasCollided = false;
    }

    public abstract void HandleBoomerang();
}
