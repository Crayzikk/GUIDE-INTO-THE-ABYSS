using UnityEngine;
using Zenject;

public class DialogTrigger : MonoBehaviour
{
    private DialogManager dialogManager;

    [Inject]
    public void Initialize(DialogManager _dialogManager)
    {
        dialogManager = _dialogManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogManager.StartDialog();
        }
    }
}
