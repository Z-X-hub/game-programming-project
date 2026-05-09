using System.Collections;
using UnityEngine;

public class ArcadeEnemy : MonoBehaviour
{
    public enum EnemyMoveMode
    {
        Chase,
        Strafe,
        Gunship
    }

    [Header("Stats")]
    public int health = 2;
    public int scoreValue = 10;
    public int contactDamage = 1;
    public float moveSpeed = 2.2f;
    public EnemyMoveMode moveMode = EnemyMoveMode.Chase;

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform muzzle;
    public float fireRate = 1.4f;
    public float stopDistance = 4.25f;

    [Header("Drops")]
    public GameObject powerUpPrefab;
    [Range(0f, 1f)]
    public float powerUpDropChance = 0.18f;

    [Header("Feedback")]
    public SpriteRenderer spriteRenderer;
    public GameObject deathEffect;
    public AudioClip hitClip;
    public AudioClip deathClip;

    private Transform player;
    private float nextFireTime;
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

    private void Start()
    {
        ArcadePlayerController playerController = FindObjectOfType<ArcadePlayerController>();
        if (playerController != null)
        {
            player = playerController.transform;
        }

        float difficulty = GameSession.instance != null ? GameSession.instance.DifficultyMultiplier : 1f;
        moveSpeed *= difficulty;
        fireRate = Mathf.Max(0.7f, fireRate / difficulty);
    }

    private void Update()
    {
        if (GameSession.instance != null && !GameSession.instance.CanPlay)
        {
            return;
        }

        Move();
        TryShoot();

        if (Mathf.Abs(transform.position.x) > 12f || Mathf.Abs(transform.position.y) > 8f)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (hitClip != null)
        {
            AudioSource.PlayClipAtPoint(hitClip, transform.position);
        }
        StartCoroutine(FlashHit());

        if (health <= 0)
        {
            Die();
        }
    }

    private void Move()
    {
        if (player == null)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
            return;
        }

        Vector3 toPlayer = player.position - transform.position;
        if (toPlayer.sqrMagnitude > 0.001f)
        {
            transform.up = toPlayer.normalized;
        }

        if (moveMode == EnemyMoveMode.Gunship && toPlayer.magnitude < stopDistance)
        {
            return;
        }

        if (moveMode == EnemyMoveMode.Strafe)
        {
            Vector3 strafe = new Vector3(Mathf.Sin(Time.time * 2f), -0.85f, 0f).normalized;
            transform.position += strafe * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;
        }
    }

    private void TryShoot()
    {
        if (moveMode == EnemyMoveMode.Chase || projectilePrefab == null || muzzle == null || player == null)
        {
            return;
        }

        if (Time.time < nextFireTime)
        {
            return;
        }

        nextFireTime = Time.time + fireRate;
        Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ArcadePlayerController playerController = other.GetComponent<ArcadePlayerController>();
        if (playerController != null)
        {
            playerController.TakeDamage(contactDamage);
            DieWithoutScore();
        }
    }

    private void Die()
    {
        if (GameSession.instance != null)
        {
            GameSession.instance.RegisterEnemyDefeated(scoreValue, transform.position);
        }

        TryDropPowerUp();
        PlayDeathFeedback();
        Destroy(gameObject);
    }

    private void DieWithoutScore()
    {
        PlayDeathFeedback();
        Destroy(gameObject);
    }

    private void TryDropPowerUp()
    {
        if (powerUpPrefab != null && Random.value <= powerUpDropChance)
        {
            Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
        }
    }

    private void PlayDeathFeedback()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
        if (deathClip != null)
        {
            AudioSource.PlayClipAtPoint(deathClip, transform.position);
        }
    }

    private IEnumerator FlashHit()
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
