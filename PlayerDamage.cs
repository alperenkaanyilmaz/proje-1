using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP;
    public GameManager gm;

    void Start()
    {
        currentHP = maxHP;
        if (gm == null) gm = GameManager.Instance;
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        if (currentHP <= 0f)
        {
            currentHP = 0f;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died.");
        if (gm != null) gm.PlayerDied();
        
    }
}
