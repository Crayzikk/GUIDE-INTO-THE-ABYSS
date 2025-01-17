using UnityEngine;

public class Health : MonoBehaviour
{
    public bool healthInZero;
    public bool healthInMax;
    
    private int healthObject;
    private int maxHealthObject;

    public void InitializeHealth(int health)
    {
        maxHealthObject = health;
        healthObject = health;
        healthInZero = false;
    }

    public void TakeDamage(int damage)
    {
        healthObject -= damage;
        healthInMax = false;

        if(healthObject <= 0)
            healthInZero = true;
    }
    
    public void Heal(int amount)
    {
        healthObject += amount;

        if(healthObject > maxHealthObject)
        {
            healthInMax = true;
            healthObject = maxHealthObject; 
        }
            
    }

    public void DebugHealh()
    {
        Debug.Log(healthObject);
    }
}
