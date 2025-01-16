using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rigidbodyPlayer;
    private CheckGround checkGround;

    [Inject]
    public void Initialize(CheckGround _checkGround)
    {
        checkGround = _checkGround;
    }

    void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody>();
    }

    public void RunPlayer(float h, float v, float speedPlayer)
    {
        Vector3 velocity = (transform.right * h + transform.forward * v).normalized * speedPlayer;

        velocity.y = rigidbodyPlayer.linearVelocity.y;
        rigidbodyPlayer.linearVelocity = velocity;
    }

    public void JumpPlayer(float jumpForce)
    {
        rigidbodyPlayer.linearVelocity = new Vector3(rigidbodyPlayer.linearVelocity.x, 0, rigidbodyPlayer.linearVelocity.z);
        rigidbodyPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        checkGround.playerTouchGround = false; 
    }

    public bool PlayerCheckGround() => checkGround.playerTouchGround;
}
