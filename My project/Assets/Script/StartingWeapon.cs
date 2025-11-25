using UnityEditor.Search;
using UnityEngine;

public class StartingWeapon : MonoBehaviour
{
    public GameObject[] items;

    private void OnEnable()
    {
        int random= Random.Range(0, items.Length);

        
        Item startWeapon = items[random].GetComponent<Item>();

        startWeapon.OnClick();
    }
}
