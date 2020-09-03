using UnityEngine;

public class AimState : State
{
    private SpriteRenderer PlayerSprite { get; set; }
    
    private Color startingColor;
    private Color aimColor;


    public AimState(PlayerControllerData playerData) : base(playerData)
    {
        PlayerSprite = PlayerData.Player.GetComponent<SpriteRenderer>();
        startingColor = PlayerSprite.color;
        aimColor = Color.blue;
    }

    public override void Tick()
    {
        //Debug.Log("Current State: " + this);
        HandleAim();
    }

    public override void OnStateEnter()
    {
        //Debug.Log("Now ENTERING the AIM state.");
        PlayerData.CanMove = false;
        PlayerData.CanJump = false;
        PlayerSprite.color = aimColor;
    }

    public override void OnStateExit()
    {
        //Debug.Log("Now EXITING the AIM state.");
        PlayerData.CanMove = true;
        PlayerData.CanJump = true;
        PlayerSprite.color = startingColor;
    }

    private void HandleAim()
    {
        if (Input.GetMouseButton(1))
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f)) - PlayerData.Player.transform.position;
            PlayerData.angle = Mathf.Clamp(Vector2.SignedAngle(Vector2.right, direction), -45.0f, 45.0f);

            //PlayerData.BoomerangTarget = 

            //---------------------------------------------------------------
            if (PlayerData.angle > -45.0f && PlayerData.angle < 45.0f)
            {
                Debug.Log("The player is aiming to the right.");
                PlayerData.BoomerangTarget = PlayerData.PlayerCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            }
            //else if(PlayerData.angle < 135.0f && PlayerData.angle > -135.0f)
            //{
            //    PlayerData.BoomerangTarget = PlayerData.PlayerCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            //}


            //PlayerData.BoomerangTarget = PlayerData.PlayerCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            Debug.DrawRay(PlayerData.Player.transform.position, (PlayerData.BoomerangTarget - PlayerData.Player.transform.position).normalized *
                                                                 (PlayerData.BoomerangTarget - PlayerData.Player.transform.position).magnitude, Color.red);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            PlayerData.SetState(PlayerData.Throw);
        }
    }
}
