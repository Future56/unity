using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum SkillType
{
    None,Skill01,
    Skill02,
    Skill03,
    Max,
    //Range(1,3)==>(1,3]
}
public enum MoveType
{
    /// <summary>
    /// 向目标移动
    /// </summary>
    Move,
    /// <summary>
    /// 攻击结束回到原位
    /// </summary>
    Reset,
}
/// <summary>
/// 每个Npc共有的共性，提取出来
/// </summary>
public class Entity : MonoBehaviour
{
    protected Animator m_anim;
    protected Unity_Entity m_entity;
    //角色的原位置
    private Vector3 m_v3SrcPos;
    //
    public bool m_bAutoAttack;
    //血量
    private float m_fHp;
    //是否死亡
    public bool mbIsDead {
        get;
        private set;

    }
    //是否已经进攻
    [HideInInspector]
    public bool mbIsAleadyAttack;
    // Start is called before the first frame update
    void Start()
    {
        m_anim = transform.Find("Anim").GetComponent<Animator>();
        m_entity = transform.GetComponent<Unity_Entity>();
        m_v3SrcPos = transform.position;
        m_fHp = 100;
        //为每位角色设置血量
        m_entity.SetHp(m_fHp);

        mbIsAleadyAttack = false;
    }
    //自动攻击
    public void CaskSkill(Entity target)
    {
        int indexSkill= UnityEngine.Random.Range((int)SkillType.Skill01, (int)SkillType.Max);//随机选一个技能
        CaskSkill(target, (SkillType)indexSkill);
    
        }
    //手动攻击
    public void CaskSkill(Entity target,SkillType skillType)
    {
        mbIsAleadyAttack = true;
        Move(MoveType.Move, target.transform.position + (target.transform.forward * 3), skillType);
    }
    /// <summary>
    /// 角色攻击时的移动
    /// </summary>
    /// <param name="moveType">移动的类型</param>
    /// <param name="targetPos">攻击的目标位置</param>
    /// <param name="emSkillType">到了攻击那里要释放的技能</param>
    public void Move(MoveType moveType,Vector3 targetPos,SkillType emSkillType)
    {
        if (moveType == MoveType.Move)
        {
            //正常攻击的移动
            //1播放动画
            m_anim.SetBool("bMove", true);
            //2移动至目标
            transform.DOMove(targetPos, 1).OnComplete(() =>
            {
                m_anim.SetBool("bMove", false);
                //3发起进攻
                Attack(emSkillType);
            }
            
            );
        }
        else if (moveType == MoveType.Reset)
        {
            //攻击完归位的移动
            transform.DOMove(targetPos, 1).OnComplete(() =>
            {
                m_anim.SetBool("bMove", false);
                //攻击完需要切换攻击人
                BattleManage.Instance.ChangeCast();
            }

);
        }
    }
    private void Attack(SkillType emSkillType)
    {
        switch (emSkillType)
        {
            case SkillType.Skill01:
                m_anim.SetTrigger("triggerAttack1");
                break;
            case SkillType.Skill02:
                m_anim.SetTrigger("triggerAttack2");
                break;
            case SkillType.Skill03:
                m_anim.SetTrigger("triggerSkill");
                break;
        }
    }
    public void Damage(float damage)
    {
        m_fHp -= damage;
        if (m_fHp <= 0)
        {
            m_fHp = 0;
            mbIsDead = true;
            m_anim.SetTrigger("triggerDead");
        }
        else
        {
            m_anim.SetTrigger("triggerHurt");
        }
        m_entity.Damage(damage);
    }
    public void ResetMoveEvent()
    {
        Move(MoveType.Reset, m_v3SrcPos, SkillType.None);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
