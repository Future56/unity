using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 皮卡丘与npc的谈话面板
/// </summary>
public class UIMain_Dialog : MonoBehaviour
{
    private Text m_txtName;//讲话的人
    private Text m_txtDetails;//说的内容
    private Button m_btnNext;
    private Button m_btnClose;
    /// <summary>
    /// 当前Npc要说的话的数据
    /// </summary>
    private Dictionary<emTaskState, List<string>> m_dicContents;//当前Npc的所有对话数据
    private TaskDefine m_NpcTaskDefine;//当前Npc的任务系统面版
    private int m_index;//当前对话的下标
    private Player m_player;//皮卡丘
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
        m_dicContents = DialogManager.Instance.GetDialog(npc.name);//对话数据
        m_NpcTaskDefine=TaskManager.Instance.GetTaskDefineByNpc(npc);//任务面板
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
        this.gameObject.SetActive(false);//关闭当前对话面板
    }
    private void OnClickNext()
    {
        Debug.Log("dianjia");
        List<string> list = null;
        //这里的if-else是因为，只有小火龙和斗笠菇才有任务系统。而妙蛙老师的对话是根据小火龙的任务状态而改变
        if (m_NpcTaskDefine != null)
        {
            list = m_dicContents[m_NpcTaskDefine.mTaskState];
        }
        else
        {
            
            Npc targetNpc = GameObject.Find("小火龙").GetComponent<Npc>();//获取小火龙
            TaskDefine targetDefine = TaskManager.Instance.GetTaskDefineByNpc(targetNpc); //获取小火龙的任务系统，因为TaskDefine又任务状态的记录
            list =m_dicContents[targetDefine.mTaskState];//根据小火龙的任务状态获得对应的对话数据
        }
        //如果对话下标大于等于对话数据长度
        if (m_index >= list.Count)
        {
            //妙蛙老师根据小火龙的任务状态
            if (m_npc.gameObject.name == "妙蛙老师")
            {
                Npc targetNpc = GameObject.Find("小火龙").GetComponent<Npc>();
                TaskDefine targetDefine = TaskManager.Instance.GetTaskDefineByNpc(targetNpc);
                //如果小火龙的任务状态是接受未完成，则自动寻路到斗笠菇那里
                if (targetDefine.mTaskState == emTaskState.Incomplete)
                {
                    m_player.AutoPath(GameObject.Find(targetDefine.mStrTargetNpcName).transform.position);
                }
            }
            else if(m_NpcTaskDefine.mNpc.name == "斗笠菇")
            {
                if (m_NpcTaskDefine.mTaskState == emTaskState.Available)
                {
                    UIMain.Instance.ShowTask(m_npc);//显示任务面板
                }
                else if(m_NpcTaskDefine.mTaskState == emTaskState.Complete)
                {
                    m_player.AutoPath(GameObject.Find(m_NpcTaskDefine.mStrTargetNpcName).transform.position);
                }
            }
            else if(m_NpcTaskDefine.mNpc.name == "小火龙")
            {
                //breloom是斗笠菇,小火龙根据斗笠菇的状态来
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
        //    //接受任务显示任务面板
        //}


    }
    private void OnClickClose()
    {
        Close();
    }
}
