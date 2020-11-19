using System;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSword : MonoBehaviour
{
    public float PatrolSpeed;
    public float ChaseSpeed;
    public float BounceSpeed;
    public float ChargeSpeed;
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
        Chase,
        Attack,
        Charge,
    }

    public EnemyStates EnemyState;

    // Start is called before the first frame update
    void Start()
    {
       
        rb2d = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Player = GameObject.FindGameObjectWithTag("Player");
        WaitTime = StartWaitTime;
        curPoint = PatrolPoints.Length;
        isFacingLeft = true;

        dic = new Dictionary<EnemyStates, Action>
        {
            {EnemyStates.Patrol, HandlePatrol},
            {EnemyStates.Chase, HandleChase},
            {EnemyStates.Attack, HandleAttack},
            {EnemyStates.Charge, AllJackedUpOnMountainDew},
        };

        EnemyState = EnemyStates.Patrol;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (CanSeePlayer)
        {
            EnemyState = EnemyStates.Chase;

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

    private void HandleChase()
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

        if (Vector2.Distance(transform.position, Target.position) < 3.0f)
        {
            EnemyState = EnemyStates.Charge;
        }
    }

    private void HandleAttack()
    {
        DoDamage();

        if(transform.localScale.x == 1)
        {
            rb2d.velocity = new Vector2(BounceSpeed, 0);
        }
        else if(transform.localScale.x == -1)
        {
            rb2d.velocity = new Vector2(-BounceSpeed, 0);
        }
    }

    private void AllJackedUpOnMountainDew()
    {
        
        if (transform.localScale.x == -1)
        {
            rb2d.velocity = new Vector2(ChargeSpeed, 0);
        }
        else if (transform.localScale.x == 1)
        {
            rb2d.velocity = new Vector2(-ChargeSpeed, 0);
        }

        if (Vector2.Distance(transform.position, Target.position) > 2.0f)
        {
            EnemyState = EnemyStates.Chase;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EnemyState = EnemyStates.Attack;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        EnemyState = EnemyStates.Chase;
    }

    public void DoDamage()
    {
         Player.GetComponent<PlayerController>().PlayerData.TakeDamage(25);
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
        }
        
    }
}
