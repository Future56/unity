using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BattleState
{
    Player,
    EnEmy,
    Win,
    Lose,
}
public class BattleManage : MonoBehaviour
{
    private List<Entity> m_listEnemy;
    private List<Entity> m_listPlayer;

    private Entity m_player;

    //ʩ����
    private Entity m_caster;
    //��ʩ����
    private Entity m_bycaster;
    public BattleState mbattleState {
        get;private set;
    }
    public static BattleManage Instance;

    // Start is called before the first frame update
    void Start()
    {
        BuildEntity();
        Instance = this;
        SceneManager.Instance.currentScene = CurrentScene.Battle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //����Ӫ��ʼ��
    private void BuildEntity()
    {
        m_listEnemy = new List<Entity>();
        m_listEnemy.Add(GameObject.FindWithTag("Enemy1").GetComponent<Entity>());
        m_listEnemy.Add(GameObject.FindWithTag("Enemy2").GetComponent<Entity>());
        m_listEnemy.Add(GameObject.FindWithTag("Enemy3").GetComponent<Entity>());

        m_player = GameObject.FindWithTag("Player").GetComponent<Entity>();

        m_listPlayer = new List<Entity>();
        m_listPlayer.Add(m_player);
        m_listPlayer.Add(GameObject.FindWithTag("Player2").GetComponent<Entity>());
        m_listPlayer.Add(GameObject.FindWithTag("Player1").GetComponent<Entity>());
        
    }
    public void ChangeCast()
    {
        if (mbattleState == BattleState.EnEmy)
        {
            foreach(Entity enemy in m_listEnemy)
            {
                if (enemy.mbIsDead)
                {
                    continue;
                }
                if (enemy.mbIsAleadyAttack)
                {
                    continue;
                }
                Entity target = FindAlive(m_listPlayer);//��һ�����ŵĶ���
                if (target == null)
                {
                    continue;
                }
                //��ʼʩ��
                enemy.CaskSkill(target);
                //
                SetCasterAndBycaster(enemy, target);
                return;
            }
            ResetEntityAttackState();
            //��ǰ�غϽ����������Ϸ���
            if (FindAlive(m_listPlayer)==null)
            {
                mbattleState = BattleState.Lose;
                UBattleResult.Instance.Show(mbattleState);
            }
            else
            {
                //ת�����з���Ӫ����
                mbattleState = BattleState.Player;
                ChangeCast();//�ݹ鱾����
            }
        }
        else if (mbattleState == BattleState.Player)
        {
            //���Ƥ����λû��������δ������
            if(!m_player.mbIsDead && !m_player.mbIsAleadyAttack)
            {
                UISkillGroup.Instance.ChoseTips();
                return;
            }
            else
            {
                //����foreach�������Ӫ��ɫ�����������
                foreach(var player in m_listPlayer)
                {
                    if (player.tag == "Player")
                    {
                        continue;
                    }
                    if (player.mbIsDead || player.mbIsAleadyAttack)
                    {
                        continue;
                    }
                    Entity target= FindAlive(m_listEnemy);//��һ�����ŵĶ���
                    if (target == null)
                    {
                        continue;
                    }
                    player.CaskSkill(target);
                    SetCasterAndBycaster(player, target);
                    return;
                }
                {

                }
            }
            //�������ߵĹ���״̬
            ResetEntityAttackState();
            //��ǰ�غϽ����������Ϸ���
            if (FindAlive(m_listEnemy)==null)
            {
                mbattleState = BattleState.Win;
                UBattleResult.Instance.Show(mbattleState);
            }
            else
            {
                //ת�����з���Ӫ����
                mbattleState = BattleState.EnEmy;
                ChangeCast();//�ݹ鱾����
            }
        }
    }
    public void Damage(float damage)
    {
        m_bycaster.Damage(damage);
    }
    public void ResetMoveEvent()
    {
        m_caster.ResetMoveEvent();
    }
    /// <summary>
    /// Ѱ�Ҵ��ĵ�λ
    /// </summary>
    /// <returns></returns>
    public Entity FindAlive(List<Entity> list)
    {
        int index = Random.Range(0, 3);

        Entity target = list[index];
        if (target.mbIsDead)
        {
            target = null;
            foreach(Entity entity in list)
            {
                if (!entity.mbIsDead)
                {
                    target = entity;
                    break;
                }
            }
        }
        return target;

    }
    public   void SetCasterAndBycaster(Entity c,Entity bc)
    {
        m_caster = c;
        m_bycaster = bc;
    }
    /// <summary>
    /// ����
    /// </summary>
    private void ResetEntityAttackState()
    {
        foreach(Entity item in m_listEnemy)
        {
            item.mbIsAleadyAttack = false;
        }
        foreach (Entity item in m_listPlayer)
        {
            item.mbIsAleadyAttack = false;
        }
    }
}
