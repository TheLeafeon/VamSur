using UnityEngine;

public class MeleeWeapon : Weapon
{
    [Header("Melee Setting")]
    public float attackRange;
    public LayerMask hitLayer;
    public bool isPhysical;


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
        attackPower = data.baseAttackPower;
        attackRate = data.baseAttackRate;
        attackRange = data.baseAttackRange;
        hitLayer = data.hitLayer;

        switch (data.damageType)
        {
            case ItemData.DamageType.Physical:
                isPhysical = true;
                break;
            case ItemData.DamageType.Magical:
                isPhysical = false;
                break;
        }

        HandSet(data);
    }


    public override void Attack()
    {
        if (!CanAttack()) return;
        
        SpriteRenderer player = GetComponentsInParent<SpriteRenderer>()[0];

        direction = player != null && player.flipX ? Vector2.left : Vector2.right;

        //공격 범위
        Vector2 boxSize = new Vector2(attackRange, (1f+attackRange/10));
        Vector2 boxCenter = (Vector2)transform.position + direction * (attackRange / 2);

        Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 0f, hitLayer);

        //일단 이게 기본 공격
        if (hit != null && hit.CompareTag("Enemy"))
        {
            DealDamage(hit.GetComponent<Enemy>());
            Debug.Log("Melee Hit!");
        }

        SetNextAttackTime();
        
    }


    public override void LevelUp()
    {
        switch(weaponId)
        {
            case 1000:
                attackPower += 3.0f;
                break;
        }
    }




    // 공격 범위 보여주기용 기즈모
    //private void OnDrawGizmosSelected()
    //{
    //    SpriteRenderer player = GetComponentsInParent<SpriteRenderer>()[0];

    //    //1f는 언제든 수정 가능 범위가 증가함에 따라 이또한 커질 수 있음
    //    Vector2 boxSize = new Vector2(attackRange, 1f);
    //    Vector3 boxCenter = transform.position + (Vector3)(direction * attackRange / 2);


    //    Gizmos.color = Color.magenta;
    //    Gizmos.DrawCube(boxCenter, boxSize);
    //}


    protected override void DealDamage(Enemy target)
    {
        float totalDamage = attackPower + (isPhysical ? playerStats.strength : playerStats.intelligence);

        target.TakeDamage(totalDamage);
    }

}
