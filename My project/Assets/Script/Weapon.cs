using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("# Common Weapon Setting")]
    public int weaponId;
    //public int prefabId;
    public string weaponName;
    public float damage;
    public float attackRate;
    
    protected float nextAttackTime;
    public Player player;

    public abstract void Attack();

    public abstract void Init(ItemData data);



    protected virtual void Start()
    {
        nextAttackTime = 0.0f;
    }

    protected bool CanAttack()
    {
        return Time.time > nextAttackTime;
    }

    protected void SetNextAttackTime()
    {
        nextAttackTime = Time.time + 1f / attackRate;
    }

    


}
