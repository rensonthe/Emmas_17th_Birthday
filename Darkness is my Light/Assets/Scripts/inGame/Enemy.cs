using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMYTYPE { RANGED, MELEE }

public class Enemy : Character {

    [SerializeField]
    private ENEMYTYPE myType;

    private IEnemyState currentState;

    public GameObject Target { get; set; }

    public GameObject Beans;

    [SerializeField]
    private float meleeRange;
    [SerializeField]
    private float shootRange;

    public bool InMeleeRange
    {
        get
        {
            if(Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }

            return false;
        }
    }

    public bool InShootRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= shootRange;
            }

            return false;
        }
    }

    public float MeleeTimer { get; set; }

    public float RangedTimer { get; set; }

    [SerializeField]
    private Transform leftEdge;
    [SerializeField]
    private Transform rightEdge;

    // Use this for initialization
    public override void Start () {

        base.Start();
        inGamePlayer.Instance.Dead += new DeadEventHandler(RemoveTarget);

        ChangeState(new IdleState());
        SetState();
    }

    void FixedUpdate()
    {
        MeleeTimer += Time.deltaTime;
        RangedTimer += Time.deltaTime;
    }

    public void SetState()
    {
        switch (myType)
        {
            case ENEMYTYPE.RANGED:
                ChangeState(new RangedState());
                break;
            case ENEMYTYPE.MELEE:
                ChangeState(new MeleeState());
                break;
        }
    }

    // Update is called once per frame
    void Update () {
        if (!IsDead)
        {
            if (!TakingDamage)
            {
                currentState.Execute();
            }

            LookAtTarget();
        }
	}

    public void RemoveTarget()
    {
        Target = null;
        ChangeState(new PatrolState());
    }

    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;

            if (xDir < 0 && facingRight || xDir > 0 && !facingRight)
            {
                ChangeDirection();
            }
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void Move()
    {
        if (!Attack)
        {
            if ((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))
            {
                MyAnimator.SetFloat("speed", 1);

                transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
            }
            else if(currentState is PatrolState)
            {
                ChangeDirection();
            }
        }
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }

    public void SpawnBeans()
    {
        Instantiate(Beans, transform.position, Quaternion.identity);
    }

    public override IEnumerator TakeDamage()
    {
        health -= 10;

        if (!IsDead)
        {
            MyAnimator.SetTrigger("damage");
        }
        else
        {            
            MyAnimator.SetTrigger("die");
            yield return null;
        }
    }

    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    public override void Death()
    {
        Destroy(gameObject);
    }
}
