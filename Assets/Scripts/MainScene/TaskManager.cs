using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum emTaskState{
    UnAvailable,//���ɽ�
    Available,//�ɽ�
    Incomplete,//�ѽ�ȡ��δ���
    Complete,//��ȡ�����
}
/// <summary>
/// �洢�������ĸ������ݺ�npcͷ���ϵ������źź�����״̬
/// </summary>
public class TaskDefine
{
    public Npc mNpc;//��ǰ���ʵ�Npc
    public int mTasKId;//����id
    public emTaskState mTaskState;//�������״̬

    public Unity_Task munityTask;//Npcͷ���ϵ�����״̬ͼ��
    public string mStrTitle;//�������
    public string mStrDetail;//������ϸ����
    public string mStrTargetNpcName;// ����Ѱ·��Ŀ��
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
    /// ����npc���ַ���һ���������
    /// </summary>
    /// <param name="npc"></param>
    /// <returns></returns>
    public TaskDefine GetTaskDefineByNpc(Npc npc)
    {
        TaskDefine taskDefine;
        if(m_dicTaskData.TryGetValue(npc,out taskDefine)==false){
            Debug.LogErrorFormat("û�дӶԻ��������ҵ�{0}��������", npc.name);
        }
        return taskDefine;
    }
    /// <summary>
    /// ��ÿ��Npc�����������г�ʼ��
    /// </summary>
    private void InitData()
    {
        m_dicTaskData = new Dictionary<Npc, TaskDefine>();
        //Npcͷ�ϵ�ͼ��
        GameObject model = Resources.Load<GameObject>("Prefabs/UI/Unit_Task");//��̬����
         model.SetActive(false);
        //-----------------------------------------��С����������---------------------------------
        TaskDefine task1 = new TaskDefine();
        task1.mNpc = GameObject.Find("С����").GetComponent<Npc>();
        task1.mTasKId = 1001;
        task1.mTaskState = emTaskState.Available;
        task1.munityTask = BindUnitTask(model,task1.mNpc.transform, new Vector3(-0.35f, 3.5f, 0));

        task1.mStrTitle = "�ݷá����ҹ���";
        task1.mStrDetail = "��ȡ��ʱ��֮�š�Կ��";
        task1.mStrTargetNpcName = "���ҹ�";
        m_dicTaskData.Add(task1.mNpc, task1);
        //-----------------------------------------�ֶ��ҹ�������----------------------------------------
        TaskDefine task2 = new TaskDefine();
        task2.mNpc = GameObject.Find("���ҹ�").GetComponent<Npc>();
        task2.mTasKId = 1002;
        task2.mTaskState = emTaskState.UnAvailable;
        task2.munityTask = BindUnitTask(model, task2.mNpc.transform, new Vector3(-0.35f, 3.5f, 0));
        task2.mStrTitle = "�طá�С������";
        task2.mStrDetail = "���ʱ��֮�š�";
        task2.mStrTargetNpcName = "С����";
        m_dicTaskData.Add(task2.mNpc, task2);
    }

    /// <summary>
    /// ����Npcͷ�ϵ�ͼ�꣬������λ��
    /// </summary>
    /// <param name="model">Resources�ļ��µ�prefabs</param>
    /// <param name="target">����������</param>
    /// <param name="offset">ƫ��������ʵ����������GameObject������Ϸ����Npc��ͷ��</param>
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
