using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpVR : MonoBehaviour
{

    public float jumpForce = 20.0f;

    public float distanceRay = 0.05f;

    private Rigidbody rigidBody;

    // Access to the ground component of the game 
    public LayerMask groundLayerMask;

    public void Jump()
    {
        if (IsOnTheFloor())
        {
            // Vector2.up return an unitary vector, the second statment is the mode of the force  
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Check if the player is close to the ground
    bool IsOnTheFloor()
    {
        // The ray cast is to "send" one vector to the ground to know the distance of the object to another one to trigger something
        if (Physics2D.Raycast(transform.position, Vector3.down, distanceRay, groundLayerMask.value))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
}
