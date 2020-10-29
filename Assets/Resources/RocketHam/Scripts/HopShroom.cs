using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopShroom : MonoBehaviour
{
    private PlayerController player;

    [SerializeField] private float shroomJumpForce = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.PlayerData.PlayerRB.velocity = Vector2.zero;
            player.PlayerData.PlayerRB.AddForce(Vector2.up * shroomJumpForce, ForceMode2D.Impulse);
        }
    }
}
