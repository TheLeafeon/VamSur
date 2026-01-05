using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    Text myText;

    private void Awake()
    {
        myText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        myText.text = string.Format("³» Á¡¼ö\n{0}", GameManager.instance.nowScore);
    }
}
