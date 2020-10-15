using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float PatrolSpeed;
    public float ChaseSpeed;
    public float WaitTime;
    public float StartWaitTime;
    public float AgroRange;
    public Transform[] PatrolPoints;
    private Transform Target;
    public Transform CastPoint;

    Rigidbody2D rb2d;


    public bool isFacingLeft;
    public bool isAgro = false;
    public bool isSearching;

    public int curPoint;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        WaitTime = StartWaitTime;
        curPoint = PatrolPoints.Length;
        isFacingLeft = true;
    }

    void Update()
    {
        if(CanSeePlayer(AgroRange))
        {
            isAgro = true;
            //ChasePlayer();
        }
        else
        {
            if (isAgro)
            {
                if (!isSearching)
                {
                    if(!isSearching)
                    {
                        isSearching = true;
                        Invoke("Patrol", 5);

                    }
                }
            }
        }

        if(isAgro)
            {
                ChasePlayer();
            }
        else
        {
            if (curPoint < PatrolPoints.Length)
            {
                Patrol();
            }
            else
            {
               curPoint = 0;
            }
        }

    }

   void Patrol()
    {
        isAgro = false;
        isSearching = false;

        if (Vector2.Distance(transform.position, PatrolPoints[curPoint].position) < 0.2f)
        {


            if (WaitTime <= 0)
            {
                WaitTime = StartWaitTime;
                curPoint++;
            }
            else
            {
                rb2d.velocity = new Vector2(0, 0);
                WaitTime -= Time.deltaTime;
            }

        }
        else
        {
            if (transform.position.x < PatrolPoints[curPoint].position.x)
            {
                rb2d.velocity = new Vector2(PatrolSpeed, 0);
                transform.localScale = new Vector2(-1, transform.localScale.y);
                isFacingLeft = false;
            }
            else
            {
                rb2d.velocity = new Vector2(-PatrolSpeed, 0);
                transform.localScale = new Vector2(1, transform.localScale.y);
                isFacingLeft = true;
            }

        }


    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;

        if(isFacingLeft)
        {
            castDist = -distance;
        }

        Vector2 endPos = CastPoint.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(CastPoint.position, endPos, 1 <<LayerMask.NameToLayer("Action" ));

        if(hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
                
            }
            else
            {
                val = false;
            }

            Debug.DrawLine(CastPoint.position, hit.point, Color.red);
            isAgro = true;
            
        }
        else
        {
            Debug.DrawLine(CastPoint.position, endPos, Color.blue);

        }

        return val;
    }

    void ChasePlayer()
    {
        if (transform.position.x < Target.position.x)
        {
            rb2d.velocity = new Vector2(ChaseSpeed, 0);
            transform.localScale = new Vector2(-1, transform.localScale.y);
            isFacingLeft = false;
        }
        else
        {
            rb2d.velocity = new Vector2(-ChaseSpeed, 0);
            transform.localScale = new Vector2(1, transform.localScale.y);
           isFacingLeft = true;
        }
    }

    void AttackPlayer()
    {

    }

}