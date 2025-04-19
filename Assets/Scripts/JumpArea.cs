using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class JumpArea : MonoBehaviour
{
    public Transform jumpPointA;
    public Transform jumpPointB;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Luna"))
        {
            LunaController lunaController = collision.transform.GetComponent<LunaController>();
            lunaController.Jump(true);
            float distanceA = Vector3.Distance(lunaController.transform.position, jumpPointA.position);
            float distanccB = Vector3.Distance(lunaController.transform.position, jumpPointB.position);
            Transform targetTransform;
            if (distanceA > distanccB)//离B近，那么就是从B跳到A
            {
                targetTransform = jumpPointA;
                lunaController.transform.DOMove(targetTransform.position, 0.5f).OnComplete(() => {EndJump(lunaController);});
                /*() => {}   lambda表达式*/
            }
            else//离A近，那么就是从A跳到B
            {
                targetTransform = jumpPointB;
                lunaController.transform.DOMove(targetTransform.position, 0.5f).OnComplete(() => {EndJump(lunaController);});
            }
            /*三目运算符*/
            /*targetTransform = distanceA > distanceB ? jumpPointA : jumpPointB;*/

        }
    }
    //跳跃完成后需要回调
    private void EndJump(LunaController lunaController)
    {
        lunaController.Jump(false);
    }
}

