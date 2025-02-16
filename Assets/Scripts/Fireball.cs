using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10; // Damage the fireball deals to enemies
    public float maxRange = 10f; // Maximum range the fireball can travel

    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);


        if (Vector2.Distance(startPosition, transform.position) > maxRange)
        {
            Destroy(gameObject); 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemyScript = other.gameObject.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(damage);
            }

            Destroy(gameObject); 
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject); 
        }

    }
}