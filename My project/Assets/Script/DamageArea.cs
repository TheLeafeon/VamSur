using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageArea : MonoBehaviour
{
    [Header("# DoT Info")]
    [SerializeField]
    float areaSize;
    [SerializeField]
    float damage;
    [SerializeField]
    float areaTime;
    [SerializeField]
    float damageCoolTime;


    private float time;

    private List<Enemy> enemies = new List<Enemy>();





    private void OnEnable()
    {
        StartCoroutine(DoTDamage());
        StartCoroutine(AreaLifeTime());
    }

    public void Init(ItemData itemData, float damage, int level)
    {
        areaSize = itemData.baseAreaSize + (float)level / 10.0f;
        this.damage = damage * itemData.baseAreaDamage;
        areaTime = itemData.baseAreaTime;
        damageCoolTime = itemData.baseDamageCoolTime;

        transform.localScale = new Vector3(areaSize, areaSize, areaSize);
    }

    IEnumerator DoTDamage()
    {
        while (true)
        {
            // 영역 안에 있는 몬스터들에게 데미지
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null) // 죽었거나 비활성화 되지 않았다면
                {
                    Debug.Log("enemies" + i + "TakeDamage" + damage);
                    enemies[i].TakeDamage(damage);
                }
            }

            yield return new WaitForSeconds(damageCoolTime);
        }
    }

    IEnumerator AreaLifeTime()
    {
        yield return new WaitForSeconds(areaTime);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        Enemy enemy = collision.GetComponent<Enemy>();
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemies.Contains(enemy))
            enemies.Remove(enemy);
    }

}
