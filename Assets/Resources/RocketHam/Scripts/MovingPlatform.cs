using System;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Rigidbody2D Rbody { get; set; }
    [SerializeField] private bool activated;
    public bool Activated { get { return activated; } }
    [Range(1, 10)][SerializeField] private int startPos;
    [Range(0, 5)][SerializeField] private float speed;
    [SerializeField] private Transform[] targetPoints;
    [SerializeField] private int index;
    [SerializeField] private bool forward;


    private enum PlatformStates
    {
        FORWARD,
        BACKWARD,
        PINGPONG,
        LOOP,
        STOP
    }

    [SerializeField] private PlatformStates currentState;

    private Dictionary<PlatformStates, Action> dic;

    [Range(0, 1)] public float interpolate;

    // Start is called before the first frame update
    void Start()
    {
        //Rbody = GetComponent<Rigidbody2D>();

        dic = new Dictionary<PlatformStates, Action>
        {
            {PlatformStates.FORWARD, HandleForward },
            {PlatformStates.BACKWARD, HandleBackward },
            {PlatformStates.PINGPONG, HandlePingPong },
            {PlatformStates.LOOP, HandleLoop},
            {PlatformStates.STOP, HandleStop }
        };

        SetStartPosition(startPos);
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlatform();
    }

    private void HandlePlatform()
    {
        if (activated)
        {
            dic[currentState].Invoke();
        }
    }

    private void SetStartPosition(int posNum)
    {
        if(posNum > targetPoints.Length)
        {
            posNum = targetPoints.Length;
            startPos = posNum;
        }

        if(index != posNum - 1)
        {
            index = posNum - 1;
            transform.position = targetPoints[index].position;
        }
    }

    private void HandlePingPong()
    {
        if(interpolate < 1)
        {
            interpolate = Mathf.Clamp(interpolate + speed * Time.deltaTime, 0, 1);
        }

        if(targetPoints.Length == 2)
        {
            switch (index)
            {
                case 0:
                    transform.localPosition = Vector3.Lerp(targetPoints[index].localPosition, targetPoints[index + 1].localPosition, interpolate);
                    //Rbody.position = Vector3.Lerp(targetPoints[index].position, targetPoints[index + 1].position, interpolate);
                    if (transform.localPosition == targetPoints[index + 1].localPosition)
                    {
                        index++;
                        interpolate = 0;
                    }
                    break;

                case 1:
                    transform.localPosition = Vector3.Lerp(targetPoints[index].localPosition, targetPoints[index - 1].localPosition, interpolate);
                    //Rbody.position = Vector3.Lerp(targetPoints[index].position, targetPoints[index - 1].position, interpolate);
                    if (transform.localPosition == targetPoints[index - 1].localPosition)
                    {
                        index--;
                        interpolate = 0;
                    }
                    break;
            }
        }
        else if(targetPoints.Length > 2)
        {
            switch (forward)
            {
                case false:
                    if(index != 0)
                    {
                        transform.position = Vector3.Lerp(targetPoints[index].position, targetPoints[index - 1].position, interpolate);
                        if(transform.position == targetPoints[index - 1].position)
                        {
                            index--;
                            interpolate = 0;
                            if (index == 0)
                                forward = true;
                        }
                    }
                    break;

                case true:
                    if (index != targetPoints.Length - 1)
                    {
                        transform.position = Vector3.Lerp(targetPoints[index].position, targetPoints[index + 1].position, interpolate);
                        if(transform.position == targetPoints[index + 1].position)
                        {
                            index++;
                            interpolate = 0;
                            if (index == targetPoints.Length - 1)
                                forward = false;
                        }
                    }
                    break;
            }
        }
    }

    private void HandleLoop()
    {
        if(interpolate < 1)
        {
            interpolate = Mathf.Clamp(interpolate + speed * Time.deltaTime, 0, 1);
        }

        if (index != targetPoints.Length - 1)
        {
            transform.position = Vector3.Lerp(targetPoints[index].position, targetPoints[index + 1].position, interpolate);
            if(transform.position == targetPoints[index + 1].position)
            {
                index++;
                interpolate = 0;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(targetPoints[index].position, targetPoints[0].position, interpolate);
            if(transform.position == targetPoints[0].position)
            {
                index = 0;
                interpolate = 0;
            }
        }
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
            if (index != 1)
            {
                transform.position = Vector3.Lerp(targetPoints[index].position, targetPoints[index + 1].position, interpolate);
                if (transform.position == targetPoints[index + 1].position)
                    index++;
            }
            else
            {
                SetState(PlatformStates.STOP);
            }
        }
        // if there are optional points
        else if(targetPoints.Length > 2)
        {
            if (index != targetPoints.Length - 1)
            {
                transform.position = Vector3.Lerp(targetPoints[index].position, targetPoints[index + 1].position, interpolate);
                if (transform.position == targetPoints[index + 1].position)
                {
                    if((index + 1) != targetPoints.Length - 1)
                    {
                        interpolate = 0;
                    }
                    index++;
                }
            }
            else
            {
                SetState(PlatformStates.STOP);
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
            if (index != 0)
            {
                transform.position = Vector3.Lerp(targetPoints[index].position, targetPoints[index - 1].position, interpolate);
                if (transform.position == targetPoints[index - 1].position)
                    index--;
            }
            else
            {
                SetState(PlatformStates.STOP);
            }
        }
        else if(targetPoints.Length > 2)
        {
            if (index != 0)
            {
                transform.position = Vector3.Lerp(targetPoints[index].position, targetPoints[index - 1].position, interpolate);
                if (transform.position == targetPoints[index - 1].position)
                {
                    if ((index - 1) != 0)
                    {
                        interpolate = 0;
                    }
                    index--;
                }
            }
            else
            {
                SetState(PlatformStates.STOP);
            }
        }
    }

    private void HandleStop()
    {
        Debug.Log("The moving platform has stopped.");
    }

    private void SetState(PlatformStates newState)
    {
        if (currentState != newState)
            currentState = newState;
    }

    public void SetActiveState(bool value)
    {
        if(activated != value)
        {
            activated = value;
        }
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
