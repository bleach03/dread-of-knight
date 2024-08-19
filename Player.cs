using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 2.5f; // Movement speed of the player
    private Rigidbody2D rb; // Rigidbody component of the player
    public Animator animator;
    public float interactDistance = 100f; // Distance within which the player can interact with objects

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody component
    }

    void Update()
    {
        // Read the horizontal and vertical input axis
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (Mathf.Abs(moveX) > Mathf.Abs(moveY))
        {
            // Moving horizontally
            moveY = 0f;
        }
        else
        {
            // Moving vertically
            moveX = 0f;
        }

        // Set the velocity of the player based on the input axis and move speed
        Vector2 movement = new Vector2(moveX, moveY);
        rb.velocity = movement * moveSpeed;

        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }


    void Interact()
    {
        Debug.Log("Interact is being called");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactDistance);

        foreach (Collider2D collider in colliders)
        {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance <= interactDistance)
                {
                    interactable.Interact();
                }
            }
        }
    }
}