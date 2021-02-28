using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityGlyph : MonoBehaviour
{
    [SerializeField] private PuzzleElement pe;

    // Start is called before the first frame update
    void Start()
    {
        pe.PuzzleObject = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pe.SetTrigger(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pe.SetTrigger(false);
        }
    }
}
