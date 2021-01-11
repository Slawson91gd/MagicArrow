using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TetherStone : MonoBehaviour
{
    [SerializeField] private PuzzleElement pe;

    [SerializeField] private GameObject receiverStone;
    public GameObject ReceiverStone { get { return receiverStone; } }

    [SerializeField] private bool receiverActive;

    [SerializeField] private LineRenderer beam;
    [SerializeField] private float beamDistance;
    public float BeamDistance { get { return beamDistance; } }

    [SerializeField] private LayerMask layer;

    [SerializeField] private ReflectionStone reflectStone;

    private void Start()
    {
        pe.PuzzleObject = gameObject;
        pe.light.SetActive(false);
    }

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

                // if the ray hit the receiver stone
                if (rayHit.collider.gameObject == receiverStone)
                {
                    SetReceiverStatus(true);
                }

                else if(rayHit.collider.gameObject == reflectStone.gameObject)
                {
                    CheckReflectStones(true, this);
                }

                else
                {
                    SetReceiverStatus(false);
                    CheckReflectStones(false, null);
                }
            }
            else
            {
                beam.SetPosition(1, transform.position + transform.TransformDirection(Vector3.right) * beamDistance);
                SetReceiverStatus(false);
                CheckReflectStones(false, null);
            }
        }
        else
        {
            SetBeamStatus(false);
        }
    }

    private void CheckReflectStones(bool status, TetherStone obj)
    {
        if(reflectStone != null)
        {
            reflectStone.SetActiveStatus(status);
            reflectStone.SetEmitter(obj);
        }
    }

    public void SetReceiverStatus(bool status)
    {
        if (receiverActive != status)
            receiverActive = status;

        if (receiverActive)
        {
            pe.light.SetActive(true);
            pe.SetTrigger(true);
            pe.HandleTriggered();
        }
        else
        {
            pe.light.SetActive(false);
            pe.SetTrigger(false);
            pe.HandleTriggered();
        }
    }

    private void SetBeamStatus(bool status)
    {
        if (beam.enabled != status)
            beam.enabled = status;
    }
}
