using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [Header("AI Config")]
    public float followDistance;
    public float attackDistance;
    public float returnDeltaDistance;
    public float stopRange;
    public float corpsTime;
    public float health;
    public bool finalBoss;

    [Header("Attack Config")]
    public float attackRate;
    public float damage;

    [Header("Patrol Config")]
    public GameObject pointPrefab;
    public GameObject[] patrolPoints;



    Player player;

    
    ZombieMovement movement;
    Animator anim;
    Rigidbody2D rb;
    AliveOrDeath aliveVar;
    bool alive;


    GameObject nextPoint;
    GameObject previousPoint;
    bool directPatroling;
    int currentPatrolPoint;
    bool canAttack;
    public RescueZone[] rZone;


    enum ZombieStates
    {
        STAND,
        MOVE,
        ATTACK,
        PATROL,
        RETURN,
        DEATH
    }

    ZombieStates activeState;

    enum BaseBehavior
    { 
        GUARD,
        PATROLING
    }
    
    BaseBehavior activeBehavior;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        movement = GetComponent<ZombieMovement>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        aliveVar = GetComponent<AliveOrDeath>();
        AliveOrNot(true);
        
        rZone = FindObjectsOfType<RescueZone>();
        
        if (finalBoss)
        {
            ActivateRescueZone(false);
        }
        
        


        previousPoint = Instantiate(pointPrefab, transform.position, Quaternion.identity);
        currentPatrolPoint = 0;

        if (patrolPoints.Length <= 1)
        {
            activeBehavior = BaseBehavior.GUARD;        
        }
        else if ( patrolPoints.Length >1)
        {
            activeBehavior = BaseBehavior.PATROLING;
        }
        canAttack = true;
        ChangeState(ZombieStates.RETURN);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateState();
        anim.SetFloat("Speed", rb.velocity.magnitude);
    }

    void UpdateState()
    {
        float distancePlayer = Vector2.Distance(transform.position, player.transform.position);
        switch (activeState)
        {
            case ZombieStates.STAND:
                
                if (distancePlayer <= followDistance)
                {
                    previousPoint = Instantiate(pointPrefab, transform.position, Quaternion.identity);
                    ChangeState(ZombieStates.MOVE);
                } 
                // check field of view 
                break;

            case ZombieStates.MOVE:
                
                if (distancePlayer <= attackDistance)
                {
                    ChangeState(ZombieStates.ATTACK);
                }
                Rotate(player.gameObject);

                if (distancePlayer > returnDeltaDistance + followDistance)
                {
                    ChangeState(ZombieStates.RETURN);
                }

                break;

            case ZombieStates.ATTACK:


                if (distancePlayer > attackDistance)
                {
                    ChangeState(ZombieStates.MOVE);
                    break;
                }

                if (canAttack)
                {
                    Rotate(player.gameObject);
                    anim.SetTrigger("Shoot");
                    canAttack = false;
                    StartCoroutine(ZombieAttack(attackRate));

                }
                break;

            case ZombieStates.RETURN:
                
                if (distancePlayer <= followDistance)
                {
                    previousPoint = Instantiate(pointPrefab, transform.position, Quaternion.identity);
                    ChangeState(ZombieStates.MOVE);
                    break;
                }

                float returnDistance = Vector2.Distance(transform.position, previousPoint.transform.position);
                if (returnDistance < stopRange)
                {
                    Destroy(previousPoint);
                    if (activeBehavior == BaseBehavior.GUARD)
                    {
                        ChangeState(ZombieStates.STAND);
                    }
                    else
                    {
                        ChangeState(ZombieStates.PATROL);
                    }
                }
                break;

            case ZombieStates.PATROL:
                
                if (distancePlayer <= followDistance)
                {
                    previousPoint = Instantiate(pointPrefab, transform.position, Quaternion.identity);
                    ChangeState(ZombieStates.MOVE);
                    break;
                }

                float patrolDistance = Vector2.Distance(transform.position, nextPoint.transform.position);
                if (patrolDistance < stopRange)
                {
                
                    SetNextPoint();
                    movement.SetTarget(nextPoint);
                    Rotate(nextPoint);
                }
                break;

            default:
                break;
        }
    }

    public void DoDamageToPlayer()
    {
        player.DoDamage(damage);
    }

    void ChangeState(ZombieStates newState)
    {
        activeState = newState;
        switch (activeState)
        {
            case ZombieStates.STAND:
                
                // выключаем выполнение скрипта мувмента
                movement.enabled = false;
                movement.StopMovement();

                break;
            case ZombieStates.MOVE:
                
                movement.enabled = true;
                movement.StartFollow();
                movement.FollowSpeed();

                break;
            case ZombieStates.ATTACK:
                
                movement.enabled = false;
                movement.StopMovement();

                break;
            case ZombieStates.RETURN:
                
                movement.enabled = true;
                movement.SetTarget(previousPoint);
                movement.RuturnSpeed();
                Rotate(previousPoint);



                break;
            case ZombieStates.PATROL:
                
                movement.enabled = true;
                nextPoint = patrolPoints[currentPatrolPoint];
                movement.SetTarget(nextPoint);
                movement.PatrolSpeed();
                Rotate(nextPoint);

                break;
            case ZombieStates.DEATH:

                movement.enabled = false;
                movement.StopMovement();
                anim.SetTrigger("Death");
                if (finalBoss)
                {
                    ActivateRescueZone(true);
                } 
                Destroy(gameObject, corpsTime);
                break;

            default:
                break;
        }
    }

    void Rotate(GameObject target)
    {
        Vector2 direction = target.transform.position - transform.position;
        transform.up = -direction;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, followDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, returnDeltaDistance + followDistance);
    }

    void SetNextPoint()
    {
        if (directPatroling)
        {
            if (currentPatrolPoint != patrolPoints.Length - 1)
            {
                currentPatrolPoint += 1;
            }
            else
            {
                directPatroling = false;
                currentPatrolPoint -= 1;
            }
        }
        else
        {
            if (currentPatrolPoint != 0)
            {
                currentPatrolPoint -= 1;
            }
            else
            {
                directPatroling = true;
                currentPatrolPoint = 1;
            }
        }
        nextPoint = patrolPoints[currentPatrolPoint];
    }

    IEnumerator ZombieAttack(float attackRate)
    {
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            DoDamage(damageDealer.damage);
        }



    }


    public void DoDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && alive)
        {
            ChangeState(ZombieStates.DEATH);
            AliveOrNot(false);


        }
    }

    private void AliveOrNot(bool aliveSet)
    {
        alive = aliveSet;
        aliveVar.SetAlive(aliveSet);
    }

    void ActivateRescueZone(bool onOff)
    {
        for (int i = 0; i < rZone.Length; i++)
        {
            rZone[i].Activate(onOff);
        }
    }

}
