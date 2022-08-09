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
    /// ��Ŀ���ƶ�
    /// </summary>
    Move,
    /// <summary>
    /// ���������ص�ԭλ
    /// </summary>
    Reset,
}
/// <summary>
/// ÿ��Npc���еĹ��ԣ���ȡ����
/// </summary>
public class Entity : MonoBehaviour
{
    protected Animator m_anim;
    protected Unity_Entity m_entity;
    //��ɫ��ԭλ��
    private Vector3 m_v3SrcPos;
    //
    public bool m_bAutoAttack;
    //Ѫ��
    private float m_fHp;
    //�Ƿ�����
    public bool mbIsDead {
        get;
        private set;

    }
    //�Ƿ��Ѿ�����
    [HideInInspector]
    public bool mbIsAleadyAttack;
    // Start is called before the first frame update
    void Start()
    {
        m_anim = transform.Find("Anim").GetComponent<Animator>();
        m_entity = transform.GetComponent<Unity_Entity>();
        m_v3SrcPos = transform.position;
        m_fHp = 100;
        //Ϊÿλ��ɫ����Ѫ��
        m_entity.SetHp(m_fHp);

        mbIsAleadyAttack = false;
    }
    //�Զ�����
    public void CaskSkill(Entity target)
    {
        int indexSkill= UnityEngine.Random.Range((int)SkillType.Skill01, (int)SkillType.Max);//���ѡһ������
        CaskSkill(target, (SkillType)indexSkill);
    
        }
    //�ֶ�����
    public void CaskSkill(Entity target,SkillType skillType)
    {
        mbIsAleadyAttack = true;
        Move(MoveType.Move, target.transform.position + (target.transform.forward * 3), skillType);
    }
    /// <summary>
    /// ��ɫ����ʱ���ƶ�
    /// </summary>
    /// <param name="moveType">�ƶ�������</param>
    /// <param name="targetPos">������Ŀ��λ��</param>
    /// <param name="emSkillType">���˹�������Ҫ�ͷŵļ���</param>
    public void Move(MoveType moveType,Vector3 targetPos,SkillType emSkillType)
    {
        if (moveType == MoveType.Move)
        {
            //�����������ƶ�
            //1���Ŷ���
            m_anim.SetBool("bMove", true);
            //2�ƶ���Ŀ��
            transform.DOMove(targetPos, 1).OnComplete(() =>
            {
                m_anim.SetBool("bMove", false);
                //3�������
                Attack(emSkillType);
            }
            
            );
        }
        else if (moveType == MoveType.Reset)
        {
            //�������λ���ƶ�
            transform.DOMove(targetPos, 1).OnComplete(() =>
            {
                m_anim.SetBool("bMove", false);
                //��������Ҫ�л�������
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
