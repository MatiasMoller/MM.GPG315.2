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

    [Header("Third Person Settings")]
    public float thirdPersonDistance = 5f;
    public float thirdPersonHeight = 2f;

    [Header("Top Down Settings")]
    public float topDownHeight = 20f;

    [Header("Side Scroll Settings")]
    public float sideScrollDistance = 5f;
    public float sideScrollHeight = 2f;

    public float rotationSpeed = 5f;
    public float transitionSpeed = 2f;

    private Vector3 desiredPosition;
    private Quaternion desiredRotation;

    private void Update()
    {
        switch (currentMode)
        {
            case CameraMode.ThirdPerson:
                desiredPosition = target.position + new Vector3(0, thirdPersonHeight, -thirdPersonDistance);
                desiredRotation = Quaternion.LookRotation(target.position - transform.position);
                break;
            case CameraMode.TopDown:
                desiredPosition = new Vector3(target.position.x, target.position.y + topDownHeight, target.position.z);
                desiredRotation = Quaternion.Euler(90, 0, 0);
                break;
            case CameraMode.SideScroll:
                desiredPosition = new Vector3(target.position.x - sideScrollDistance, target.position.y + sideScrollHeight, target.position.z);
                desiredRotation = Quaternion.Euler(0, 90, 0);
                desiredPosition.x -= sideScrollDistance / 2;
                break;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * transitionSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
    }
}