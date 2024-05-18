using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoldierName : MonoBehaviour
{
    public bool isPlayer;
    public bool isAttack;
    public string playerName;
    public string[] defendName = { "����", "����̨", "��Ϧ����", "�㻱", "����" };
    public string[] attackName = { "���Ϻ���", "����", "��ī��", "��ɽ��", "��" };
    // Start is called before the first frame update
    void Start()
    {
        if (isPlayer)
        {
            GetComponent<TextMeshPro>().text = playerName;
        }
        else if (isAttack)
        {
            GetComponent<TextMeshPro>().text = attackName[Random.Range(0, 4)];
        }
        else
        {
            GetComponent<TextMeshPro>().text = defendName[Random.Range(0, 4)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.parent.GetComponent<Soldier>().SoldierState == Soldier.State.s_Die)
        {
            Destroy(gameObject);
        }
    }
}
