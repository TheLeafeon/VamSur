using NUnit.Framework.Interfaces;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class ExplostionWeapon : Weapon
{
    [Header("# Explostion Setting")]
    private float areaSize;
    private float areaTime;
    private int prefabId;


    public override void Init(ItemData data)
    {
        Debug.Log("범위공격 무기 장착");
        player = GameManager.instance.player;
        name = "Weapon" + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        weaponId = data.itemId;
        weaponLevel = 1;
        attackRate = data.baseAttackRate;
        attackPower = data.baseAttackPower;
        hitLayer = data.hitLayer;
        areaSize = data.baseAreaSize;
        areaTime = data.baseAreaTime;

        switch (data.damageType)
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
            if (data.DamageArea == GameManager.instance.weaponPool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        HandSet(data);
    }

    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        Attack();
        
    }

    public override void Attack()
    {
        if (!CanAttack()) return;

        if (!player.scanner.nearestTarget)
            return;

        switch(weaponId)
        {
            case 3000:
                Attack3000();
                SetNextAttackTime();
                break;
        }
        
        
    }

    public override void NonTargetAttack()
    {

    }
    public override void LevelUp()
    {
        
    }
    protected override void DealDamage(Enemy target)
    {
        
    }

    void Attack3000()
    {
        Transform damageArea = GameManager.instance.weaponPool.Get(prefabId).transform;
        damageArea.position = player.scanner.nearestTarget.position;

        float totaldamage = attackPower + (isPhysical ? playerStats.strength : playerStats.intelligence);

        damageArea.GetComponent<ExplostionArea>().Init(areaSize, totaldamage, areaTime );

        //damageArea.GetComponent<ExplostionArea>().Init();
    }
}
