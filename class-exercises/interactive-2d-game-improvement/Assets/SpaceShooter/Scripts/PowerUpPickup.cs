using UnityEngine;

public enum PowerUpKind
{
    RapidFire,
    Repair,
    Shield,
    ScoreBoost
}

public class PowerUpPickup : MonoBehaviour
{
    public PowerUpKind kind;
    public bool randomizeOnSpawn = true;
    public float fallSpeed = 1.15f;
    public float lifetime = 11f;
    public SpriteRenderer spriteRenderer;

    private float despawnTime;

    private void Awake()
    {
        despawnTime = Time.time + lifetime;
        if (randomizeOnSpawn)
        {
            kind = (PowerUpKind)Random.Range(0, 4);
        }
        ApplyVisuals();
    }

    private void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        transform.Rotate(0f, 0f, 120f * Time.deltaTime);

        if (Time.time >= despawnTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ArcadePlayerController player = other.GetComponent<ArcadePlayerController>();
        if (player == null)
        {
            return;
        }

        if (Health.instance != null)
        {
            Health.instance.ApplyPowerUp(kind);
        }
        Destroy(gameObject);
    }

    private void ApplyVisuals()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        if (spriteRenderer == null)
        {
            return;
        }

        switch (kind)
        {
            case PowerUpKind.RapidFire:
                spriteRenderer.color = new Color(0.35f, 0.8f, 1f, 1f);
                break;
            case PowerUpKind.Repair:
                spriteRenderer.color = new Color(0.35f, 1f, 0.45f, 1f);
                break;
            case PowerUpKind.Shield:
                spriteRenderer.color = new Color(1f, 0.85f, 0.25f, 1f);
                break;
            case PowerUpKind.ScoreBoost:
                spriteRenderer.color = new Color(1f, 0.45f, 0.9f, 1f);
                break;
        }
    }
}
