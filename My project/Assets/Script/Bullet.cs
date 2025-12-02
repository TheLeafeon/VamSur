using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected float damage;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int piercing;

    protected Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();    
    }

    public virtual void Init(float damage, float speed, int piercing, Vector3 dir ,int level)
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

        OnHitEnemy(collision.GetComponent<Enemy>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || piercing == -100)
            return;
        gameObject.SetActive(false);
    }


    protected virtual void OnHitEnemy(Enemy enemy)
    {
        enemy.TakeDamage(damage);

        if (piercing == -100)
            return;

        piercing--;

        if (piercing < 0)
        {
            rigid.linearVelocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
