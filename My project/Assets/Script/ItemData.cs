using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/ItemData")]
public class ItemData : ScriptableObject
{


    public enum ItemType {State, Melee, Range}
    public enum DamageType { Physical, Magical }

    [Header("#Main Info")]
    public ItemType itemType; 
    public int itemId;

    public string itemName;
    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;



    [Header("# Common Weapon Info")]
    public DamageType damageType;
    public float baseAttackPower;
    public float baseAttackRate;
    public LayerMask hitLayer;
    [TextArea]
    public string itemFirstDesc; //무기 첫 습득 시 설명

    [Header("# Weapon Sprite Info")]
    public bool isLefthand;
    public Sprite hand;
    public RuntimeAnimatorController weaponAnimCon;

    [Header("# Melee Weapon Info")]
    public float baseAttackRange; // 공격 범위

    [Header("# Range Weapon Info")]
    public GameObject projectile;
    public float baseSpeed; // 날라가는 속도
    public int baseCount; // 투사체의 수
    public int basePiercing; //관통 수

}
