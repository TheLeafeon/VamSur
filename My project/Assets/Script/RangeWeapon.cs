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
        //Basic Set
        name = "Weapon" + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        weaponId = data.itemId;
        damage = data.baseDamage;
        attackRate = data.baseAttackRate;
        speed = data.baseSpeed;
        count = data.baseCount;
        piercing = data.basePiercing;


        for (int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }


        //Hand Set
        Hand hand = player.hands[(int)data.itemType];
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

        bullet.GetComponent<Bullet>().Init(damage, speed, piercing, dir);

        SetNextAttackTime();

    }


}
