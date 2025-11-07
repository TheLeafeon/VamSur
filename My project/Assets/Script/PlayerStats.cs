using UnityEngine;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    public int playerLevel;
    public float maxHealth;
    public float currentHealth;
    public float defense;

    public int strength=1; //공격력 / 체력
    public int agility=1; //공격속도 / 이동속도
    public int intelligence=1; //마법공격력 / 공격범위  아직은 사용하지 않을 예정

    private void Awake()
    {
        

        currentHealth = maxHealth;
    }


    public float TotalMaxHealth(float maxHealth,int strength)
    {
        float totalmaxHealth = maxHealth + strength * 2;
        return totalmaxHealth;
    }

    public void TakeDamage(float damage)
    {
        float totalDamage = Mathf.Max(1.0f, damage - defense);
        

        currentHealth-= Time.deltaTime * totalDamage;

        if (currentHealth <= 0)
        {
            Debug.Log("Player Dead");
        }
    }
}
