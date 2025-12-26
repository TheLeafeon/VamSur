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

    public bool isLive;
    public EnemyData data;

    Transform player;
    EnemyBehavior behavior;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {

        // OnSpawn();
        player = GameManager.instance.player.transform;
        currentHealth = data.maxHealth;
        enemyName = data.enemyName;
        maxHealth = data.maxHealth;
        speed = data.speed;
        attackPower = data.attackPower;
        dropExp = data.dropExp;
        anim.runtimeAnimatorController = data.animator;
        behavior = GetComponent<EnemyBehavior>();
        isLive = true;
        if (behavior != null)
        {

            behavior.Init(this, player);
        }
    }

    public void TakeDamage(float damage, bool isKnockBack)
    {
        currentHealth -= damage;

        if (currentHealth > 0 )
        {
            anim.SetTrigger("Hit");

            if(isKnockBack)
            {
                StartCoroutine(behavior.KnockBack());
            }
        }
        else
        {
            Debug.Log("Enemy Dead");
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp(dropExp);
            isLive = false;

            behavior.BehaviorDead();
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }

}
