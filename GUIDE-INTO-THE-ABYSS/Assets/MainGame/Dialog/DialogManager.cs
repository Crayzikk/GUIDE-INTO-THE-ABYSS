using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DialogManager : MonoBehaviour
{
    public static bool dialogActive;

    [Header("Canvas UI")]
    [SerializeField] private GameObject canvasDialog;
    [SerializeField] private Image dialogImage;
    [SerializeField] private TextMeshProUGUI dialogName;
    [SerializeField] private TextMeshProUGUI dialogText;

    [Header("Dialog Sentence")]
    [SerializeField] List<Dialog> dialogSentences;

    private int currentIndexDialog = 0;

    void Start()
    {
       // ToggleDialogWindow(false);
    }

    void Update()
    {
        if (dialogActive && Input.GetKeyDown(KeyCode.E))
        {
            ShowNextSentence();
        }
    }

    public void StartDialog()
    {
        dialogActive = true;
        ToggleDialogWindow(true);
        ShowNextSentence(); 
    }

    private void ShowNextSentence()
    {
        Dialog currentDialog = dialogSentences[currentIndexDialog];
        
        if (currentDialog.sentence.Length > 0 && currentDialog.nameCharacterSentence.Length > 0)
        {
            dialogName.text = currentDialog.nameCharacterSentence[0];
            dialogText.text = currentDialog.sentence[0];

            currentDialog.sentence = RemoveFirstElement(currentDialog.sentence);
            currentDialog.nameCharacterSentence = RemoveFirstElement(currentDialog.nameCharacterSentence);
        }
        else
        {
            currentIndexDialog++;
            EndDialog();
        }
    }

    private void EndDialog()
    {
        dialogActive = false;
        ToggleDialogWindow(false);
    }

    private void ToggleDialogWindow(bool state)
        => canvasDialog.SetActive(state);

    private string[] RemoveFirstElement(string[] array)
    {
        if (array.Length <= 1) return new string[0];

        string[] newArray = new string[array.Length - 1];
        System.Array.Copy(array, 1, newArray, 0, newArray.Length);

        return newArray;
    }
}

