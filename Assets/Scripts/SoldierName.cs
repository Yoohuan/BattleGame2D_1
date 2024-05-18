using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoldierName : MonoBehaviour
{
    public bool isPlayer;
    public bool isAttack;
    public string playerName;
    public string[] defendName = { "蜜蜜", "柳岩台", "朝夕悠悠", "雁槐", "别离" };
    public string[] attackName = { "天南海北", "殇璃", "萧墨尘", "南山南", "琴" };
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
