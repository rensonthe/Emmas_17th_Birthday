using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DeadEventHandler();

public class inGamePlayer : Character {

    private static inGamePlayer instance;

    public static inGamePlayer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<inGamePlayer>();
            }
            return instance;
        }
    }

    public event DeadEventHandler Dead;

    public bool useBars;

    [SerializeField]
    private Stat healthStat;

    [SerializeField]
    private Stat survivalStat;
    [SerializeField]
    private float hungerRate;

    private float hungerTimer;
    private bool canHunger;

    public int currentStacks;

    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;

    public Rigidbody2D MyRigidbody { get; set; }

    public bool OnGround { get; set; }

    private Vector2 startPos;
    private bool canFlip = true;

    // Use this for initialization
    public override void Start () {

        base.Start();
        startPos = transform.position;

        MyRigidbody = GetComponent<Rigidbody2D>();

        if (useBars == true)
        {
            healthStat.Initialize();
            survivalStat.Initialize();
        }
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

            if(useBars == true)
            {
                Hunger();
            }
        }
    }

    public void OnDead()
    {
        if(Dead != null)
        {
            Dead();
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
        if(horizontal > 0 && !facingRight && canFlip || horizontal < 0 && facingRight && canFlip)
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

    private void Hunger()
    {
        hungerTimer += Time.deltaTime;

        if(survivalStat.CurrentVal <= 1)
        {
            healthStat.CurrentVal--;
        }
        if (hungerTimer >= hungerRate)
        {
            survivalStat.CurrentVal--;
            hungerTimer = 0;
        }
    }

    public override IEnumerator TakeDamage()
    {
        healthStat.CurrentVal -= 10;

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
            if(healthStat.CurrentVal <= 0)
            {
                OnDead();
            }

            return healthStat.CurrentVal <= 0;
        }
    }

    public override void Death()
    {
        UIManager.Instance.ResetScene();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Food")
        {
            Destroy(other.gameObject);
            survivalStat.CurrentVal += 25;
            healthStat.CurrentVal += 5;
        }
        if (other.gameObject.tag == "Shovel")
        {
            UIManager.Instance.StartCoroutine("Fading");
            Destroy(other.gameObject);
        }
    }
}
