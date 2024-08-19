using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class LightFollow : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public float smoothTime = 0.3f; // Time taken for camera to move to player's position
    private UnityEngine.Vector3 velocity = UnityEngine.Vector3.zero; // Current velocity of the camera

    void FixedUpdate()
    {
        // Set the target position for the camera
        UnityEngine.Vector3 targetPosition = new UnityEngine.Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);

        // Smoothly move the camera towards the target position using a dampening effect
        transform.position = UnityEngine.Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}