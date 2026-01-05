using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

/*
 * 캐릭터의 이동,  애니메이션, 손
 */
[Tooltip("캐릭터의 이동, 애니메이션, 손")]
public class Player : MonoBehaviour
{

    [Header("# 이동 관련")]
    public float speed; //이동속도
    public Vector2 inputVec;

    [Header("# Hand 오브젝트 ")]
    public Hand[] hands;

    [Header("# 몬스터 인식 범위 ")]
    public Scanner scanner;


    Rigidbody2D rigid;
    SpriteRenderer spriter;
    PlayerStats stats;
    PlayerAnimation anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
        stats = GetComponent<PlayerStats>();
        anim = GetComponent<PlayerAnimation>();
    }

    void OnMove(InputValue value)
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        inputVec = value.Get<Vector2>();

    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        Vector2 nextVec = inputVec * Time.fixedDeltaTime * speed;
        //3. 위치 이동
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        anim.SetFloat("Speed", inputVec.magnitude);


        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;

        }
    }

    public void speedUpdate()
    {
        //speed = Mathf.Clamp((speed + (stats.agility / 10)), 0, 10);

        speed += 0.1f;

        if (speed > 10.0f)
            speed = 10.0f;

        Debug.Log("변경된 스피드" +speed);
    }


    public void PlayerSpawn()
    {
        gameObject.SetActive(true);
        anim.SetBool("Dead", false);
    }

    public void SetHandActive(bool isActive)
    {
        if (hands == null)
            return;

        for (int i = 0; i < hands.Length; i++)
        {
            if (hands[i] != null)
                hands[i].gameObject.SetActive(isActive);
        }
    }
}
