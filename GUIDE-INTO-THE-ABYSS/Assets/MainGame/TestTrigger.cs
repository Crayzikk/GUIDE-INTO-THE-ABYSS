using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    [SerializeField] DialogManager dialogManager;
    [SerializeField] int index;

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            dialogManager.StartDialog(index);
        }
    }
}
