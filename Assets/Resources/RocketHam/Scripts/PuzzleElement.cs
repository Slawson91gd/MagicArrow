using System;
using UnityEngine;

[Serializable]
public class PuzzleElement
{
    // is this object active? (can i be interacted with)
    public bool isActive;

    // is the object triggered? (perform my function)
    public bool isTriggered;

    // Does object use timer?
    public float timer;

    // Does object have time limit?
    public float timeLimit;

    // Does object have a target?
    public GameObject targetObject;

    public PuzzleElement()
    {
        isActive = true;
        isTriggered = false;
        timer = 0;
    }

    // Start Timer
    public virtual void HandleTimer()
    {
        if(timer <= timeLimit)
        {
            timer += Time.fixedDeltaTime;
        }
        else
        {
            Debug.Log("Timer just finished!");
            timer = 0;
        }
    }

    public virtual void HandleTriggered()
    {
        if (isTriggered)
        {
            if(targetObject != null)
            {
                switch (targetObject.tag)
                {
                    case "Door":
                        // Functionality here
                        targetObject.SetActive(false);
                        break;
                }
            }
            else
            {
                Debug.Log("Target object is not assigned to.");
            }
        }
        else
        {
            // Don't Move
        }
    }

}
