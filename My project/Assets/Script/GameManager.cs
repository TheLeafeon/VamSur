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


}
