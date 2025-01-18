using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlotManager : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] private AICharacter aICharacterLeader;
    [SerializeField] private AICharacter aICharacterViktor;
    [SerializeField] private AICharacter aICharacterVlad;

    [Header("Sounds")]
    [SerializeField] private AudioClip scream;
    [SerializeField] private AudioClip rustle;
    [SerializeField] private AudioClip shoot;

    [Header("Other")]
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private GameObject rustleObject;
    [SerializeField] private GameObject prefabSpawner;
    [SerializeField] private Image imageCamp;

    [SerializeField] private AudioClip music1;

    public static bool isPlayerInTrigger;
    public static bool eventStart;
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
            characterStop = true;
            PlayerInForest();
            ivents++;
        }
        else if(aICharacterViktor.indexTargetPoint == 4 && ivents == 1)
        {
            PlayerHeardScream();
            ivents++;
        }
        else if(eventStart && ivents == 2)
        {
            PlayerInCamp();
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

        Invoke("ContinuePlayerHeardScream", 1f);
        Invoke("PlayerKillAllMonster", 2f);
    }

    private void ContinuePlayerHeardScream()
    {
        Instantiate(prefabSpawner, aICharacterViktor.gameObject.transform.position, Quaternion.identity);
        aICharacterViktor.Die();
        Invoke("PlayerKillAllMonster", 1f);
        audioSource.clip = music1;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void PlayerKillAllMonster()
    {
        dialogManager.StartDialog();
    }

    private void PlayerInCamp()
    {
        dialogManager.StartDialog();
        Invoke("IncreaseTransparency", 5f);
        Destroy(aICharacterLeader.gameObject);
        Destroy(aICharacterVlad.gameObject);
        audioSource.clip = shoot;
        audioSource.Play();
    }

    public void IncreaseTransparency()
    {
        StartCoroutine(Fade(1f));  // Прозорість = 1 (повністю видимий)
        Invoke("DecreaseTransparency", 2f);
    }

    // Функція для зменшення прозорості
    public void DecreaseTransparency()
    {
        StartCoroutine(Fade(0f));  // Прозорість = 0 (повністю прозорий)
        Invoke("IncreaseTransparency", 4f);
        dialogManager.StartDialog();

    }

    // Корутина для анімації зміни прозорості
    private IEnumerator Fade(float targetAlpha)
    {
        float currentAlpha = imageCamp.color.a;
        float timeElapsed = 0f;

        while (timeElapsed < 1f)
        {
            float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, timeElapsed / 1f);
            imageCamp.color = new Color(imageCamp.color.r, imageCamp.color.g, imageCamp.color.b, newAlpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        imageCamp.color = new Color(imageCamp.color.r, imageCamp.color.g, imageCamp.color.b, targetAlpha);
    }
}
