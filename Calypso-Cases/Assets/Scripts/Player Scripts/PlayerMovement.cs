using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    void FixedUpdate()
    {
        //Updates the characters movement based on what key is pressed and the movement speed provided
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movement = ctx.ReadValue<Vector2>();
        transform.rotation = Quaternion.LookRotation(movement);
    }
}
