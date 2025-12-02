using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    public InfoType type;

    Text myText;
    Slider mySlider;
    PlayerStats stats;


    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
        
    }
    private void Start()
    {
        stats = GameManager.instance.player.GetComponent<PlayerStats>();
    }

    private void LateUpdate()
    {
        switch(type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp;

                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                break;

            case InfoType.Kill:
                break;

            case InfoType.Time:
                break;

            case InfoType.Health:
                float curHealth =  stats.currentHealth;
                float maxhealth = stats.maxHealth;

                mySlider.value = curHealth / maxhealth; 
                break;

        }
    }
}
