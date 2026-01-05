using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    public ItemData data;
    public Weapon weapon;
    public int level;

    public bool isEquip= false;

    Image icon;
    Text textName;
    Text textFirstDesc;
    Text textDesc;

    Player player;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textName = texts[0];
        textDesc = texts[1];
        

        textName.text = data.itemName;
    }
    private void Start()
    {
        player = GameManager.instance.player;
    }
    private void OnEnable()
    {
        switch(data.itemType)
        {
            case ItemData.ItemType.State:
                textDesc.text = string.Format(data.itemDesc);
                break;
            case ItemData.ItemType.Weapon:
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

    //무기 장착부분
    public void OnClick()
    {
        switch(data.itemType)
        {
            case ItemData.ItemType.State:
                switch(data.stateType)
                {
                    case ItemData.StateType.Strength:
                        player.GetComponent<PlayerStats>().strength++;
                        player.GetComponent<PlayerStats>().MaxHealthUpdate();
                        break;
                    case ItemData.StateType.Agility:
                        player.GetComponent<PlayerStats>().agility++;
                        player.GetComponent<Player>().speedUpdate();
                        break;
                    case ItemData.StateType.Intelligence:
                        player.GetComponent<PlayerStats>().intelligence++;
                        break;
                }
                break;

            case ItemData.ItemType.Weapon:
                if(level == 0)
                {
                    EquipWeapon();
                    weapon.Init(data);
                    isEquip = true;
                    GameManager.instance.EquipWeaponCnt++;

                }
                else
                {
                    weapon.LevelUp();
                }
                level++;

                break;   
        }
            
    }

    private void EquipWeapon()
    {
        GameObject newWeapon = new GameObject();
        GameManager.instance.equipWeapon++;
        switch (data.weaponType)
        {
            case ItemData.WeaponType.Melee:
                weapon = newWeapon.AddComponent<MeleeWeapon>();

                break;
            case ItemData.WeaponType.Range:
                weapon = newWeapon.AddComponent<RangeWeapon>();
                break;
            case ItemData.WeaponType.Explostion:
                weapon = newWeapon.AddComponent<ExplostionWeapon>();
                break;
        }
    }
}
