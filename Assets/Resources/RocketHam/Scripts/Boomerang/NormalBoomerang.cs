using UnityEngine;

public class NormalBoomerang : Boomerang
{
    public NormalBoomerang(BoomerangObj boomerang) : base(boomerang)
    {
        
    }

    public override void HandleBoomerang()
    {
        float proximity;

        switch (Mode)
        {
            case BoomerangModes.IDLE:
                Debug.Log("IDLE MODE");
                HandleIdleFloat();
                break;

            case BoomerangModes.TRAVEL:
                Debug.Log("TRAVEL MODE");
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
                Debug.Log("RETURN MODE");
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
