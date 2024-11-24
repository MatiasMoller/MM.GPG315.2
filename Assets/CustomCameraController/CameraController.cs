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


    [Header("General Settings")]
    public float rotationSpeed = 5f;
    public float transitionSpeed = 2f;

    [Header("Third Person Settings")]
    public float thirdPersonDistance = 5f;
    public float thirdPersonHeight = 2f;

    [Header("Top Down Settings")]
    public float topDownHeight = 20f;

    [Header("Side Scroll Settings")]
    public float sideScrollDistance = 5f;
    public float sideScrollHeight = 2f;
    public float cameraOffset = 0f; // Add this variable for custom offset


   

    private Vector3 desiredPosition;
    private Quaternion desiredRotation;


    //SHAKE
    [Header("Shake Settings")]
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.1f;

    private float shakeTimer;

    public void ShakeCamera()
    {
        shakeTimer = shakeDuration;
    }


    public void SetThirdPersonMode()
    {
        currentMode = CameraMode.ThirdPerson;
    }
    public void SetTopDownMode()
    {
        currentMode = CameraMode.TopDown;
    }

    public void SetSideScrollMode()
    {
        currentMode = CameraMode.SideScroll;
    }

    //ZOOM
    [Range(1f, 10f)]
    public float zoomLevel = 5f;

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
                desiredPosition.z -= cameraOffset; // Adjust Z position for left/right movement
                break;
        }

        if (shakeTimer > 0)
        {
            Vector3 cameraPos = transform.position;
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);
            cameraPos.x += offsetX;
            cameraPos.y += offsetY;
            transform.position = cameraPos;

            shakeTimer -= Time.deltaTime;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * transitionSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);

        // Adjust camera field of view based on zoom level
        Camera.main.fieldOfView = 60f / zoomLevel;
    }

   
}