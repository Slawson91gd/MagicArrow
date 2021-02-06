using UnityEngine;

public class DetectWallJump : MonoBehaviour
{
    PlayerController player;

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("World") && player.PlayerData.CurrentState == player.PlayerData.InAir)
        {
            // if wall is on the right or left of the player and they are moving toward it, enter the wall jump state.
            /*if(collision.gameObject.transform.position.x > player.transform.position.x && Input.GetAxisRaw("Horizontal") > 0 ||
                collision.gameObject.transform.position.x < player.transform.position.x && Input.GetAxisRaw("Horizontal") < 0)
            {
                if (player.PlayerData.CurrentWallJumps != player.PlayerData.WallJumpLimit)
                {
                    player.PlayerData.SetState(player.PlayerData.WallJump);
                }
            }*/

            if (collision.GetContact(0).point.x > player.transform.position.x && Input.GetAxisRaw("Horizontal") > 0 ||
                collision.GetContact(0).point.x < player.transform.position.x && Input.GetAxisRaw("Horizontal") < 0)
            {
                if (player.PlayerData.CurrentWallJumps != player.PlayerData.WallJumpLimit)
                {
                    player.PlayerData.SetState(player.PlayerData.WallJump);
                }
            }
        }
    }
}
