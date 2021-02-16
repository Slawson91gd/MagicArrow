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

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("World") && player.PlayerData.CurrentState == player.PlayerData.InAir)
        {
            // if wall is on the right or left of the player and they are moving toward it, enter the wall jump state.
            if (collision.GetContact(0).point.x > player.transform.position.x && Input.GetAxisRaw("Horizontal") > 0 ||
                collision.GetContact(0).point.x < player.transform.position.x && Input.GetAxisRaw("Horizontal") < 0)
            {
                if (player.PlayerData.CurrentWallJumps != player.PlayerData.WallJumpLimit)
                {
                    //player.PlayerData.SetState(player.PlayerData.WallJump);
                }
            }
        }
    }*/

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
