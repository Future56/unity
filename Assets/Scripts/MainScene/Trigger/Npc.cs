using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主人物是否靠近NPC，有进入，离开，和无三种状态
/// </summary>
public enum emTriggerType
{
    None,
    Enter,
    Exit,
}
public class Npc : MonoBehaviour
{

    private Transform m_tfPlayer;//主人物
    private emTriggerType m_emTriggerType;//
    public float mfRotateSpeed;//NPc旋转的速度
    private float m_fTimer;//Npc旋转的时间
    private Quaternion m_srcRotation;
    // Start is called before the first frame update
    void Start()
    {
        m_tfPlayer = GameObject.FindObjectOfType<Player>().transform;
        m_emTriggerType = emTriggerType.None;
        m_srcRotation = transform.rotation;
        mfRotateSpeed = 2.5f;
        m_fTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_emTriggerType == emTriggerType.Enter)
        {
            //根据向量减法，相减得到妙蛙指向皮卡丘的向量
            Vector3 dir = m_tfPlayer.transform.position - this.transform.position;
            Quaternion targetQ=Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetQ, Time.deltaTime * mfRotateSpeed);
            m_fTimer += Time.deltaTime;
            if (m_fTimer > 0.5f)
            {
                m_fTimer = 0;
                m_emTriggerType = emTriggerType.None;
            }
        }
        else if (m_emTriggerType == emTriggerType.Exit)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, m_srcRotation, Time.deltaTime * mfRotateSpeed);
            m_fTimer += Time.deltaTime;
            if (m_fTimer > 0.5f)
            {
                m_fTimer = 0;
                m_emTriggerType = emTriggerType.None;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //设置状态，再update使用
        if (other.transform != m_tfPlayer)
        {
            return;
        }
        Debug.Log("OnTriggerEnter：" + other.gameObject.name);
        m_emTriggerType = emTriggerType.Enter;

        //弹出对话
        UIMain.Instance.ShowDialog(this);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform != m_tfPlayer)
        {
           
            return;
        }
        m_emTriggerType = emTriggerType.Exit;
        Debug.Log("OnTriggerExit：" + other.gameObject.name);
        UIMain.Instance.CloseDialog();
    }
}
