using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine.Unity;

public class PanelControl : MonoBehaviour
{

    public GameObject leaderboard;
    public GameObject skill;
    public Text scoreUI;
    public Transform skillPos;
    public float skillCD;

    int score = 0;
    float useCD;
    bool canUse;

    private void Start()
    {
        useCD = skillCD;
        canUse = true;
    }

    private void Update()
    {

        RefreshCD();
        scoreUI.text = score.ToString();
    }

    public void OpenLeaderBoard()
    {
        Time.timeScale = 0f;
        leaderboard.SetActive(true);
    }

    public void CloseLeaderBoard()
    {
        Time.timeScale = 1f;
        leaderboard.SetActive(false);
    }

    public void UseSkill()
    {
        if (canUse)
        {
            Instantiate(skill, skillPos.position, Quaternion.identity);
            canUse = false;
        }

    }

    public void ReturnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        
    } 

    

    void RefreshCD()
    {
        if (!canUse)
        {
            if (useCD <= 0)
            {
                canUse = true;
                useCD = skillCD;
                return;
            }
            useCD -= Time.deltaTime;
        }
    }

    public void AddScore() 
    {
        score += 10;
    }


}
