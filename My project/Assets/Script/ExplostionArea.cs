using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplostionArea : MonoBehaviour
{
    [Header("# Explostion Area Info")]
    [SerializeField]
    float areaSize;
    [SerializeField]
    float damage;
    [SerializeField]
    float areaTime;

    private List<Enemy> enemies = new List<Enemy>();

    private void OnEnable()
    {
        StartCoroutine(AreaLifeTime());
    }

    public void Init(float size, float damage, float areaTime)
    {
        areaSize = size;
        this.damage = damage;
        this.areaTime = areaTime;

        transform.localScale = new Vector3(areaSize, areaSize, areaSize);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        Enemy enemy = collision.GetComponent<Enemy>();

        //enemy.TakeDamage(damage);

        if (!enemies.Contains(enemy))
            enemies.Add(enemy);

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null) // 죽었거나 비활성화 되지 않았다면
            {
                Debug.Log("enemies" + i + "TakeDamage" + damage);
                enemies[i].TakeDamage(damage);
            }
        }
    }

    IEnumerator AreaLifeTime()
    {
        yield return new WaitForSeconds(areaTime);
        gameObject.SetActive(false);
    }
}
