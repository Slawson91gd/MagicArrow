using UnityEngine;

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
    }

    private void FixedUpdate()
    {
        PlayerData.OnGround = PlayerData.IsGrounded();
    }

}
