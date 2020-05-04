using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float followSpeed;
    public float patrolSpeed;
    public float returnSpeed;

    public GameObject target;
    float speedCoef = 1;
    float speedOfState;

    //other game objects
    Player player;
    
    



    //current game object components
    Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        //SetTarget(player.gameObject);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        { 
            Vector2 direction = target.transform.position - transform.position;
            rb.velocity = direction.normalized * speedOfState * speedCoef;
        }
        
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    public void IncreaseSpeed(float newSpeedCoef)
    {
        speedCoef *= newSpeedCoef;
    }

    public void ReturnToBaseSpeed()
    {
        speedCoef = 1;
    }

    public void StartFollow()
    {
        target = player.gameObject;
    }

    public void RuturnSpeed()
    {
        speedOfState = returnSpeed;
    }
    public void PatrolSpeed()
    {
        speedOfState = patrolSpeed;
    }

    public void FollowSpeed()
    {
        speedOfState = followSpeed;
    }


}
