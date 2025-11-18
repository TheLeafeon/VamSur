using JetBrains.Annotations;
using UnityEngine;

public  class Enemy : MonoBehaviour
{
    [Header("# Common Enemy Info")]
    public string enemyName;
    public float maxHealth;
    public float currentHealth;
    public float speed;
    public float attackPower;
    public int dropExp;

    protected float nextAttackTime;


    public EnemyData data;

    Transform player;
    EnemyBehavior behavior;

    public void OnEnable()
    {
        
        OnSpawn();
    }

    public void OnSpawn()
    {
        player = GameManager.instance.player.transform;
        currentHealth = data.maxHealth;
        enemyName = data.enemyName;
        maxHealth = data.maxHealth;
        speed = data.speed;
        attackPower = data.attackPower;
        dropExp = data.dropExp;

        behavior = GetComponent<EnemyBehavior>();

        if (behavior != null)
        {
            behavior.Init(this, player);

        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Debug.Log("Enemy Dead");

            GameManager.instance.kill++;
            GameManager.instance.GetExp(dropExp);

            Dead();
            gameObject.SetActive(false);
        }
    }

    void Dead()
    {

    }

}
