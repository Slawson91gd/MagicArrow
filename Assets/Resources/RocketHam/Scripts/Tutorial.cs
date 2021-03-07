using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private bool displayMessage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayMessage();
    }

    private void DisplayMessage()
    {
        // if a message is to be displayed
        if (displayMessage)
        {
            // print a message to the console.
            Debug.Log("Press the 'A' & 'D' keys to move the player left or right.");

            // pause the game
            if(Time.timeScale != 0)
            {
                Time.timeScale = 0;
            }

            // if the player presses ENTER, turn off message and reset time scale
            if (Input.GetKeyDown(KeyCode.Return))
            {
                displayMessage = !displayMessage;
            }
        }
        else
        {
            if(Time.timeScale != 1)
            {
                Time.timeScale = 1;
            }
        }
    }
}
