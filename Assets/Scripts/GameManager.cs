using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance;//创建GameManager单例
    public int lunaHP=100;//最大生命值
    // public int lunaHP
    // {
    //     get { return lunaHP; }//属性
    // }
    public int lunaCurrentHP;//当前生命值
    /*public int Health
    {
        get { return lunaCurrentHP; }//属性
        /*set { lunaCurrentHP = value; }#1#
    }*/

    public GameObject BattleGo;//战斗场景
    // public GameObject Panel_Battle;
    public int lunaMP=100;
    public int lunaCurrentMP=100;
    //怪物
    public int monsterCurHP=60;
    
    private void Awake()
    {
        Instance = this;//this等于当前脚本的GameManager
        lunaCurrentHP = lunaHP=100;
        lunaMP = lunaCurrentMP = 100;
    }
    public void ChangeHealth(int amount)
    {
        if (amount > 0)
        {
            lunaCurrentHP = Mathf.Clamp(lunaCurrentHP + amount, 0, lunaHP);
            Debug.Log("当前生命值:"+lunaCurrentHP+" "+"最大生命值:"+lunaHP);//同时输出lunaCurrentHP和lunaHP
        }
        // lunaCurrentHP = Mathf.Clamp(lunaCurrentHP + amount, 0, lunaHP);
        // Debug.Log("当前生命值:"+lunaCurrentHP+" "+"最大生命值:"+lunaHP);//同时输出lunaCurrentHP和lunaHP
    }

    public void EnterOrExitBattle(bool enter=true)
    {
        UIManager.Instance.ShowOrHideBattlePanel(enter);
        BattleGo.SetActive(enter);
        // Panel_Battle.SetActive(enter);
    }
/// <summary>
/// luna血量改变的方法
/// </summary>
/// <param name="value"></param>
    public void AddOrDecreaseHP(int value)
    {
        lunaCurrentHP += value;
        if (lunaCurrentHP >= lunaHP)
        {
            lunaCurrentHP = lunaHP;
        }

        if (lunaCurrentHP <= 0)
        {
            lunaCurrentHP = 0;
        }
        UIManager.Instance.SetHPValue((float)lunaCurrentHP/lunaHP);
    }
    public void AddOrDecreaseMP(int value)
    {
        lunaCurrentMP += value;
        if (lunaCurrentMP >= lunaMP)
        {
            lunaCurrentMP = lunaMP;
        }

        if (lunaCurrentMP <= 0)
        {
            lunaCurrentMP = 0;
        }
        UIManager.Instance.SetMPValue((float)lunaCurrentMP/lunaMP);
    }
/// <summary>
/// 是否可以使用相关技能耗费蓝量
/// </summary>
/// <returns></returns>
    public bool CanUsePlayerMP(int value)
    {
        return lunaCurrentMP>=value;
    }
    /// <summary>
    /// monster血量改变的方法
    /// </summary>
    /// <param name="value"></param>
    public int AddOrDecreaseMonsterHP(int value)
    {
        monsterCurHP += value;
        return monsterCurHP;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
