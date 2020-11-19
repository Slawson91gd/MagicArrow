using System;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBow : MonoBehaviour
{
    public float PatrolSpeed;
    private float WaitTime;
    public float SearchTime;
    public float StartWaitTime;
    public float distance;
    public float Timer;
    public Transform[] PatrolPoints;
    private Transform Target;
    public GameObject Player;
    public Transform CastPoint;

    Rigidbody2D rb2d;

    public bool isFacingLeft, isSearching, CanSeePlayer, isAgro;

    private int curPoint;

    public Dictionary<EnemyStates, Action> dic;

    public enum EnemyStates
    {
        Patrol,
        Shoot,
    }

    public EnemyStates EnemyState;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Player = GameObject.FindGameObjectWithTag("Player");
        WaitTime = StartWaitTime;
        WaitTime = StartWaitTime;
        curPoint = PatrolPoints.Length;
        isFacingLeft = true;

        dic = new Dictionary<EnemyStates, Action>
        {
            {EnemyStates.Patrol, HandlePatrol },
            {EnemyStates.Shoot, HandleShoot },
        };

        EnemyState = EnemyStates.Patrol;
    }

    // Update is called once per frame
    void Update()
    {
        DoDamage();
        LFP();
        dic[EnemyState].Invoke();

    }

    private void HandlePatrol()
    {

        isSearching = false;


        if (curPoint >= PatrolPoints.Length)
        {
            curPoint = 0;
        }

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

    private void HandleShoot()
    {
        
    }

    public void DoDamage()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Player.GetComponent<PlayerController>().PlayerData.TakeDamage(25);
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
                isAgro = true;
            }
            else
            {
                Debug.DrawLine(CastPoint.position, endPos, Color.blue);
                CanSeePlayer = false;
                isSearching = true;
            }

            if (CanSeePlayer)
            {
                EnemyState = EnemyStates.Shoot;
            }
            else
            {
                if (isAgro)
                {
                    Timer += Time.deltaTime;

                    if (Timer >= SearchTime)
                    {
                        WaitTime = StartWaitTime;
                        EnemyState = EnemyStates.Patrol;
                        Timer = 0;
                        isAgro = false;
                    }
                }
            }
        }
    }
}
