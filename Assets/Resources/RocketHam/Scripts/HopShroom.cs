using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopShroom : MonoBehaviour
{
    private PlayerController Player { get; set; }

    [SerializeField] private float shroomJumpForce = 0;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.PlayerData.PlayerRB.velocity = Vector2.zero;
            Player.PlayerData.PlayerRB.AddForce(Vector2.up * shroomJumpForce, ForceMode2D.Impulse);
        }
    }
}
