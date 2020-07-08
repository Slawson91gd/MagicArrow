using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerControllerData
{
    public PlayerController Player { get; private set; }
    public Rigidbody2D PlayerRB { get; private set; }
    private BoxCollider2D MainCollider { get; set; }


    // State Variables
    public State CurrentState { get; private set; }
    public IdleState Idle { get; private set; }
    public MoveState Movement { get; private set; }
    public JumpState Jump { get; private set; }
    public InAirState InAir { get; private set; }

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

    [SerializeField] private float jumpForce = 5.0f;
    public float JumpForce { get { return jumpForce; } private set { jumpForce = value; } }
    private LayerMask PlatformLayer { get; set; }

    public PlayerControllerData(PlayerController player)
    {
        Player = player;
        PlayerRB = Player.GetComponent<Rigidbody2D>();
        MainCollider = Player.GetComponent<BoxCollider2D>();

        PlatformLayer = LayerMask.GetMask("Platform");

        Idle = new IdleState(this);
        Movement = new MoveState(this);
        Jump = new JumpState(this);
        InAir = new InAirState(this);
        SetState(Idle);
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
        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }
}
