﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerControllerData playerData;
    public PlayerControllerData PlayerData { get { return playerData; } private set { playerData = value; } }

    // Start is called before the first frame update
    void Awake()
    {
        PlayerData = new PlayerControllerData(this);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerData.CurrentState.Tick();
        PlayerData.SelectBoomerangUpgrade();
        PlayerData.PlayAnim();


        // Test code to reduce player health
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayerData.TakeDamage(10);
        }

        // Test Code to increase player health
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PlayerData.HealDamage(20);
        }

        // Test code to use potion (increase player health, decrease player potion)
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerData.UsePotion(PlayerData.MaxPlayerHealth - PlayerData.PlayerHealth);
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        PlayerData.OnGround = PlayerData.IsGrounded();
    }

}
