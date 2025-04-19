using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class LunaController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigidbody2D;//创建2D刚体
    public float moveSpeed;
    /*private int maxHealth = 5;//最大生命值
    public int MaxHealth
    {
        get { return maxHealth; }//属性
    }
    public int currentHealth;//当前生命值
    public int Health
    {
        get { return currentHealth; }//属性
        /*set { currentHealth = value; }#1#
    }*/

    public Animator animator;
    private Vector2 lookDirection = new Vector2(1, 0);
    private float moveScale;
    private Vector2 move;
    void Start()
    {
        
        // Application.targetFrameRate = 10;//设置帧速率
        rigidbody2D = GetComponent<Rigidbody2D>();//获取刚体2D
        //BoxCollider2D Dog = GetComponent<BoxCollider2D>();
        /*currentHealth = maxHealth-2;*/
        animator = GetComponentInChildren<Animator>();
        
        // int LunaHp = GetCurrentLunaHp();
        // Debug.Log(LunaHp);
    }

    // Update is called once per frame
    private void Update()
    {
        //玩家输入监听
        float horizontal = Input.GetAxisRaw("Horizontal");//调用Input类中的GetAxis方法获取水平轴向的值，返回值在（-1，1）之间
        float vertical = Input.GetAxisRaw("Vertical");//调用Input类中的GetAxis方法获取垂直轴向的值，返回值在（-1，1）之间
        move = new Vector2(horizontal,vertical);
        // animator.SetFloat("MoveValue",0);
        //如果luna X轴向输入值不为0，或者Y轴向输入值不为0的时候执行if循环
        if (!Mathf.Approximately(move.x, 0)||!Mathf.Approximately(move.y, 0))//用于比较两个浮点数是否比较接近
        {
            lookDirection.Set(move.x,move.y);
            lookDirection.Normalize();//单位化
            // animator.SetFloat("MoveValue",1);
        }
        //动画控制
        animator.SetFloat("Look X",lookDirection.x);
        animator.SetFloat("Look Y",lookDirection.y);
        moveScale = move.magnitude;
        if (moveScale > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveScale = 2;
                moveSpeed = 4.5f;
            }
            else
            {
                moveScale = 1;
                moveSpeed = 2.5f;
            }
        }
        
        animator.SetFloat("MoveValue",moveScale);//等于上面的animator.SetFloat("MoveValue",0);和animator.SetFloat("MoveValue",1);
        
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        // position.x = position.x + moveSpeed * horizontal * Time.deltaTime;
        // position.y = position.y + moveSpeed * vertical * Time.deltaTime;
        position = position + moveSpeed * move * Time.fixedDeltaTime;//用fxedDeltaTime来替换Time.DeltaTime因为在FixedUpdate中
        rigidbody2D.MovePosition(position);
    }

    /*public void ChangeHealth(int amount)
    {
        if (amount > 0)
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            Debug.Log("当前生命值:"+currentHealth+" "+"最大生命值:"+maxHealth);//同时输出currentHealth和maxHealth
        }
        // currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        // Debug.Log("当前生命值:"+currentHealth+" "+"最大生命值:"+maxHealth);//同时输出currentHealth和maxHealth
    }*/

    public void Climb(bool start)
    {
        animator.SetBool("Climb",start);
            
    }
    // private int GetCurrentLunaHp()
    // {
    //     return currentHealth;
    // }
    public void Jump(bool isJump)
    {
        animator.SetBool("Jump",isJump);
        rigidbody2D.simulated = !isJump;
    }

    
}
