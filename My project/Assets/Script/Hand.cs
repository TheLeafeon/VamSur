using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    Vector3 leftPos = new Vector3(-0.4f, -0.2f, 0);
    Vector3 leftPosReverse = new Vector3(0.4f, -0.2f, 0);

    Vector3 rightPos = new Vector3(0.4f, -0.2f, 0);
    Vector3 rightPosReverse = new Vector3(-0.4f, -0.2f, 0);


    SpriteRenderer player;

    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate()
    {
        bool isReverse = player.flipX;

        if(isLeft)
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
}
