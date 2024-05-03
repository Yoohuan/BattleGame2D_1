using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Skill : MonoBehaviour
{
    public SkeletonAnimation Animator;
    public AnimationReferenceAsset anim_Skill;
    public GameObject Audio;
    public float skillTime;
    public bool isAttack;
    float t;
    private string currentState;
    GameObject[] enemy;


    // Start is called before the first frame update
    void Start()
    {
        t = skillTime;
        

        GetAnimation(Animator);
        AnimationSet(Animator, anim_Skill, false, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        WaitSkill();
        if (Animator.AnimationState.GetCurrent(0).IsComplete)
        {
            Destroy(gameObject);

        }
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

    void WaitSkill()
    {
        if (t <= 0)
        {
            Audio.SetActive(true);
            if (isAttack)
            {
                enemy = GameObject.FindGameObjectsWithTag("DefendSoldier");
            }
            else
            {
                enemy = GameObject.FindGameObjectsWithTag("AttackSoldier");
            }
            for (int i = 0; i < enemy.Length; i++)
            {
                if (!enemy[i])
                {
                    continue;
                }
                enemy[i].GetComponent<Soldier>().health -= 10000;
            }
            return;
        }
        t -= Time.deltaTime;
    }
}


