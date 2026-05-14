using System.Collections;
using UnityEngine;

public class DestructibleAsteroid : MonoBehaviour
{
    public int health = 3;
    public int contactDamage = 1;
    public int scoreValue = 6;
    public float driftSpeed = 0.9f;
    public Vector2 driftDirection = new Vector2(0f, -1f);
    public SpriteRenderer spriteRenderer;
    public AudioClip hitClip;
    public AudioClip breakClip;

    private Color defaultColor = Color.white;

    private void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        if (spriteRenderer != null)
        {
            defaultColor = spriteRenderer.color;
        }
    }

    private void Update()
    {
        transform.position += (Vector3)(driftDirection.normalized * driftSpeed * Time.deltaTime);
        transform.Rotate(0f, 0f, 35f * Time.deltaTime);

        if (Mathf.Abs(transform.position.x) > 12f || Mathf.Abs(transform.position.y) > 8f)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (hitClip != null)
        {
            AudioSource.PlayClipAtPoint(hitClip, transform.position);
        }
        StartCoroutine(Flash());

        if (health <= 0)
        {
            Break();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ArcadePlayerController player = other.GetComponent<ArcadePlayerController>();
        if (player != null)
        {
            player.TakeDamage(contactDamage);
            Break();
        }
    }

    private void Break()
    {
        if (Health.instance != null)
        {
            Health.instance.RegisterHazardDestroyed(scoreValue);
        }
        if (breakClip != null)
        {
            AudioSource.PlayClipAtPoint(breakClip, transform.position);
        }
        CameraShake.Shake(0.12f, 0.12f);
        Destroy(gameObject);
    }

    private IEnumerator Flash()
    {
        if (spriteRenderer == null)
        {
            yield break;
        }

        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.06f);
        spriteRenderer.color = defaultColor;
    }
}
