using UnityEngine;
using UnityEngine.UI;

public class StateUI : MonoBehaviour
{
    public enum InfoType { Level,
                           Health,
                           Strength,
                           Agility,
                           Intelligence
                         }

    public InfoType type;

    Text myText;

    private void Awake()
    {
        myText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        switch(type)
        {
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.instance.player.GetComponent<PlayerStats>().playerLevel);
                break;
            case InfoType.Health:
                myText.text = string.Format("{0:F0}", GameManager.instance.player.GetComponent<PlayerStats>().maxHealth);
                break;
            case InfoType.Strength:
                myText.text = string.Format("{0:F0}", GameManager.instance.player.GetComponent<PlayerStats>().strength);
                break;
            case InfoType.Agility:
                myText.text = string.Format("{0:F0}", GameManager.instance.player.GetComponent<PlayerStats>().agility);
                break;
            case InfoType.Intelligence:
                myText.text = string.Format("{0:F0}", GameManager.instance.player.GetComponent<PlayerStats>().intelligence);
                break;
        }
    }

}
