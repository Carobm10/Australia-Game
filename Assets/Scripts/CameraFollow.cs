using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform target;

    [Header("Seguimiento")]
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 1f, -10f);

    [Header("Limites")]
    public float minX = 0f;
    public float maxX = 50f;

    void LateUpdate()
    {
        if (target == null)
            return;

        float targetX = Mathf.Clamp(target.position.x, minX, maxX);

        Vector3 desiredPosition = new Vector3(
            targetX + offset.x,
            target.position.y + offset.y,
            offset.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}