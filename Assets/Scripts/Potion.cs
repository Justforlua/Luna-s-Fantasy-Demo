using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Potion : MonoBehaviour
{
    
    // Start is called before the first frame update
    public GameObject effectGo;
    /*private void OnTriggerEnter2D(Collider2D collision) //两个触发器之间进行了碰撞
    {
        Instantiate(effectGo, transform.position, Quaternion.identity);
        Debug.Log("血瓶被" + collision + "吃掉了,生命值加1");
        LunaController lunaController = collision.GetComponent<LunaController>();
        if (lunaController != null)
        {
            if (lunaController.Health < lunaController.MaxHealth)
            {
                lunaController.ChangeHealth(1);
                Destroy(gameObject);
            }
            
        }
        
    }*/
    private void OnTriggerEnter2D(Collider2D collision) //两个触发器之间进行了碰撞
    {
        Instantiate(effectGo, transform.position, Quaternion.identity);
        Debug.Log("血瓶被" + collision + "吃掉了,生命值加40");
        LunaController lunaController = collision.GetComponent<LunaController>();
        if (lunaController != null)
        {
            if (GameManager.Instance.lunaCurrentHP < GameManager.Instance.lunaHP)
            {
                GameManager.Instance.AddOrDecreaseHP(40);
                Destroy(gameObject);
            }
            
        }
        
    }


    // private void OnTriggerStay2D(Collider2D other) //trigger待在里面
    // {
    //     throw new NotImplementedException();
    // }
    //
    // private void OnTriggerExit2D(Collider2D other) //trigger出来了
    // {
    //     throw new NotImplementedException();
    // }
}
