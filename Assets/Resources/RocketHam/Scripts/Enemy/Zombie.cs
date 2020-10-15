using System;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float WalkSpeed;
    public float SearchTime;
    private float Timer;
    private Transform Target;
    public Transform CastPoint;
    public float distance;

    Rigidbody2D rb2d;

    public bool CanSeePlayer;
    public bool isFacingLeft;
    public bool isSearching;


    public Dictionary<EnemyStates, Action> dic;

    public enum EnemyStates
    {
        Idle,
        Walk
    }

    public EnemyStates EnemyState;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        isFacingLeft = true;

        dic = new Dictionary<EnemyStates, Action>
        {
            {EnemyStates.Idle, HandleIdle },
            {EnemyStates.Walk, Movement }
        };

        EnemyState = EnemyStates.Idle;

    }

    // Update is called once per frame
    void Update()
    {
        LFP();
        dic[EnemyState].Invoke();
    }

    private void HandleIdle()
    {
        Timer = 0;
        isSearching = false;
    }

    private void Movement()
    {
        if (transform.position.x < Target.position.x)
        {
            rb2d.velocity = new Vector2(WalkSpeed, 0);
            transform.localScale = new Vector2(-1, transform.localScale.y);
            isFacingLeft = false;
        }
        else
        {
            rb2d.velocity = new Vector2(-WalkSpeed, 0);
            transform.localScale = new Vector2(1, transform.localScale.y);
            isFacingLeft = true;
        }
    }

    private void LFP()
    {
        {
            float castDist = distance;

            if (isFacingLeft)
            {
                castDist = -distance;
            }

            Vector2 endPos = CastPoint.position + Vector3.right * castDist;
            RaycastHit2D hit = Physics2D.Linecast(CastPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));

            if (hit.collider != null)
            {
              
                Debug.DrawLine(CastPoint.position, hit.point, Color.red);
                CanSeePlayer = true;
            }
            else
            {
                Debug.DrawLine(CastPoint.position, endPos, Color.blue);
                CanSeePlayer = false;
                isSearching = true;
            }

            if (CanSeePlayer)
            {
                EnemyState = EnemyStates.Walk;
            }
            else if (!CanSeePlayer)
            {
                if(isSearching)
                {
                    Timer += Time.deltaTime;
                    if(Timer >= SearchTime)
                    {
                        EnemyState = EnemyStates.Idle;
                    }
                }
                else
                {
                    EnemyState = EnemyStates.Idle;

                }
            }
        }
    }
}
