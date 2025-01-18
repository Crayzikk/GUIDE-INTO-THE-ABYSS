using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] TextMeshProUGUI textHealth;

    void Update()
    {
        textHealth.text = $"HP: {health.GetCurrentHealth()}";
    }
}
