using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRB;
    private Animator myAnim;
    private bool isAttacking = false;
    private float attackDuration = 0.5f;
    private float attackDurationTimer = 0f;
    private float attackSpeedMultiplier = 0.3f;
    public float originalSpeed;

    public GameObject fireballPrefab;
    public bool fireballUnlocked;
    public float fireballCooldown = 2f;
    private float fireballCooldownTimer = 0f;
    public PlayerSwordController swordController;

    [SerializeField]
    private float speed = 1f;

    public float Speed
    {
        get { return speed; }
        set { speed = Mathf.Max(0, value); }
    }

    public AudioSource swordSwingAudio; // Reference to the Audio Source for sword swing sound

    void Awake()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        originalSpeed = speed;

        // Apply speed upgrades from GameManager if it exists
        if (GameManager.instance != null)
        {
            ApplySpeedUpgrades(GameManager.instance.speedUpgradeCount);
            fireballUnlocked = GameManager.instance.fireballUnlocked;
        }
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
            swordController.DisableSwordAttack();
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
            string direction = Mathf.Abs(attackDirection.x) > Mathf.Abs(attackDirection.y) ?
                               (attackDirection.x > 0 ? "Right" : "Left") :
                               (attackDirection.y > 0 ? "Up" : "Down");

            myAnim.Play("Attack" + direction, -1, 0f);
            swordController.EnableSwordAttack(direction);

            isAttacking = true;
            attackDurationTimer = attackDuration;
            speed *= attackSpeedMultiplier;

            // Play the sword swing sound
            if (swordSwingAudio != null)
            {
                swordSwingAudio.PlayOneShot(swordSwingAudio.clip); // Play the assigned audio clip
            }
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

    public void UnlockFireball()
    {
        fireballUnlocked = true;
    }

    public void UpgradeSpeed(float increaseAmount)
    {
        speed += increaseAmount;
        originalSpeed = speed;
    }

    private void ApplySpeedUpgrades(int upgradeCount)
    {
        float speedIncreaseAmount = 0.05f; // The amount speed increases per upgrade
        speed += speedIncreaseAmount * upgradeCount;
        originalSpeed = speed;
    }
}