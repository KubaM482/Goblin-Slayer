using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    protected int MaxHealth;
    protected int currentHealth;
    

    public void Init(int amount)
    {
        MaxHealth = amount;
        currentHealth = amount;
    }


    public virtual void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameObject.SetActive(false);
        }

    }
    

    public bool isDead() => currentHealth <= 0;
    public float getHealthPercent() => (float)currentHealth / MaxHealth;
    public int getCurrentHealth() => currentHealth;
    public int getMaxHealth() => MaxHealth;

}
