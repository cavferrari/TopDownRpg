using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private Vector2 movementInput;

    void Start()
    {
        playerRigidbody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("Speed", movementInput.sqrMagnitude);
    }

    void FixedUpdate()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + (movementInput * speed * Time.fixedDeltaTime));
    }
}
