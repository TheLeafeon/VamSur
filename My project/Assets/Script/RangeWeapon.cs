using System.Collections;
using Unity.Android.Gradle.Manifest;
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
    public bool isPhysical;



    private void Update()
    {
        Attack();
    }

    public override void Init(ItemData data)
    {
        Debug.Log("원거리 아이템 장착");
        player = GameManager.instance.player;
        //Basic Set
        name = "Weapon" + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        weaponId = data.itemId;
        attackRate = data.baseAttackRate;
        attackPower = data.baseAttackPower;
        speed = data.baseSpeed;
        count = data.baseCount;
        piercing = data.basePiercing;
        hitLayer = data.hitLayer;
        
        switch(data.damageType)
        {
            case ItemData.DamageType.Physical:
                isPhysical = true;
                break;
            case ItemData.DamageType.Magical:
                isPhysical = false;
                break;
        }
        

        for (int index = 0; index < GameManager.instance.projectilePool.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.projectilePool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        HandSet(data);
    }

    public override void Attack()
    {
        if (!CanAttack()) return;

        if (!player.scanner.nearestTarget)
            return;

        
        switch(weaponId)
        {
            case 2000:
                StartCoroutine("Attack2000");
                break;
            case 3000:
                Attack3000();
                break;
            
        }



        SetNextAttackTime();

    }
    
    
    private IEnumerator Attack2000()
    {
        float totaldamage = attackPower + (isPhysical ? playerStats.strength : playerStats.intelligence);

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        float shotDelay = 0.1f;

        for(int i=0;i<count;i++)
        {
            Transform bullet = GameManager.instance.projectilePool.Get(prefabId).transform;

            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

            bullet.GetComponent<Bullet>().Init(totaldamage, speed, piercing, dir);


            yield return new WaitForSeconds(shotDelay);
        }
    }

    private void Attack3000()
    {
        float totaldamage = attackPower + (isPhysical ? playerStats.strength : playerStats.intelligence);

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        float spreadAngle = 60f; // 전체 퍼짐 각도 (예: 30도)
        int middleIndex = count / 2; // 중앙 발의 인덱스

        for (int i = 0; i < count; i++)
        {
            // 중앙을 기준으로 각도를 부여 (좌우 대칭)
            float angle = (i - middleIndex) * (spreadAngle / (count - 1));
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 spreadDir = rot * dir;

            Transform bullet = GameManager.instance.projectilePool.Get(prefabId).transform;

            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, spreadDir);

            bullet.GetComponent<Bullet>().Init(
                attackPower + playerStats.strength, speed, piercing, spreadDir);
        }
    }

    public override void LevelUp()
    {
        switch(weaponId)
        {
            case 2000:
                attackPower += 3.0f;
                attackRate += 0.1f;
                break;
            case 3000:
                attackPower += 3.0f;
                if (count < 5)
                    count++;
                break;
        }
    }

    protected override void DealDamage(Enemy target)
    {
        
    }
}
