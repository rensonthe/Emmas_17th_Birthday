using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; set; }

    private bool canInteract = true;

    // Normal Movements Variables
    public float moveSpeed;

    private Animator myAnimator;
    private Vector2 playerMovement;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        myAnimator = GetComponent<Animator>();
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
}
