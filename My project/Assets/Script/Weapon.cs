using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("# Common Weapon Info")]
    public string weaponName;
    public int weaponId;
    public int weaponLevel;

    [Header("# Common Weapon Setting")]
    public bool isPhysical;
    public float attackPower;
    public float attackRate;
    [SerializeField]
    protected float nextAttackTime;

    protected Player player;
    protected LayerMask hitLayer;
    protected PlayerStats playerStats;


    public abstract void Init(ItemData data);
    public abstract void Attack();
    public abstract void NonTargetAttack();
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
        float totalAttackRate = attackRate + ((float)GameManager.instance.player.GetComponent<PlayerStats>().agility / 10.0f);
        nextAttackTime = Time.time + 1f / totalAttackRate;
    }

   protected void HandSet(ItemData data)
    {
        player = GameManager.instance.player;

        Hand hand = player.hands[data.isLefthand ? 0 : 1];
        hand.spriter.sprite = data.hand;
    }

    


}
