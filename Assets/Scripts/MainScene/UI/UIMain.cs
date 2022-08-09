using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMain : MonoBehaviour
{
    public GameObject mobjDialog;//对话框
    public GameObject mobjTask;//任务框
    private UIMain_Dialog m_dialog;
    private UIMain_Task m_task;
    public static UIMain Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        m_task=mobjTask.AddComponent<UIMain_Task>();
        m_dialog=mobjDialog.AddComponent<UIMain_Dialog>();
        mobjDialog.SetActive(false);
        mobjTask.SetActive(false);
    }
    void Start()
    {

    }
    public void ShowTask(Npc npc)
    {
        m_task.Show(npc);
    }
    public void CloswTask()
    {
        m_task.Close();
    }
    public void ShowDialog(Npc npc)
    {
        Debug.Log("here");
        m_dialog.Show(npc);
    }
    public void CloseDialog()
    {
        m_dialog.Close();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
