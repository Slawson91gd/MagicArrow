using UnityEngine;


public class TetherStone : MonoBehaviour
{
    [SerializeField] private PuzzleElement pe;

    [SerializeField] private GameObject receiverStone;
    [SerializeField] private bool receiverActive;

    [SerializeField] private LineRenderer beam;
    [SerializeField] private float beamDistance;
    [SerializeField] private LayerMask layer;

    private void Update()
    {
        EmitBeam(pe.isActive);
    }

    public void EmitBeam(bool status)
    {
        if (status)
        {
            SetBeamStatus(true);
            beam.SetPosition(0, transform.position);
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.right), beamDistance, layer);
            if (rayHit.collider != null)
            {
                beam.SetPosition(1, rayHit.point);
                SetReceiverStatus(true);
            }
            else
            {
                beam.SetPosition(1, transform.position + transform.TransformDirection(Vector3.right) * beamDistance);
                SetReceiverStatus(false);
            }
        }
        else
        {
            SetBeamStatus(false);
        }
    }

    private void SetReceiverStatus(bool status)
    {
        if (receiverActive != status)
            receiverActive = status;
    }

    private void SetBeamStatus(bool status)
    {
        if (beam.enabled != status)
            beam.enabled = status;
    }
}
