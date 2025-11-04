using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    public ItemData data;
    public MeleeWeapon meleeWeapon;
    public RangeWeapon rangeWeapon;


    public void OnClick()
    {
        switch(data.itemType)
        {
            case ItemData.ItemType.Melee:
                GameObject newMeleeWeapon = new GameObject();

                meleeWeapon = newMeleeWeapon.AddComponent<MeleeWeapon>();
                meleeWeapon.Init(data);
                break;
            case ItemData.ItemType.Range:
                GameObject newRangeWeapon = new GameObject();

                rangeWeapon = newRangeWeapon.AddComponent<RangeWeapon>();
                rangeWeapon.Init(data);
                break;

               


                
        }
            
    }
}
