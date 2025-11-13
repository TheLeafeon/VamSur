using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("# Common Weapon Setting")]
    public int weaponId;
    //public int prefabId;
    public string weaponName;
    public float attackPower;
    public float attackRate;
    
    protected float nextAttackTime;
    protected Player player;

    protected PlayerStats playerStats;




    public abstract void Init(ItemData data);
    public abstract void Attack();

    public abstract void LevelUp();
    protected abstract void DealDamage(Enemy target);
    


    protected virtual void Start()
    {
        nextAttackTime = 0.0f;
        playerStats = GameManager.instance.player.GetComponent<PlayerStats>();
    }

    protected bool CanAttack()
    {
        return Time.time > nextAttackTime;
    }

    protected void SetNextAttackTime()
    {
        nextAttackTime = Time.time + 1f / attackRate;
    }

   protected void HandSet(ItemData data)
    {
        player = GameManager.instance.player;

        Hand hand = player.hands[data.isLefthand ? 0 : 1];
        hand.spriter.sprite = data.hand;
    }

    


}
