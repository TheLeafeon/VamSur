using UnityEngine;

public class MeleeWeapon : Weapon
{
    [Header("Melee Setting")]
    public float attackRange;
    public LayerMask hitLayer;


    Vector2 direction;

    private void Update()
    {
        Attack();
        
    }

    public override void Init(ItemData data)
    {

        player = GameManager.instance.player;
        //Basic Set
        name = "Weapon" + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        weaponId = data.itemId;
        damage = data.baseDamage;
        attackRate = data.baseAttackRate;
        attackRange = data.baseAttackRange;
        hitLayer = data.hitLayer;

        //Hand Set
        Hand hand = player.hands[(int)data.itemType];
        hand.spriter.sprite = data.hand;
    }


    public override void Attack()
    {
        if (!CanAttack()) return;
        
        SpriteRenderer player = GetComponentsInParent<SpriteRenderer>()[0];

        direction = player != null && player.flipX ? Vector2.left : Vector2.right;

        Vector2 boxSize = new Vector2(attackRange, 1f);
        Vector2 boxCenter = (Vector2)transform.position + direction * (attackRange / 2);

        Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 0f, hitLayer);

        if (hit != null && hit.CompareTag("Enemy"))
        {
            hit.GetComponent<Enemy>().health -= damage;
            Debug.Log("Melee Hit!");
        }

        SetNextAttackTime();
        
    }




    private void OnDrawGizmosSelected()
    {
        SpriteRenderer player = GetComponentsInParent<SpriteRenderer>()[0];

        //1f는 언제든 수정 가능 범위가 증가함에 따라 이또한 커질 수 있음
        Vector2 boxSize = new Vector2(attackRange, 1f);
        Vector3 boxCenter = transform.position + (Vector3)(direction * attackRange / 2);


        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(boxCenter, boxSize);
    }


}
