using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum emTaskState{
    UnAvailable,//不可接
    Available,//可接
    Incomplete,//已接取，未完成
    Complete,//接取，完成
}
/// <summary>
/// 存储任务面板的各个数据和npc头顶上的任务信号和任务状态
/// </summary>
public class TaskDefine
{
    public Npc mNpc;//当前访问的Npc
    public int mTasKId;//任务id
    public emTaskState mTaskState;//任务接受状态

    public Unity_Task munityTask;//Npc头顶上的任务状态图标
    public string mStrTitle;//任务标题
    public string mStrDetail;//任务详细描述
    public string mStrTargetNpcName;// 任务寻路的目标
}
/// <summary>
/// 
/// </summary>
public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;
    private Dictionary<Npc, TaskDefine> m_dicTaskData;

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
        if (SceneManager.Instance.currentScene==CurrentScene.Main)
        {
            foreach (KeyValuePair<Npc, TaskDefine> keyValue in m_dicTaskData)
            {
                keyValue.Value.munityTask.UpdateTaskState(keyValue.Value.mTaskState);
            }
        }

    }
    /// <summary>
    /// 根据npc名字返回一个任务面板
    /// </summary>
    /// <param name="npc"></param>
    /// <returns></returns>
    public TaskDefine GetTaskDefineByNpc(Npc npc)
    {
        TaskDefine taskDefine;
        if(m_dicTaskData.TryGetValue(npc,out taskDefine)==false){
            Debug.LogErrorFormat("没有从对话管理器找到{0}的任务定义", npc.name);
        }
        return taskDefine;
    }
    /// <summary>
    /// 对每个Npc的任务面板进行初始化
    /// </summary>
    private void InitData()
    {
        m_dicTaskData = new Dictionary<Npc, TaskDefine>();
        //Npc头上的图标
        GameObject model = Resources.Load<GameObject>("Prefabs/UI/Unit_Task");//动态加载
         model.SetActive(false);
        //-----------------------------------------种小火龙的任务---------------------------------
        TaskDefine task1 = new TaskDefine();
        task1.mNpc = GameObject.Find("小火龙").GetComponent<Npc>();
        task1.mTasKId = 1001;
        task1.mTaskState = emTaskState.Available;
        task1.munityTask = BindUnitTask(model,task1.mNpc.transform, new Vector3(-0.35f, 3.5f, 0));

        task1.mStrTitle = "拜访【斗笠菇】";
        task1.mStrDetail = "获取“时空之门”钥匙";
        task1.mStrTargetNpcName = "斗笠菇";
        m_dicTaskData.Add(task1.mNpc, task1);
        //-----------------------------------------种斗笠菇的任务----------------------------------------
        TaskDefine task2 = new TaskDefine();
        task2.mNpc = GameObject.Find("斗笠菇").GetComponent<Npc>();
        task2.mTasKId = 1002;
        task2.mTaskState = emTaskState.UnAvailable;
        task2.munityTask = BindUnitTask(model, task2.mNpc.transform, new Vector3(-0.35f, 3.5f, 0));
        task2.mStrTitle = "回访【小火龙】";
        task2.mStrDetail = "激活“时空之门”";
        task2.mStrTargetNpcName = "小火龙";
        m_dicTaskData.Add(task2.mNpc, task2);
    }

    /// <summary>
    /// 创建Npc头上的图标，并设置位置
    /// </summary>
    /// <param name="model">Resources文件下的prefabs</param>
    /// <param name="target">？？？？？</param>
    /// <param name="offset">偏移量，让实例化出来的GameObject放在游戏场景Npc的头上</param>
    private Unity_Task BindUnitTask(GameObject model,Transform target,Vector3 offset)
    {
        GameObject obj= Instantiate<GameObject>(model);
        obj.transform.parent = GameObject.Find("UnitTaskRoot").transform;
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        obj.SetActive(true);
        Unity_Task unityTask = obj.AddComponent<Unity_Task>();
        unityTask.Init(target, offset);
        return unityTask;
    }

}
