using UnityEngine;

public class EffectBullet : Bullet
{
    public ItemData itemData;
    int level;
    int prefabId;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public override void Init(float damage, float speed, int piercing, Vector3 dir, int level)
    {
        this.damage = damage;
        this.speed = speed; 
        this.piercing = piercing;
        this.level = level;
        if (piercing >= 0)
        {
            rigid.linearVelocity = dir * speed;

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        OnHitEnemy(collision.GetComponent<Enemy>());
        Effect();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || piercing == -100)
            return;
        gameObject.SetActive(false);
    }
    
    private void Effect()
    {
        switch(itemData.itemId)
        {
            case 2003:
                for (int index = 0; index < GameManager.instance.weaponPool.prefabs.Length; index++)
                {
                    if (itemData.DamageArea == GameManager.instance.weaponPool.prefabs[index])
                    {
                        prefabId = index;
                        break;
                    }
                }

                Transform area = GameManager.instance.weaponPool.Get(prefabId).transform;
                area.position = transform.position;
                area.GetComponent<DoTDamageArea>().Init(itemData, damage, level);
                break;
        }
    }
}
