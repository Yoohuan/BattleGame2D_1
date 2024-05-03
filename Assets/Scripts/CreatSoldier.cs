using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatSoldier : MonoBehaviour
{
    public bool isPlayerAttack;
    public Transform[] DefendPos;
    public Transform[] AttackPos;

    public SoldierFactorySO soldierFactory;

    List<Soldier> attackList = new List<Soldier>();
    List<Soldier> defendList = new List<Soldier>();
    Soldier[] attackSoldierType;
    Soldier[] defendSoldierType;

    public float baseCreatCD;
    public float playerCreatCD;
    public int attackLimit;
    public int defendLimit;
    float playerCD;
    float creatCD;
    bool canCreat;

    public GameObject soldier1Panel, soldier2Panel, soldier3Panel, soldier4Panel;
    public SkeletonGraphic soldier1Anim, soldier2Anim, soldier3Anim, soldier4Anim;
    public AnimationReferenceAsset anim1, anim2, anim3, anim4;
    private string currentState;
    private void Start()
    {
        attackSoldierType = new Soldier[]
            {soldierFactory.a_Soldier1.prefab,soldierFactory.a_Soldier2.prefab,soldierFactory.a_Soldier3.prefab,soldierFactory.a_Soldier4.prefab,
             soldierFactory.a_Soldier5.prefab,soldierFactory.a_Soldier6.prefab,soldierFactory.a_Soldier7.prefab};
        defendSoldierType = new Soldier[]
            {soldierFactory.d_Soldier1.prefab,soldierFactory.d_Soldier2.prefab,
             soldierFactory.d_Soldier3.prefab,soldierFactory.d_Soldier4.prefab,soldierFactory.d_Soldier5.prefab};
        creatCD = baseCreatCD;
        playerCD = playerCreatCD;
        canCreat = true;
    }

    private void Update()
    {
        AnimControl();
        BaseCreat();
        RefreshCD();
    }


    public void CreatSoldier1()
    {
        if (canCreat && isPlayerAttack)
        {
            soldierFactory.A_CreatSoldier1(AttackPos);
            canCreat = false;
            soldier1Panel.SetActive(true);
            AnimationSet(soldier1Anim, anim1, false, 1f);
            GetAnimation(soldier1Anim);
        }
        else if (canCreat && !isPlayerAttack)
        {
            soldierFactory.D_CreatSoldier1(DefendPos);
            canCreat = false;
            soldier1Panel.SetActive(true);
            AnimationSet(soldier1Anim, anim1, false, 1f);
            GetAnimation(soldier1Anim);
        }
    }

    public void CreatSoldier2()
    {
        if (canCreat && isPlayerAttack)
        {
            soldierFactory.A_CreatSoldier2(AttackPos);
            canCreat = false;
            soldier2Panel.SetActive(true);
            AnimationSet(soldier2Anim, anim2, false, 1f);
            GetAnimation(soldier2Anim);
        }
        else if (canCreat && !isPlayerAttack)
        {
            soldierFactory.D_CreatSoldier2(DefendPos);
            canCreat = false;
            soldier2Panel.SetActive(true);
            AnimationSet(soldier2Anim, anim2, false, 1f);
            GetAnimation(soldier2Anim);
        }
    }

    public void CreatSoldier3()
    {
        if (canCreat && isPlayerAttack)
        {
            soldierFactory.A_CreatSoldier3(AttackPos);
            canCreat = false;
            soldier3Panel.SetActive(true);
            AnimationSet(soldier3Anim, anim3, false, 1f);
            GetAnimation(soldier3Anim);
        }
        else if (canCreat && !isPlayerAttack)
        {
            soldierFactory.D_CreatSoldier3(DefendPos);
            canCreat = false;
            soldier3Panel.SetActive(true);
            AnimationSet(soldier3Anim, anim3, false, 1f);
            GetAnimation(soldier3Anim);
        }
    }

    public void CreatSoldier4()
    {
        if (canCreat && isPlayerAttack)
        {
            soldierFactory.A_CreatSoldier4(AttackPos);
            canCreat = false;
            soldier4Panel.SetActive(true);
            AnimationSet(soldier4Anim, anim4, false, 1f);
            GetAnimation(soldier4Anim);
        }
        else if (canCreat && !isPlayerAttack)
        {
            soldierFactory.D_CreatSoldier4(DefendPos);
            canCreat = false;
            soldier4Panel.SetActive(true);
            AnimationSet(soldier4Anim, anim4, false, 1f);
            GetAnimation(soldier4Anim);
        }
    }

    void AnimControl()
    {
        if (soldier1Anim.AnimationState.GetCurrent(0).IsComplete)
        {
            soldier1Panel.SetActive(false);
        }
        if (soldier2Anim.AnimationState.GetCurrent(0).IsComplete)
        {
            soldier2Panel.SetActive(false);
        }
        if (soldier3Anim.AnimationState.GetCurrent(0).IsComplete)
        {
            soldier3Panel.SetActive(false);
        }
        if (soldier4Anim.AnimationState.GetCurrent(0).IsComplete)
        {
            soldier4Panel.SetActive(false);
        }
    }

    void BaseCreat()
    {
        Test();
        if (!isPlayerAttack)
        {
            if (attackList.Count > attackLimit)
            {
                return;
            }
        }
        else
        {
            if (defendList.Count > defendLimit)
            {
                return;
            }
        }
        
        if (creatCD == baseCreatCD)
        {
            if (!isPlayerAttack)
            {
                for (int i = 0; i < 6; i++)
                {
                    attackList.Add(Instantiate(attackSoldierType[Random.Range(0, 6)], GetRandomPos(AttackPos), Quaternion.identity));
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    defendList.Add(Instantiate(defendSoldierType[Random.Range(0, 4)], GetRandomPos(DefendPos), Quaternion.identity));
                }
            }

            creatCD -= Time.deltaTime;
        }
        else
        {
            creatCD -= Time.deltaTime;
            if (creatCD <= 0)
            {
                creatCD = baseCreatCD;
            } 
        }
    }

    void RefreshCD()
    {
        if (!canCreat)
        {
            if (!isPlayerAttack && playerCD <= 0 && defendList.Count < defendLimit)
            {
                canCreat = true;
                playerCD = playerCreatCD;
                return;
            }
            else if (isPlayerAttack && playerCD <= 0 && attackList.Count < attackLimit)
            {
                canCreat = true;
                playerCD = playerCreatCD;
                return;
            }
            playerCD -= Time.deltaTime;
        }
    }

    void Test()
    {
        for ( int i = 0;i < attackList.Count;i++)
        {
            if (attackList[i] == null)
            {
                attackList.RemoveAt(i);
            }
        }
        for (int i = 0; i < defendList.Count; i++)
        {
            if (defendList[i] == null)
            {
                defendList.RemoveAt(i);
            }
        }
    }
    Vector2 GetRandomPos(Transform[] Pos)
    {
        Vector2 RandomPos = new Vector2(Random.Range(Pos[0].position.x, Pos[1].position.x), Random.Range(Pos[0].position.y, Pos[1].position.y));
        return RandomPos;
    }

    void GetAnimation(SkeletonGraphic skeletonAnimation)
    {
        //��ȡ���ڲ��ŵĶ���
        currentState = skeletonAnimation.initialSkinName;
    }
    void AnimationSet(SkeletonGraphic skeletonAnimation, AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentState))
        {
            return;
        }
        skeletonAnimation.AnimationState.SetAnimation(0, animation, loop).TimeScale = timeScale;
    }

}
