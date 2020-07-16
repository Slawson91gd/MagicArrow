using UnityEngine;

public class CustomCameraFollow : MonoBehaviour
{
    private PlayerController Player { get; set; }

    [Tooltip("Speed at which the camera will follow the player.")]
    [Range(0, 3)]
    [SerializeField] private float smoothing = 2.5f;

    [Tooltip("This is how far away the camera is from the player object (z axis).")]
    [Range(1, 20)]
    [SerializeField] private float camDistance = 10.0f;

    [Tooltip("This is how far the player can move from the center of the screen before the camera starts following the player. (X & Y)")]
    [Range(0, 4)]
    [SerializeField] private int camOffset = 1;

    [Range(0, 4)]
    [SerializeField] private int camOffsetY = 3;

    // Start is called before the first frame update
    void Start()
    {
        if(Player == null)
        {
            Player = FindObjectOfType<PlayerController>();
        }

        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + camOffsetY);
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (Player != null)
        {
            if(transform.position.z != camDistance)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -camDistance);
            }

            Vector3 playerPos = new Vector3(Player.transform.position.x, Player.transform.position.y, -camDistance);
            float distance = (playerPos - transform.position).magnitude;
            if(distance > camOffset)
            {
                Vector3 camMovement = new Vector3(playerPos.x - transform.position.x, (playerPos.y + camOffsetY) - transform.position.y);
                transform.position += camMovement * smoothing * Time.deltaTime;
            }
        }
    }
}
