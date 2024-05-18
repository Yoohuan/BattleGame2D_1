using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine.Unity;
using System;
using Unity.VisualScripting;

public class PanelControl : MonoBehaviour
{
    public bool isPlayerAttack;
    public GameObject leaderboard;
    public GameObject[] DefendButton;
    public GameObject[] AttackButton;
    public GameObject defendSkill;
    public GameObject attackSkill;
    public Text scoreUI;
    public Text defendAwardUI;
    public Text attackAwardUI;
    public Transform defendSkillPos;
    public Transform attackSkillPos;
    public float skillCD;

    int score = 0;
    float useCD;
    bool canUse;
    bool hasUseAward;
    bool hasChoose;

    private void Start()
    {
        hasChoose = false;
        useCD = skillCD;
        canUse = true;
    }

    private void Update()
    {

        if (!hasChoose)
        {
            score = 0;  
        }
        RefreshCD();
        scoreUI.text = score.ToString();
        attackAwardUI.text = PlayerPrefs.GetInt("AttackAward", 0).ToString();
        defendAwardUI.text = PlayerPrefs.GetInt("DefendAward", 0).ToString();

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

    public void UseDefendSkill()
    {
        if (canUse)
        {
            Instantiate(defendSkill, defendSkillPos.position, Quaternion.identity);
            canUse = false;
        }

    }

    public void UseAttackSkill()
    {
        if (canUse)
        {
            Instantiate(attackSkill, attackSkillPos.position, Quaternion.identity);
            canUse = false;
        }

    }

    public void ReStart()
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

    public void AddScore1()
    {
        score += 1;
    }

    public void AddScore2() 
    {
        score += 2;
    }

    public void End()
    {

        if (GameObject.FindWithTag("Canvas").GetComponent<CreatSoldier>().isPlayerAttack && hasChoose)
        {
            PlayerPrefs.SetInt("AttackAward", PlayerPrefs.GetInt("AttackAward", 0) + 1);
        }
        else if (hasChoose)
        {
            PlayerPrefs.SetInt("DefendAward", PlayerPrefs.GetInt("DefendAward", 0) + 1);
        }
    }

    public void Award()
    {
        if (hasUseAward)
        {
            score += (int)(score * 0.1);
        }
    }

    public void UseAward()
    {
        if (!hasChoose)
        {
            return;
        }
        if (GameObject.FindWithTag("Canvas").GetComponent<CreatSoldier>().isPlayerAttack)
        {
            if (!hasUseAward && PlayerPrefs.GetInt("AttackAward", 0) > 0)
            {
                PlayerPrefs.SetInt("AttackAward", PlayerPrefs.GetInt("AttackAward", 0) - 1);
                hasUseAward = true;
            }
        }
        else
        {
            if (!hasUseAward && PlayerPrefs.GetInt("DefendAward", 0) > 0)
            {
                PlayerPrefs.SetInt("DefendAward", PlayerPrefs.GetInt("DefendAward", 0) - 1);
                hasUseAward = true;
            }
        }
    }

    public void AddDefend()
    {
        isPlayerAttack = false;
        GameObject.FindWithTag("Canvas").GetComponent<CreatSoldier>().isPlayerAttack = false;
        GameObject.FindWithTag("AttackHealth").GetComponent<Health>().isPlayerAttack = false;
        GameObject.FindWithTag("DefendHealth").GetComponent<Health>().isPlayerAttack = false;
        for (int i = 0; i < DefendButton.Length; i++)
        {
            AttackButton[i].SetActive(false);
            DefendButton[i].SetActive(true);
        }
        hasChoose = true;
    }

    public void AddAttack()
    {
        isPlayerAttack = true;
        GameObject.FindWithTag("Canvas").GetComponent<CreatSoldier>().isPlayerAttack = true;
        GameObject.FindWithTag("AttackHealth").GetComponent<Health>().isPlayerAttack = true;
        GameObject.FindWithTag("DefendHealth").GetComponent<Health>().isPlayerAttack = true;
        for (int i = 0; i < AttackButton.Length; i++)
        {
            DefendButton[i].SetActive(false);
            AttackButton[i].SetActive(true);
        }
        hasChoose = true;
    }
}
