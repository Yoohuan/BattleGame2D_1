using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllFacingCamera : MonoBehaviour
{
    [Space(10)]
    [Header("使所有子物体面向摄像机（不作翻转）")]
    Transform[] childs;

    // Start is called before the first frame update
    void Start()
    {
        childs = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i < childs.Length;i++)
        {
            childs[i].rotation = Camera.main.transform.rotation;
        }
    }
}
