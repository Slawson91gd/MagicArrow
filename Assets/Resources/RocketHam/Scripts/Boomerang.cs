﻿using UnityEngine;

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