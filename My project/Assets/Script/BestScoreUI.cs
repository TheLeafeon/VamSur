using UnityEngine;
using UnityEngine.UI;

public class BestScoreUI : MonoBehaviour
{
    Text myText;

    private void Awake()
    {
        myText = GetComponent<Text>();
    }

    private void Start()
    {
        if(GameManager.instance.bestScore > 0)
        {
            myText.text = string.Format("최고기록: {0}", GameManager.instance.bestScore);
        }
        else
        {
            myText.text = string.Format("최고기록: X ");
        }
    }
}
