using UnityEngine;

public class RegenLevelColliders : MonoBehaviour
{
    private CompositeCollider2D cc2d;

    // Start is called before the first frame update
    void Start()
    {
        if(cc2d == null)
        {
            cc2d = GetComponent<CompositeCollider2D>();
        }

        // Regenerate the composite collider at run time.
        cc2d.GenerateGeometry();
    }
}
