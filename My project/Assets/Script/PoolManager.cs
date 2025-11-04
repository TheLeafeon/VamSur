using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리팹을 보관할 변수

    public GameObject[] prefabs;

    // 풀을 담당하는 리스트

    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }

        Debug.Log(pools.Length);
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // 그때 새롭게 생성하여 select에 할당

        foreach (GameObject item in pools[index])
        {
            //선택한 풀의 놀고있는(비활성화) 게임 오브젝트 접근
            if (!item.activeSelf)
            {
                //발견하면 select변수에 할당
                select = item;

                select.SetActive(true);
                break;
            }
        }

        //이미 모두 사용하고있다면 or 못찾았다면
        if (select == null)
        {
            //Instantiate: 원본 오브젝를 복제하여 장면에 생성하는 함수
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}

