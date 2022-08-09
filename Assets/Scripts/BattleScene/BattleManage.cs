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

    //施法者
    private Entity m_caster;
    //被施法者
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
    //对阵营初始化
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
                Entity target = FindAlive(m_listPlayer);//找一个活着的对手
                if (target == null)
                {
                    continue;
                }
                //开始施法
                enemy.CaskSkill(target);
                //
                SetCasterAndBycaster(enemy, target);
                return;
            }
            ResetEntityAttackState();
            //当前回合结束，检测游戏结果
            if (FindAlive(m_listPlayer)==null)
            {
                mbattleState = BattleState.Lose;
                UBattleResult.Instance.Show(mbattleState);
            }
            else
            {
                //转换到敌方阵营攻击
                mbattleState = BattleState.Player;
                ChangeCast();//递归本方法
            }
        }
        else if (mbattleState == BattleState.Player)
        {
            //如果皮卡丘单位没有死亡或未攻击过
            if(!m_player.mbIsDead && !m_player.mbIsAleadyAttack)
            {
                UISkillGroup.Instance.ChoseTips();
                return;
            }
            else
            {
                //走完foreach代表该阵营角色都发起过攻击
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
                    Entity target= FindAlive(m_listEnemy);//找一个活着的对手
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
            //重置两边的攻击状态
            ResetEntityAttackState();
            //当前回合结束，检测游戏结果
            if (FindAlive(m_listEnemy)==null)
            {
                mbattleState = BattleState.Win;
                UBattleResult.Instance.Show(mbattleState);
            }
            else
            {
                //转换到敌方阵营攻击
                mbattleState = BattleState.EnEmy;
                ChangeCast();//递归本方法
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
    /// 寻找存活的单位
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
    /// 重置
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
