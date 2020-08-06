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

    [Tooltip("This is how far the player can move from the center of the screen before the camera starts following the player. (Y)")]
    [Range(0, 6)]
    [SerializeField] private int camOffsetY = 2;

    [SerializeField] private float camDistanceX = 0;

    public Vector3 playerPos;

    private float speed;
    [SerializeField] private float lookSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(Player == null)
        {
            Player = FindObjectOfType<PlayerController>();
        }

        // Starting camera position
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + camOffsetY, -camDepth);
        curCamOffsetX = camMinOffsetX;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        HandleCamera();
        //transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + camOffsetY, -camDepth);
    }

    private void HandleCamera()
    {
        if (Player != null)
        {
            playerPos = new Vector3(Player.transform.position.x, Player.transform.position.y + camOffsetY, Player.transform.position.z);
            Vector3 camPos = transform.position;
            Vector3 relativeCamPos = new Vector3(transform.position.x, transform.position.y, 0);
            Vector3 camDirection = playerPos - relativeCamPos;
            camDistanceX = camDirection.magnitude;

            if (!IsCursorRight() && !IsCursorLeft())
            {
                if (curCamOffsetX != camMinOffsetX)
                {
                    curCamOffsetX = camMinOffsetX;
                }

                // Move camera with player
                speed = camDistanceX;  // (curCamOffsetX - smoothing);
                if (camDistanceX >= curCamOffsetX)
                {
                    transform.position += camDirection * speed * Time.deltaTime;
                }
            }
            else if(IsCursorRight() || IsCursorLeft())
            {
                if (curCamOffsetX != camMaxOffsetX)
                {
                    curCamOffsetX = camMaxOffsetX;
                }

                if (IsCursorRight() && camDistanceX != curCamOffsetX)
                {
                    float interpolate = lookSpeed * Time.deltaTime;
                    camPos.x = Mathf.Lerp(transform.position.x, Player.transform.position.x + curCamOffsetX, interpolate);
                    camPos.y = Mathf.Lerp(transform.position.y, Player.transform.position.y + camOffsetY, interpolate);
                    transform.position = camPos;
                }
                else if (IsCursorLeft() && camDistanceX != curCamOffsetX)
                {
                    float interpolate = lookSpeed * Time.deltaTime;
                    camPos.x = Mathf.Lerp(transform.position.x, Player.transform.position.x - curCamOffsetX, interpolate);
                    camPos.y = Mathf.Lerp(transform.position.y, Player.transform.position.y + camOffsetY, interpolate);
                    transform.position = camPos;
                }
            }
        }
    }

    private bool IsCursorRight() => Input.mousePosition.x >= Screen.width * 0.95f;
    private bool IsCursorLeft() => Input.mousePosition.x <= Screen.width * 0.05f;
}
