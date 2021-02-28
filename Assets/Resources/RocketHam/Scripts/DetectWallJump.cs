using UnityEngine;

public class DetectWallJump : MonoBehaviour
{
    PlayerController player;
    public bool onWall;

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
        onWall = false;
    }

    private void Update()
    {
        HandleWallJumpTransition();
    }

    private void HandleWallJumpTransition()
    {
        if (onWall && player.PlayerData.CurrentState == player.PlayerData.InAir)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && player.PlayerData.CurrentWallJumps != player.PlayerData.WallJumpLimit)
            {
                player.PlayerData.SetState(player.PlayerData.WallJump);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("World"))
        {
            if (!onWall)
            {
                onWall = !onWall;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("World"))
        {
            if (onWall)
            {
                onWall = !onWall;
            }
        }
    }
}
