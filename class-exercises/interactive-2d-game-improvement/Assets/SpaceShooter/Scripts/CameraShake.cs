using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private static CameraShake instance;

    public float damping = 12f;

    private Vector3 startPosition;
    private float shakeTime;
    private float shakeStrength;

    private void Awake()
    {
        instance = this;
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        if (shakeTime > 0f)
        {
            shakeTime -= Time.unscaledDeltaTime;
            Vector2 offset = Random.insideUnitCircle * shakeStrength;
            transform.localPosition = startPosition + new Vector3(offset.x, offset.y, 0f);
            shakeStrength = Mathf.MoveTowards(shakeStrength, 0f, damping * Time.unscaledDeltaTime);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, damping * Time.unscaledDeltaTime);
        }
    }

    public static void Shake(float duration, float strength)
    {
        if (instance == null)
        {
            return;
        }

        instance.shakeTime = Mathf.Max(instance.shakeTime, duration);
        instance.shakeStrength = Mathf.Max(instance.shakeStrength, strength);
    }
}
