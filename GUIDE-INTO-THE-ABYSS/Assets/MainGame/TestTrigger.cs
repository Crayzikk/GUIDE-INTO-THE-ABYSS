using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    [SerializeField] DialogManager dialogManager;

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            dialogManager.StartDialog();
        }
    }
}
