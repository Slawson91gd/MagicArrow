using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsidianWall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<BoomerangObj>() != null)
        {
            BoomerangObj rang = collision.gameObject.GetComponent<BoomerangObj>();
            if(rang.CurrentBoomerang.Type == Boomerang.BoomerangTypes.OBSIDIAN)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("This is the wrong boomerang type. Try the OBSIDIAN boomerang.");
            }
        }
    }
}
