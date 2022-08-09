using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Unity_Entity : MonoBehaviour
{
    private GameObject m_objModel;//保存创建的人物名字血条的实例
    private Text m_txtCharacterName;//人物名字
    private GameObject m_objHPBar;//血条的父物体
    private Image m_hp;//血条
    private float m_fHP;
    public Vector3 mv3OffSet;//人物名字的偏移量
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
    /// 血条的减少
    /// </summary>
    /// <param name="damage">伤害值</param>
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
