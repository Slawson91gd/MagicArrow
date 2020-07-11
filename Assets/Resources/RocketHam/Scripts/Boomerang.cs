using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private PlayerControllerData PlayerData;
    private Rigidbody2D Brb { get { return GetComponent<Rigidbody2D>(); } }

    [SerializeField] private float travelSpeed;
    [SerializeField] private float returnSpeed;

    public Vector3 Direction { get; set; }

    public enum BoomerangModes
    {
        TRAVEL,
        RETURN
    }
    [SerializeField] private BoomerangModes Mode;

    // Start is called before the first frame update
    void Start()
    {
        PlayerData = FindObjectOfType<PlayerController>().PlayerData;
        //travelSpeed = 0.5f;
        returnSpeed = travelSpeed * 2.0f;
        Mode = BoomerangModes.TRAVEL;
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        HandleBoomerang();
    }

    private bool IsTraveling() => Mode == BoomerangModes.TRAVEL;
    private bool IsReturning() => Mode == BoomerangModes.RETURN;

    private void HandleBoomerang()
    {
        float proximity;
        if (IsTraveling())
        {
            // Move with rigidbody toward the target position.
            Direction = PlayerData.BoomerangTarget - transform.position;
            proximity = Direction.magnitude;
            if (proximity > 1.0f)
            {
                Brb.MovePosition(transform.position + (Direction.normalized * travelSpeed * Time.deltaTime));
            }
            else
            {
                Mode = BoomerangModes.RETURN;
            }
        }
        else if (IsReturning())
        {
            // Move with rigidbody toward the player.
            Direction = (PlayerData.Player.transform.position - transform.position);
            proximity = Direction.magnitude;
            if(proximity > 1.0f)
            {
                Brb.MovePosition(transform.position + (Direction.normalized * returnSpeed * Time.deltaTime));
            }
            else
            {
                PlayerData.BoomerangDeployed = false;
                Destroy(gameObject);
            }
        }
    }
}
