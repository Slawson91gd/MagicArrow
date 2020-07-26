using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX0_Custom2DCameraFollow : MonoBehaviour
{
    [Tooltip("This is the object you want the camera to follow.")]
    [SerializeField] private GameObject targetObject;

    [Tooltip("Speed at which the camera will follow the player.")]
    [Range(0, 10)]
    [SerializeField] private float smoothing;

    [Tooltip("This is how far away the camera is from the player object (z axis).")]
    [Range(1, 20)]
    [SerializeField] private float camDepth;

    [Tooltip("This is how far the player can move from the center of the screen before the camera starts following the player. (X)")]
    [Range(0, 6)]
    [SerializeField] private int camOffsetX;

    [Tooltip("This is how far the player can move from the center of the screen before the camera starts following the player. (Y)")]
    [Range(0, 6)]
    [SerializeField] private int camOffsetY;

    private float camDistanceX;
    private float camDistanceY;



    // Start is called before the first frame update
    void Start()
    {
        if(targetObject != null)
        transform.position = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y + camOffsetY);
    }

    // Update is called once per frame
    void Update()
    {
        HandleCamera();
    }

    private void HandleCamera()
    {
        if (targetObject != null)
        {
            if (transform.position.z != camDepth)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -camDepth);
            }

            Vector3 playerPos = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, -camDepth);
            camDistanceX = new Vector3(playerPos.x - transform.position.x, 0).magnitude;
            camDistanceY = new Vector3(0, (playerPos.y + camOffsetY) - transform.position.y).magnitude;

            if (camDistanceX > camOffsetX)
            {
                Vector3 camMovementX = new Vector3(playerPos.x - transform.position.x, 0);
                transform.position += camMovementX * smoothing * Time.deltaTime;
            }

            if (camDistanceY > camOffsetY)
            {
                Vector3 camMovementY = new Vector3(0, (playerPos.y + camOffsetY) - transform.position.y);
                transform.position += camMovementY * smoothing * Time.deltaTime;
            }

        }
    }
}
