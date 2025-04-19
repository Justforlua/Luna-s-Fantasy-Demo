using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool vertical;//轴向控制：竖直方向来回移动
    public float speed = 3f;
    private Rigidbody2D rigidbody2D;
    //方向控制
    private int direction = 1;
    //方向改变的时间间隔
    public float changeTime = 3;
    //计时器
    private float timer;
    //动画控制器
    private Animator animator;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.fixedDeltaTime;//使计时器递减
        if (timer < 0)//时间由3秒减至0的时候使其反向
        {
            direction = -direction;//方向改为反向
            timer = changeTime;//重置计时器
        }
    }

    void FixedUpdate()
    {
        Vector3 pos = rigidbody2D.position;
        if (vertical) //垂直轴向移动
        {
            animator.SetFloat("LookX",0);
            animator.SetFloat("LookY",direction);
            pos.y = pos.y + speed * direction * Time.fixedDeltaTime;
        }
        else//水平轴向移动
        {
            animator.SetFloat("LookX",direction);
            animator.SetFloat("LookY",0);
            pos.x = pos.x + speed * direction * Time.fixedDeltaTime;
        }
        rigidbody2D.MovePosition(pos);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Luna"))
        {
            GameManager.Instance.EnterOrExitBattle();
        }
    }
}
