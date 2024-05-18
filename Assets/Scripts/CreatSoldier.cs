using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public float defendUpdateMul;
    public float attackUpdateMul;
    public float baseCreatCD;
    public float playerCreatCD;
    public int attackLimit;
    public int defendLimit;

    int soldier1Level;
    int soldier2Level;
    int soldier3Level;
    int soldier4Level;
    bool hasDefend1;
    bool hasDefend2;
    bool hasDefend3;
    bool hasDefend4;
    float playerCD;
    float creatCD;
    bool canCreat;
    bool firstCreatDefender;

    public GameObject defendSoldier1Panel, defendSoldier2Panel, defendSoldier3Panel, defendSoldier4Panel, defendStartPos, 
        attackSoldier1Panel, attackSoldier2Panel, attackSoldier3Panel, attackSoldier4Panel, attackStartPos, endPos;
    public SkeletonGraphic defendSoldier1Anim, defendSoldier2Anim, defendSoldier3Anim, defendSoldier4Anim, 
        attackSoldier1Anim, attackSoldier2Anim, attackSoldier3Anim, attackSoldier4Anim;
    public AnimationReferenceAsset defendAnim1, defendAnim2, defendAnim3, defendAnim4, attackAnim1, attackAnim2, attackAnim3, attackAnim4;

    public GameObject baseDefender;
    private string currentState;
    private void Start()
    {
        soldier1Level = 1;
        soldier2Level = 1;
        soldier3Level = 1;
        soldier4Level = 1;
        firstCreatDefender = true;
        hasDefend1 = false;
        hasDefend2 = false;
        hasDefend3 = false;
        hasDefend4 = false;
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

    public void CreatBaseSoldier()
    {
        if (canCreat && isPlayerAttack)
        {
            soldierFactory.A_CreatSoldier1(AttackPos);
            canCreat = false;
        }
        else if (canCreat && !isPlayerAttack && firstCreatDefender)
        {
            baseDefender.SetActive(true);
            firstCreatDefender = false;
            return;

        }
    }

    public void CreatSoldier1()
    {
        if (canCreat && isPlayerAttack)
        {
            soldierFactory.A_CreatSoldier1(AttackPos);
            canCreat = false;
            attackSoldier1Panel.SetActive(true);
            attackSoldier1Panel.transform.localPosition = attackStartPos.transform.localPosition;
            StartCoroutine(MoveObject_Time(attackSoldier1Panel, attackSoldier1Panel.transform.localPosition, endPos.transform.localPosition, 1f));
            AnimationSet(attackSoldier1Anim, attackAnim1, false, 1f);
            GetAnimation(attackSoldier1Anim);
            for (int i = 0; i < attackList.Count; i++)
            {
                if (attackList[i].soldier1 && soldier1Level <= 10)
                {
                    attackList[i].atkTime = 
                        soldierFactory.a_Soldier1.prefab.atkTime - soldierFactory.a_Soldier1.prefab.atkTime * soldier1Level * attackUpdateMul;
                    attackList[i].speed =
                        soldierFactory.a_Soldier1.prefab.speed + soldierFactory.a_Soldier1.prefab.speed * soldier1Level * attackUpdateMul;
                }
            }
            soldier1Level += 1;
        }
        else if (canCreat && !isPlayerAttack)
        {
            canCreat = false;
            defendSoldier1Panel.SetActive(true);
            defendSoldier1Panel.transform.localPosition = defendStartPos.transform.localPosition;
            StartCoroutine(MoveObject_Time(defendSoldier1Panel, defendSoldier1Panel.transform.localPosition, endPos.transform.localPosition, 1f));
            AnimationSet(defendSoldier1Anim, defendAnim1, false, 1f);
            GetAnimation(defendSoldier1Anim);
            if (!hasDefend1)
            {
                soldierFactory.D_CreatSoldier1(DefendPos);
                hasDefend1 = true;
                return;
            }
            for (int i = 0; i < defendList.Count; i++)
            {
                if (defendList[i].soldier1 && soldier1Level <= 10)
                {
                    defendList[i].atkTime =
                        soldierFactory.d_Soldier1.prefab.atkTime - soldierFactory.d_Soldier1.prefab.atkTime * soldier1Level * defendUpdateMul;
                }
            }
            soldier1Level += 1;
        }
    }

    public void CreatSoldier2()
    {
        if (canCreat && isPlayerAttack)
        {
            soldierFactory.A_CreatSoldier2(AttackPos);
            canCreat = false;
            attackSoldier2Panel.SetActive(true);
            attackSoldier2Panel.transform.localPosition = attackStartPos.transform.localPosition;
            StartCoroutine(MoveObject_Time(attackSoldier2Panel, attackSoldier2Panel.transform.localPosition, endPos.transform.localPosition, 1f));
            AnimationSet(attackSoldier2Anim, attackAnim2, false, 1f);
            GetAnimation(attackSoldier2Anim);
            for (int i = 0; i < attackList.Count; i++)
            {
                if (attackList[i].soldier2 && soldier2Level <= 10)
                {
                    attackList[i].atkTime =
                        soldierFactory.a_Soldier2.prefab.atkTime - soldierFactory.a_Soldier2.prefab.atkTime * soldier2Level * attackUpdateMul;
                    attackList[i].speed =
                        soldierFactory.a_Soldier2.prefab.speed + soldierFactory.a_Soldier2.prefab.speed * soldier2Level * attackUpdateMul;
                }
            }
            soldier2Level += 1;
        }
        else if (canCreat && !isPlayerAttack)
        {
            canCreat = false;
            defendSoldier2Panel.SetActive(true);
            defendSoldier2Panel.transform.localPosition = defendStartPos.transform.localPosition;
            StartCoroutine(MoveObject_Time(defendSoldier2Panel, defendSoldier2Panel.transform.localPosition, endPos.transform.localPosition, 1f));
            AnimationSet(defendSoldier2Anim, defendAnim2, false, 1f);
            GetAnimation(defendSoldier2Anim);
            if (!hasDefend2)
            {
                soldierFactory.D_CreatSoldier2(DefendPos);   
                hasDefend2 = true;
                return;
            }
            for (int i = 0; i < defendList.Count; i++)
            {
                if (defendList[i].soldier2 && soldier2Level <= 10)
                {
                    defendList[i].atkTime =
                        soldierFactory.d_Soldier2.prefab.atkTime - soldierFactory.d_Soldier2.prefab.atkTime * soldier2Level * defendUpdateMul;
                }
            }
            soldier2Level += 1;
        }
    }

    public void CreatSoldier3()
    {
        if (canCreat && isPlayerAttack)
        {
            soldierFactory.A_CreatSoldier3(AttackPos);
            canCreat = false;
            attackSoldier3Panel.SetActive(true);
            attackSoldier3Panel.transform.localPosition = attackStartPos.transform.localPosition;
            StartCoroutine(MoveObject_Time(attackSoldier3Panel, attackSoldier3Panel.transform.localPosition, endPos.transform.localPosition, 1f));
            AnimationSet(attackSoldier3Anim, attackAnim3, false, 1f);
            GetAnimation(attackSoldier3Anim);
            for (int i = 0; i < attackList.Count; i++)
            {
                if (attackList[i].soldier3 && soldier3Level <= 10)
                {
                    attackList[i].atkTime =
                        soldierFactory.a_Soldier3.prefab.atkTime - soldierFactory.a_Soldier3.prefab.atkTime * soldier3Level * attackUpdateMul;
                    attackList[i].speed =
                        soldierFactory.a_Soldier3.prefab.speed + soldierFactory.a_Soldier3.prefab.speed * soldier3Level * attackUpdateMul;
                }
            }
            soldier3Level += 1;
        }
        else if (canCreat && !isPlayerAttack)
        {
            canCreat = false;
            defendSoldier3Panel.SetActive(true);
            defendSoldier3Panel.transform.localPosition = defendStartPos.transform.localPosition;
            StartCoroutine(MoveObject_Time(defendSoldier3Panel, defendSoldier3Panel.transform.localPosition, endPos.transform.localPosition, 1f));
            AnimationSet(defendSoldier3Anim, defendAnim3, false, 1f);
            GetAnimation(defendSoldier3Anim);
            if (!hasDefend3)
            {
                soldierFactory.D_CreatSoldier3(DefendPos);

                hasDefend3 = true;
                return;
            }
            for (int i = 0; i < defendList.Count; i++)
            {
                if (defendList[i].soldier3 && soldier3Level <= 10)
                {
                    defendList[i].atkTime =
                        soldierFactory.d_Soldier3.prefab.atkTime - soldierFactory.d_Soldier3.prefab.atkTime * soldier3Level * defendUpdateMul;
                }
            }
            soldier3Level += 1;
        }
    }

    public void CreatSoldier4()
    {
        if (canCreat && isPlayerAttack)
        {
            soldierFactory.A_CreatSoldier4(AttackPos);
            canCreat = false;
            attackSoldier4Panel.SetActive(true);
            attackSoldier4Panel.transform.localPosition = attackStartPos.transform.localPosition;
            StartCoroutine(MoveObject_Time(attackSoldier4Panel, attackSoldier4Panel.transform.localPosition, endPos.transform.localPosition, 1f));
            AnimationSet(attackSoldier4Anim, attackAnim4, false, 1f);
            GetAnimation(attackSoldier4Anim);
            for (int i = 0; i < attackList.Count; i++)
            {
                if (attackList[i].soldier4 && soldier4Level <= 10)
                {
                    attackList[i].atkTime =
                        soldierFactory.a_Soldier4.prefab.atkTime - soldierFactory.a_Soldier4.prefab.atkTime * soldier4Level * attackUpdateMul;
                    attackList[i].speed =
                        soldierFactory.a_Soldier4.prefab.speed + soldierFactory.a_Soldier4.prefab.speed * soldier4Level * attackUpdateMul;
                }
            }
            soldier4Level += 1;
        }
        else if (canCreat && !isPlayerAttack)
        {
            canCreat = false;
            defendSoldier4Panel.SetActive(true);
            defendSoldier4Panel.transform.localPosition = defendStartPos.transform.localPosition;
            StartCoroutine(MoveObject_Time(defendSoldier4Panel, defendSoldier4Panel.transform.localPosition, endPos.transform.localPosition, 1f));
            AnimationSet(defendSoldier4Anim, defendAnim4, false, 1f);
            GetAnimation(defendSoldier4Anim);
            if (!hasDefend4)
            {
                soldierFactory.D_CreatSoldier4(DefendPos);

                hasDefend4 = true;
                return;
            }
            for (int i = 0; i < defendList.Count; i++)
            {
                if (defendList[i].soldier4 && soldier4Level <= 10)
                {
                    defendList[i].atkTime =
                        soldierFactory.d_Soldier4.prefab.atkTime - soldierFactory.d_Soldier4.prefab.atkTime * soldier4Level * defendUpdateMul;
                }
            }
            soldier4Level += 1;
        }
    }

    void AnimControl()
    {
        if (attackSoldier1Anim.AnimationState.GetCurrent(0).IsComplete)
        {
            attackSoldier1Panel.SetActive(false);
        }
        if (attackSoldier2Anim.AnimationState.GetCurrent(0).IsComplete)
        {
            attackSoldier2Panel.SetActive(false);
        }
        if (attackSoldier3Anim.AnimationState.GetCurrent(0).IsComplete)
        {
            attackSoldier3Panel.SetActive(false);
        }
        if (attackSoldier4Anim.AnimationState.GetCurrent(0).IsComplete)
        {
            attackSoldier4Panel.SetActive(false);
        }
        if (defendSoldier1Anim.AnimationState.GetCurrent(0).IsComplete)
        {
            defendSoldier1Panel.SetActive(false);
        }
        if (defendSoldier2Anim.AnimationState.GetCurrent(0).IsComplete)
        {
            defendSoldier2Panel.SetActive(false);
        }
        if (defendSoldier3Anim.AnimationState.GetCurrent(0).IsComplete)
        {
            defendSoldier3Panel.SetActive(false);
        }
        if (defendSoldier4Anim.AnimationState.GetCurrent(0).IsComplete)
        {
            defendSoldier4Panel.SetActive(false);
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
                for (int i = 0; i < 10; i++)
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

    void GetAnimation(SkeletonAnimation skeletonAnimation)
    {
        currentState = skeletonAnimation.AnimationName;
    }
    void AnimationSet(SkeletonAnimation skeletonAnimation, AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentState))
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
    }

    private IEnumerator MoveObject_Time(GameObject UIGameObject, Vector3 startPos, Vector3 endPos, float time)
    {
        float dur = 0.0f;
        while (dur <= time)
        {
            dur += Time.deltaTime;
            UIGameObject.transform.localPosition = Vector3.Lerp(startPos, endPos, dur / time);
            yield return null;
        }
    }

}
