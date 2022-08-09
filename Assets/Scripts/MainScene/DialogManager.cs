using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;
    //首先根据Npc名字确认，然后根据任务状态。
    private Dictionary<string, Dictionary<emTaskState, List<string>>> m_dicDialogs;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        InitData();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 根据Npc名字返回对话数据
    /// </summary>
    /// <param name="strNpcName">npc的名字</param>
    /// <returns>对话数据</returns>
    public Dictionary<emTaskState,List<string>> GetDialog(string strNpcName)
    {
        Dictionary<emTaskState, List<string>> dic;
        if(m_dicDialogs.TryGetValue(strNpcName,out dic)==false)
        {
            Debug.LogErrorFormat("没有从对话管理器找到{0}的数据", strNpcName);
           
        }
        return dic;
    }
    /// <summary>
    /// 对数据初始化
    /// </summary>
    private void InitData()
    {
        m_dicDialogs =new Dictionary<string, Dictionary<emTaskState, List<string>>>();
        //-------------------------------------小火龙的对话进行数据存储---------------------------------------------
        Dictionary<emTaskState, List<string>> dic1 = new Dictionary<emTaskState, List<string>>();
        //
        List<string> d1_list1 = new List<string>(){
            "小火龙|呜呜呜...",
            "皮卡丘|怎么了，谁欺负你啦",
            "小火龙|娘亲被火焰鸡掳走了",
            "皮卡丘|不要哭了，我去救你娘亲，可...是...去哪救呢",
            "小火龙|这附近有个“时空门”，娘亲就是在这里被火焰鸡掳走的",
            "皮卡丘|时空门?皮卡丘充满疑惑的揉了揉眼睛",
            "小火龙|在你旁边，要想让这时空门，显示出来，唯一的办法需要去斗笠菇 那里取得时空门的钥匙",
        };
        //任务已经领取还未完成的对话
        List<string> d1_list2 = new List<string>(){
            "小火龙|皮卡丘，斗笠菇给你时空门的钥匙了吗"
        };
        //任务完成时的对话
        List<string> d1_list3 = new List<string>(){
            "小火龙|快看，时空之门出来了，oh yeah,娘亲有救喽~"
        };
        dic1.Add(emTaskState.Available, d1_list1);
        dic1.Add(emTaskState.Incomplete, d1_list2);
        dic1.Add(emTaskState.Complete, d1_list3);
        m_dicDialogs.Add("小火龙", dic1);
        //----------------------------------斗笠菇的对话数据存储-----------------------------------------
        Dictionary<emTaskState, List<string>> dic2 = new Dictionary<emTaskState, List<string>>();
        //任务不可接的时候的对话
        List<string> d2_list1 = new List<string>()
        {
            "斗笠菇|HI，皮卡丘，你来的正好小火龙不知道为什么哭了，快去看看他吧",
        };
        //任务可以领取对话
        List<string> d2_list2 = new List<string>()
        {
            "斗笠菇|嘘，我知道你的来意，这是时空之门的钥匙，快去救它的娘亲吧，最后...希望你能看到明天的太阳",
        };
        List<string> d2_list3 = new List<string>()
        {
            "斗笠菇|咳咳，比卡丘，时空门的钥匙已经给你咯，现在快去找小火龙吧",
        };
        dic2.Add(emTaskState.UnAvailable, d2_list1);
        dic2.Add(emTaskState.Available, d2_list2);
        dic2.Add(emTaskState.Complete, d2_list3);
        m_dicDialogs.Add("斗笠菇", dic2);
        //----------------------------------妙蛙老师的对话数据存储-----------------------------------------
        Dictionary<emTaskState, List<string>> dic3 = new Dictionary<emTaskState, List<string>>();
        //任务可接的时候的对话
        List<string> d3_list1 = new List<string>()
        {
            "妙蛙老师|HI，你好，皮卡丘，欢迎来到Vip Skill世界",
        };
        //任务已经领取但还未完成的对话
        List<string> d3_list2 = new List<string>()
        {
            "妙蛙老师|皮卡丘，你是要找斗笠菇吗？他在家，赶快去找他吧",
        };
        //任务接取，完成
        List<string> d3_list3 = new List<string>()
        {
            "妙蛙老师|皮卡丘，听说你要去救小火龙的娘亲了，希望能活着回来",
        };
        dic3.Add(emTaskState.Available, d3_list1);
        dic3.Add(emTaskState.Incomplete, d3_list2);
        dic3.Add(emTaskState.Complete, d3_list3);
        m_dicDialogs.Add("妙蛙老师", dic3);
    }
}
