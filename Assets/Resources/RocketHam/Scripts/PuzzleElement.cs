using System;
using UnityEngine;

[Serializable]
public class PuzzleElement
{
    public GameObject PuzzleObject { get; set; }

    // is this object active? (can i be interacted with)
    public bool isActive;

    // is the object triggered? (perform my function)
    [SerializeField] private bool isTriggered;

    // Does object use timer?
    public float timer;

    // Does object have time limit?
    public float timeLimit;

    // Does object have a target?
    public GameObject targetObject;

    // Does object have a light?
    public GameObject light;

    public PuzzleElement(GameObject puzzleObj)
    {
        PuzzleObject = puzzleObj;
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

    public void HandleTriggered()
    {
        if (isTriggered)
        {
            if (PuzzleObject.GetComponent<CrystalButton>() != null)
            {
                if (targetObject != null)
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
            else if (PuzzleObject.GetComponent<ShockCrystal>() != null)
            {
                // Turn on the light
                if (light != null && !light.activeSelf)
                {
                    light.SetActive(true);
                }

                // Disable the target object
                if(targetObject != null && targetObject.activeSelf)
                {
                    targetObject.SetActive(false);
                }
                else
                {
                    Debug.Log("Target object is not assigned to.");
                }
            }
            else if(PuzzleObject.GetComponent<TetherStone>() != null)
            {
                if (targetObject.activeSelf)
                {
                    targetObject.SetActive(false);
                }
            }
        }
        else
        {
            if(PuzzleObject.GetComponent<TetherStone>() != null)
            {
                if (!targetObject.activeSelf)
                {
                    targetObject.SetActive(true);
                }
            }
        }
    }

    public void SetTrigger(bool value)
    {
        if (isTriggered != value)
            isTriggered = value;

        HandleTriggered();
    }
}
