using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speedWalkPlayer;
    [SerializeField] private float speedRunPlayer;
    [SerializeField] private float jumpForcePlayer;
    [SerializeField] private float stepLerp;

    [Header("Health Settings")]
    [SerializeField] private int healthPlayer;

    // Reference to components
    private PlayerMovement playerMovement;

    // Inputs
    private float horizontalInput;
    private float verticalInput;
    private bool runInput;
    private bool jumpInput;

    // Player State
    private float currentSpeedPlayer;
    private int currentHealthPlayer;
    
    void Start()
    {
        currentSpeedPlayer = speedWalkPlayer;
        currentHealthPlayer = healthPlayer;

        playerMovement = GetComponent<PlayerMovement>();
    }
    
    void Update()
    {
        GettingInputs();

        float targetSpeed = runInput ? speedRunPlayer : speedWalkPlayer;
        currentSpeedPlayer = Mathf.Lerp(currentSpeedPlayer, targetSpeed, stepLerp * Time.deltaTime);

        playerMovement.RunPlayer(horizontalInput, verticalInput, currentSpeedPlayer);
    }

    void FixedUpdate()
    {
        if(playerMovement.PlayerCheckGround() && jumpInput)
            playerMovement.JumpPlayer(jumpForcePlayer);
    }

    private void GettingInputs()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        jumpInput = Input.GetKeyDown(KeyCode.Space);
        runInput = Input.GetKey(KeyCode.LeftShift);
    }
}