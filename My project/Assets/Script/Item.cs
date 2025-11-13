using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    public ItemData data;
    public MeleeWeapon meleeWeapon;
    public RangeWeapon rangeWeapon;


    public int level;

    Image icon;
    Text textName;
    Text textFirstDesc;
    Text textDesc;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textName = texts[0];
        textDesc = texts[1];
        

        textName.text = data.itemName;
    }

    private void OnEnable()
    {
        switch(data.itemType)
        {
            case ItemData.ItemType.State:
                textDesc.text = string.Format(data.itemDesc);
                break;
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if(level == 0)
                {
                    textDesc.text = string.Format(data.itemFirstDesc);
                }
                else
                {
                    textDesc.text = string.Format(data.itemDesc);
                }
                    
                break;

        }
    }

    public void OnClick()
    {
        switch(data.itemType)
        {
            case ItemData.ItemType.State:
                Debug.Log("Click State!");

                break;

            case ItemData.ItemType.Melee:

                if(level == 0)
                {
                    GameObject newMeleeWeapon = new GameObject();

                    meleeWeapon = newMeleeWeapon.AddComponent<MeleeWeapon>();
                    meleeWeapon.Init(data);
                }
                else
                {
                    Debug.Log("Melee Level Up!");
                    meleeWeapon.LevelUp();
                }

                level++;

                break;
            case ItemData.ItemType.Range:
                if(level == 0)
                {
                    GameObject newRangeWeapon = new GameObject();

                    rangeWeapon = newRangeWeapon.AddComponent<RangeWeapon>();
                    rangeWeapon.Init(data);
                }
                else
                {
                    Debug.Log("Range Level Up!");
                    rangeWeapon.LevelUp();
                }

                level++;

                break;

               


                
        }
            
    }
}
