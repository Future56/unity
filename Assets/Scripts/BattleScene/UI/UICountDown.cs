using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UICountDown : MonoBehaviour
{
    public Text mText;
    private int m_iMaxSecond=3;
    // Start is called before the first frame update

    void Start()
    {
        InvokeRepeating("CountDownFunc", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CountDownFunc()
    {
        m_iMaxSecond -= 1;
        if (m_iMaxSecond > 0)
        {
            mText.text = m_iMaxSecond.ToString();
        }
        else
        {
            mText.text = "GO!";
            CancelInvoke("CountDownFunc");
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
        //倒计时结束后显示己方回合，开始进攻
        UISkillGroup.Instance.Show();
    }
}
