using UnityEngine;

public class BoomerangObj : MonoBehaviour
{
    [SerializeField] private Boomerang cb;
    private NormalBoomerang nb;

    // Start is called before the first frame update
    void Start()
    {
        nb = new NormalBoomerang();
        cb = nb;
        Debug.Log("Current Boomerang = " + cb);
    }

    private void Update()
    {

    }

    void FixedUpdate()
    {
        //HandleBoomerang();
        cb.HandleBoomerang();
    }

    private void HandleBoomerang()
    {
        float proximity;
        /*switch (Mode)
        {
            case BoomerangModes.TRAVEL:
                Direction = PlayerData.BoomerangTarget - transform.position;
                proximity = Direction.magnitude;
                if (proximity > 0.5f && !hasCollided)
                {
                    Brb.MovePosition(transform.position + (Direction.normalized * travelSpeed * Time.deltaTime));
                }
                else if (proximity < 0.5f || hasCollided)
                {
                    Mode = BoomerangModes.RETURN;
                }
                break;

            case BoomerangModes.RETURN:
                Direction = (PlayerData.Player.transform.position - transform.position);
                proximity = Direction.magnitude;
                if (proximity > 1.0f)
                {
                    Brb.MovePosition(transform.position + (Direction.normalized * returnSpeed * Time.deltaTime));
                }
                else
                {
                    PlayerData.BoomerangDeployed = false;
                    Destroy(gameObject);
                }
                break;
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //hasCollided = true;
    }


    /*oy... great little leap today, got a good bit of progress made on starting the different types of boomerangs and a base for all.... I just... I can't "WORDS" today... 
     
     THANKS FOR WATCHING!!! I'm gonna quit for tonight. Hope you tune in again tomorrow same time. I'll be working on this and all its potential glory lol. BYEEEEE!!!!*/
}