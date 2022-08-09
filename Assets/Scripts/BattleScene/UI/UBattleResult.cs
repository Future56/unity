using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 游戏结果的显示
/// </summary>
public class UBattleResult : MonoBehaviour
{
    public static UBattleResult Instance;
    public GameObject mObjPanle;
    public GameObject mObjWin;
    public GameObject mObjLoose;
    // Start is called before the first frame update
    void Start()
    {
        mObjPanle.SetActive(false);
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Show(BattleState emResult)
    {
        SceneManager.Instance.currentScene = CurrentScene.Battle;
        if (emResult == BattleState.Win)
        {
            mObjWin.SetActive(true);
            mObjLoose.SetActive(false);
            StartCoroutine(SceneManager.Instance.ChangeScene("MiddleScene",3f));
        }
        else if (emResult == BattleState.Lose)
        {
            mObjWin.SetActive(false);
            mObjLoose.SetActive(true);
            StartCoroutine(SceneManager.Instance.ChangeScene("MiddleScene",3f));
        }
        mObjPanle.SetActive(true);
    }
}
