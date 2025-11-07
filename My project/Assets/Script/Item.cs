using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    public ItemData data;
    public MeleeWeapon meleeWeapon;
    public RangeWeapon rangeWeapon;

    public int level;


    public void OnClick()
    {
        switch(data.itemType)
        {
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
                }

                level++;

                break;

               


                
        }
            
    }
}
