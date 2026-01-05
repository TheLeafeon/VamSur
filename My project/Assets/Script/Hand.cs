using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool leftHand;
    public bool isAttacking;
    public bool isEquip;
    public SpriteRenderer spriter;
    public Animator anim;

    Vector3 leftPos = new Vector3(-0.4f, -0.2f, 0);
    Vector3 leftPosReverse = new Vector3(0.4f, -0.2f, 0);

    Vector3 rightPos = new Vector3(0.4f, -0.2f, 0);
    Vector3 rightPosReverse = new Vector3(-0.4f, -0.2f, 0);


    SpriteRenderer player;

    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
        anim = GetComponent<Animator>();
        isEquip = false;
    }

    
    private void LateUpdate()
    {
        if (isAttacking)
            return;

        bool isReverse = player.flipX;

        if (leftHand)
        {
            transform.localPosition = isReverse ? leftPosReverse : leftPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        else
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }

    }

    
    public void AttackAnimTrigger()
    {
        isAttacking = true;

        if (player.flipX)
        {
            //왼쪽을 공격
            Debug.Log("Left Attack");
            anim.SetTrigger("LeftAttack");
        }
        else
        {
            //오른쪽을 공격
            Debug.Log("Right Attack");
            anim.SetTrigger("RightAttack");
        }
            
        
    }

    public void AttackEnd()
    {
        isAttacking=false;
    }

}
