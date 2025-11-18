using JetBrains.Annotations;
using UnityEngine;


[System.Serializable]
public class SpawnEntry
{
    // public GameObject enemyPrefab;
    public EnemyData enemyData;
    public int weight;
}
[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptble Object/WaveData")]
public class WaveData : ScriptableObject
{
    public float spawnInterval; // 웨이브의 스폰 간격
    public SpawnEntry[] enemies; // 웨이브마다 등장하는 몬스터의 리스트
}
