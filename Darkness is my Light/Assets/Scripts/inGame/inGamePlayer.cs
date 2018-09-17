using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inGamePlayer : Character {

    private static inGamePlayer instace;

    public static inGamePlayer Instance
    {
        get
        {
            if (instace == null)
            {
                instace = FindObjectOfType<inGamePlayer>();
            }
            return instace;
        }
    }

    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;

    public Rigidbody2D MyRigidbody { get; set; }

    public bool OnGround { get; set; }

    private Vector2 startPos;

    // Use this for initialization
    public override void Start () {

        base.Start();
        startPos = transform.position;

        MyRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!TakingDamage && !IsDead)
        {
            HandleInput();
        }
	}

    void FixedUpdate()
    {
        if (!TakingDamage && !IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");

            OnGround = IsGrounded();

            HandleMovement(horizontal);

            Flip(horizontal);
        }
    }

    void HandleMovement(float horizontal)
    {
        if(MyRigidbody.velocity.y < 0)
        {
            MyAnimator.SetBool("land", true);
        }
        if (!Attack)
        {
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyAnimator.velocity.y);
        }

        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown((0)))
        {
            MyAnimator.SetTrigger("shoot");
        }
    }

    public override void ShootBullet(int value)
    {
        //if(!OnGround && value == 1 || OnGround && value == 0)
        //{
        base.ShootBullet(value);
        //}
    }

    private void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }

    private bool IsGrounded()
    {
        if(MyRigidbody.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position,groundRadius,whatIsGround);

                for(int i = 0; i < colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
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
        }

        yield return null;
    }

    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }
}
