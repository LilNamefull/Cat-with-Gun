using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, 5);
        DontDestroyOnLoad(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        

        // Ignor Boss, enemys and own Bullet
        if (collision.CompareTag("Boss") || collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet"))
        {
            return; 
        }

        // Ignor Backgorund and Untagged 
        
        if (collision.CompareTag("Background") || collision.CompareTag("Untagged"))
        {
            return;
        }

        // Hit Player
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            Destroy(gameObject);
            return; 
        }

        
    }
}