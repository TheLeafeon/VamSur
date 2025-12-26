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

    HashSet<Enemy> hitEnemies = new HashSet<Enemy>();

    private void OnEnable()
    {
        hitEnemies.Clear();
    }

    public void Init(float size, float damage, float areaTime)
    {
        areaSize = size;
        this.damage = damage;
        this.areaTime = areaTime;

        transform.localScale = Vector3.one * areaSize;
        StartCoroutine(AreaLifeTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy == null || !enemy.isLive)
            return;

        if (hitEnemies.Add(enemy))
        {
            enemy.TakeDamage(damage, true);
        }
    }

    IEnumerator AreaLifeTime()
    {
        yield return new WaitForSeconds(areaTime);
        hitEnemies.Clear();
        gameObject.SetActive(false);
    }
}
