using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; set; }

    private bool canInteract = true;

    // Normal Movements Variables
    public float moveSpeed;
    public bool clothed;

    private Animator myAnimator;
    private Vector2 playerMovement;
    private Transform self;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        self = GetComponent<Transform>();
        myAnimator = GetComponent<Animator>();
        if (clothed)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
    }

    void Update()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            myAnimator.SetBool("walking", true);
            PlayerDirection(playerMovement);
        }
        else
        {
            myAnimator.SetBool("walking", false);
        }
    }

    void FixedUpdate()
    {
        // Move senteces
        GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, Input.GetAxisRaw("Vertical") * moveSpeed);
        playerMovement = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, Input.GetAxisRaw("Vertical") * moveSpeed);
    }

    void PlayerDirection(Vector2 input)
    {
        myAnimator.SetFloat("inputX", input.x);
        myAnimator.SetFloat("inputY", input.y);
    }

    public void ChangeClothes()
    {
        myAnimator.SetLayerWeight(1, 1);
    }

    public void ChangeScale()
    {
        self.transform.localScale += new Vector3(0.5f, 0.5f, 0);
    }

    public void SetActive()
    {
        moveSpeed = 3;
    }

    public void SetInactive()
    {
        moveSpeed = 0;
    }
}
