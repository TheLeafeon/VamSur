using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public WaveData[] waveData;


    //한 레벨의 시간.
    public float levelTime;

    float timer;
    int level;
    int spawnEnemy; // 웨이브의 어떤 Enemy를 뽑을것인가

    private void Start()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        levelTime = GameManager.instance.maxGameTime / waveData.Length; // 각 레벨마다의 시간을 waveData의 갯수만큼 나눠서 각 레벨의 시간을 균등하게 나누겠다.
    }

    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
        //level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime))
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), waveData.Length - 1);

        if (timer > waveData[level].spawnInterval)
        {
            Spawn();
            timer = 0;
        }
    }

    void Spawn()
    {
        //  가중치에 따라 몇번째꺼를 뽑을지를 찾아야함
        //Random.Range(1, 101); 랜덤 뽑기 1부터 100까지
        //예를 들어서 어떤 웨이브는 100, 어떤 웨이브는 60 40 어떤 웨이브는 60 20 20 일경우 

        GameObject enemy= GameManager.instance.enemyPool.Get(SpawnEnemyChoice());

        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;

        enemy.GetComponent<Enemy>().OnSpawn();
    }

    int SpawnEnemyChoice()
    {
        //웨이브에 몬스터 하나만 있는 경우
        if (waveData[level].enemies.Length == 1)
        {
            //return waveData[level].enemies[0].enemyPrefab.GetComponent<Enemy>().data.prefabId;
            return waveData[level].enemies[0].enemyData.prefabId;
        }

        int ran = Random.Range(1, 101); //랜덤 뽑기 1부터 100까지
        int count = 0;
        for (int i = 0; i< waveData[level].enemies.Length;i++)
        {
            count += waveData[level].enemies[i].weight;
            if (ran < count)
            {
                return waveData[level].enemies[i].enemyData.prefabId;
            }
        }

        return waveData[level].enemies[0].enemyData.prefabId;
    }

}