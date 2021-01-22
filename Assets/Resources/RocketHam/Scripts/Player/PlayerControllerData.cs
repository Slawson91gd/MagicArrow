using System;
using System.Dynamic;
using UnityEngine;

[Serializable]
public class PlayerControllerData : IDamageable
{
    public PlayerController Player { get; private set; }
    public Rigidbody2D PlayerRB { get; private set; }
    private CapsuleCollider2D MainCollider { get; set; }
    public SpriteRenderer PlayerSpriteRenderer { get; private set; }
    public Camera PlayerCam { get; private set; }
    public Animator PlayerAnimator { get; private set; }
    public HUD PlayerHUD { get; private set; }
    public BoomerangObj PlayerBoomerang { get; private set; }


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

    [SerializeField] private GameObject checkpoint = null;
    public GameObject Checkpoint { get { return checkpoint; } set { checkpoint = value; } }

    public PlayerControllerData(PlayerController player)
    {
        Player = player;
        PlayerRB = Player.GetComponent<Rigidbody2D>();
        MainCollider = Player.GetComponent<CapsuleCollider2D>();
        PlayerSpriteRenderer = Player.GetComponent<SpriteRenderer>();
        PlayerCam = Camera.main;
        PlayerAnimator = Player.GetComponent<Animator>();
        PlayerHUD = GameObject.Find("HUD_Base_Panel").GetComponent<HUD>();
        PlayerBoomerang = Player.GetComponentInChildren<BoomerangObj>();

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
        if (playerHealth > 0)
        {
            playerHealth += health;
            PlayerHUD.UpdateHealth(playerHealth, maxPlayerHealth);
        }
        else
        {
            playerHealth = 0;
            PlayerHUD.UpdateHealth(playerHealth, maxPlayerHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        if(playerHealth >= damage)
        {
            playerHealth -= damage;
        }
        else
        {
            playerHealth -= playerHealth;
        }
        
        PlayerHUD.UpdateHealth(playerHealth, maxPlayerHealth);
        if(playerHealth == 0)
        {
            Debug.Log("The player SHOULD be dead right now.");
        }
    }

    public void HealDamage(float heals)
    {
        float result = playerHealth + heals;
        if(result >= maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }
        else
        {
            playerHealth += heals;
        }

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

    public void RestoreHealth()
    {
        if(playerHealth != maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
            PlayerHUD.UpdateHealth(playerHealth, maxPlayerHealth);
        }
    }

    public void RestorePotion()
    {
        if (playerPotion != maxPlayerPotion)
        {
            playerPotion = maxPlayerPotion;
            PlayerHUD.UpdatePotion(playerPotion, maxPlayerPotion);
        }
    }

    public void AdjustMoveSpeed(float newSpeed)
    {
        MoveSpeed = newSpeed;
    }

    public void SelectBoomerangUpgrade()
    {
        if (!BoomerangDeployed)
        {
            // If player presses "1", equip normal boomerang
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PlayerBoomerang.SetBoomerang(PlayerBoomerang.NormalRang);
                PlayerHUD.UpdateBoomerangType(PlayerBoomerang.CurrentBoomerang);
            }
            // If player presses "2", equip fire boomerang
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PlayerBoomerang.SetBoomerang(PlayerBoomerang.FireRang);
                PlayerHUD.UpdateBoomerangType(PlayerBoomerang.CurrentBoomerang);
            }
            // If player presses "3", equip ice boomerang
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PlayerBoomerang.SetBoomerang(PlayerBoomerang.IceRang);
                PlayerHUD.UpdateBoomerangType(PlayerBoomerang.CurrentBoomerang);
            }
            // If player presses "4", equip shock boomerang
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                PlayerBoomerang.SetBoomerang(PlayerBoomerang.ShockRang);
                PlayerHUD.UpdateBoomerangType(PlayerBoomerang.CurrentBoomerang);
            }
            // If player presses "5", equip wind Boomerang
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                PlayerBoomerang.SetBoomerang(PlayerBoomerang.WindRang);
                PlayerHUD.UpdateBoomerangType(PlayerBoomerang.CurrentBoomerang);
            }
            // If player presses "6", equip obsidian boomerang
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                PlayerBoomerang.SetBoomerang(PlayerBoomerang.ObsidianRang);
                PlayerHUD.UpdateBoomerangType(PlayerBoomerang.CurrentBoomerang);
            }
        }
    }
}
