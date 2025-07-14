using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] protected int MaxHealth = 100;
    protected int currentHealth;
    

    public void Init()
    {
        
        currentHealth = MaxHealth;
    }


    public void ChangeHealth(int amount)
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
