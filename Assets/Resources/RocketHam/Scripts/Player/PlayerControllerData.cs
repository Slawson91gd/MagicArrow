using System;
using System.Dynamic;
using UnityEngine;

[Serializable]
public class PlayerControllerData : IDamageable
{
    public PlayerController Player { get; private set; }
    public Rigidbody2D PlayerRB { get; private set; }
    private CapsuleCollider2D MainCollider { get; set; }
    public Camera PlayerCam { get; private set; }
    public Animator PlayerAnimator { get; private set; }
    public HUD PlayerHUD { get; private set; }


    // State Variables
    public State CurrentState { get; private set; }
    public IdleState Idle { get; private set; }
    public MoveState Movement { get; private set; }
    public JumpState Jump { get; private set; }
    public InAirState InAir { get; private set; }
    public AimState Aim { get; private set; }
    public ThrowState Throw { get; private set; }
    public WallJumpState WallJump { get; private set; }

    // Health Variables
    [SerializeField] private float playerHealth = 0;
    public float PlayerHealth { get { return playerHealth; } set { playerHealth = value; } }

    [SerializeField] private float maxPlayerHealth = 0;
    public float MaxPlayerHealth { get { return maxPlayerHealth; } private set { maxPlayerHealth = value; } }

    [SerializeField] private float playerPotion = 0;
    public float PlayerPotion { get { return playerPotion; } private set { playerPotion = value; } }

    [SerializeField] private float maxPlayerPotion = 0;
    public float MaxPlayerPotion { get { return maxPlayerPotion; } private set { maxPlayerPotion = value; } }

    // Movement Variables
    public float MoveInputX { get; set; }

    [SerializeField] private bool canMove = true;
    public bool CanMove { get { return canMove; } set { canMove = value; } }

    [SerializeField] private float moveSpeed = 10.0f;
    public float MoveSpeed { get { return moveSpeed; } private set { moveSpeed = value; } }

    // Jumping Variables
    [SerializeField] private bool canJump = true;
    public bool CanJump { get { return canJump; } set { canJump = value; } }

    [SerializeField] private bool onGround = false;
    public bool OnGround { get { return onGround; } set { onGround = value; } }

    [SerializeField] private float jumpForce = 8.0f;
    public float JumpForce { get { return jumpForce; } private set { jumpForce = value; } }
    private LayerMask PlatformLayer { get; set; }

    [SerializeField] private readonly float fallMultiplyer = 2.5f;
    public float FallMultiplyer { get { return fallMultiplyer; } }

    [SerializeField] private readonly float lowJumpMultiplyer = 2.0f;
    public float LowJumpMultiplyer { get { return lowJumpMultiplyer; } }

    // Boomerang Variables
    [SerializeField] private bool canThrow = true;
    public bool CanThrow { get { return canThrow; } set { canThrow = value; } }

    [SerializeField] private bool boomerangDeployed = false;
    public bool BoomerangDeployed { get { return boomerangDeployed; } set { boomerangDeployed = value; } }

    [SerializeField] private Vector3 boomerangTarget;
    public Vector3 BoomerangTarget { get { return boomerangTarget; } set { boomerangTarget = value; } }

    [SerializeField] private float boomerangDistance = 15.0f;
    public float BoomerangDistance { get { return boomerangDistance; } private set { boomerangDistance = value; } }

    public PlayerControllerData(PlayerController player)
    {
        Player = player;
        PlayerRB = Player.GetComponent<Rigidbody2D>();
        MainCollider = Player.GetComponent<CapsuleCollider2D>();
        PlayerCam = Camera.main;
        PlayerAnimator = Player.GetComponent<Animator>();
        PlayerHUD = GameObject.Find("HUD_Base_Panel").GetComponent<HUD>();

        PlatformLayer = LayerMask.GetMask("Platform");

        Idle = new IdleState(this);
        Movement = new MoveState(this);
        Jump = new JumpState(this);
        InAir = new InAirState(this);
        Aim = new AimState(this);
        Throw = new ThrowState(this);
        WallJump = new WallJumpState(this);
        SetState(Idle);

        maxPlayerHealth = 100.0f;
        playerHealth = maxPlayerHealth;
        maxPlayerPotion = 50.0f;
        playerPotion = maxPlayerPotion;
    }

    public void SetState(State state)
    {
        if(CurrentState != null)
        {
            CurrentState.OnStateExit();
        }

        CurrentState = state;

        if(CurrentState != null)
        {
            CurrentState.OnStateEnter();
        }
    }

    public bool IsGrounded()
    {
        float extraHeight = 0.05f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(MainCollider.bounds.center, MainCollider.bounds.size, 0, Vector3.down, extraHeight, PlatformLayer);
        Color rayColor;
        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(MainCollider.bounds.center + new Vector3(MainCollider.bounds.extents.x, 0), Vector2.down * (MainCollider.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(MainCollider.bounds.center - new Vector3(MainCollider.bounds.extents.x, 0), Vector2.down * (MainCollider.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(MainCollider.bounds.center - new Vector3(MainCollider.bounds.extents.x, MainCollider.bounds.extents.y + extraHeight), Vector2.right * (MainCollider.bounds.extents.y), rayColor);
        
        return raycastHit.collider != null;
    }

    public void ModifyHP(int health)
    {
        playerHealth += health;
        PlayerHUD.UpdateHealth(playerHealth, maxPlayerHealth);
    }

    public void UsePotion(float difference)
    {
        // If player health is not equal to max
        if (playerHealth != maxPlayerHealth)
        {
            // If potion is greater than difference of health and max health
            if (playerPotion >= difference)
            {
                playerPotion -= difference;
                playerHealth += difference;
                PlayerHUD.UpdateHealth(playerHealth, maxPlayerHealth);
                PlayerHUD.UpdatePotion(playerPotion, maxPlayerPotion);
            }
            else if (playerPotion < difference && playerPotion > 0)
            {
                playerHealth += playerPotion;
                playerPotion -= playerPotion;
                PlayerHUD.UpdateHealth(playerHealth, maxPlayerHealth);
                PlayerHUD.UpdatePotion(playerPotion, maxPlayerPotion);
            }
            else if (playerPotion == 0)
            {
                Debug.Log("The player is out of potion!");
            }
        }
        else
        {
            Debug.Log("Player is already at max health!");
        }
    }

    public void AdjustMoveSpeed(float newSpeed)
    {
        MoveSpeed = newSpeed;
    }
}
