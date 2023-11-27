using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRB;
    private Animator myAnim;
    private bool isAttacking = false;
    private float attackDuration = 0.5f;
    private float attackDurationTimer = 0f;
    private float attackSpeedMultiplier = 0.3f; 
    private float originalSpeed; 

    public GameObject fireballPrefab; // Assign this in the Inspector
    public bool fireballUnlocked = true; // Initially false, can be unlocked in-game

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
        HandleFireballInput(); // Handling the fireball input
    }

    void HandleMovementInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput).normalized;

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
        if (isAttacking && attackDurationTimer > 0)
        {
            attackDurationTimer -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0) && !isAttacking) // Right mouse button for regular attack
        {
            Vector2 attackDirection = GetAttackDirection();

            // Attack animation logic
            if (Mathf.Abs(attackDirection.x) > Mathf.Abs(attackDirection.y))
            {
                if (attackDirection.x > 0)
                {
                    myAnim.Play("AttackRight", -1, 0f);
                }
                else
                {
                    myAnim.Play("AttackLeft", -1, 0f);
                }
            }
            else
            {
                if (attackDirection.y > 0)
                {
                    myAnim.Play("AttackUp", -1, 0f);
                }
                else
                {
                    myAnim.Play("AttackDown", -1, 0f);
                }
            }

            isAttacking = true;
            attackDurationTimer = attackDuration;
            speed *= attackSpeedMultiplier;
        }

        if (isAttacking && attackDurationTimer <= 0)
        {
            isAttacking = false;
            speed = originalSpeed;
        }
    }

    void HandleFireballInput()
    {
        if (fireballUnlocked && Input.GetMouseButtonDown(1)) // Middle mouse button for fireball
        {
            LaunchFireballTowardsCursor();
        }
    }

    void LaunchFireballTowardsCursor()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.nearClipPlane;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0;

        Vector2 fireballDirection = (mouseWorldPosition - transform.position).normalized;

        // Calculate the spawn offset
        float offsetDistance = 0.1f; // Adjust this value as needed
        Vector2 spawnPosition = transform.position + (Vector3)(fireballDirection * offsetDistance);

        // Instantiate the fireball at the offset position
        GameObject fireball = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);

        // Set the fireball's velocity
        fireball.GetComponent<Rigidbody2D>().velocity = fireballDirection * 1.2f; // Adjust the speed as needed

        Destroy(fireball, 5f); // Fireball disappears after 5 seconds
    }






    Vector2 GetAttackDirection()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 attackDirection = mouseWorldPosition - transform.position;
        return attackDirection.normalized;
    }
}
