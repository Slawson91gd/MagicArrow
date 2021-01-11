using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionStone : MonoBehaviour
{
    [SerializeField] private bool active;
    [SerializeField] private SpriteRenderer stoneSprite;
    [SerializeField] private TetherStone emitter;

    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color currentColor;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float currentAngle;
    [SerializeField] private float currentRadian;
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;
    [SerializeField] private float distance;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private LineRenderer beam;

    // Start is called before the first frame update
    private void Start()
    {
        active = false;
        stoneSprite = GetComponent<SpriteRenderer>();
        emitter = null;
        minAngle = 0;
        maxAngle = 180.0f;
    }

    // Update is called once per frame
    void Update()
    {
        SetColor();
        HandleReflection();
    }

    private void HandleReflection()
    {
        if (active)
        {
            float newDist;
            float xDir;
            if(emitter.transform.position.x > transform.position.x)
            {
                newDist = (emitter.transform.position.x - (transform.position.x + minDistance));
                xDir = 1;
            }
            else
            {
                newDist = (transform.position.x - (emitter.transform.position.x + minDistance));
                xDir = -1;
            }
            distance = Mathf.Clamp(newDist, 0, maxDistance);
            currentAngle = Mathf.Clamp(((distance / maxDistance) * maxAngle), minAngle, maxAngle);
            currentRadian = currentAngle * Mathf.Deg2Rad;
            Vector3 newDir = new Vector3(Mathf.Cos(currentRadian) * xDir, Mathf.Sin(currentRadian), 0);
            Debug.DrawRay(transform.position, newDir.normalized * 6, Color.green);

            RaycastHit2D reflectRay = Physics2D.Raycast(transform.position, newDir.normalized, emitter.BeamDistance, layer);
            if (beam.GetPosition(0) != transform.position)
            {
                beam.SetPosition(0, transform.position);
            }
            if (reflectRay.collider != null)
            {
                SetBeamStatus(true);
                beam.SetPosition(1, reflectRay.point);

                if(reflectRay.collider.gameObject == emitter.ReceiverStone)
                {
                    emitter.SetReceiverStatus(true);
                }
                else
                {
                    emitter.SetReceiverStatus(false);
                }
            }
            else
            {
                beam.SetPosition(1, transform.position + (newDir.normalized * emitter.BeamDistance));
            }
        }
        else
        {
            SetBeamStatus(false);
            currentAngle = 0;
        }
    }

    public void SetActiveStatus(bool status)
    {
        if (active != status)
        {
            active = status;
        }
    }

    public void SetEmitter(TetherStone obj)
    {
        if(emitter != obj)
        {
            emitter = obj;
        }
    }

    private void SetColor()
    {
        if (active)
        {
            currentColor = activeColor;
        }
        else
        {
            currentColor = inactiveColor;
        }

        if(stoneSprite.color != currentColor)
        {
            stoneSprite.color = currentColor;
        }
    }

    private void SetBeamStatus(bool value)
    {
        if(beam.enabled != value)
        {
            beam.enabled = value;
        }
    }
}
