using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Npc头上的感叹号
/// </summary>
public class Unity_Task : MonoBehaviour
{
    public  GameObject m_objAvailable;
    public GameObject m_objIncomplete;
    private Transform m_tfTarget;
    private Vector3 m_v3Offset;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject.Find不能查找到为隐藏的物体
        //transform.find可以查找到隐藏的物体，但前提是根节点必须为可见
        m_objAvailable = transform.Find("Available").gameObject;
        m_objIncomplete = transform.Find("Incomplete").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(Transform tfTarget,Vector3 v3Offset)
    {
        m_tfTarget = tfTarget;
        m_v3Offset = v3Offset;
    }
    /// <summary>
    /// 根据任务的状态，刷新头顶图标
    /// </summary>
    /// <param name="state"></param>
    public void UpdateTaskState(emTaskState state)
    {
        if (state == emTaskState.Available)
        {
            m_objAvailable.SetActive(true);
            m_objIncomplete.SetActive(false);
        }
        else if (state == emTaskState.Incomplete)
        {
            m_objAvailable.SetActive(false);
            m_objIncomplete.SetActive(true);
        }
        else if(state==emTaskState.Complete || state == emTaskState.UnAvailable)//任务已经完成和任务不可接
        {
            m_objAvailable.SetActive(false);
            m_objIncomplete.SetActive(false);
        }
        transform.position= Camera.main.WorldToScreenPoint(m_tfTarget.position + m_v3Offset);
    }
}
