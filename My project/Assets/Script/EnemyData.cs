using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptble Object/EnemyData")]
public class EnemyData : ScriptableObject
{
    public enum EnemyType {Melee, Range }

    [Header("#Main Info")]
    public int prefabId; // prefabID
    public string enemyName;

    [Header("# Common Enemy Info")]
    public float maxHealth;
    public float speed;
    public float attackPower;
    public int dropExp;

    [Header("# Range Enmey Info")]
    public GameObject projectile;
    public float attackRange; // 공격 인식 범위 , 변수명 수정 가능
    public int count; //투사체 수
    public float attackRate; //공격 속도
    public float projectileSpeed; //투사체 속도



}
