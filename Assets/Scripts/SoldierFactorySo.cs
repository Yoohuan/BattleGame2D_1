using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SoldierFactory", menuName = "Factory/SoldierFactory")]
public class SoldierFactorySO : ScriptableObject
{
    [Space(10)]
    [Header("��ұ���1")]
    public SoldierData d_Soldier1;
    [Space(10)]
    [Header("��ұ���2")]
    public SoldierData d_Soldier2;
    [Space(10)]
    [Header("��ұ���3")]
    public SoldierData d_Soldier3;
    [Space(10)]
    [Header("��ұ���4")]
    public SoldierData d_Soldier4;
    [Space(10)]
    [Header("��ұ���5")]
    public SoldierData d_Soldier5;

    [Space(10)]
    [Header("�з�����1")]
    public SoldierData a_Soldier1;
    [Space(10)]
    [Header("�з�����2")]
    public SoldierData a_Soldier2;
    [Space(10)]
    [Header("�з�����3")]
    public SoldierData a_Soldier3;
    [Space(10)]
    [Header("�з�����4")]
    public SoldierData a_Soldier4;
    [Space(10)]
    [Header("�з�����5")]
    public SoldierData a_Soldier5;
    [Space(10)]
    [Header("�з�����6")]
    public SoldierData a_Soldier6;
    [Space(10)]
    [Header("�з�����7")]
    public SoldierData a_Soldier7;



    public void D_CreatSoldier1(Transform[] DefendPos)
    {
        for (int i = 0; i < d_Soldier1.quantity; i++)
        {
            Instantiate(d_Soldier1.prefab, GetRandomPos(DefendPos), Quaternion.identity);
        }
    }

    public void D_CreatSoldier2(Transform[] DefendPos)
    {
        for (int i = 0; i < d_Soldier2.quantity; i++)
        {
            Instantiate(d_Soldier2.prefab, GetRandomPos(DefendPos), Quaternion.identity);
        }
    }

    public void D_CreatSoldier3(Transform[] DefendPos)
    {
        for (int i = 0; i < d_Soldier3.quantity; i++)
        {
            Instantiate(d_Soldier3.prefab, GetRandomPos(DefendPos), Quaternion.identity);
        }
    }

    public void D_CreatSoldier4(Transform[] DefendPos)
    {
        for (int i = 0; i < d_Soldier4.quantity; i++)
        {
            Instantiate(d_Soldier4.prefab, GetRandomPos(DefendPos), Quaternion.identity);
        }
    }

    public void A_CreatSoldier1(Transform[] AttackPos)
    {
        for (int i = 0; i < a_Soldier1.quantity; i++)
        {
            Instantiate(a_Soldier1.prefab, GetRandomPos(AttackPos), Quaternion.identity);
        }
    }

    public void A_CreatSoldier2(Transform[] AttackPos)
    {
        for (int i = 0; i < a_Soldier2.quantity; i++)
        {
            Instantiate(a_Soldier2.prefab, GetRandomPos(AttackPos), Quaternion.identity);
        }
    }

    public void A_CreatSoldier3(Transform[] AttackPos)
    {
        for (int i = 0; i < a_Soldier3.quantity; i++)
        {
            Instantiate(a_Soldier3.prefab, GetRandomPos(AttackPos), Quaternion.identity);
        }
    }

    public void A_CreatSoldier4(Transform[] AttackPos)
    {
        for (int i = 0; i < a_Soldier4.quantity; i++)
        {
            Instantiate(a_Soldier4.prefab, GetRandomPos(AttackPos), Quaternion.identity);
        }
    }



    Vector2 GetRandomPos(Transform[] Pos)
    {
        Vector2 RandomPos = new Vector2(Random.Range(Pos[0].position.x, Pos[1].position.x), Random.Range(Pos[0].position.y, Pos[1].position.y));
        return RandomPos;
    }
}
