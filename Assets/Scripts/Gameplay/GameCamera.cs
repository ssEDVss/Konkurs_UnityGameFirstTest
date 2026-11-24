using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 14f;
    public float minDistance = 6f;
    public float maxDistance = 28f;
    public float zoomSpeed = 3f;
    public float pitch = 50f;
    public float yaw = 0f;
    public float rotateSpeed = 90f;

    void LateUpdate()
    {
        if (target == null) return;

        if (Input.GetKey(KeyCode.Q)) yaw -= rotateSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E)) yaw += rotateSpeed * Time.deltaTime;

        distance -= Input.mouseScrollDelta.y * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        Vector3 focus = target.position + Vector3.up;
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        transform.position = focus - rotation * Vector3.forward * distance;
        transform.LookAt(focus);
    }
}