using UnityEngine;

public class CustomCameraFollow : MonoBehaviour
{
    private PlayerController Player { get; set; }

    [Tooltip("This is how far away the camera is from the player object (z axis).")]
    [Range(1, 20)]
    [SerializeField] private float camDepth = 10.0f;

    [Tooltip("This is how far the player can move from the center of the screen before the camera starts following the player. (X)")]
    [Range(0, 10)]
    [SerializeField] private int camMinOffsetX = 3;

    [Tooltip("The added offset for cam (X)")]
    [Range(0, 10)]
    [SerializeField] private int camMaxOffsetX;

    [Tooltip("Cameras current X axis offset.")]
    [SerializeField] private float curCamOffsetX;

    [Tooltip("The cameras current distance from the player on the X axis.")]
    [SerializeField] private float camDistanceX = 0;

    [Tooltip("This is how far the player can move from the center of the screen before the camera starts following the player. (Y)")]
    [Range(0, 10)]
    [SerializeField] private int camMinOffsetY = 2;

    [Tooltip("This is the added offset on the Y axis for looking 'off screen'.")]
    [Range(0, 10)]
    [SerializeField] private int camMaxOffsetY;

    [Tooltip("Cameras current Y axis offset.")]
    [SerializeField] private int curCamOffsetY = 0;

    [Tooltip("The cameras current distance from the player on the Y axis.")]
    [SerializeField] private float camDistanceY = 0;

    public Vector3 playerPos;

    [Tooltip("The speed at which the camera follows the player and moves to offsets.")]
    [Range(1, 10)]
    [SerializeField] private float lookSpeed = 5.0f;

    private Vector3 targetPos;

    public enum CameraDirection
    {
        NORMAL,
        LEFT,
        RIGHT,
        UP,
        DOWN,
        LEFT_UP,
        RIGHT_UP,
        RIGHT_DOWN,
        LEFT_DOWN
    }

    [Tooltip("The current directional state of the camera. (!!--HANDLED BY CODE!!--")]
    public CameraDirection CamDirection;

    // Start is called before the first frame update
    void Start()
    {
        if(Player == null)
        {
            Player = FindObjectOfType<PlayerController>();
        }

        // Starting camera position
        targetPos = new Vector3(Player.transform.position.x, Player.transform.position.y + camMinOffsetY, -camDepth);
        transform.position = targetPos;
        CamDirection = CameraDirection.NORMAL;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SetDirection();
        HandleCamera();
    }

    private void HandleCamera()
    {
        if (Player != null)
        {
            playerPos = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
            Vector3 camPos = transform.position;
            Vector3 relativeCamPos = new Vector3(camPos.x, camPos.y, 0);
            camDistanceX = Mathf.Abs(playerPos.x - relativeCamPos.x);
            camDistanceY = Mathf.Abs(playerPos.y - relativeCamPos.y);
            float interpolate = lookSpeed * Time.deltaTime;

            switch (CamDirection)
            {
                case CameraDirection.NORMAL:
                    // Move camera X with player X
                    if (camDistanceX > camMinOffsetX)
                    {
                        if(camPos.x > playerPos.x)
                        {
                            targetPos.x = playerPos.x + camMinOffsetX;
                        }
                        else
                        {
                            targetPos.x = playerPos.x - camMinOffsetX;
                        }
                    }

                    if (camDistanceY > camMinOffsetY)
                    {
                        if(camPos.y > playerPos.y)
                        {
                            targetPos.y = playerPos.y + camMinOffsetY;
                        }
                        else
                        {
                            targetPos.y = playerPos.y - camMinOffsetY;
                        }
                    }
                    break;

                case CameraDirection.RIGHT:
                    if (camDistanceX != camMaxOffsetX)
                    {
                        targetPos.x = playerPos.x + camMaxOffsetX;
                    }
                    if (camDistanceY > camMinOffsetY)
                    {
                        if (camPos.y > playerPos.y)
                        {
                            targetPos.y = playerPos.y + camMinOffsetY;
                        }
                        else
                        {
                            targetPos.y = playerPos.y - camMinOffsetY;
                        }
                    }
                    break;

                case CameraDirection.LEFT:
                    if (camDistanceX != camMaxOffsetX)
                    {
                        targetPos.x = playerPos.x - camMaxOffsetX;
                    }
                    if (camDistanceY > camMinOffsetY)
                    {
                        if(camPos.y > playerPos.y)
                        {
                            targetPos.y = playerPos.y + camMinOffsetY;
                        }
                        else
                        {
                            targetPos.y = playerPos.y - camMinOffsetY;
                        }
                    }
                    break;

                case CameraDirection.UP:
                    if (camDistanceX > camMinOffsetX)
                    {
                        if(camPos.x > playerPos.x)
                        {
                            targetPos.x = playerPos.x + camMinOffsetX;
                        }
                        else
                        {
                            targetPos.x = playerPos.x - camMinOffsetX;
                        }
                    }
                    if (camDistanceY != camMaxOffsetY)
                    {
                        targetPos.y = playerPos.y + camMaxOffsetY;
                    }
                    break;

                case CameraDirection.DOWN:
                    if (camDistanceX > camMinOffsetX)
                    {
                        if (camPos.x > playerPos.x)
                        {
                            targetPos.x = playerPos.x + camMinOffsetX;
                        }
                        else
                        {
                            targetPos.x = playerPos.x - camMinOffsetX;
                        }
                    }
                    if (camDistanceY != camMaxOffsetY)
                    {
                        targetPos.y = playerPos.y - camMaxOffsetY;
                    }
                    break;

                case CameraDirection.RIGHT_UP:
                    if (camDistanceX != camMaxOffsetX)
                    {
                        targetPos.x = playerPos.x + camMaxOffsetX;
                    }
                    if (camDistanceY != camMaxOffsetY)
                    {
                        targetPos.y = playerPos.y + camMaxOffsetY;
                    }
                    break;

                case CameraDirection.RIGHT_DOWN:
                    if (camDistanceX != camMaxOffsetX)
                    {
                        targetPos.x = playerPos.x + camMaxOffsetX;
                    }
                    if (camDistanceY != camMaxOffsetY)
                    {
                        targetPos.y = playerPos.y - camMaxOffsetY;
                    }
                    break;

                case CameraDirection.LEFT_UP:
                    if (camDistanceX != camMaxOffsetX)
                    {
                        targetPos.x = playerPos.x - camMaxOffsetX;
                    }
                    if (camDistanceY != camMaxOffsetY)
                    {
                        targetPos.y = playerPos.y + camMaxOffsetY;
                    }
                    break;

                case CameraDirection.LEFT_DOWN:
                    if (camDistanceX != camMaxOffsetX)
                    {
                        targetPos.x = playerPos.x - camMaxOffsetY;
                    }
                    if (camDistanceY != camMaxOffsetY)
                    {
                        targetPos.y = playerPos.y - camMaxOffsetY;
                    }
                    break;
            }
            transform.position = Vector3.Lerp(camPos, targetPos, interpolate);
        }
    }

    private bool IsCursorRight() => Input.mousePosition.x >= Screen.width * 0.95f && Input.GetMouseButton(1);
    private bool IsCursorLeft() => Input.mousePosition.x <= Screen.width * 0.05f && Input.GetMouseButton(1);
    private bool IsCursorUp() => Input.mousePosition.y >= Screen.height * 0.95f && Input.GetMouseButton(1);
    private bool IsCursorDown() => Input.mousePosition.y <= Screen.height * 0.05f && Input.GetMouseButton(1);
    private bool IsCursorRightDown() => Input.mousePosition.x >= Screen.width * 0.95f && Input.mousePosition.y <= Screen.height * 0.05f && Input.GetMouseButton(1);
    private bool IsCursorRightUp() => Input.mousePosition.x >= Screen.width * 0.95f && Input.mousePosition.y >= Screen.height * 0.95f && Input.GetMouseButton(1);
    private bool IsCursorLeftUp() => Input.mousePosition.x <= Screen.width * 0.05f && Input.mousePosition.y >= Screen.height * 0.95f && Input.GetMouseButton(1);
    private bool IsCursorLeftDown() => Input.mousePosition.x <= Screen.width * 0.05f && Input.mousePosition.y <= Screen.height * 0.05f && Input.GetMouseButton(1);

    private void SetDirection()
    {
        if (IsCursorRight())
        {
            if(CamDirection != CameraDirection.RIGHT)
            CamDirection = CameraDirection.RIGHT;
        }
        else if (IsCursorLeft())
        {
            if(CamDirection != CameraDirection.LEFT)
            CamDirection = CameraDirection.LEFT;
        }
        else if (IsCursorUp())
        {
            if(CamDirection != CameraDirection.UP)
            CamDirection = CameraDirection.UP;
        }
        else if (IsCursorDown())
        {
            if(CamDirection != CameraDirection.DOWN)
            CamDirection = CameraDirection.DOWN;
        }
        else
        {
            if(CamDirection != CameraDirection.NORMAL)
            CamDirection = CameraDirection.NORMAL;
        }

        if(IsCursorRightDown())
        {
            if(CamDirection != CameraDirection.RIGHT_DOWN)
            CamDirection = CameraDirection.RIGHT_DOWN;
        }
        else if (IsCursorRightUp())
        {
            if (CamDirection != CameraDirection.RIGHT_UP)
                CamDirection = CameraDirection.RIGHT_UP;
        }
        else if (IsCursorLeftUp())
        {
            if (CamDirection != CameraDirection.LEFT_UP)
                CamDirection = CameraDirection.LEFT_UP;
        }
        else if (IsCursorLeftDown())
        {
            if (CamDirection != CameraDirection.LEFT_DOWN)
                CamDirection = CameraDirection.LEFT_DOWN;
        }
    }
}
