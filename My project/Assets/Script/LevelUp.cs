using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Progress;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    public Item[] items;
    public Item[] startWeapons;
    
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Debug.Log("Level Up UI Open");

        //배경 띄우기
        rect.localScale = Vector3.one;

        //선택지 버튼들 띄우기
        Next();



    }

    public void StartWeapon()
    {
        int random = Random.Range(0, startWeapons.Length);
        startWeapons[random].OnClick();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
    }

    //레벨업 선택지
    void Next()
    {
        //초기화
        foreach(Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        List<Item> candidates = new List<Item>();
        
        foreach(Item item in items)
        {
            //스텟은 무조건 등장
            if(item.data.itemType  == ItemData.ItemType.State)
            {
                candidates.Add(item);
            }
            //무기는 장착하고 있는 무기가 4개 이상일 경우 장착하지 않은 무기는 등장하지 않음
            else if(item.data.itemType == ItemData.ItemType.Weapon)
            {
                //장착 무기가 4개 미만인가
                if(GameManager.instance.EquipWeaponCnt < 4)
                {
                    candidates.Add(item);
                }
                else if(item.isEquip)
                {
                    candidates.Add(item);
                }
            }
        }

        for(int i = 0; i<3;i++)
        {
            int random = Random.Range(0, candidates.Count);

            Item selected = candidates[random];

            selected.gameObject.SetActive(true);
            candidates.RemoveAt(random); // 중복 방지
        }
        

    }
}

