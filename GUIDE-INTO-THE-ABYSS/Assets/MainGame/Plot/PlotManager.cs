using UnityEngine;

public class PlotManager : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] private AICharacter aICharacterLeader;
    [SerializeField] private AICharacter aICharacterViktor;
    [SerializeField] private AICharacter aICharacterVlad;

    [Header("Sounds")]
    [SerializeField] private AudioClip scream;
    [SerializeField] private AudioClip rustle;

    [Header("Other")]
    [SerializeField] private DialogManager dialogManager;

    public static bool isPlayerInTrigger;
    public static bool eventStart;
    public static bool characterStop;
    private static int ivents = 0;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        dialogManager.StartDialog(); 
    }

    void Update()
    {
        characterStop = DialogManager.dialogActive;

        if(aICharacterLeader.indexTargetPoint == 3 && ivents == 0)
        {
            characterStop = true;
            PlayerInForest();
            ivents++;
        }
        else if(aICharacterViktor.indexTargetPoint == 3 && ivents == 1)
        {
            
        }
    }

    private void PlayerInForest()
    {
        audioSource.clip = scream;
        audioSource.Play();
        dialogManager.StartDialog();
    }

    private void PlayerHeardScream()
    {

    }

    private void PlayerKillAllMonster()
    {

    }

    private void PlayerInCamp()
    {

    }
}
