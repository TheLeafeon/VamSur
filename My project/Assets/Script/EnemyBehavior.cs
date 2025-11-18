using TMPro.EditorUtilities;
using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour
{
    protected Enemy enemy;
    protected Transform player;
    protected SpriteRenderer spriter;
    protected Rigidbody2D rigid;
    //protected bool initialized = false;
    public void OnEnable()
    {
        spriter = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        
    }

    public virtual void Init(Enemy enemy, Transform palyer)
    {
        this.enemy = enemy;
        this.player = palyer;
        //initialized = true;
    }

    protected void FlipToPlayer()
    {
        if (player == null) return;

        bool isPlayerOnLeft = player.position.x < transform.position.x;
        spriter.flipX = isPlayerOnLeft;
    }
}
