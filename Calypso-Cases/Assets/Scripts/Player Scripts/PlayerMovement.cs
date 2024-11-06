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

    private bool canMove = true;

    public bool CanMove { get { return canMove; } set { canMove = value; } }

    [SerializeField] private Animator animator;

    void FixedUpdate()
    {
        //Updates the characters movement based on what key is pressed and the movement speed provided
        if (DialogueManager.GetInstance().dialogueIsPlaying || !canMove)
        {
            return;
        }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movement = ctx.ReadValue<Vector2>();

        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            animator.SetFloat("velx", ctx.ReadValue<Vector2>().x);
            animator.SetFloat("vely", ctx.ReadValue<Vector2>().y);
        }
        else
        {
            animator.SetFloat("velx", 0);
            animator.SetFloat("vely", 0);
        }

    }
}
