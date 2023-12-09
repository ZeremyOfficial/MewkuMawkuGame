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

    public GameObject fireballPrefab;
    public bool fireballUnlocked = false; // Set this to false initially if the fireball needs to be unlocked in-game
    public float fireballCooldown = 2f;
    private float fireballCooldownTimer = 0f;
    public PlayerSwordController swordController;

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
        HandleFireballInput();

        if (fireballCooldownTimer > 0)
        {
            fireballCooldownTimer -= Time.deltaTime;
        }

        if (isAttacking && attackDurationTimer > 0)
        {
            attackDurationTimer -= Time.deltaTime;
        }
        else if (isAttacking)
        {
            isAttacking = false;
            speed = originalSpeed;
            swordController.DisableSwordAttack(); // Make sure to disable the sword hitbox after attacking
        }
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
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            Vector2 attackDirection = GetAttackDirection();
            string direction;

            // Determine the direction of the attack and play the corresponding animation
            if (Mathf.Abs(attackDirection.x) > Mathf.Abs(attackDirection.y))
            {
                direction = attackDirection.x > 0 ? "Right" : "Left";
            }
            else
            {
                direction = attackDirection.y > 0 ? "Up" : "Down";
            }

            // Play the attack animation based on direction
            myAnim.Play("Attack" + direction, -1, 0f);

            // Enable the sword attack with the direction
            swordController.EnableSwordAttack(direction);

            isAttacking = true;
            attackDurationTimer = attackDuration;
            speed *= attackSpeedMultiplier; 
        }
    }

    void HandleFireballInput()
    {
        if (fireballUnlocked && fireballCooldownTimer <= 0 && Input.GetMouseButtonDown(1))
        {
            LaunchFireballTowardsCursor();
            fireballCooldownTimer = fireballCooldown;
        }
    }

    void LaunchFireballTowardsCursor()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.nearClipPlane;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0;

        Vector2 fireballDirection = (mouseWorldPosition - transform.position).normalized;
        float offsetDistance = 0.1f;
        Vector2 spawnPosition = (Vector2)transform.position + fireballDirection * offsetDistance;

        GameObject fireball = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
        fireball.GetComponent<Rigidbody2D>().velocity = fireballDirection * 1.2f;

        Destroy(fireball, 5f);
    }

    Vector2 GetAttackDirection()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mouseWorldPosition - transform.position).normalized;
    }

    public void UpgradeFireballCooldown(float amount)
    {
        fireballCooldown = Mathf.Max(0, fireballCooldown - amount);
    }
}
