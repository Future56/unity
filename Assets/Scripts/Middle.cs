using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Middle : MonoBehaviour
{
    public GameObject p;
    public Slider mslider;
    public Text progress;
    private AsyncOperation async;
    private uint nowprocess=0;
    private Animator animator;
    public Transform s;
    private Vector3 vector;
    // Start is called before the first frame update
    void Start()
    {
        vector = s.position - p.transform.position;
        animator = p.transform.Find("Anim").gameObject.GetComponent<Animator>();
        animator.SetBool("bMove", true);
        StartCoroutine(LoadScene());
        Debug.Log(s.position.x);
        Debug.Log(p.transform.position.x);
        Debug.Log(vector.x);
    }

    IEnumerator LoadScene()
    {
        if (SceneManager.Instance.currentScene == CurrentScene.Battle)
        {
            async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainScene");
            async.allowSceneActivation = false;
            yield return async;
        }
        else if(SceneManager.Instance.currentScene == CurrentScene.Main)
        {
            async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("BattleScene");
            async.allowSceneActivation = false;
            yield return async;
        }

    }
    // Update is called once per frame
    void Update()
    {
      
        if (async == null) {
            // 如果没加载完，就跳出update方法，不继续执行return下面的代码
            return;
        }

        // 进度条需要到达的进度值
        uint toProcess;
        Debug.Log(async.progress * 100);

        // async.progress 你正在读取的场景的进度值  0---0.9
        // 如果当前的进度小于0.9，说明它还没有加载完成，就说明进度条还需要移动
        // 如果，场景的数据加载完毕，async.progress 的值就会等于0.9
        if (async.progress < 0.9f)
        {
            //  进度值
            toProcess = (uint)(async.progress * 100);
        }
        // 如果能执行到这个else，说明已经加载完毕
        else
        {
            // 手动设置进度值为100
            toProcess = 100;
        }
        Debug.Log(toProcess);
        // 如果滑动条的当前进度，小于，当前加载场景的方法返回的进度
        if (nowprocess < toProcess)
        {
            // 当前滑动条的进度加一
            nowprocess++;
            progress.text = nowprocess.ToString() + "%";
        }
        if (nowprocess <= 94)
        {
            p.transform.position = s.position - vector;
        }
       
        // 设置滑动条的value
        mslider.value = nowprocess / 100f;
        //p.transform.position += new Vector3(1, 0,0)*Time.deltaTime* 0.08f * toProcess;
        // p.transform.position = Vector3.MoveTowards(p.transform.position, p.transform.position + new Vector3(0.08f*toProcess,0,0),toProcess/100);
        // 如果滑动条的值等于100，说明加载完毕
        if (nowprocess == 100)
        {
            // 设置为true的时候，如果场景数据加载完毕，就可以自动跳转场景
            async.allowSceneActivation = true;
        }
    }
}
