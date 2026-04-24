using UnityEngine;

public class OrbitMotion : MonoBehaviour
{
    public Transform orbitCenter;
    public float orbitSpeedDegrees = 12f;
    public float selfRotationSpeedDegrees = 35f;
    public Vector3 orbitAxis = Vector3.up;

    private void Update()
    {
        float delta = Time.deltaTime;

        if (orbitCenter != null && Mathf.Abs(orbitSpeedDegrees) > 0.001f)
        {
            transform.RotateAround(orbitCenter.position, orbitAxis.normalized, orbitSpeedDegrees * delta);
        }

        if (Mathf.Abs(selfRotationSpeedDegrees) > 0.001f)
        {
            transform.Rotate(Vector3.up, selfRotationSpeedDegrees * delta, Space.Self);
        }
    }
}
