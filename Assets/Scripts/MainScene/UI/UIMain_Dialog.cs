using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Ƥ������npc��̸�����
/// </summary>
public class UIMain_Dialog : MonoBehaviour
{
    private Text m_txtName;//��������
    private Text m_txtDetails;//˵������
    private Button m_btnNext;
    private Button m_btnClose;
    /// <summary>
    /// ��ǰNpcҪ˵�Ļ�������
    /// </summary>
    private Dictionary<emTaskState, List<string>> m_dicContents;//��ǰNpc�����жԻ�����
    private TaskDefine m_NpcTaskDefine;//��ǰNpc������ϵͳ���
    private int m_index;//��ǰ�Ի����±�
    private Player m_player;//Ƥ����
    private Npc m_npc;
    // Start is called before the first frame update
    void Awake()
    {
        m_txtName = transform.Find("Dialog/Name/Text").GetComponent<Text>();
        m_txtDetails = transform.Find("Dialog/Text").GetComponent<Text>();
        m_btnNext = transform.Find("Dialog/Name (1)/Button").GetComponent<Button>();
        m_btnClose = transform.Find("ButtonClose").GetComponent<Button>();
        m_btnClose.onClick.AddListener(OnClickClose);
        m_btnNext.onClick.AddListener(OnClickNext);
        m_player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Show(Npc npc)
    {
        m_dicContents = DialogManager.Instance.GetDialog(npc.name);//�Ի�����
        m_NpcTaskDefine=TaskManager.Instance.GetTaskDefineByNpc(npc);//�������
        m_npc = npc;
        if (m_dicContents != null)
        {
            OnClickNext();
        }
        
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        m_index = 0;
        this.gameObject.SetActive(false);//�رյ�ǰ�Ի����
    }
    private void OnClickNext()
    {
        Debug.Log("dianjia");
        List<string> list = null;
        //�����if-else����Ϊ��ֻ��С�����Ͷ��ҹ���������ϵͳ����������ʦ�ĶԻ��Ǹ���С����������״̬���ı�
        if (m_NpcTaskDefine != null)
        {
            list = m_dicContents[m_NpcTaskDefine.mTaskState];
        }
        else
        {
            
            Npc targetNpc = GameObject.Find("С����").GetComponent<Npc>();//��ȡС����
            TaskDefine targetDefine = TaskManager.Instance.GetTaskDefineByNpc(targetNpc); //��ȡС����������ϵͳ����ΪTaskDefine������״̬�ļ�¼
            list =m_dicContents[targetDefine.mTaskState];//����С����������״̬��ö�Ӧ�ĶԻ�����
        }
        //����Ի��±���ڵ��ڶԻ����ݳ���
        if (m_index >= list.Count)
        {
            //������ʦ����С����������״̬
            if (m_npc.gameObject.name == "������ʦ")
            {
                Npc targetNpc = GameObject.Find("С����").GetComponent<Npc>();
                TaskDefine targetDefine = TaskManager.Instance.GetTaskDefineByNpc(targetNpc);
                //���С����������״̬�ǽ���δ��ɣ����Զ�Ѱ·�����ҹ�����
                if (targetDefine.mTaskState == emTaskState.Incomplete)
                {
                    m_player.AutoPath(GameObject.Find(targetDefine.mStrTargetNpcName).transform.position);
                }
            }
            else if(m_NpcTaskDefine.mNpc.name == "���ҹ�")
            {
                if (m_NpcTaskDefine.mTaskState == emTaskState.Available)
                {
                    UIMain.Instance.ShowTask(m_npc);//��ʾ�������
                }
                else if(m_NpcTaskDefine.mTaskState == emTaskState.Complete)
                {
                    m_player.AutoPath(GameObject.Find(m_NpcTaskDefine.mStrTargetNpcName).transform.position);
                }
            }
            else if(m_NpcTaskDefine.mNpc.name == "С����")
            {
                //breloom�Ƕ��ҹ�,С�������ݶ��ҹ���״̬��
                Npc breloom = GameObject.Find(m_NpcTaskDefine.mStrTargetNpcName).GetComponent<Npc>();
                TaskDefine breloomDefine = TaskManager.Instance.GetTaskDefineByNpc(breloom);
                if (breloomDefine.mTaskState == emTaskState.Available)
                {
                    m_player.AutoPath(GameObject.Find(breloom.gameObject.name).transform.position);
                    //m_player.AutoPath(breloom.transform.position);
                }
                else if (breloomDefine.mTaskState == emTaskState.Complete||breloomDefine.mTaskState == emTaskState.UnAvailable)
                {
                    UIMain.Instance.ShowTask(m_npc);
                }
            }
            Close();
            return;
        }
        string str = list[m_index++];
        string[] array = str.Split('|');
        m_txtName.text = array[0];
        m_txtDetails.text = array[1];
        //if (m_index < list.Count)
        //{
        //    string str = list[m_index++];
        //    string[] array = str.Split('|');
        //    m_txtName.text = array[0];
        //    m_txtDetails.text = array[1];
        //}
        //else
        //{
        //    //����������ʾ�������
        //}


    }
    private void OnClickClose()
    {
        Close();
    }
}
