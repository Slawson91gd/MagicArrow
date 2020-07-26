using Boo.Lang.Environments;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomCameraFollow : MonoBehaviour
{
    private PlayerController Player { get; set; }

    [Tooltip("Speed at which the camera will follow the player.")]
    [Range(0, 10)]
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

    [Tooltip("This is how far the player can move from the center of the screen before the camera starts following the player. (Y)")]
    [Range(0, 6)]
    [SerializeField] private int camOffsetY = 2;

    [SerializeField] private float camDistanceX = 0;
    [SerializeField] private float curCamOffset;

    public float testDistance;
    public Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        if(Player == null)
        {
            Player = FindObjectOfType<PlayerController>();
        }

        // Starting camera position
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + camOffsetY);
        curCamOffset = camMinOffsetX;
    }

    // Update is called once per frame
    void Update()
    {
        HandleCamera();

        
    }

    private void HandleCamera()
    {
        if (Player != null)
        {
            playerPos = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
            Vector3 camPos = new Vector3(transform.position.x, transform.position.y, 0);
            camDistanceX = playerPos.x - camPos.x;
            
            // Set Camera Depth
            if (transform.position.z != camDepth)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -camDepth);
            }

            // Standard camera follow
            //if(camDistanceX > curCamOffset - 0.5f || camDistanceX < -curCamOffset + 0.5f)
            //{
            //    transform.position += new Vector3(playerPos.x - camPos.x, 0) * smoothing * Time.deltaTime;
            //}

              if(camDistanceX > curCamOffset || camDistanceX < -curCamOffset)
              {
                //transform.position += new Vector3(playerPos.x - camPos.x, 0) * smoothing * Time.deltaTime;
                transform.position += new Vector3(playerPos.x - camDistanceX, 0) * smoothing * Time.deltaTime;
              }


            // if cursor is on the right side of the screen
            if (IsCursorRight())
            {
                // Set maximum offset
                if(curCamOffset != camMaxOffsetX)
                {
                    curCamOffset = camMaxOffsetX;
                    smoothing = 1.0f;
                }

                // If camDistance is still in range, move camera to the right of the player. 
                if(camDistanceX > -curCamOffset + 1)
                {
                    transform.position += Vector3.right * 15 * Time.deltaTime;
                }
                else if(camDistanceX < -curCamOffset + 0.75f)
                {
                    transform.position += new Vector3(playerPos.x - camPos.x, 0) * smoothing * Time.deltaTime;
                }
            }
            else
            {
                if (curCamOffset != camMinOffsetX)
                {
                    curCamOffset = camMinOffsetX;
                    smoothing = 1.0f;
                }
            }
        }
    }

    private bool IsCursorRight() => Input.mousePosition.x >= Screen.width * 0.95f;
    private bool IsCursorLeft() => Input.mousePosition.x <= Screen.width * 0.05f;
}
