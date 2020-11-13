using System;
using UnityEngine;

public abstract class Boomerang
{
    protected BoomerangObj BoomerangObject { get; set; }
    public PlayerControllerData PlayerData { get; private set; }
    protected Rigidbody2D BoomerangRB { get; set; }
    protected CircleCollider2D BoomerangCollider { get; private set; }
    private Vector2 FloatPoint { get; set; }
    private Vector2 RightFloatPoint { get; set; }
    private Vector2 LeftFloatPoint { get; set; }

    protected float IdleFollowSpeed { get; private set; }
    protected float TravelSpeed { get; private set; }
    protected float ReturnSpeed { get; private set; }
    protected bool HasCollided { get; private set; }

    public Vector3 Direction { get; set; }

    public enum BoomerangModes
    {
        IDLE,
        TRAVEL,
        RETURN
    }
    public BoomerangModes Mode { get; private set; }

    public enum BoomerangTypes
    {
        NORMAL,
        FIRE,
        SHOCK,
        ICE,
        WIND,
        OBSIDIAN
    }
    public BoomerangTypes Type { get; private set; }

    protected Boomerang(BoomerangObj boomerang)
    {
        BoomerangObject = boomerang;
        PlayerData = UnityEngine.Object.FindObjectOfType<PlayerController>().PlayerData;
        BoomerangRB = BoomerangObject.GetComponent<Rigidbody2D>();
        BoomerangCollider = BoomerangObject.GetComponent<CircleCollider2D>();

        IdleFollowSpeed = 10.0f;
        TravelSpeed = 10.0f;
        ReturnSpeed = TravelSpeed * 2.0f;
        HasCollided = false;
        Mode = BoomerangModes.IDLE;
        Type = BoomerangTypes.NORMAL;
    }

    public abstract void HandleBoomerang();

    public void Collided(bool value)
    {
        if(HasCollided != value)
        {
            HasCollided = value;
        }
    }

    public void SetMode(BoomerangModes mode)
    {
        if (Mode != mode)
            Mode = mode;
    }

    protected void SetType(BoomerangTypes type)
    {
        if (Type != type)
            Type = type;
    }

    protected void HandleIdleFloat()
    {
        if(PlayerData.PlayerSpriteRenderer.flipX)
        {
            RightFloatPoint = new Vector2(PlayerData.Player.transform.position.x + 1, PlayerData.Player.transform.position.y + 0.5f);

            if (FloatPoint != RightFloatPoint)
                FloatPoint = RightFloatPoint;
        }
        else
        {
            LeftFloatPoint = new Vector2(PlayerData.Player.transform.position.x - 1, PlayerData.Player.transform.position.y + 0.5f);

            if (FloatPoint != LeftFloatPoint)
                FloatPoint = LeftFloatPoint;
        }

        BoomerangObject.transform.position = Vector2.Lerp(BoomerangObject.transform.position, FloatPoint, IdleFollowSpeed * Time.deltaTime);
    }
}
