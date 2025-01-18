using UnityEngine;

public class PlotManager : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] private AICharacter aICharacterLeader;
    [SerializeField] private AICharacter aICharacterViktor;
    //[SerializeField] private AICharacter aICharacterVlad;

    [Header("Sounds")]
    [SerializeField] private AudioClip scream;
    [SerializeField] private AudioClip rustle;

    [Header("Other")]
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private GameObject rustleObject;
    [SerializeField] private GameObject prefabSpawner;

    public static bool isPlayerInTrigger;
    private bool eventMonsterStart;
    public static bool characterStop;
    private static int ivents = 0;


    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rustleObject.SetActive(false);
        dialogManager.StartDialog();
    }

    void Update()
    {
        characterStop = DialogManager.dialogActive;

        if(aICharacterLeader.indexTargetPoint == 3 && ivents == 0)
        {
            PlayerInForest();
            ivents++;
        }
        else if(aICharacterViktor.indexTargetPoint == 4 && ivents == 1)
        {
            PlayerHeardScream();
            ivents++;
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
        audioSource.clip = rustle;
        audioSource.Play();
        rustleObject.SetActive(true);
        dialogManager.StartDialog();
        Destroy(rustleObject, 0.2f);

        Invoke("ContinuePlayerHeardScream", 1f);
        Invoke("PlayerKillAllMonster", 3f);
    }

    private void ContinuePlayerHeardScream()
    {
        Instantiate(prefabSpawner, aICharacterViktor.gameObject.transform.position, Quaternion.identity);
        aICharacterViktor.Die();
    }

    private void PlayerKillAllMonster()
    {
        dialogManager.StartDialog();
    }

    private void PlayerInCamp()
    {

    }
}
