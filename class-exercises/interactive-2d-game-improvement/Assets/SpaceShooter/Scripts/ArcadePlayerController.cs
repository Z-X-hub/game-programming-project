using System.Collections;
using UnityEngine;

public class ArcadePlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 7.5f;
    public Vector2 playAreaMin = new Vector2(-8.2f, -4.35f);
    public Vector2 playAreaMax = new Vector2(8.2f, 4.35f);

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform muzzle;
    public float baseFireRate = 0.14f;

    [Header("Feedback")]
    public SpriteRenderer spriteRenderer;
    public AudioSource audioSource;
    public AudioClip fireClip;
    public float damageInvulnerability = 1.1f;

    private float nextFireTime;
    private float invulnerableUntil;
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
        if (Health.instance == null || !Health.instance.CanPlay)
        {
            return;
        }

        Move();
        AimAtMouse();
        TryFire();
    }

    public void TakeDamage(int damageAmount)
    {
        if (Time.time < invulnerableUntil)
        {
            return;
        }

        invulnerableUntil = Time.time + damageInvulnerability;
        if (Health.instance != null)
        {
            Health.instance.TakeDamage(damageAmount);
        }
        StartCoroutine(FlashDamage());
    }

    private void Move()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
        if (input.sqrMagnitude > 1f)
        {
            input.Normalize();
        }

        Vector3 nextPosition = transform.position + input * moveSpeed * Time.deltaTime;
        nextPosition.x = Mathf.Clamp(nextPosition.x, playAreaMin.x, playAreaMax.x);
        nextPosition.y = Mathf.Clamp(nextPosition.y, playAreaMin.y, playAreaMax.y);
        transform.position = nextPosition;
    }

    private void AimAtMouse()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            return;
        }

        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mouseWorld - transform.position;
        if (aimDirection.sqrMagnitude > 0.001f)
        {
            transform.up = aimDirection.normalized;
        }
    }

    private void TryFire()
    {
        if (!Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Space))
        {
            return;
        }

        float fireMultiplier = Health.instance != null ? Health.instance.FireRateMultiplier : 1f;
        if (Time.time < nextFireTime)
        {
            return;
        }

        nextFireTime = Time.time + baseFireRate * fireMultiplier;
        if (projectilePrefab != null && muzzle != null)
        {
            Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
        }
        if (audioSource != null && fireClip != null)
        {
            audioSource.PlayOneShot(fireClip);
        }
    }

    private IEnumerator FlashDamage()
    {
        if (spriteRenderer == null)
        {
            yield break;
        }

        for (int i = 0; i < 4; i++)
        {
            spriteRenderer.color = new Color(1f, 0.35f, 0.35f, 0.8f);
            yield return new WaitForSeconds(0.08f);
            spriteRenderer.color = defaultColor;
            yield return new WaitForSeconds(0.08f);
        }
    }
}
