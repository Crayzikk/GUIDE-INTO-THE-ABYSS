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
    private Health playerHealthController;

    // Reference to components
    private PlayerMovement playerMovement;

    // Inputs
    private float horizontalInput;
    private float verticalInput;
    private bool runInput;
    private bool jumpInput;

    // Player State
    private float currentSpeedPlayer;

    // Cheking
    public static bool isRunning;

    
    void Start()
    {
        currentSpeedPlayer = speedWalkPlayer;
        playerHealthController = GetComponent<Health>();

        playerMovement = GetComponent<PlayerMovement>();
    }
    
    void Update()
    {
        if(!DialogManager.dialogActive)
        {
            GettingInputs();

            float targetSpeed = runInput ? speedRunPlayer : speedWalkPlayer;
            currentSpeedPlayer = Mathf.Lerp(currentSpeedPlayer, targetSpeed, stepLerp * Time.deltaTime);

            playerMovement.RunPlayer(horizontalInput, verticalInput, currentSpeedPlayer);            
        }

    }

    void FixedUpdate()
    {
        if(playerMovement.PlayerCheckGround() && jumpInput && !DialogManager.dialogActive)
            playerMovement.JumpPlayer(jumpForcePlayer);
    }

    private void GettingInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        jumpInput = Input.GetKeyDown(KeyCode.Space);
        runInput = Input.GetKey(KeyCode.LeftShift);
        isRunning = runInput;
    }
}
