using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    bool actionInput;

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    void FixedUpdate()
    {
        //Updates the characters movement based on what key is pressed and the movement speed provided
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        //TODO: Get a reference to an object to interact with
        //      Allow an interaction between the player and the object (maybe like a text box that says you interacted with it, maybe you pick it up)
        if (actionInput)
        {
            //Interacts with item
        }
    }

    void PlayerInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        actionInput = Input.GetKeyDown(KeyCode.E);
    }
}
