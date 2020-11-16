using UnityEngine;

public class ShockCrystal : MonoBehaviour
{
    [SerializeField] private PuzzleElement pe;

    // Start is called before the first frame update
    void Start()
    {
        pe = new PuzzleElement(gameObject);
        pe.isActive = true;
        pe.isTriggered = false;
        pe.light = transform.GetChild(0).gameObject;
        pe.light.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<BoomerangObj>() != null)
        {
            BoomerangObj rang = collision.gameObject.GetComponent<BoomerangObj>();
            if(rang.CurrentBoomerang.Type == Boomerang.BoomerangTypes.SHOCK)
            {
                pe.isTriggered = true;
                pe.HandleTriggered();
                pe.light.SetActive(true);
            }
            else
            {
                Debug.Log("This is the wrong boomerang type!");
            }
        }
    }
}
