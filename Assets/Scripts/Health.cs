using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public bool isPlayerAttack;
    public float healthCurrent;
    public float healthMax;
    public bool isAttack;
    public Barricade barricade;
    public GameObject endPanel;
    public Text timeText;
    GameObject[] attackSoldier;
    GameObject[] defendSoldier;
    int remTime;
    bool end;
    Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        if (isAttack)
        {
            remTime = (int)healthMax;
            InvokeRepeating("RemaningTime", 0, 1);
        }

        end = false;
        healthCurrent = healthMax;
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = healthCurrent/healthMax;
        if (isAttack)
        {
            healthCurrent = healthCurrent -1 * Time.deltaTime;
        }
        else
        {
            healthCurrent = barricade.health;
        }
        if (healthCurrent <= 0)
        {
            if (!end && isAttack && !isPlayerAttack)
            {
                GameObject.FindWithTag("Canvas").GetComponent<PanelControl>().End();
                end = true;
            }
            else if (!end && !isAttack && isPlayerAttack)
            {
                GameObject.FindWithTag("Canvas").GetComponent<PanelControl>().End();
                end = true;
            }
            GameObject.FindWithTag("Canvas").GetComponent<PanelControl>().Award();

            attackSoldier = GameObject.FindGameObjectsWithTag("AttackSoldier");
            defendSoldier = GameObject.FindGameObjectsWithTag("DefendSoldier");
            for (int i = 0; i < attackSoldier.Length; i++)
            {
                Destroy(attackSoldier[i]);
            }
            for (int i = 0; i < defendSoldier.Length; i++)
            {
                Destroy(defendSoldier[i]);
            }
            endPanel.SetActive(true);
        }
    }

    void RemaningTime()
    {
        timeText.text = remTime.ToString();
        if (remTime > 0)
        {
            remTime -= 1;
        }
    }
}
