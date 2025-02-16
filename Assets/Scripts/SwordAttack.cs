using UnityEngine;
using System.Collections.Generic;

public class SwordAttack : MonoBehaviour
{
    public int attackDamage = 10; // Damage the sword deals to enemies
    private BoxCollider2D swordCollider;
    public Transform hitboxPivot;

 
    private HashSet<Enemy> hitEnemies = new HashSet<Enemy>();

    public Vector2 hitboxSizeRight;
    public Vector2 hitboxSizeLeft;
    public Vector2 hitboxSizeUp;
    public Vector2 hitboxSizeDown;

    public Vector2 hitboxOffsetRight;
    public Vector2 hitboxOffsetLeft;
    public Vector2 hitboxOffsetUp;
    public Vector2 hitboxOffsetDown;

    private void Awake()
    {
        swordCollider = GetComponent<BoxCollider2D>();
    }

    public void EnableAttack(string direction)
    {
        swordCollider.enabled = true; 
        UpdateHitbox(direction); 
        hitEnemies.Clear(); 
    }

    public void DisableAttack()
    {
        swordCollider.enabled = false;
    }

    private void UpdateHitbox(string direction)
    {
       
        switch (direction)
        {
            case "Right":
                swordCollider.size = hitboxSizeRight;
                swordCollider.offset = hitboxOffsetRight;
                break;
            case "Left":
                swordCollider.size = hitboxSizeLeft;
                swordCollider.offset = hitboxOffsetLeft;
                break;
            case "Up":
                swordCollider.size = hitboxSizeUp;
                swordCollider.offset = hitboxOffsetUp;
                break;
            case "Down":
                swordCollider.size = hitboxSizeDown;
                swordCollider.offset = hitboxOffsetDown;
                break;
            default:
                Debug.LogError("Invalid direction specified for hitbox update: " + direction);
                break;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null && !hitEnemies.Contains(enemy))
            {
                enemy.TakeDamage(attackDamage);
                hitEnemies.Add(enemy); 
            }
        }
    }
}
