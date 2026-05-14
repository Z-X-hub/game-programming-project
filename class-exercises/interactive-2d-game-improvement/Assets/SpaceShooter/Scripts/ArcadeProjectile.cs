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
        if (Damage.Apply(other.gameObject, damage, teamId))
        {
            Impact();
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
