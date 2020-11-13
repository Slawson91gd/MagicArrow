using UnityEngine;

public class BoomerangObj : MonoBehaviour
{
    [SerializeField] private Boomerang cb;
    public Boomerang CurrentBoomerang { get { return cb; } private set { cb = value; } }
    private NormalBoomerang nb;

    // Start is called before the first frame update
    void Start()
    {
        nb = new NormalBoomerang(this);
        cb = nb;

        
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        cb.HandleBoomerang();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (cb.Mode == Boomerang.BoomerangModes.TRAVEL)
            cb.Collided(true);
    }
}