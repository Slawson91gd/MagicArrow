using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCameraFollow : MonoBehaviour
{
    private PlayerController Player { get; set; }

    [Tooltip("Speed at which the camera will follow the player.")]
    [SerializeField] private float moveSpeed = 5.0f;

    [Tooltip("This is how far away the camera is from the player object (z axis).")]
    [SerializeField] private float distance = 10.0f;

    private Vector3 CameraMovement { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if(Player == null)
        {
            Player = FindObjectOfType<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        CameraMovement = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z - distance);
        transform.position = CameraMovement;
    }
}
