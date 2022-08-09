using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Unity_Entity : MonoBehaviour
{
    private GameObject m_objModel;//���洴������������Ѫ����ʵ��
    private Text m_txtCharacterName;//��������
    private GameObject m_objHPBar;//Ѫ���ĸ�����
    private Image m_hp;//Ѫ��
    private float m_fHP;
    public Vector3 mv3OffSet;//�������ֵ�ƫ����
    // Start is called before the first frame update
    void Start()
    {
        GameObject uiUnit = Resources.Load<GameObject>("Prefabs/UI/Unit_Entity");
        uiUnit.SetActive(false);
        m_objModel = Instantiate<GameObject>(uiUnit);
        m_objModel.transform.parent = GameObject.Find("UnitEntityRoot").transform;
        m_txtCharacterName = m_objModel.transform.Find("Context/txtCharacterName").GetComponent<Text>();
        m_objHPBar = m_objModel.transform.Find("Context/HPBar").gameObject;
        m_hp = m_objHPBar.transform.Find("hp").GetComponent<Image>();
        m_txtCharacterName.text = this.gameObject.name;
        m_objHPBar.SetActive(!SceneManager.Instance.IsMainScene);
        m_objModel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        m_objModel.transform.position=Camera.main.WorldToScreenPoint(this.transform.position + mv3OffSet);
    }
    public void SetHp(float hp)
    {
        m_fHP = hp;
    }
    /// <summary>
    /// Ѫ���ļ���
    /// </summary>
    /// <param name="damage">�˺�ֵ</param>
    public void Damage(float damage)
    {
        m_fHP -= damage;
        if (m_fHP <= 0)
        {
            m_fHP = 0;
        }
        m_hp.fillAmount = m_fHP / 100;
    }
}
