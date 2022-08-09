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
        mtxtTurnTips.text = BattleManage.Instance.mbattleState == BattleState.Player ? "�����غ�" : "�з��غ�";
        if (Input.GetMouseButton(0))
        {
            //û��������û�й����������ǵĻغ�
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
                            Debug.Log("��ǰѡ�еĵ�����{0}"+ hit.collider.gameObject.name); 
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
    /// �ͷ�һ����
    /// </summary>
    public void OnClickSkill01()
    {
        if (CanUseSkill())
        {
            m_player.CaskSkill(m_target,SkillType.Skill01);
            Debug.Log("�������1");
            BattleManage.Instance.SetCasterAndBycaster(m_player, m_target);
            m_target = null;
        }
        else
        {
            StartCoroutine(ShowTips());
        }
       
    }

    /// <summary>
    /// �ͷŶ�����
    /// </summary>
    public void OnClickSkill02()
    {
        if (CanUseSkill())
        {
            m_player.CaskSkill(m_target, SkillType.Skill02);
            Debug.Log("�������2");
            BattleManage.Instance.SetCasterAndBycaster(m_player, m_target);
            m_target = null; 
        }
        else
        {
            StartCoroutine(ShowTips());
        }

    }

    /// <summary>
    /// �ͷŶ�����
    /// </summary>
    public void OnClickSkill03()
    {
        if (CanUseSkill())
        {
            m_player.CaskSkill(m_target, SkillType.Skill03);
            Debug.Log("�������3");
            BattleManage.Instance.SetCasterAndBycaster(m_player, m_target);
            m_target = null;
        }
        else
        {
            StartCoroutine(ShowTips());
        }

    }
   
    /// <summary>
    /// �ж��ܷ�ʹ�ü���
    /// </summary>
    /// <returns></returns>
    private bool CanUseSkill()
    {
        if (BattleManage.Instance.mbattleState == BattleState.EnEmy)
        {
            mtxtOptTips.text = "��ǰΪ�з��غ�";
            return false;
        }
        else
        {
            if (m_player.mbIsDead)
            {
                mtxtOptTips.text = "��������";
                return false;
            }
            else if (m_player.mbIsAleadyAttack)
            {
                mtxtOptTips.text = "���Ѿ����������";
                return false;
            }
            else if (m_target == null)
            {
                mtxtOptTips.text = "����Ҫһ��Ŀ��";
                return false;
            }
        }
        return true;
    }
   
    /// <summary>
    /// ѡ����ʾ
    /// </summary>
    public void ChoseTips()
    {
        mtxtOptTips.text = "����Ҫһ��Ŀ��";
        StartCoroutine(ShowTips());
    }
   
    /// <summary>
    /// ����ʾ1.5�����ʧ����һ������Ч�������ÿ���
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowTips()
    {
        mtxtOptTips.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        mtxtOptTips.gameObject.SetActive(false);
    }

}
