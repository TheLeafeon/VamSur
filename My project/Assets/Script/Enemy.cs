using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public float health;
    public float maxHealth;



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
}
