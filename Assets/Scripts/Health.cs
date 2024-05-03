using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public float healthCurrent;
    public float healthMax;
    public bool isAttack;
    public Barricade barricade;
    public GameObject endPanel;
    GameObject[] attackSoldier;
    GameObject[] defendSoldier;

    Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
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
}
