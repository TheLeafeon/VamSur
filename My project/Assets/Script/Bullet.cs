using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float speed;
    public int piercing;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();    
    }

    public void Init(float damage, float speed, int piercing, Vector3 dir )
    {
        this.damage = damage;
        this.speed = speed;
        this.piercing = piercing;

        if(piercing >=0)
        {
            rigid.linearVelocity = dir * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        piercing--;

        if(piercing <0)
        {
            rigid.linearVelocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;
        gameObject.SetActive(false);
    }
}
