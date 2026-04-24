using UnityEngine;

public class CometTrail : MonoBehaviour
{
    public Transform faceTarget;
    public float bobSpeed = 1.8f;
    public float bobHeight = 0.35f;

    private Vector3 startLocalOffset;

    private void Start()
    {
        startLocalOffset = transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, startLocalOffset.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight, transform.position.z);

        if (faceTarget != null)
        {
            transform.LookAt(faceTarget.position);
        }
    }
}
