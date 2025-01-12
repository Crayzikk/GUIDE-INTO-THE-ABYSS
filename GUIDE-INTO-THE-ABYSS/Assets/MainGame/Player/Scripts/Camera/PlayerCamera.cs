using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float senseX;  
    [SerializeField] private float senseY;  
    [SerializeField] private Transform player;  
    [SerializeField] private float smoothSpeed;

    private float xRotation;
    private float yRotation;
    private float currentXRotation;
    private float currentYRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senseX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senseY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        currentXRotation = Mathf.LerpAngle(currentXRotation, xRotation, smoothSpeed);
        currentYRotation = Mathf.LerpAngle(currentYRotation, yRotation, smoothSpeed);

        transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
        player.rotation = Quaternion.Euler(0, currentYRotation, 0);
    }
}