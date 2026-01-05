using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player")]
    public Player player;
    public int playerId; //나중에 시작 캐릭터 종류 다양해질때 사용

    [Header("Managers")]
    public PoolManager weaponPool;
    public PoolManager enemyPool;
    public AchiveManager achiveManager;

    [Header("Game Time")]
    public float gameTime;     // 현재 플레이 시간
    public float maxGameTime = 600f; // 예: 10분 생존

    [Header("# Game State")]
    public bool isLive;
    public int equipWeapon;

    [Header("# Progress")]
    public int level;
    public int kill;
    public int exp;
    public int nextExp;
    public int bestScore;
    public int nowScore=0;
    public int EquipWeaponCnt=0;

    [Header("# Game UI")]
    public LevelUp uiLevelUp;
    public GameObject uiGameOver;
    public Transform uiJoy;


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

        nowScore += dropExp;
        exp += dropExp;
        if(exp > nextExp)
        {
            level++;
            //초과 경험치 경우
            int extraExp = exp - nextExp;

            SetNextExp();

            exp = 0;

            exp += extraExp;

            player.GetComponent<PlayerStats>().playerLevel++;
            
            uiLevelUp.Show();
        }
        else if(exp == nextExp)
        {
            level++;
            SetNextExp();

            //정해진 경험치에 맞는 경우
            exp = 0;
            player.GetComponent<PlayerStats>().playerLevel++;
            
            uiLevelUp.Show();
        }


    }
    public void GameStart()
    {
        player.PlayerSpawn();
        player.SetHandActive(true);
        uiJoy.localScale = Vector3.one;
        uiLevelUp.StartWeapon();
        isLive = true;
        Time.timeScale = 1;
    }
    public void GameStop()
    {
        isLive =false;
        Time.timeScale = 0;
        uiJoy.localScale = Vector3.zero;
    }
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
        uiJoy.localScale = Vector3.one;
    }
    public void GameQuit()
    {
        Application.Quit();
    }
    public void GameOver()
    {
        if(nowScore > bestScore)
        {
            achiveManager.SaveBestScore();
        }

        GameStop();
        uiGameOver.SetActive(true);
    }
    public void GameRetry()
    {
        SceneManager.LoadScene("SampleScene");
    }


    void SetNextExp()
    { 
        if (level % 5 == 0)
        {
            nextExp += 15;
        }
        else
        {
            nextExp += 5;
        }
    }
}
