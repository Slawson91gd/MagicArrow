using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherStone : MonoBehaviour
{
    [SerializeField] private PuzzleElement pe;

    // Start is called before the first frame update
    void Start()
    {
        pe = new PuzzleElement(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
