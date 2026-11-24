using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 12f;
    public Transform cameraTransform;

    private CharacterController controller;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (cameraTransform == null) cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        forward.Normalize();
        Vector3 right = cameraTransform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 direction = forward * v + right * h;
        if (direction.sqrMagnitude > 1f) direction.Normalize();

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion target = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, rotateSpeed * Time.deltaTime);
        }

        if (controller.isGrounded) verticalVelocity = -2f;
        else verticalVelocity -= 9.81f * Time.deltaTime;

        Vector3 move = direction * moveSpeed + Vector3.up * verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }
}