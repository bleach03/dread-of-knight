using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMovement : MonoBehaviour
{
    // Enemy movement script

    // Set the speed of the enemy
    public float speed = 5f;

    // Set the distance the enemy needs to be from the player to start dashing
    public float dashDistance = 3f;

    // Set the duration of the enemy's dash
    public float dashDuration = 1f;

    // Set the time the enemy needs to wait before dashing again
    public float pauseDuration = 2f;

    // Reference to the player object
    private GameObject player;

    public Transform playerTransform;

    public float rotationSpeed = 5.0f;

    public Sprite dashSprite;

    public Sprite idleSprite;

    public float interpolationFactor = 0.1f;

    public bool isDashing = false;

    public BoxCollider2D boxCollider;
    public TilemapCollider2D tilemapCollider;


    // Current state of the enemy's movement
    private enum MovementState
    {
        MovingTowardsPlayer,
        Pausing,
        Dashing,
    }

    private MovementState currentState;

    // The direction the enemy should dash towards
    private UnityEngine.Vector3 dashDirection;

    // The time when the enemy should start dashing or pause
    private float targetTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = MovementState.MovingTowardsPlayer;
        Physics2D.IgnoreCollision(boxCollider, tilemapCollider, true);
    }

    void Update()
    {
        switch (currentState)
        {
            case MovementState.MovingTowardsPlayer:
                MoveTowardsPlayer();
                break;
            case MovementState.Pausing:
                Pause();
                break;
            case MovementState.Dashing:
                Dash();
                EndDash();
                break;
        }

        {
            if (!isDashing && playerTransform != null)
            {
                UnityEngine.Vector3 direction = playerTransform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                angle = Mathf.Clamp(angle, -60f, 60f); // Make sure the z-axis rotation value is between -90 and 90 degrees
                UnityEngine.Quaternion targetRotation = UnityEngine.Quaternion.Euler(new UnityEngine.Vector3(0, 0, angle));
                transform.rotation = UnityEngine.Quaternion.Slerp(transform.rotation, targetRotation, interpolationFactor * rotationSpeed * Time.deltaTime);
            }
        }
    }
        

    void MoveTowardsPlayer()
    {
        // Move towards the player
        UnityEngine.Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;


        // Check if the enemy is close enough to the player to start dashing
        if (UnityEngine.Vector3.Distance(transform.position, player.transform.position) <= dashDistance)
        {
            // Set the direction for the enemy to dash towards
            dashDirection = (player.transform.position - transform.position).normalized;
            // Set the target time for the enemy to start dashing
            targetTime = Time.time + pauseDuration;
            // Change the state to pausing
            currentState = MovementState.Pausing;
        }
    }

    void Pause()
    {
        // Wait for the target time to start dashing
        if (Time.time >= targetTime)
        {
            // Set the target time for the enemy to stop dashing
            targetTime = Time.time + dashDuration;
            // Change the state to dashing
            currentState = MovementState.Dashing;
        }

        GetComponent<SpriteRenderer>().sprite = idleSprite;
    }

    void Dash()
    {
        isDashing = true;
        GetComponent<SpriteRenderer>().sprite = dashSprite;
        dashDirection.Normalize();
        // Dash towards the player
        transform.position += dashDirection * speed * Time.deltaTime;
        

        // Check if the enemy has dashed for the duration and should pause again
        if (Time.time >= targetTime)
        {
            // Change the state back to moving towards the player
            currentState = MovementState.MovingTowardsPlayer;
        }

        
    }

    void EndDash()
    {
        isDashing = false;
    }
}
