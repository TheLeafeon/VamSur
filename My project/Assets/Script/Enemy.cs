using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("# Enemy Info")]
    public float health;
    public float maxHealth;
    public float attackPower;
    public int dropExp;



    Rigidbody2D rigid;
    Collider2D coll;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        health = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Melee":
                //health -= collision.GetComponent<MeleeWeapon>().damage;
                break;

            case "Bullet":
                Debug.Log("Range hit");
                break;

        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Debug.Log("Enemy Dead");

            GameManager.instance.kill++;
            GameManager.instance.GetExp(dropExp);

            gameObject.SetActive(false);
        }
    }
}
