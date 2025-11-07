using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    


    [Header("# Player Info")]
    public  int playerId; //나중에 시작 캐릭터 종류 다양해질때 사용
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    public float health;
    public float maxHealth=100;


    [Header("# Game Object")]
    public Player player;
    public PoolManager pool;

    private void Awake()
    {
        instance = this;

        Application.targetFrameRate = 60;
    }

    public void GetExp(int dropExp)
    {
        //여러번 쓰일꺼라서 변수 하나 사용
        int nowLevel = player.GetComponent<PlayerStats>().playerLevel;
        exp += dropExp;
        if(exp > nextExp[nowLevel])
        {
            //초과 경험치 경우
            int extraExp = exp - nextExp[nowLevel];

            exp = 0;

            exp += extraExp;

            player.GetComponent<PlayerStats>().playerLevel++;
            Debug.Log("레벨업!");
        }
        else if(exp == nextExp[nowLevel])
        {
            //정해진 경험치에 맞는 경우
            exp = 0;
            player.GetComponent<PlayerStats>().playerLevel++;
            Debug.Log("레벨업!");
        }


    }

}
