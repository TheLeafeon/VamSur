using UnityEngine;

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

    void Next()
    {
        foreach(Item item in items)
        {
            item.gameObject.SetActive(false);  
        }

        //선택지 3개
        int[] random = new int[3];

        while(true)
        {
            //전체 랜덤
            random[0] = Random.Range(0, items.Length);
            random[1] = Random.Range(0, items.Length);
            random[2] = Random.Range(0, items.Length);

            if (random[0] != random[1] && random[1] != random[2] && random[0] != random[2])
                break;
            
        }

        for(int i=0;i<random.Length; i++)
        {
            Item ranItem = items[random[i]];

            ranItem.gameObject.SetActive(true);
        }

    }
}
