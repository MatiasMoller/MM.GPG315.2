using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum CameraMode
    {
        ThirdPerson,
        TopDown,
        SideScroll
    }

    public CameraMode currentMode = CameraMode.ThirdPerson;

    public Transform target;
    public float distance = 5f;
    public float height = 2f;
    public float rotationSpeed = 5f;
    public float transitionSpeed = 2f;

    private Vector3 desiredPosition;
    private Quaternion desiredRotation;
    private float originalHeight;

    private void Update()
    {
        switch (currentMode)
        {
            case CameraMode.ThirdPerson:
                originalHeight = height; // Store original height
                desiredPosition = target.position + new Vector3(0, height, -distance);
                desiredRotation = Quaternion.LookRotation(target.position - transform.position);
                break;
            case CameraMode.TopDown:
                height = 20f; // Set height for top-down view
                desiredPosition = new Vector3(target.position.x, target.position.y + height, target.position.z);
                desiredRotation = Quaternion.Euler(90, 0, 0);
                break;
            case CameraMode.SideScroll:
                height = originalHeight; // Restore original height
                desiredPosition = new Vector3(target.position.x - distance, target.position.y + height, target.position.z);
                desiredRotation = Quaternion.Euler(0, 90, 0);
                desiredPosition.x -= distance / 2;
                break;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * transitionSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
    }
}