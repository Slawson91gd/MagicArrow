using System;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private bool activated;
    [Range(0, 5)][SerializeField] private float speed;
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [SerializeField] private Transform[] targetPoints;


    private enum PlatformStates
    {
        FORWARD,
        BACKWARD,
        MULTIPOINT_FORWARD,
        MULTIPOINT_BACKWARD,
        STOP
    }

    [SerializeField] private PlatformStates currentState;

    private Dictionary<PlatformStates, Action> dic;

    [Range(0, 1)] public float interpolate;

    // Start is called before the first frame update
    void Start()
    {
        //currentState = PlatformStates.STOP;
        currentState = PlatformStates.FORWARD;

        dic = new Dictionary<PlatformStates, Action>
        {
            {PlatformStates.FORWARD, HandleForward },
            {PlatformStates.BACKWARD, HandleBackward }
        };
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlatform();
    }

    private void HandlePlatform()
    {
        dic[currentState].Invoke();
    }

    private void HandleForward()
    {
        Debug.Log("This is the HANDLEFORWARD method.");

        // if there are no optional target points, just move from start to finish and then back again.
        if(targetPoints.Length == 0)
        {
            if (interpolate < 1)
            {
                interpolate = Mathf.Clamp(interpolate + speed * Time.deltaTime, 0, 1);
            }
            transform.position = Vector3.Lerp(start.position, end.position, interpolate);

            if(interpolate == 1)
            {
                SetState(PlatformStates.BACKWARD);
                interpolate = 0;
            }
        }
        else
        {
            // This is what happens if there are optional target points
        }
    }

    private void HandleBackward()
    {
        Debug.Log("This is the HANDLEBACKWARD method.");

        // if there are no optional target points
        if(targetPoints.Length == 0)
        {
            if (interpolate < 1)
            {
                interpolate = Mathf.Clamp(interpolate + speed * Time.deltaTime, 0, 1);
            }
            transform.position = Vector3.Lerp(end.position, start.position, interpolate);

            if (interpolate == 1)
            {
                SetState(PlatformStates.FORWARD);
                interpolate = 0;
            }
        }
        else
        {
            // this is what happens if there are optional target points
        }
    }

    private void SetState(PlatformStates newState)
    {
        if (currentState != newState)
            currentState = newState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("The player just entered the moving platform trigger.");
            collision.gameObject.transform.SetParent(gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("The player just exited the moving platform trigger.");
            collision.gameObject.transform.SetParent(null);
        }
    }
}
