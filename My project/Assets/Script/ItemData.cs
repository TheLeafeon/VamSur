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
    public enum WeaponType {Melee, Range, Explostion }
    public enum DamageType { Physical, Magical }

    [Header("#Main Info")]
    public ItemType itemType; 
    public int itemId;

    public string itemName;
    [TextArea]
    public string itemDesc; 
    public Sprite itemIcon; 

    [Header("# State Info")]
    [Tooltip("아이템의 종류")]
    public StateType stateType; 

    [Header("# Common Weapon Info")]
    [Tooltip("무기의 대략적인 공격방식 유형")]
    public WeaponType weaponType;
    [Tooltip("물리데미지? 마법데미지?")]
    public DamageType damageType;
    [Tooltip("기본 공격력")]
    public float baseAttackPower; //무기의 기본 공격력 DamageType과 무관
    [Tooltip("기본 공격속도")]
    public float baseAttackRate; //무기의 기본 공격 속도
    [Tooltip("타격 가능한 레이어")]
    public LayerMask hitLayer;
    [Tooltip("무기 첫 습득 시 설명")]
    [TextArea]
    public string itemFirstDesc; //무기 첫 습득 시 설명

    [Header("# Weapon Sprite Info")]
    [Tooltip("손 이미지")]
    public Sprite hand; //무기를 들고있는 Sprite
    [Tooltip("왼손에 들고있는지?")]
    public bool isLefthand; //무기를 왼손에 드는지 확인
    public RuntimeAnimatorController weaponAnimCon; //공격모션 Anim

    [Header("# Melee Weapon Info")]
    [Tooltip("공격 범위")]
    public float baseAttackRange; // 공격 범위

    [Header("# Range Weapon Info")]
    [Tooltip("투사체 오브젝트 ")]
    public GameObject bullet;
    [Tooltip("투사체가 날아가는 속도")]
    public float baseSpeed; // 날라가는 속도
    [Tooltip("투사체의 갯수 ")]
    public int baseCount; // 투사체의 수
    [Tooltip("투사체의 관통 수 ")]
    public int basePiercing; //관통 수

    [Header("# Area Info")]
    [Tooltip("영역 오브젝트")]
    public GameObject DamageArea; // 데미지 영역 프리팹
    [Tooltip("영역의 크기")]
    public float baseAreaSize; //영역 크기
    [Tooltip("영역의 지속시간")]
    public float baseAreaTime; // 영역의 지속시간

    [Header("# DoT Info")]
    [Tooltip("도트데미지 계산, 데미지 * damagePercent으로 도트데미지가 정해짐")]
    public float damagePercent; // 영역 내에서의 데미지 , attackpower에 얼마만큼 비례하는지
    [Tooltip("도트데미지를 입히는 주기(단위: 초)")]
    public float baseDamageCoolTime; // 영역 내에서 데미지 주기
}
