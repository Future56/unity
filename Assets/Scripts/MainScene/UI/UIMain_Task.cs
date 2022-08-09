using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIMain_Task : MonoBehaviour
{
    private Text m_txtTitle;
    private Text m_txtDetails;
    private GameObject m_objGroup;
    private Button m_btnAccept;
    private Button m_btnDeny;
    private Button m_btnFinish;
    private Button m_btnClose;

    private TaskDefine m_taskDefine;
    private Player m_player;
    // Start is called before the first frame update
    void Awake()
    {
        m_txtTitle = transform.Find("Dialog/Title/TextTitle").GetComponent<Text>();
        m_txtDetails = transform.Find("Dialog/Description/TextDescription").GetComponent<Text>();
        m_objGroup = transform.Find("Dialog/OpenButtons").gameObject;
        m_btnAccept = m_objGroup.transform.Find("ButtonAccept").GetComponent<Button>();
        m_btnDeny = m_objGroup.transform.Find("ButtonDeny").GetComponent<Button>();
        m_btnFinish = transform.Find("Dialog/submitButtons/ButtonFinish").GetComponent<Button>();
        m_btnClose = transform.Find("Dialog/ButtonClose").GetComponent<Button>();
        m_btnAccept.onClick.AddListener(OnClickAccept);
        m_btnDeny.onClick.AddListener(OnClickDeny);
        m_btnFinish.onClick.AddListener(OnClickFinish);
        m_btnClose.onClick.AddListener(OnClickClose);
        m_player = GameObject.Find("�ȿ���").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Show(Npc npc)
    {
        Debug.LogFormat("��ʾ��ǰNPc={0}������", npc.name);
        m_taskDefine = TaskManager.Instance.GetTaskDefineByNpc(npc);
        m_txtTitle.text = m_taskDefine.mStrTitle;
        m_txtDetails.text = m_taskDefine.mStrDetail;
        //�������ɽӣ�����ʾ���ܻ�ܾ�����
        if (m_taskDefine.mTaskState == emTaskState.Available)
        {
            m_btnAccept.gameObject.SetActive(true);
            m_btnDeny.gameObject.SetActive(true);
            m_btnFinish.gameObject.SetActive(false);
        }
        //���
        else if (m_taskDefine.mTaskState == emTaskState.Incomplete)
        {
            //����ֻ��С����������״̬ΪIncomplete
            //�ȼ�鶷�ҹ�������״̬
            Npc breloom = GameObject.Find(m_taskDefine.mStrTargetNpcName).GetComponent<Npc>();
            TaskDefine breloomTaskDefine = TaskManager.Instance.GetTaskDefineByNpc(breloom);
            //������ҹ�������״̬�������
            if (breloomTaskDefine.mTaskState == emTaskState.Complete)
            {
                m_btnAccept.gameObject.SetActive(false);
                m_btnDeny.gameObject.SetActive(false);
                m_btnFinish.gameObject.SetActive(true);
            }
        }
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
    private void OnClickFinish()
    {
        Close();
        m_taskDefine.mTaskState = emTaskState.Complete;
        //��ʾ������
        GameObject obj = GameObject.Find("EFX_Portal_prefab");
        obj.transform.Find("chuansongmen_anim").gameObject.SetActive(true);
        
        obj.transform.Find("chuansongmen_anim/Cube").gameObject.AddComponent<Portal>();
    }

    private void OnClickClose()
    {
        Close();
    }

    private void OnClickDeny()
    {
        Close();
    }

    private void OnClickAccept()
    {
        Npc breloom = GameObject.Find(m_taskDefine.mStrTargetNpcName).GetComponent<Npc>();
        TaskDefine breloomTaskDefine = TaskManager.Instance.GetTaskDefineByNpc(breloom);
        //���������Ϊ1001��С������
        if (m_taskDefine.mTasKId == 1001)
        {
            //��������ǿɽӣ�����ܺ��ɽ���δ��ɣ�Ȼ�����ö��ҹ�������״̬��Ѱ·ȥ���ҹ�����
            if (m_taskDefine.mTaskState == emTaskState.Available)
            {
                m_taskDefine.mTaskState = emTaskState.Incomplete;

                breloomTaskDefine.mTaskState = emTaskState.Available;
                //Ѱ·Ŀ�궷�ҹ�
                m_player.AutoPath(breloom.transform.position);
            }
        }
        else if(m_taskDefine.mTasKId == 1002)
        {
            //breloomTaskDefine.mTaskState = emTaskState.Complete;
            m_taskDefine.mTaskState = emTaskState.Complete;
            m_player.AutoPath(GameObject.Find(m_taskDefine.mStrTargetNpcName).transform.position);
        }
        Close();
    }
}
