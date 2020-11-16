using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockBoomerang : Boomerang
{
    public ShockBoomerang(BoomerangObj boomerang) : base(boomerang)
    {
        Type = BoomerangTypes.SHOCK;
        BoomerangColor = Color.yellow;
    }

    public override void HandleBoomerang()
    {
        float proximity;

        switch (Mode)
        {
            case BoomerangModes.IDLE:
                HandleIdleFloat();
                break;

            case BoomerangModes.TRAVEL:
                Direction = PlayerData.BoomerangTarget - BoomerangObject.transform.position;
                proximity = Direction.magnitude;
                if (proximity > 0.5f && !HasCollided)
                {
                    BoomerangRB.MovePosition(BoomerangObject.transform.position + (Direction.normalized * TravelSpeed * Time.deltaTime));
                }
                else if (proximity < 0.5f || HasCollided)
                {
                    SetMode(BoomerangModes.RETURN);
                }
                break;

            case BoomerangModes.RETURN:
                Direction = (PlayerData.Player.transform.position - BoomerangObject.transform.position);
                proximity = Direction.magnitude;
                if (proximity > 1.0f)
                {
                    BoomerangRB.MovePosition(BoomerangObject.transform.position + (Direction.normalized * ReturnSpeed * Time.deltaTime));
                }
                else
                {
                    PlayerData.BoomerangDeployed = false;
                    if (HasCollided)
                    {
                        Collided(false);
                    }
                    SetMode(BoomerangModes.IDLE);
                }
                break;
        }
    }
}
