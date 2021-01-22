using System;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private bool activated;
    [Range(0, 5)][SerializeField] private float speed;
    [SerializeField] private Transform[] targetPoints;
    [SerializeField] private int targetIndex;


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

        targetIndex = 0;
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
        if (interpolate < 1)
        {
            interpolate = Mathf.Clamp(interpolate + speed * Time.deltaTime, 0, 1);
        }

        // if there are no optional target points, just move from start to finish and then back again.
        if (targetPoints.Length == 2)
        {
            transform.position = Vector3.Lerp(targetPoints[targetIndex].position, targetPoints[targetIndex + 1].position, interpolate);

            if(interpolate == 1)
            {
                SetState(PlatformStates.BACKWARD);
                interpolate = 0;
            }
        }
        else if(targetPoints.Length > 2)
        {
            transform.position = Vector3.Lerp(targetPoints[targetIndex].position, targetPoints[targetIndex + 1].position, interpolate);

            if(interpolate == 1 && targetIndex < targetPoints.Length - 2)
            {
                interpolate = 0;
                targetIndex++;
            }
            else if(interpolate == 1 && targetIndex == targetPoints.Length - 2)
            {
                Debug.Log("The multipoint forward sequence is complete.");
                SetState(PlatformStates.BACKWARD);
            }
        }
    }

    private void HandleBackward()
    {
        Debug.Log("This is the HANDLEBACKWARD method.");
        if (interpolate < 1)
        {
            interpolate = Mathf.Clamp(interpolate + speed * Time.deltaTime, 0, 1);
        }

        // if there are no optional target points
        if (targetPoints.Length == 2)
        {
            transform.position = Vector3.Lerp(targetPoints[1].position, targetPoints[0].position, interpolate);

            if (interpolate == 1)
            {
                SetState(PlatformStates.FORWARD);
                interpolate = 0;
            }
        }
        else if(targetPoints.Length > 2)
        {
            // this is what happens if there are optional target points
            transform.position = Vector3.Lerp(targetPoints[targetIndex].position, targetPoints[targetIndex - 1].position, interpolate);

            if (interpolate == 1 && targetIndex > 1)
            {
                interpolate = 0;
                targetIndex--;
            }
            else if (interpolate == 1 && targetIndex == 1)
            {
                Debug.Log("The multipoint backward sequence is complete.");
            }
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
