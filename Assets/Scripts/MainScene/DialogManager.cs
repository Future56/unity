using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;
    //���ȸ���Npc����ȷ�ϣ�Ȼ���������״̬��
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
    /// ����Npc���ַ��ضԻ�����
    /// </summary>
    /// <param name="strNpcName">npc������</param>
    /// <returns>�Ի�����</returns>
    public Dictionary<emTaskState,List<string>> GetDialog(string strNpcName)
    {
        Dictionary<emTaskState, List<string>> dic;
        if(m_dicDialogs.TryGetValue(strNpcName,out dic)==false)
        {
            Debug.LogErrorFormat("û�дӶԻ��������ҵ�{0}������", strNpcName);
           
        }
        return dic;
    }
    /// <summary>
    /// �����ݳ�ʼ��
    /// </summary>
    private void InitData()
    {
        m_dicDialogs =new Dictionary<string, Dictionary<emTaskState, List<string>>>();
        //-------------------------------------С�����ĶԻ��������ݴ洢---------------------------------------------
        Dictionary<emTaskState, List<string>> dic1 = new Dictionary<emTaskState, List<string>>();
        //
        List<string> d1_list1 = new List<string>(){
            "С����|������...",
            "Ƥ����|��ô�ˣ�˭�۸�����",
            "С����|���ױ����漦°����",
            "Ƥ����|��Ҫ���ˣ���ȥ�������ף���...��...ȥ�ľ���",
            "С����|�⸽���и���ʱ���š������׾��������ﱻ���漦°�ߵ�",
            "Ƥ����|ʱ����?Ƥ��������ɻ���������۾�",
            "С����|�����Աߣ�Ҫ������ʱ���ţ���ʾ������Ψһ�İ취��Ҫȥ���ҹ� ����ȡ��ʱ���ŵ�Կ��",
        };
        //�����Ѿ���ȡ��δ��ɵĶԻ�
        List<string> d1_list2 = new List<string>(){
            "С����|Ƥ���𣬶��ҹ�����ʱ���ŵ�Կ������"
        };
        //�������ʱ�ĶԻ�
        List<string> d1_list3 = new List<string>(){
            "С����|�쿴��ʱ��֮�ų����ˣ�oh yeah,�����о��~"
        };
        dic1.Add(emTaskState.Available, d1_list1);
        dic1.Add(emTaskState.Incomplete, d1_list2);
        dic1.Add(emTaskState.Complete, d1_list3);
        m_dicDialogs.Add("С����", dic1);
        //----------------------------------���ҹ��ĶԻ����ݴ洢-----------------------------------------
        Dictionary<emTaskState, List<string>> dic2 = new Dictionary<emTaskState, List<string>>();
        //���񲻿ɽӵ�ʱ��ĶԻ�
        List<string> d2_list1 = new List<string>()
        {
            "���ҹ�|HI��Ƥ��������������С������֪��Ϊʲô���ˣ���ȥ��������",
        };
        //���������ȡ�Ի�
        List<string> d2_list2 = new List<string>()
        {
            "���ҹ�|�꣬��֪��������⣬����ʱ��֮�ŵ�Կ�ף���ȥ���������װɣ����...ϣ�����ܿ��������̫��",
        };
        List<string> d2_list3 = new List<string>()
        {
            "���ҹ�|�ȿȣ��ȿ���ʱ���ŵ�Կ���Ѿ����㿩�����ڿ�ȥ��С������",
        };
        dic2.Add(emTaskState.UnAvailable, d2_list1);
        dic2.Add(emTaskState.Available, d2_list2);
        dic2.Add(emTaskState.Complete, d2_list3);
        m_dicDialogs.Add("���ҹ�", dic2);
        //----------------------------------������ʦ�ĶԻ����ݴ洢-----------------------------------------
        Dictionary<emTaskState, List<string>> dic3 = new Dictionary<emTaskState, List<string>>();
        //����ɽӵ�ʱ��ĶԻ�
        List<string> d3_list1 = new List<string>()
        {
            "������ʦ|HI����ã�Ƥ���𣬻�ӭ����Vip Skill����",
        };
        //�����Ѿ���ȡ����δ��ɵĶԻ�
        List<string> d3_list2 = new List<string>()
        {
            "������ʦ|Ƥ��������Ҫ�Ҷ��ҹ������ڼң��Ͽ�ȥ������",
        };
        //�����ȡ�����
        List<string> d3_list3 = new List<string>()
        {
            "������ʦ|Ƥ������˵��Ҫȥ��С�����������ˣ�ϣ���ܻ��Ż���",
        };
        dic3.Add(emTaskState.Available, d3_list1);
        dic3.Add(emTaskState.Incomplete, d3_list2);
        dic3.Add(emTaskState.Complete, d3_list3);
        m_dicDialogs.Add("������ʦ", dic3);
    }
}
