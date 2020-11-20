using UnityEngine;

public class TetherStone : MonoBehaviour
{
    [SerializeField] private PuzzleElement pe;

    private LineRenderer lineRenderer;

    [SerializeField] private GameObject receiverStone;
    public GameObject ReceiverStone { get { return receiverStone; } }

    [SerializeField] private float beamDistance;
    [SerializeField] private LayerMask layer;

    // Start is called before the first frame update
    void Start()
    {
        pe = new PuzzleElement(gameObject);
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        EmitBeam();
    }

    private void EmitBeam()
    {
        lineRenderer.SetPosition(0, transform.position);
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.right), beamDistance, layer);
        if(rayHit.collider != null)
        {
            lineRenderer.SetPosition(1, rayHit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, transform.position + transform.TransformDirection(Vector3.right) * beamDistance);
        }
        
    }
}
