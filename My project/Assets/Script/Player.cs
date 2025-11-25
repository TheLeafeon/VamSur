using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed; //기본 이동속도
    public Hand[] hands;
    public Scanner scanner;


    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    PlayerStats stats;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        scanner = GetComponent<Scanner>();
        anim = GetComponent<Animator>();
        hands = GetComponentsInChildren<Hand>(true);
        stats = GetComponent<PlayerStats>();
    }

    void OnMove(InputValue value)
    {

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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Enemy"))
            return;

        stats.TakeDamage(collision.gameObject.GetComponent<Enemy>().attackPower);

       

    }

    public void speedUpdate()
    {


        //speed = Mathf.Clamp((speed + (stats.agility / 10)), 0, 10);

        speed += 0.1f;

        if (speed > 10.0f)
            speed = 10.0f;


         

        Debug.Log("변경된 스피드" +speed);
    }

}
