using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;
public class PlayerHealthSystem : HealthSystem
{
    [SerializeField] private int PlayerHealthAmount;
    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private Image image;
    

    private void Start()
    {
        base.Init(PlayerHealthAmount);
        playerHealthText.text = "HP: \n" + getCurrentHealth() + "/" + getMaxHealth();
    }

    public override void ChangeHealth(int amount)
    {
        currentHealth += amount;

        playerHealthText.text = "HP: \n" + getCurrentHealth() + "/" + getMaxHealth();
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

    private void Update()
    {
        Debug.Log("Player health: " + getCurrentHealth());
    }
    
    
    

}
