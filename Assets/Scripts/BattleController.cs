using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public Animator LunaAnimator;
    public Animator MonsterAnimator;

    public Transform lunaTrans;

    public Transform monsterTrans;

    private Vector3 monsterInitpos;//怪物初始位置
    private Vector3 lunaInitpos;//luna初始位置
    public SpriteRenderer monsterSR; 
    public SpriteRenderer lunaSR;
    public GameObject SkillEffectGo;

    private void Awake()
    {
        monsterInitpos = monsterTrans.localPosition;
        lunaInitpos = lunaTrans.localPosition;
    }

    private void OnEnable()//每次显示都会调用
    {
        monsterSR.DOFade(1, 0.01f);
        lunaSR.DOFade(1, 0.01f);
        lunaTrans.localPosition = lunaInitpos;
        monsterTrans.localPosition = monsterInitpos;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LunaAttack()
    {
        StartCoroutine(PerformAttackLogic());
    }

    IEnumerator PerformAttackLogic()//协程
    {
        //隐藏战斗UI
        UIManager.Instance.ShowOrHideBattlePanel(false);
        LunaAnimator.SetBool("MoveState",true);
        LunaAnimator.SetFloat("MoveValue",-1);
        lunaTrans.DOLocalMove(monsterInitpos+new Vector3(1,0,0),0.5f).OnComplete
        (
            () =>
            {
                LunaAnimator.SetBool("MoveState",false);
                LunaAnimator.SetFloat("MoveValue",0);
                LunaAnimator.CrossFade("Attack",0);
                monsterSR.DOFade(0.3f, 0.2f).OnComplete(() =>
                {
                    JudgemonsterHP(-30);
                });
            }
        );
        yield return new WaitForSeconds(1.167f);
        LunaAnimator.SetBool("MoveState",true);
        LunaAnimator.SetFloat("MoveValue",1);
        lunaTrans.DOLocalMove(lunaInitpos, 0.5f).OnComplete((() =>
        {
            LunaAnimator.SetBool("MoveState", false);
        }));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MonsterAttack());
    }

    IEnumerator MonsterAttack()
    {
        monsterTrans.DOLocalMove(lunaInitpos - new Vector3(1.5f, 0, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        monsterTrans.DOLocalMove(lunaInitpos, 0.2f).OnComplete((() =>
        {
            monsterTrans.DOLocalMove(lunaInitpos - new Vector3(1.5f, 0, 0), 0.2f);
            LunaAnimator.CrossFade("Hit",0);//播放hit动画
            lunaSR.DOFade(0.3f, 0.2f).OnComplete((() =>
            {
                lunaSR.DOFade(1f, 0.2f);
            }));//luna受击闪烁
            JudgePlayerHP(-20);
        }));
        yield return new WaitForSeconds(0.4f);
        monsterTrans.DOLocalMove(monsterInitpos, 0.5f).OnComplete((() =>
        {
            UIManager.Instance.ShowOrHideBattlePanel(true);
        }));
    }
    private void JudgePlayerHP(int value)
    {
        GameManager.Instance.AddOrDecreaseHP(value);
        if (GameManager.Instance.lunaCurrentHP <= 0)
        {
            LunaAnimator.CrossFade("Die",0);
            lunaSR.DOFade(0, 0.8f).OnComplete((() =>
            {
                GameManager.Instance.EnterOrExitBattle(false);
            }));
        }
        
    }
    private void JudgemonsterHP(int value)
    {
        if (GameManager.Instance.AddOrDecreaseMonsterHP(value) <= 0)
        {
            LunaAnimator.CrossFade("Die",0);
            monsterSR.DOFade(0, 0.4f).OnComplete((() =>
            {
                GameManager.Instance.EnterOrExitBattle(false);
            }));
        }
        else
        {
            monsterSR.DOFade(1, 0.2f);
        }
        
    }

    public void lunaDefend()
    {
        StartCoroutine(PerformDefendLogic());
    }
/// <summary>
/// luna使用技能
/// </summary>
    public void lunaUseSkill()
    {
        if (!GameManager.Instance.CanUsePlayerMP(30))
        {
            return;
        }

        StartCoroutine(PerformSkillLogic());
    }

    IEnumerator PerformSkillLogic()
    {
        UIManager.Instance.ShowOrHideBattlePanel(false);
        LunaAnimator.CrossFade("Skill",0);
        GameManager.Instance.AddOrDecreaseMP(-30);
        yield return new WaitForSeconds(0.35f);
        // Instantiate(SkillEffectGo, monsterInitpos, Quaternion.identity);
        GameObject go=Instantiate(SkillEffectGo, monsterTrans);
        go.transform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(0.4f);
        monsterSR.DOFade(0.3f, 0.2f).OnComplete((() =>
        {
            JudgemonsterHP(-40);
        }));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MonsterAttack());

    }

    public void RecoverLunaHP()
    {
        if (!GameManager.Instance.CanUsePlayerMP(50))
        {
            return;
        }

        StartCoroutine(PerformRecoveryLogic());
    }

    IEnumerator PerformRecoveryLogic()
    {
        UIManager.Instance.ShowOrHideBattlePanel(false);
        LunaAnimator.CrossFade("RecoverHP",0);
        //消耗50mp
        GameManager.Instance.AddOrDecreaseMP(-50);
        yield return new WaitForSeconds(0.1f);
        //播放回血特效
        GameObject go=Instantiate(SkillEffectGo, monsterTrans);
        go.transform.localPosition = Vector3.zero;
        //血量恢复
        GameManager.Instance.AddOrDecreaseHP(40);
        yield return new WaitForSeconds(0.5f);
        //开启怪物攻击的携程
        StartCoroutine(MonsterAttack());
    }

    IEnumerator PerformDefendLogic()
    {
        UIManager.Instance.ShowOrHideBattlePanel(false);
        LunaAnimator.SetBool("Defend",true);
        monsterTrans.DOLocalMove(lunaInitpos - new Vector3(1.5f, 0, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        monsterTrans.DOLocalMove(lunaInitpos, 0.2f).OnComplete((() =>
        {
            monsterTrans.DOLocalMove(lunaInitpos - new Vector3(1.5f, 0, 0), 0.2f);
            lunaTrans.DOLocalMove(lunaInitpos + new Vector3(1, 0, 0), 0.2f).OnComplete((() =>
            {
                lunaTrans.DOLocalMove(lunaInitpos, 0.2f);
            }));
        }));
        yield return new WaitForSeconds(0.4f);
        monsterTrans.DOLocalMove(monsterInitpos, 0.5f).OnComplete((() =>
        {
            UIManager.Instance.ShowOrHideBattlePanel(true);
            LunaAnimator.SetBool("Defend",false);
        }));
    }

    public void LunaEscape()
    {
        UIManager.Instance.ShowOrHideBattlePanel(false);
        lunaTrans.DOLocalMove(lunaInitpos + new Vector3(5, 0, 0), 0.5f).OnComplete((() =>
        {
            GameManager.Instance.EnterOrExitBattle(false);
        }));
        LunaAnimator.SetBool("MoveState",true);
        LunaAnimator.SetFloat("MoveValue",1);
    }
}
