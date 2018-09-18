using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    protected bool facingRight;

    public float movementSpeed;

    [SerializeField]
    protected int health;

    [SerializeField]
    private List<string> damageSources;

    public bool TakingDamage { get; set; }

    public abstract bool IsDead { get; }

    public abstract void Death();

    [SerializeField]
    private BoxCollider2D meleeCollider;

    public BoxCollider2D MeleeCollider
    {
        get
        {
            return meleeCollider;
        }
    }

    public bool Attack { get; set; }

    public Animator MyAnimator { get; private set; }

    [SerializeField]
    protected GameObject bulletPrefab;

    [SerializeField]
    protected Transform bulletPos;

    // Use this for initialization
    public virtual void Start () {

        facingRight = true;

        MyAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public abstract IEnumerator TakeDamage();

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    public void MeleeAttack()
    {
        MeleeCollider.enabled = true;
    }

    public virtual void ShootBullet(int value)
    {
        if (facingRight)
        {
            GameObject tmp = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
            tmp.GetComponent<Bullet>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = Instantiate(bulletPrefab, bulletPos.position, Quaternion.Euler(new Vector3(0,0,180)));
            tmp.GetComponent<Bullet>().Initialize(Vector2.left);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(damageSources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        }
    }
}
