using Unity.VisualScripting.Antlr3.Runtime.Misc;
using System.Collections;
using UnityEngine;

/*
 * 스텟에 따른 업데이트
 */
[System.Serializable]
public class PlayerStats : MonoBehaviour
{

    [Header("# 레벨")]
    public int playerLevel ;

    [Header("# 스텟 3종")]
    public int strength = 1; //공격력 / 체력
    public int agility = 1; //공격속도 / 이동속도
    public int intelligence = 1; //마법공격력 

    [Header("# 체력")]
    public float maxHealth;
    public float currentHealth;
    float baseMaxHealth;


    PlayerAnimation anim;

    private void Awake()
    {
        baseMaxHealth = maxHealth;
        currentHealth = maxHealth;
        anim = GetComponent<PlayerAnimation>();
    }


    public void MaxHealthUpdate()
    {
        maxHealth = maxHealth + strength * 2;
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Enemy"))
            return;

        TakeDamage(collision.gameObject.GetComponent<Enemy>().attackPower);
    }

    public void TakeDamage(float damage)
    {

        currentHealth -= Time.deltaTime * damage;

        if (currentHealth <= 0)
        {
            Debug.Log("Player Dead");
            GameManager.instance.player.SetHandActive(false);
            anim.SetBool("Dead",true);
            StartCoroutine(DeadAnimEnd());
        }
    }


    private IEnumerator DeadAnimEnd()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.instance.GameOver();
    }
}
