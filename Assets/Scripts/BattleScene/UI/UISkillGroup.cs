using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISkillGroup : MonoBehaviour
{
    public GameObject mPanel;
    public Text mtxtTurnTips;
    public Text mtxtOptTips;
    public static UISkillGroup Instance;

    private Entity m_player;
    private Entity m_target;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        mPanel.SetActive(false);
        m_player = GameObject.FindWithTag("Player").GetComponent<Entity>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mtxtTurnTips.text = BattleManage.Instance.mbattleState == BattleState.Player ? "己方回合" : "敌方回合";
        if (Input.GetMouseButton(0))
        {
            //没有死亡，没有攻击过，我们的回合
            if ((BattleManage.Instance.mbattleState == BattleState.Player)&&
                m_player.mbIsDead==false &&m_player.mbIsAleadyAttack==false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit))
                {
                    if(hit.collider.tag=="Enemy1"|| hit.collider.tag == "Enemy2"|| hit.collider.tag == "Enemy3")
                    {
                        Entity emeny = hit.collider.GetComponent<Entity>();
                        if (emeny.mbIsDead == false)
                        {
                            m_target = emeny;
                            mtxtOptTips.gameObject.SetActive(false);
                            Debug.Log("当前选中的敌人是{0}"+ hit.collider.gameObject.name); 
                        }
                    }
                }
            }
        }
    }
    public void Show()
    {
        mPanel.SetActive(true);
    }
    /// <summary>
    /// 释放一技能
    /// </summary>
    public void OnClickSkill01()
    {
        if (CanUseSkill())
        {
            m_player.CaskSkill(m_target,SkillType.Skill01);
            Debug.Log("发起进攻1");
            BattleManage.Instance.SetCasterAndBycaster(m_player, m_target);
            m_target = null;
        }
        else
        {
            StartCoroutine(ShowTips());
        }
       
    }

    /// <summary>
    /// 释放二技能
    /// </summary>
    public void OnClickSkill02()
    {
        if (CanUseSkill())
        {
            m_player.CaskSkill(m_target, SkillType.Skill02);
            Debug.Log("发起进攻2");
            BattleManage.Instance.SetCasterAndBycaster(m_player, m_target);
            m_target = null; 
        }
        else
        {
            StartCoroutine(ShowTips());
        }

    }

    /// <summary>
    /// 释放二技能
    /// </summary>
    public void OnClickSkill03()
    {
        if (CanUseSkill())
        {
            m_player.CaskSkill(m_target, SkillType.Skill03);
            Debug.Log("发起进攻3");
            BattleManage.Instance.SetCasterAndBycaster(m_player, m_target);
            m_target = null;
        }
        else
        {
            StartCoroutine(ShowTips());
        }

    }
   
    /// <summary>
    /// 判断能否使用技能
    /// </summary>
    /// <returns></returns>
    private bool CanUseSkill()
    {
        if (BattleManage.Instance.mbattleState == BattleState.EnEmy)
        {
            mtxtOptTips.text = "当前为敌方回合";
            return false;
        }
        else
        {
            if (m_player.mbIsDead)
            {
                mtxtOptTips.text = "你已阵亡";
                return false;
            }
            else if (m_player.mbIsAleadyAttack)
            {
                mtxtOptTips.text = "你已经发起过进攻";
                return false;
            }
            else if (m_target == null)
            {
                mtxtOptTips.text = "你需要一个目标";
                return false;
            }
        }
        return true;
    }
   
    /// <summary>
    /// 选择提示
    /// </summary>
    public void ChoseTips()
    {
        mtxtOptTips.text = "你需要一个目标";
        StartCoroutine(ShowTips());
    }
   
    /// <summary>
    /// 让提示1.5秒后消失，起一个过渡效果（更好看）
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowTips()
    {
        mtxtOptTips.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        mtxtOptTips.gameObject.SetActive(false);
    }

}
