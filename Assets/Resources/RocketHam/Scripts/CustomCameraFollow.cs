using UnityEngine;

public class CustomCameraFollow : MonoBehaviour
{
    private PlayerController Player { get; set; }

    [Tooltip("Speed at which the camera will follow the player.")]
    [Range(0, 2)]
    [SerializeField] private float smoothing = 1.5f;

    [Tooltip("This is how far away the camera is from the player object (z axis).")]
    [Range(1, 20)]
    [SerializeField] private float camDepth = 10.0f;

    [Tooltip("This is how far the player can move from the center of the screen before the camera starts following the player. (X)")]
    [Range(0, 10)]
    [SerializeField] private int camMinOffsetX = 3;

    [Tooltip("The added offset for cam (X)")]
    [Range(0, 10)]
    [SerializeField] private int camMaxOffsetX;

    [SerializeField] private float curCamOffsetX;

    [SerializeField] private float camDistanceX = 0;

    [Tooltip("This is how far the player can move from the center of the screen before the camera starts following the player. (Y)")]
    [Range(0, 10)]
    [SerializeField] private int camMinOffsetY = 2;

    [Tooltip("This is the added offset on the Y axis for looking 'off screen'.")]
    [Range(0, 10)]
    [SerializeField] private int camMaxOffsetY;

    [SerializeField] private int curCamOffsetY = 0;

    [SerializeField] private float camDistanceY = 0;

    public Vector3 playerPos;

    private float speedX;
    private float speedY;
    [SerializeField] private float lookSpeed = 5.0f;

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

    public CameraDirection CamDirection;

    // Start is called before the first frame update
    void Start()
    {
        if(Player == null)
        {
            Player = FindObjectOfType<PlayerController>();
        }

        // Starting camera position
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + camMinOffsetY, -camDepth);
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
                speedX = camDistanceX;
                if (camDistanceX >= camMinOffsetX)
                {
                    transform.position += new Vector3(playerPos.x - relativeCamPos.x, 0, 0) * speedX * Time.deltaTime;
                }

                speedY = camDistanceY;
                if (camDistanceY >= camMinOffsetY)
                {
                    transform.position += new Vector3(0, playerPos.y - relativeCamPos.y, 0) * speedY * Time.deltaTime;
                }

                break;

            case CameraDirection.RIGHT:
                if(camDistanceX != camMaxOffsetX)
                {
                    camPos.x = Mathf.Lerp(transform.position.x, playerPos.x + camMaxOffsetX, interpolate);
                }
                if(camDistanceY > camMinOffsetY)
                {
                    camPos.y = Mathf.Lerp(transform.position.y, playerPos.y, camDistanceY * Time.deltaTime);
                }
                transform.position = camPos;
                break;

            case CameraDirection.LEFT:
                if(camDistanceX != camMaxOffsetX)
                {
                    camPos.x = Mathf.Lerp(transform.position.x, playerPos.x - camMaxOffsetX, interpolate);
                }
                if(camDistanceY > camMinOffsetY)
                {
                    camPos.y = Mathf.Lerp(transform.position.y, playerPos.y, camDistanceY * Time.deltaTime);
                }
                transform.position = camPos;
                break;

            case CameraDirection.UP:
                if(camDistanceX > camMinOffsetX)
                {
                    camPos.x = Mathf.Lerp(transform.position.x, playerPos.x, camDistanceX * Time.deltaTime);
                }
                if(camDistanceY != camMaxOffsetY)
                {
                    camPos.y = Mathf.Lerp(transform.position.y, playerPos.y + camMaxOffsetY, interpolate);
                }
                transform.position = camPos;
                break;

            case CameraDirection.DOWN:
                if(camDistanceX > camMinOffsetX)
                {
                    camPos.x = Mathf.Lerp(transform.position.x, playerPos.x, camDistanceX * Time.deltaTime);
                }
                if(camDistanceY != camMaxOffsetY)
                {
                    camPos.y = Mathf.Lerp(transform.position.y, playerPos.y - camMaxOffsetY, interpolate);
                }
                transform.position = camPos;
                break;

            case CameraDirection.RIGHT_UP:
                if (camDistanceX != camMaxOffsetX)
                {
                    camPos.x = Mathf.Lerp(transform.position.x, playerPos.x + camMaxOffsetX, interpolate);
                }
                if(camDistanceY != camMaxOffsetY)
                {
                    camPos.y = Mathf.Lerp(transform.position.y, playerPos.y + camMaxOffsetY, interpolate);
                }
                transform.position = camPos;
                break;

            case CameraDirection.RIGHT_DOWN:
                if(camDistanceX != camMaxOffsetX)
                {
                    camPos.x = Mathf.Lerp(transform.position.x, playerPos.x + camMaxOffsetX, interpolate);
                }
                if(camDistanceY != camMaxOffsetY)
                {
                    camPos.y = Mathf.Lerp(transform.position.y, playerPos.y - camMaxOffsetY, interpolate);
                }
                transform.position = camPos;
                break;

            case CameraDirection.LEFT_UP:
                if(camDistanceX != camMaxOffsetX)
                {
                    camPos.x = Mathf.Lerp(transform.position.x, playerPos.x - camMaxOffsetX, interpolate);
                }
                if(camDistanceY != camMaxOffsetY)
                {
                    camPos.y = Mathf.Lerp(transform.position.y, playerPos.y + camMaxOffsetY, interpolate);
                }
                transform.position = camPos;
                break;

            case CameraDirection.LEFT_DOWN:
                if(camDistanceX != camMaxOffsetX)
                {
                    camPos.x = Mathf.Lerp(transform.position.x, playerPos.x - camMaxOffsetX, interpolate);
                }
                if(camDistanceY != camMaxOffsetY)
                {
                    camPos.y = Mathf.Lerp(transform.position.y, playerPos.y - camMaxOffsetY, interpolate);
                }
                transform.position = camPos;
                break;
        }
    }

    private bool IsCursorRight() => Input.mousePosition.x >= Screen.width * 0.95f;
    private bool IsCursorLeft() => Input.mousePosition.x <= Screen.width * 0.05f;
    private bool IsCursorUp() => Input.mousePosition.y >= Screen.height * 0.95f;
    private bool IsCursorDown() => Input.mousePosition.y <= Screen.height * 0.05f;
    private bool IsCursorRightDown() => Input.mousePosition.x >= Screen.width * 0.95f && Input.mousePosition.y <= Screen.height * 0.05f;
    private bool IsCursorRightUp() => Input.mousePosition.x >= Screen.width * 0.95f && Input.mousePosition.y >= Screen.height * 0.95f;
    private bool IsCursorLeftUp() => Input.mousePosition.x <= Screen.width * 0.05f && Input.mousePosition.y >= Screen.height * 0.95f;
    private bool IsCursorLeftDown() => Input.mousePosition.x <= Screen.width * 0.05f && Input.mousePosition.y <= Screen.height * 0.05f;

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


        /*else if(IsCursorRight() && IsCursorUp())
        {
            CamDirection = CameraDirection.RIGHT_UP;
        }
        else if(IsCursorRight() && IsCursorDown())
        {
            CamDirection = CameraDirection.RIGHT_DOWN;
        }
        else if(IsCursorLeft() && IsCursorUp())
        {
            CamDirection = CameraDirection.LEFT_UP;
        }
        else if(IsCursorLeft() && IsCursorDown())
        {
            CamDirection = CameraDirection.LEFT_DOWN;
        }
        else if (IsCursorRightDown())
        {
            CamDirection = CameraDirection.RIGHT_DOWN;
        }
        else
        {
            CamDirection = CameraDirection.NORMAL;
        }*/
    }
}
