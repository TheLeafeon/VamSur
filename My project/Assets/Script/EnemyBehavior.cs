using TMPro.EditorUtilities;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Processors;


public abstract class EnemyBehavior : MonoBehaviour
{
    protected bool isKnockback;

    protected Enemy enemy;
    protected Transform player;
    protected SpriteRenderer spriter;
    protected Rigidbody2D rigid;
    protected Collider2D coll;
    WaitForFixedUpdate wait;
    protected virtual void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        wait = new WaitForFixedUpdate();
    }

    protected virtual void OnEnable()
    {
        coll.enabled = true;
        rigid.simulated = true;
    }
    public virtual void Init(Enemy enemy, Transform palyer)
    {
       
        this.enemy = enemy;
        this.player = palyer;

        Debug.Log("Rigid: " + rigid);
    }

    protected void FlipToPlayer()
    {
        if (player == null) return;
        

        bool isPlayerOnLeft = player.position.x < transform.position.x;
        spriter.flipX = isPlayerOnLeft;
    }

    public virtual void BehaviorDead()
    {
        coll.enabled = false;
        rigid.simulated = false;
    }

    public virtual IEnumerator KnockBack()
    {
        isKnockback = true;

       
        
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;

        rigid.AddForce(dirVec.normalized * 1, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f); ;
        isKnockback = false;
    }
}
