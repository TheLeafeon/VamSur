using System.Collections;
using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class RangeWeapon : Weapon
{
    [Header("# Range Setting")]
    private float speed;
    private int count;
    private int piercing;
    private int prefabId;

    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        Attack();
        NonTargetAttack();
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
        weaponLevel = 1;
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
        
        for (int index = 0; index < GameManager.instance.weaponPool.prefabs.Length; index++)
        {
            if (data.bullet == GameManager.instance.weaponPool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }


        switch (weaponId)
        {
            case 2002:
                Batch();
                break;
        }
        HandSet(data);
    }


    public override void Attack()
    {
        if (!CanAttack()) return;

        if (!player.scanner.nearestTarget)
            return;


        switch (weaponId)
        {
            case 2000:
            case 2003:
                StartCoroutine("Attack2000");
                SetNextAttackTime();
                break;
            case 2001:
                Attack2001();
                SetNextAttackTime();
                break;


        }
    }

    public override void NonTargetAttack()
    {
        if (!CanAttack()) return;

        switch(weaponId)
        {
            case 2002:
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
                break;
        }
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
            Transform bullet = GameManager.instance.weaponPool.Get(prefabId).transform;

            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

            bullet.GetComponent<Bullet>().Init(totaldamage, speed, piercing, dir,weaponLevel);


            if(count > 1)
            {
                yield return new WaitForSeconds(shotDelay);
            }
        }
    }

    private void Attack2001()
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

            Transform bullet = GameManager.instance.weaponPool.Get(prefabId).transform;

            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, spreadDir);

            bullet.GetComponent<Bullet>().Init(
                totaldamage, speed, piercing, spreadDir, weaponLevel);
        }
    }

    public  void Batch()
    {
        

        for (int index = 0; index < count; index++)
        {
            Transform bullet;
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.weaponPool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            //불렛의 로컬 위치, 각도 초기화
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(1, speed, piercing, Vector3.zero, weaponLevel); // -100 is Infinity Per;

        }
    }

    public override void LevelUp()
    {
        weaponLevel++;
        switch (weaponId)
        {
            case 2000:
                attackPower += 3.0f;
                attackRate += 0.1f;
                break;
            case 2001:
                attackPower += 3.0f;
                if (count < 5)
                    count++;
                break;
            case 2002:
                attackPower += 3.0f;
                if (count < 5)
                    count++;
                Batch();
                break;
        }
    }

}
