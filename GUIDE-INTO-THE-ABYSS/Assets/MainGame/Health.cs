using UnityEngine;

public class Health : MonoBehaviour
{
    public bool healthInZero;
    public bool healthInMax;
    public bool healthNotMax;
    
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
        healthNotMax = true;

        if(healthObject <= 0)
            healthInZero = true;
    }
    
    public void Heal(int amount)
    {
        healthObject += amount;

        if(healthObject > maxHealthObject)
        {
            healthNotMax = false;
            healthInMax = true;
            healthObject = maxHealthObject; 
        }
            
    }

    public int GetCurrentHealth()
    {
        return healthObject;
    }

    public void DebugHealh()
    {
        Debug.Log(healthObject);
    }
}
