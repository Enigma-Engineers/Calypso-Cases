using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    private Vector3 intialPosition;

    private bool canMove = true;

    private bool hasMoved = false;

    public bool CanMove { get { return canMove; } set { canMove = value; } }
    public bool HasMoved { get { return hasMoved; } }

    [SerializeField] private Animator animator;

    public void Awake()
    {
        intialPosition = transform.position;
    }

    void FixedUpdate()
    {

        //Updates the characters movement based on what key is pressed and the movement speed provided
        if (DialogueManager.GetInstance().dialogueIsPlaying || !canMove)
        {
            return;
        }
        else{
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        

        // If the player's position has changed in any way since the initial frame, then 
        // the player has moved
        if(transform.position != intialPosition)
        {
            hasMoved = true;
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        
        if (!DialogueManager.GetInstance().dialogueIsPlaying && canMove)
        {
            movement = ctx.ReadValue<Vector2>();
            animator.Play("Movement");
            animator.SetFloat("velx", ctx.ReadValue<Vector2>().x);
            animator.SetFloat("vely", ctx.ReadValue<Vector2>().y);

            if (movement.x == 1 && movement.y == 0)
            {
                animator.SetFloat("IdleValue", 1);
            }
            else if (movement.x == -1 && movement.y == 0)
            {
                animator.SetFloat("IdleValue", 3);
            }
            else if (movement.y == 1)
            {
                animator.SetFloat("IdleValue", 2);
            }
            else if (movement.y == -1)
            {
                animator.SetFloat("IdleValue", 0);
            }
            else if(movement.y == 0 && movement.x == 0)
            {
                animator.Play("idle");
            }

        }
        else
        { 
            movement = Vector2.zero;
            animator.SetFloat("velx", 0);
            animator.SetFloat("vely", 0);
        }

    }
}
