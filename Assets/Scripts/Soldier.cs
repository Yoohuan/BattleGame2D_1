using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public int atk;
    public int health;
    public float atkTime;
    public float speed;
    public float atkRange;
    public bool isAttack;
    public bool initDir_Right;
    public bool farAttack1;
    public bool farAttack2; 
    public bool farAttack3;
    public bool farAttack4;

    List<GameObject> opponentList = new List<GameObject>();
    GameObject barricade;
    GameObject Effect;
    Health attackHealthBar;
    Rigidbody2D rb;
    Vector2 purPos;
    float distance;
    float atkTimeCD;
    int purOpponentID;

    bool isHit;

    public SkeletonAnimation soldierAnimator;
    public AnimationReferenceAsset anim_Move, anim_Attack, anim_Die, anim_Idle, anim_Hit;
    private string currentState;

    public GameObject attackAudio;
    public GameObject dieAudio;

    public enum State
    {
        s_Idle,
        s_Move,
        s_Attack,
        s_Die,
    }

    State soldierState;

    public State SoldierState
    {
        get { return soldierState; }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackAudio.SetActive(false);
        dieAudio.SetActive(false);
        isHit = false;
        if (!isAttack)
        {
            soldierState = State.s_Idle;
            purPos = transform.position;
            distance = Vector2.Distance(transform.position, purPos);
        }
        else
        {
            barricade = GameObject.FindWithTag("Barricade");
            opponentList.Add(barricade);
            soldierState = State.s_Move;
            purPos = opponentList[0].transform.position;
            distance = Vector2.Distance(transform.position, barricade.transform.position);
        }
        purOpponentID = 0;
        atkTimeCD = atkTime;
        attackHealthBar = GameObject.FindWithTag("AttackHealth").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        Control();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((!isAttack) && other.CompareTag("AttackSoldier"))
        {
            for (int i = 0; i < opponentList.Count; i++)
            {
                if (other.gameObject == opponentList[i])
                {
                    return;
                }
            }
            opponentList.Add(other.gameObject);
            purPos = opponentList[0].transform.position;
            distance = Vector2.Distance(transform.position, purPos);
        }
        else if (isAttack && other.CompareTag("DefendSoldier"))
        {
            for (int i = 0; i < opponentList.Count; i++)
            {
                if (other.gameObject == opponentList[i])
                {
                    return;
                }
            }
            opponentList.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ((!isAttack) && other.CompareTag("AttackSoldier"))
        {
            opponentList.Remove(other.gameObject);
            purPos = transform.position;
            distance = Vector2.Distance(transform.position, purPos);
            purOpponentID = 0;
        }
        else if (isAttack && other.CompareTag("DefendSoldier"))
        {
            if(!opponentList[0])
            {
                return;
            }
            opponentList.Remove(other.gameObject);
            purPos = opponentList[0].transform.position;
            distance = Vector2.Distance(transform.position, barricade.transform.position);
            purOpponentID = 0;
        }
    }

    void Control()
    {
        switch (soldierState)
        {
            case State.s_Idle:
                Idle();
                break;
            case State.s_Move:
                Move();
                break;
            case State.s_Attack:
                Attack();
                break;
            case State.s_Die:
                Die();
                break;
            default:
                break;

        }
    }

    void Idle()
    {
        attackAudio.SetActive(false);
        AnimationSet(soldierAnimator, anim_Idle, true, 1f);
        GetAnimation(soldierAnimator);
        if (initDir_Right)
        {
            transform.localRotation = Quaternion.Euler(-45, 0, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(45, 180, 0);
        }
        if (opponentList.Count - 1 < 0)
        {
            purPos = transform.position;
            distance = Vector2.Distance(transform.position, purPos);
            purOpponentID = 0;
            atkTimeCD = atkTime;
            return;
        }
        else
        {
            purPos = opponentList[0].transform.position;
            distance = Vector2.Distance(transform.position, purPos);
            purOpponentID = 0;
            atkTimeCD = atkTime;
            soldierState = State.s_Move;
        }
    }

    void Move()
    {
        attackAudio.SetActive(false);
        if (health <= 0)
        {
            soldierState = State.s_Die;
            return;
        }
        Flip();
        if(isHit)
        {
            AnimationSet(soldierAnimator, anim_Hit, true, 1f);
            GetAnimation(soldierAnimator);
        }
        else
        {
            AnimationSet(soldierAnimator, anim_Move, true, 1f);
            GetAnimation(soldierAnimator);
        }
        Test();
        for (int i = 0; i < opponentList.Count; i++)
        {
            if (!opponentList[i])
            {
                opponentList.RemoveAt(i);
                continue;
            }
            if (distance > Vector2.Distance(transform.position, opponentList[i].transform.position))
            {
                distance = Vector2.Distance(transform.position, opponentList[i].transform.position);
                purPos = opponentList[i].transform.position;
                purOpponentID = i;
            }
        }
        if (distance <= atkRange)
        {
            soldierState = State.s_Attack;
            return;
        }
        Vector2 dir = new Vector2(purPos.x - transform.position.x, purPos.y - transform.position.y).normalized;
        rb.velocity = dir * speed;
        //transform.position = Vector2.MoveTowards(transform.position, purPos, speed * Time.deltaTime);
    }

    void Attack()
    {

        Flip();
        Test();
        if (health <= 0)
        {
            if ((!isAttack) && farAttack1)
            {
                Effect = opponentList[purOpponentID].transform.Find("Effect1").gameObject;
                opponentList[purOpponentID].GetComponent<Soldier>().OutHit();
                Effect.SetActive(false);
            }
            else if ((!isAttack) && farAttack2)
            {
                Effect = opponentList[purOpponentID].transform.Find("Effect2").gameObject;
                opponentList[purOpponentID].GetComponent<Soldier>().OutHit();
                Effect.SetActive(false);
            }
            else if ((!isAttack) && farAttack3)
            {
                Effect = opponentList[purOpponentID].transform.Find("Effect3").gameObject;
                opponentList[purOpponentID].GetComponent<Soldier>().OutHit();
                Effect.SetActive(false);
            }
            else if ((!isAttack) && farAttack4)
            {
                Effect = opponentList[purOpponentID].transform.Find("Effect4").gameObject;
                opponentList[purOpponentID].GetComponent<Soldier>().OutHit();
                Effect.SetActive(false);
            }
            soldierState = State.s_Die;
            return;
        }
        for (int i = 0; i < opponentList.Count; i++)
        {
            if (!opponentList[i])
            {
                opponentList.RemoveAt(i);
                continue;
            }
            if (distance > Vector2.Distance(transform.position, opponentList[i].transform.position))
            {
                distance = Vector2.Distance(transform.position, opponentList[i].transform.position);
                purPos = opponentList[i].transform.position;
                purOpponentID = i;
            }
        }
        distance = Vector2.Distance(transform.position, purPos);
        if (distance > atkRange || (!isAttack && purOpponentID <= opponentList.Count-1 && opponentList[purOpponentID].GetComponent<Soldier>().SoldierState == State.s_Die))
        {
            if ((!isAttack) && farAttack1)
            {
                Effect = opponentList[purOpponentID].transform.Find("Effect1").gameObject;
                opponentList[purOpponentID].GetComponent<Soldier>().OutHit();
                Effect.SetActive(false);
            }
            else if ((!isAttack) && farAttack2)
            {
                Effect = opponentList[purOpponentID].transform.Find("Effect2").gameObject;
                opponentList[purOpponentID].GetComponent<Soldier>().OutHit();
                Effect.SetActive(false);
            }
            else if ((!isAttack) && farAttack3)
            {
                Effect = opponentList[purOpponentID].transform.Find("Effect3").gameObject;
                opponentList[purOpponentID].GetComponent<Soldier>().OutHit();
                Effect.SetActive(false);
            }
            else if ((!isAttack) && farAttack4)
            {
                Effect = opponentList[purOpponentID].transform.Find("Effect4").gameObject;
                opponentList[purOpponentID].GetComponent<Soldier>().OutHit();
                Effect.SetActive(false);
            }
            if (distance > atkRange)
            {
                soldierState = State.s_Move;
                return;
            }
            else
            {
                opponentList.RemoveAt(purOpponentID);
                soldierState = State.s_Attack;
                return;
            }
        }
        Vector2 dir = new Vector2(purPos.x - transform.position.x, purPos.y - transform.position.y).normalized;
        rb.velocity = dir * 0f;
        attackAudio.SetActive(true);
        if(isHit)
        {
            AnimationSet(soldierAnimator, anim_Hit, true, 1f);
            GetAnimation(soldierAnimator);
        }
        else
        {
            AnimationSet(soldierAnimator, anim_Attack, true, 1f);
            GetAnimation(soldierAnimator);
        }
        //transform.LookAt(purPos);
        //animator.SetBool("Attack", true);
        Test();
        if (atkTimeCD <= 0)
        {
            if (purOpponentID == 0 && isAttack)
            {
                opponentList[purOpponentID].GetComponent<Barricade>().health -= atk;
            }
            else
            {
                opponentList[purOpponentID].GetComponent<Soldier>().health -= atk;
            }
            atkTimeCD = atkTime;
        }
        else
        {
            atkTimeCD -= Time.deltaTime;
        }

        //Test
        if ((!isAttack) && opponentList.Count - 1 < 0)
        {
            purPos = transform.position;
            distance = Vector2.Distance(transform.position, purPos);
            purOpponentID = 0;
            atkTimeCD = atkTime;
            soldierState = State.s_Idle;
            return;
        }
        if ((purOpponentID > opponentList.Count - 1) || (!opponentList[purOpponentID]))
        {
            if (purOpponentID > opponentList.Count - 1)
            {
                purPos = opponentList[0].transform.position;
                distance = Vector2.Distance(transform.position, barricade.transform.position);
                purOpponentID = 0;
                atkTimeCD = atkTime;
                soldierState = State.s_Move;
            }
            else
            {
                opponentList.RemoveAt(purOpponentID);
                purPos = opponentList[0].transform.position;
                distance = Vector2.Distance(transform.position, barricade.transform.position);
                purOpponentID = 0;
                atkTimeCD = atkTime;
                soldierState = State.s_Move;
            }
        }
        if ((!isAttack) && opponentList.Count - 1 < 0)
        {
            purPos = transform.position;
            distance = Vector2.Distance(transform.position, purPos);
            purOpponentID = 0;
            atkTimeCD = atkTime;
            soldierState = State.s_Idle;
            return;
        }
        if ((!isAttack) && farAttack1)
        {
            Effect = opponentList[purOpponentID].transform.Find("Effect1").gameObject;
            opponentList[purOpponentID].GetComponent<Soldier>().InHit();
            Effect.SetActive(true);
        }
        if ((!isAttack) && farAttack2)
        {
            Effect = opponentList[purOpponentID].transform.Find("Effect2").gameObject;
            opponentList[purOpponentID].GetComponent<Soldier>().InHit();
            Effect.SetActive(true);
        }
        if ((!isAttack) && farAttack3)
        {
            Effect = opponentList[purOpponentID].transform.Find("Effect3").gameObject;
            opponentList[purOpponentID].GetComponent<Soldier>().InHit();
            Effect.SetActive(true);
        }
        if ((!isAttack) && farAttack4)
        {
            Effect = opponentList[purOpponentID].transform.Find("Effect4").gameObject;
            opponentList[purOpponentID].GetComponent<Soldier>().InHit();
            Effect.SetActive(true);
        }

    }

    public void InHit()
    {
        isHit = true;
    }

    public void OutHit()
    {
        isHit = false;
    }

    void Flip()
    {
        if ((!isAttack))
        {
            if (initDir_Right)
            {
                transform.localRotation = Quaternion.Euler(-45,0,0);
            }
            else 
            {
                transform.localRotation = Quaternion.Euler(45, 180, 0);
            }
        }
        else
        {
            if (initDir_Right)
            {
                transform.localRotation = Quaternion.Euler(45, 180, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(-45, 0, 0);
            }
        }
    }

    void Die()
    {
        Vector2 dir = new Vector2(purPos.x - transform.position.x, purPos.y - transform.position.y).normalized;
        rb.velocity = dir * 0f;
        attackAudio.SetActive(false);
        dieAudio.SetActive(true);
        AnimationSet(soldierAnimator, anim_Die, false, 1f);
        GetAnimation(soldierAnimator);
        if (soldierAnimator.AnimationState.GetCurrent(0).IsComplete)
        {
            if (isAttack)
            {
                attackHealthBar.healthCurrent -= 1;
                GameObject.FindWithTag("Canvas").GetComponent<PanelControl>().AddScore();
            }
            Destroy(gameObject);
        }

    }

    void Test()
    {
        if ((!isAttack) && (opponentList.Count - 1 < 0 || purOpponentID > opponentList.Count - 1))
        {
            purPos = transform.position;
            distance = Vector2.Distance(transform.position, purPos);
            purOpponentID = 0;
            atkTimeCD = atkTime;
            soldierState = State.s_Idle;
            return;
        }
        if ((purOpponentID > opponentList.Count - 1) || (!opponentList[purOpponentID]))
        {
            if (purOpponentID > opponentList.Count - 1)
            {
                purPos = opponentList[0].transform.position;
                distance = Vector2.Distance(transform.position, barricade.transform.position);
                purOpponentID = 0;
                atkTimeCD = atkTime;
                soldierState = State.s_Move;
            }
            else
            {
                opponentList.RemoveAt(purOpponentID);
                purPos = opponentList[0].transform.position;
                distance = Vector2.Distance(transform.position, barricade.transform.position);
                purOpponentID = 0;
                atkTimeCD = atkTime;
                soldierState = State.s_Move;
            }
        }
        if ((!isAttack) && opponentList.Count - 1 < 0)
        {
            purPos = transform.position;
            distance = Vector2.Distance(transform.position, purPos);
            purOpponentID = 0;
            atkTimeCD = atkTime;
            soldierState = State.s_Idle;
            return;
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
}
