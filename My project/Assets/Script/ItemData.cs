using UnityEditor.Animations;
using UnityEngine;
/*
 * ItemType에 관하여
 * 1. State: 플레이어의 스텟
 * 2. Weapon: 무기
 * 
 * 
 * DamageType에 관하여
 * 1. ItemType이 State가 아닌 경우에만 사용
 * 2. 무기의 데미지가 어느 스텟을 영향받느냐에 따라 Physical 또는 Magical을 사용
 * 
 */
[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/ItemData")]
public class ItemData : ScriptableObject
{


    public enum ItemType {State, Weapon}
    public enum StateType { Strength, Agility, Intelligence }
    public enum WeaponType {Melee, Range }
    public enum DamageType { Physical, Magical }

    [Header("#Main Info")]
    public ItemType itemType; 
    public int itemId;

    public string itemName;
    [TextArea]
    public string itemDesc; 
    public Sprite itemIcon; 

    [Header("# State Info")]
    public StateType stateType; 

    [Header("# Common Weapon Info")]
    public WeaponType weaponType;
    public DamageType damageType;
    public float baseAttackPower; //무기의 기본 공격력 DamageType과 무관
    public float baseAttackRate; //무기의 기본 공격 속도
    public LayerMask hitLayer;
    [TextArea]
    public string itemFirstDesc; //무기 첫 습득 시 설명

    [Header("# Weapon Sprite Info")]
    public bool isLefthand; //무기를 왼손에 드는지 확인
    public Sprite hand; //무기를 들고있는 Sprite
    public RuntimeAnimatorController weaponAnimCon; //공격모션 Anim

    [Header("# Melee Weapon Info")]
    public float baseAttackRange; // 공격 범위

    [Header("# Range Weapon Info")]
    public GameObject bullet;
    public float baseSpeed; // 날라가는 속도
    public int baseCount; // 투사체의 수
    public int basePiercing; //관통 수

    [Header("# DoT Weapon Info")]
    public GameObject DamageArea; // 데미지 영역 프리팹
    public float baseAreaSize; //영역 크기
    public float baseAreaTime; // 영역의 지속시간
    public float baseAreaDamage; // 영역 내에서의 데미지 , attackpower에 얼마만큼 비례하는지
    public float baseDamageCoolTime; // 영역 내에서 데미지 주기
}
