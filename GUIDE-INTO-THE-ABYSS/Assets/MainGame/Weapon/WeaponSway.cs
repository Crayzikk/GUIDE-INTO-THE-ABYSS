using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplayer;

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplayer;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplayer;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseX, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;
        
        transform.localRotation = Quaternion.Slerp(
            transform.localRotation, 
            targetRotation,
            smooth * Time.deltaTime
        );   
    }
}
