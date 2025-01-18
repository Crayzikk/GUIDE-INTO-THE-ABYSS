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
    [SerializeField] private float timeToHeal = 0.3f; 
    private float healTimer;

    // Reference to components
    private PlayerMovement playerMovement;
    private Animator playerAnimator;

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
        playerAnimator = GetComponent<Animator>();

        playerHealthController.InitializeHealth(healthPlayer);
        healTimer = timeToHeal; 
    }
    
    void Update()
    {
        //playerHealthController.DebugHealh();

        if(!DialogManager.dialogActive)
        {
            GettingInputs();

            float targetSpeed = runInput ? speedRunPlayer : speedWalkPlayer;
            currentSpeedPlayer = Mathf.Lerp(currentSpeedPlayer, targetSpeed, stepLerp * Time.deltaTime);

            playerMovement.RunPlayer(horizontalInput, verticalInput, currentSpeedPlayer); 
            playerAnimator.SetBool("IsRunning", runInput);

            if (playerHealthController.healthNotMax)
            {
                healTimer -= Time.deltaTime; 

                if (healTimer <= 0)
                {
                    playerHealthController.Heal(1); 
                    healTimer = timeToHeal; 
                }
            }
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
