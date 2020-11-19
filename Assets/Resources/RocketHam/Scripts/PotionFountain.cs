using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionFountain : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.PlayerData.RestoreHealth();
            player.PlayerData.RestorePotion();
            if (player.PlayerData.Checkpoint != this)
            {
                player.PlayerData.Checkpoint = gameObject;
            }
        }
    }
}
