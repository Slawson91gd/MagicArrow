using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private PlayerControllerData PlayerData;
    private Rigidbody2D Brb { get { return GetComponent<Rigidbody2D>(); } }

    [SerializeField] private float travelSpeed;
    [SerializeField] private float returnSpeed;

    [SerializeField] private bool hasCollided;

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

        hasCollided = false;
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        HandleBoomerang();
    }

    private void HandleBoomerang()
    {
        float proximity;
        switch (Mode)
        {
            case BoomerangModes.TRAVEL:
                Direction = PlayerData.BoomerangTarget - transform.position;
                proximity = Direction.magnitude;
                if (proximity > 0.5f && !hasCollided)
                {
                    Brb.MovePosition(transform.position + (Direction.normalized * travelSpeed * Time.deltaTime));
                }
                else if(proximity < 0.5f || hasCollided)
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasCollided = true;
    }
}
