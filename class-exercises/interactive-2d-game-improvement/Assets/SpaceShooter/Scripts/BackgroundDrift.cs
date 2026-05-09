using UnityEngine;

public class BackgroundDrift : MonoBehaviour
{
    public Vector3 rotationPerSecond = new Vector3(0f, 0f, 4f);
    public Vector3 bobAmplitude = new Vector3(0.15f, 0.08f, 0f);
    public float bobSpeed = 0.7f;

    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        transform.Rotate(rotationPerSecond * Time.deltaTime);
        float wave = Mathf.Sin(Time.time * bobSpeed);
        transform.position = startPosition + bobAmplitude * wave;
    }
}
