using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRB;
    private Animator myAnim;
    private bool isAttacking = false;
    private bool isCooldown = false; // Added a flag for cooldown
  //  private float attackCooldown = 1f; // Adjust this value as needed
    private float attackCooldownTimer = 0f;
    private float attackDuration = 0.5f; // Adjust this value as needed
    private float attackDurationTimer = 0f;
    private float attackSpeedMultiplier = 0.3f; // Adjust this value to control attack speed
    private float originalSpeed; // Store the original speed

    [SerializeField]
    private float speed;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        originalSpeed = speed;
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
        myRB.velocity = inputVector * (isAttacking ? speed * attackSpeedMultiplier : speed);

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
        // Check if the attack duration has elapsed
        if (isAttacking && attackDurationTimer > 0)
        {
            attackDurationTimer -= Time.deltaTime;
        }

        // Check if the attack cooldown has elapsed
        if (isCooldown && attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        // Check for attack input and that the player is not already attacking and the cooldown has elapsed
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking && attackCooldownTimer <= 0)
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
            isCooldown = true;
            attackDurationTimer = attackDuration;
            speed *= attackSpeedMultiplier; // Reduce speed during attack
        }

        // Check if the attack animation has finished
        if (isAttacking && attackDurationTimer <= 0)
        {
            isAttacking = false;
        }

        // Check if the cooldown has elapsed
        if (isCooldown && attackCooldownTimer <= 0)
        {
            isCooldown = false;
            speed = originalSpeed; // Restore the original speed
        }
    }
}
