using UnityEngine;

public class BrambleDamage : MonoBehaviour
{
    private PlayerController player;

    private int brambleDamage;
    private bool playerDetected;

    private float timer;
    [SerializeField] private float timeLimit = 0;

    private float inBrambleSpeed;
    private float originSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        brambleDamage = 10;
        playerDetected = false;
        timer = 0;

        originSpeed = player.PlayerData.MoveSpeed;
        inBrambleSpeed = originSpeed / 2;
    }

    // Update is called once per frame
    void Update()
    {
        DamagePlayer(playerDetected);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            player.PlayerData.ModifyHP(-brambleDamage);
            player.PlayerData.AdjustMoveSpeed(inBrambleSpeed);
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            player.PlayerData.AdjustMoveSpeed(originSpeed);
            playerDetected = false;
            timer = 0;
        }
    }

    private void DamagePlayer(bool detected)
    {
        if (detected != false)
        {
            if(timer < timeLimit)
            {
                timer += Time.fixedDeltaTime;
            }
            else
            {
                timer = 0;
                player.PlayerData.ModifyHP(-brambleDamage);
            }
        }
    }
}
