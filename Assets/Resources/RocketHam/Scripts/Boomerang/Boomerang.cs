using System;
using UnityEngine;

public abstract class Boomerang
{
    protected BoomerangObj BoomerangObject { get; set; }
    public PlayerControllerData PlayerData { get; private set; }
    protected Rigidbody2D BoomerangRB { get; set; }
    protected CircleCollider2D BoomerangCollider { get; private set; }
    protected SpriteRenderer BoomerangSprite { get; private set; }
    private Vector3 FloatPoint { get; set; }
    private Vector3 RightFloatPoint { get; set; }
    private Vector3 LeftFloatPoint { get; set; }
    public Color BoomerangColor { get; set; }
    public Vector3 BoomerangTarget { get; private set; }
    public float BoomerangDistance { get; private set; }

    protected float IdleFollowSpeed { get { return BoomerangObject.idleFollowSpeed; } }
    protected float TravelSpeed { get { return BoomerangObject.travelSpeed; } }
    protected float ReturnSpeed { get { return BoomerangObject.returnSpeed; } }
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
        ICE,
        SHOCK,
        WIND,
        OBSIDIAN
    }
    public BoomerangTypes Type { get; set; }

    protected Boomerang(BoomerangObj boomerang)
    {
        BoomerangObject = boomerang;
        PlayerData = UnityEngine.Object.FindObjectOfType<PlayerController>().PlayerData;
        BoomerangRB = BoomerangObject.GetComponent<Rigidbody2D>();
        BoomerangCollider = BoomerangObject.GetComponent<CircleCollider2D>();
        BoomerangSprite = BoomerangObject.GetComponent<SpriteRenderer>();
        BoomerangDistance = 15.0f;
        
        HasCollided = false;
        Mode = BoomerangModes.IDLE;
    }

    public virtual void HandleBoomerang()
    {
        float proximity;

        switch (Mode)
        {
            case BoomerangModes.IDLE:
                HandleIdleFloat();
                break;

            case BoomerangModes.TRAVEL:
                if(BoomerangObject.transform.parent != null)
                {
                    BoomerangObject.transform.SetParent(null);
                }

                Direction = BoomerangTarget - BoomerangObject.transform.position;
                proximity = Direction.magnitude;
                if (proximity > 0.5f && !HasCollided)
                {
                    BoomerangRB.MovePosition(BoomerangObject.transform.position + (Direction.normalized * TravelSpeed * Time.deltaTime));
                }
                else if (proximity < 0.5f || HasCollided)
                {
                    SetMode(BoomerangModes.RETURN);
                }
                break;

            case BoomerangModes.RETURN:
                Direction = (PlayerData.Player.transform.position - BoomerangObject.transform.position);
                proximity = Direction.magnitude;
                if (proximity > 1.0f)
                {
                    BoomerangRB.MovePosition(BoomerangObject.transform.position + (Direction.normalized * ReturnSpeed * Time.deltaTime));
                }
                else
                {
                    PlayerData.BoomerangDeployed = false;
                    if (HasCollided)
                    {
                        Collided(false);
                    }
                    if(BoomerangObject.transform.parent == null)
                    {
                        BoomerangObject.transform.SetParent(PlayerData.Player.transform);
                    }
                    SetMode(BoomerangModes.IDLE);
                }
                break;
        }
    }

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

    public void SetColor(Color boomerangColor)
    {
        if (BoomerangSprite.color != boomerangColor)
            BoomerangSprite.color = boomerangColor;
    }

    protected void HandleIdleFloat()
    {
        if(PlayerData.PlayerSpriteRenderer.flipX)
        {
            RightFloatPoint = new Vector3(PlayerData.Player.transform.position.x + 1, PlayerData.Player.transform.position.y + 0.5f, PlayerData.Player.transform.position.z);

            if (FloatPoint != RightFloatPoint)
                FloatPoint = RightFloatPoint;
        }
        else
        {
            LeftFloatPoint = new Vector3(PlayerData.Player.transform.position.x - 1, PlayerData.Player.transform.position.y + 0.5f, PlayerData.Player.transform.position.z);

            if (FloatPoint != LeftFloatPoint)
                FloatPoint = LeftFloatPoint;
        }
        BoomerangRB.position = Vector2.Lerp(BoomerangObject.transform.position, FloatPoint, IdleFollowSpeed * Time.deltaTime);
    }

    public void SetTarget(Vector3 targetPos)
    {
        if(BoomerangTarget != targetPos)
        {
            BoomerangTarget = targetPos;
        }
    }
}
