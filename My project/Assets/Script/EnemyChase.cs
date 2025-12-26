using UnityEngine;

public class EnemyChase : EnemyBehavior
{
    private void FixedUpdate()
    {
        //Debug.Log("EnemyChase.FixedUpdate isLive: " + enemy.isLive);
        if (!GameManager.instance.isLive)
            return;
        if (!enemy.isLive)
            return;
        if (isKnockback)
            return;

        Vector2 dirVec = (Vector2)player.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * enemy.speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.linearVelocity = Vector2.zero;

    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        if (!enemy.isLive)
            return;
        FlipToPlayer();
    }
}
