using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class RangeWeapon : Weapon
{
    [Header("Range Setting")]
    public float speed;
    public int count;
    public int piercing;
    public int prefabId;
    public LayerMask hitLayer;




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
        speed = data.baseSpeed;
        count = data.baseCount;
        piercing = data.basePiercing;
        hitLayer = data.hitLayer;




        for (int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }


        //Hand Set
       
        //왼손 고정하는 방법 추가 필요, 0은 하드코딩
        Hand hand = player.hands[0];
        hand.spriter.sprite = data.hand;


    }

    public override void Attack()
    {
        if (!CanAttack()) return;

        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;

        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        bullet.GetComponent<Bullet>().Init(attackPower, speed, piercing, dir);

        SetNextAttackTime();

    }


    protected override void DealDamage(Enemy target)
    {
        Debug.Log("Range Weapon Deal Damage ");
    }
}
