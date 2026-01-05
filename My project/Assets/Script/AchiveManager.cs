using System;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    WaitForSecondsRealtime wait;

    public GameObject uiBestSocre;
    bool isBestScore = false;

    private void Awake()
    {

        wait = new WaitForSecondsRealtime(5);

        //Mydata 라는 키가 존재하지 않는다면
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            //초기화
            PlayerPrefs.SetInt("BestScore", 0);
        }
        
    }

    private void Start()
    {
        LoadBestScore();
    }

    private void LateUpdate()
    {
        if (GameManager.instance.nowScore > GameManager.instance.bestScore)
        {
            //최고 기록 경신 UI 1회 호출
            //게임 중에 1회만 호출되야함
            CheckBestScore();
        }
    }

    public void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", GameManager.instance.nowScore);
    }

    void LoadBestScore()
    {
        GameManager.instance.bestScore = PlayerPrefs.GetInt("BestScore");
    }

    void CheckBestScore()
    {
        if (isBestScore)
            return;

        if(GameManager.instance.nowScore > GameManager.instance.bestScore )
        {
            isBestScore = true;
            StartCoroutine(NoticeRoutine());
        }

    }

    IEnumerator NoticeRoutine()
    {
        uiBestSocre.SetActive(true);

        yield return wait;

        uiBestSocre.SetActive(false);
    }
}
