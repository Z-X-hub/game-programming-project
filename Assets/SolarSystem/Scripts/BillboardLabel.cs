using UnityEngine;

public class BillboardLabel : MonoBehaviour
{
    public Transform target;
    public Vector3 worldOffset = new Vector3(0f, 1.4f, 0f);
    public float distanceScale = 0.035f;
    public float minScale = 0.18f;
    public float maxScale = 0.55f;

    private Camera sceneCamera;

    private void Start()
    {
        sceneCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (sceneCamera == null)
        {
            sceneCamera = Camera.main;
        }

        if (target != null)
        {
            transform.position = target.position + worldOffset;
        }

        if (sceneCamera == null)
        {
            return;
        }

        transform.rotation = Quaternion.LookRotation(transform.position - sceneCamera.transform.position, Vector3.up);

        float distance = Vector3.Distance(sceneCamera.transform.position, transform.position);
        float scale = Mathf.Clamp(distance * distanceScale, minScale, maxScale);
        transform.localScale = Vector3.one * scale;
    }
}
