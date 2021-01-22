using UnityEngine;

public class BoomerangObj : MonoBehaviour
{
    public Boomerang CurrentBoomerang { get; private set; }
    public NormalBoomerang NormalRang { get; private set; }
    public FireBoomerang FireRang { get; private set; }
    public IceBoomerang IceRang { get; private set; }
    public ShockBoomerang ShockRang { get; private set; }
    public WindBoomerang WindRang { get; private set; }
    public ObsidianBoomerang ObsidianRang { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        NormalRang = new NormalBoomerang(this);
        FireRang = new FireBoomerang(this);
        IceRang = new IceBoomerang(this);
        ShockRang = new ShockBoomerang(this);
        WindRang = new WindBoomerang(this);
        ObsidianRang = new ObsidianBoomerang(this);
        CurrentBoomerang = NormalRang;

        CurrentBoomerang.SetColor(CurrentBoomerang.BoomerangColor);
        Debug.Log(CurrentBoomerang.Type);
    }

    private void Update()
    {
        //CurrentBoomerang.SetColor(CurrentBoomerang.BoomerangColor);
    }

    void FixedUpdate()
    {
        CurrentBoomerang.HandleBoomerang();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CurrentBoomerang.Mode == Boomerang.BoomerangModes.TRAVEL)
            CurrentBoomerang.Collided(true);
    }

    public void SetBoomerang(Boomerang newType)
    {
        if(CurrentBoomerang != newType)
        {
            CurrentBoomerang = newType;
            CurrentBoomerang.SetColor(CurrentBoomerang.BoomerangColor);
            Debug.Log("Current Boomerang = " + CurrentBoomerang + " and Booomerang Color = " + CurrentBoomerang.BoomerangColor);
        }

    }
}