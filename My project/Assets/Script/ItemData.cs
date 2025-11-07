using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/ItemData")]
public class ItemData : ScriptableObject
{


    public enum ItemType {Melee, Range, Armor, Shoe, Heal }

    [Header("#Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Common Weapon Info")]
    public float baseAttackPower;
    public float baseAttackRate;
    public LayerMask hitLayer;

    [Header("# Melee Weapon Info")]
    public float baseAttackRange;

    [Header("# Range Weapon Info")]
    public float baseSpeed;
    public int baseCount;
    public int basePiercing;

    [Header("# Weapon")]
    public GameObject projectile;
    public Sprite hand;
    public RuntimeAnimatorController weaponAnimCon;

    //없어질 수도 있음
    [Header("# Level Data")]
    public float[] damages;
    public float[] attackRate;




}
