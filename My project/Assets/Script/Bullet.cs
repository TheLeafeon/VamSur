using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float damage;
    [SerializeField]
    float speed;
    [SerializeField]
    int piercing;

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

        collision.GetComponent<Enemy>().TakeDamage(damage);

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
