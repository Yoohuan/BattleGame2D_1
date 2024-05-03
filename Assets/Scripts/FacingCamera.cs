using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCamera : MonoBehaviour
{
    [Space(10)]
    [Header("使物体面向摄像机,勾选使物体水平翻转")]
    [Header("若调整摄像机角度需更改代码参数")]
    public bool flip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flip)
        {
            transform.localRotation = Quaternion.Euler(45, 180, 0);
        }
        else
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
