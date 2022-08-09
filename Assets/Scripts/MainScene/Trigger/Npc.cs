using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������Ƿ񿿽�NPC���н��룬�뿪����������״̬
/// </summary>
public enum emTriggerType
{
    None,
    Enter,
    Exit,
}
public class Npc : MonoBehaviour
{

    private Transform m_tfPlayer;//������
    private emTriggerType m_emTriggerType;//
    public float mfRotateSpeed;//NPc��ת���ٶ�
    private float m_fTimer;//Npc��ת��ʱ��
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
            //������������������õ�����ָ��Ƥ���������
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
        //����״̬����updateʹ��
        if (other.transform != m_tfPlayer)
        {
            return;
        }
        Debug.Log("OnTriggerEnter��" + other.gameObject.name);
        m_emTriggerType = emTriggerType.Enter;

        //�����Ի�
        UIMain.Instance.ShowDialog(this);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform != m_tfPlayer)
        {
           
            return;
        }
        m_emTriggerType = emTriggerType.Exit;
        Debug.Log("OnTriggerExit��" + other.gameObject.name);
        UIMain.Instance.CloseDialog();
    }
}
