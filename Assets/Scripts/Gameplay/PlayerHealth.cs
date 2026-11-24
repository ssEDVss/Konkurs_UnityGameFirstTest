using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public Image healthFill;   // сюда перетащить BarFill из StatHealth
    public int current;

    void Start()
    {
        current = maxHealth;
        UpdateBar();
    }

    public void TakeDamage(int amount)
    {
        current = Mathf.Max(current - amount, 0);
        UpdateBar();
        Debug.Log("Игрок: здоровье " + current + "/" + maxHealth);
        if (current <= 0) Debug.Log("Игрок погиб");
    }

    void UpdateBar()
    {
        if (healthFill != null)
            healthFill.fillAmount = (float)current / maxHealth;
    }
}