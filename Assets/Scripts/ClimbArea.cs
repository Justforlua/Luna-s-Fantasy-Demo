using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbArea : MonoBehaviour
{
    //触发检测
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //三种方式任选其一
        // if (collision.name == "Luna")
        // {
        //     
        // }
        //
        // if (collision.tag == "Luna")
        // {
        //     
        // }

        if (collision.CompareTag("Luna"))
        {
            collision.GetComponent<LunaController>().Climb(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Luna"))
        {
            collision.GetComponent<LunaController>().Climb(false);
        }
    }
}
