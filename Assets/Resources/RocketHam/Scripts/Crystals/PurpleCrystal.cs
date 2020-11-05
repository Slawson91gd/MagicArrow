using UnityEngine;

public class PurpleCrystal : MonoBehaviour
{
    [SerializeField] private PuzzleElement pe;


    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boomerang"))
        {
            pe.isTriggered = true;
            pe.HandleTriggered();
        }
    }
}
