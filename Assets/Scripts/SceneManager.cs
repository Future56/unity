using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentScene
{
    Main,
    Middle,
    Battle,
}
public class SceneManager : MonoBehaviour
{
    public CurrentScene currentScene;
    public static SceneManager Instance;
    // Start is called before the first frame update
    public bool IsMainScene
    {
        get
        {
            UnityEngine.SceneManagement.Scene curScene=UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            //if (curScene.name == "MainScene")
            //{
            //    return true;
            //}
            //return false;
            return curScene.name == "MainScene";
        }
    }
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeScene(string strName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(strName);
    }
    public IEnumerator ChangeScene(string strName,float time)
    {
        yield return new WaitForSeconds(time);
        UnityEngine.SceneManagement.SceneManager.LoadScene(strName);
    }
}
