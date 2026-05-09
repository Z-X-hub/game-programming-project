using UnityEngine;

public class ArcadeProjectile : MonoBehaviour
{
    public int teamId;
    public int damage = 1;
    public float speed = 12f;
    public float lifetime = 3f;
    public GameObject hitEffect;
    public AudioClip hitClip;

    private float despawnTime;

    private void Awake()
    {
        despawnTime = Time.time + lifetime;
    }

    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        if (Time.time >= despawnTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (teamId == 0)
        {
            ArcadeEnemy enemy = other.GetComponent<ArcadeEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Impact();
                return;
            }

            DestructibleAsteroid asteroid = other.GetComponent<DestructibleAsteroid>();
            if (asteroid != null)
            {
                asteroid.TakeDamage(damage);
                Impact();
            }
        }
        else
        {
            ArcadePlayerController player = other.GetComponent<ArcadePlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Impact();
            }
        }
    }

    private void Impact()
    {
        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
        }

        if (hitClip != null)
        {
            AudioSource.PlayClipAtPoint(hitClip, transform.position);
        }

        Destroy(gameObject);
    }
}
