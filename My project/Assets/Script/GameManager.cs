using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player")]
    public Player player;
    public int playerId; //나중에 시작 캐릭터 종류 다양해질때 사용

    [Header("Managers")]
    public PoolManager projectilePool;
    public PoolManager enemyPool;
    //public Spawner spawner;

    [Header("Game Time")]
    public float gameTime;     // 현재 플레이 시간
    public float maxGameTime = 600f; // 예: 10분 생존

    [Header("# Game State")]
    public bool isLive;

    [Header("# Progress")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    public float health;
    public float maxHealth=100;

    [Header("# Game UI")]
    public LevelUp uiLevelUp;

    private void Awake()
    {
        instance = this;

        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        // 게임 종료 조건 (원하면 수정 가능)
        if (gameTime >= maxGameTime)
        {
            Debug.Log("Game Clear!!");
        }
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
            level++;
            uiLevelUp.Show();
        }
        else if(exp == nextExp[nowLevel])
        {
            //정해진 경험치에 맞는 경우
            exp = 0;
            player.GetComponent<PlayerStats>().playerLevel++;
            level++;
            uiLevelUp.Show();
        }


    }

}
