using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomShroom : MonoBehaviour
{
    [SerializeField] private float explosionRadius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<BoomerangObj>() != null)
        {
            BoomerangObj boomerang = collision.gameObject.GetComponent<BoomerangObj>();

            if(boomerang.CurrentBoomerang.Type == Boomerang.BoomerangTypes.FIRE)
            {
                // Trigger the boom shroom
                Debug.Log("The BOOM SHROOM has been triggered!");
            }
            else
            {
                Debug.Log("That's the wrong boomerang type. Try using the FIRE boomerang!");
            }
        }
    }
}
