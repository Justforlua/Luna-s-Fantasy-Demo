using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject effectGo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(effectGo,transform.position,Quaternion.identity);//在transform.position处生成effectGO对象（特效）
           // ，并且无旋转
        Destroy(gameObject);
    }
}
