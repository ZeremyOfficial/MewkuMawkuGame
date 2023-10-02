using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRB;
    private Animator myAnim;
    private bool isAttacking = false;
    private float attackCooldown = 0.5f; // Adjust this value as needed
    private float attackCooldownTimer = 0f;

    [SerializeField]
    private float speed;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovementInput();
        HandleAttackInput();
    }

    void HandleMovementInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput).normalized;

        // Apply velocity directly to the rigidbody
        myRB.velocity = inputVector * speed;

        myAnim.SetFloat("moveX", inputVector.x);
        myAnim.SetFloat("moveY", inputVector.y);

        if (inputVector != Vector2.zero)
        {
            myAnim.SetFloat("lastMoveX", inputVector.x);
            myAnim.SetFloat("lastMoveY", inputVector.y);
        }
    }

    void HandleAttackInput()
    {
        // Check if the attack cooldown timer has elapsed
        if (attackCooldownTimer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Determine the direction the character is facing
                float lastMoveX = myAnim.GetFloat("lastMoveX");
                float lastMoveY = myAnim.GetFloat("lastMoveY");

                // Set the appropriate trigger based on direction
                if (Mathf.Abs(lastMoveX) > Mathf.Abs(lastMoveY))
                {
                    // Character is primarily moving horizontally
                    if (lastMoveX > 0)
                    {
                        myAnim.Play("AttackRight", -1, 0f); // Play attack animation immediately
                    }
                    else
                    {
                        myAnim.Play("AttackLeft", -1, 0f); // Play attack animation immediately
                    }
                }
                else
                {
                    // Character is primarily moving vertically
                    if (lastMoveY > 0)
                    {
                        myAnim.Play("AttackUp", -1, 0f); // Play attack animation immediately
                    }
                    else
                    {
                        myAnim.Play("AttackDown", -1, 0f); // Play attack animation immediately
                    }
                }

                isAttacking = true;
                attackCooldownTimer = attackCooldown; // Reset the cooldown timer
            }
        }
        else
        {
            // Decrement the attack cooldown timer
            attackCooldownTimer -= Time.deltaTime;
            if (attackCooldownTimer <= 0)
            {
                isAttacking = false;
            }
        }
    }
}
