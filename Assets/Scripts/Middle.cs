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
            // ���û�����꣬������update������������ִ��return����Ĵ���
            return;
        }

        // ��������Ҫ����Ľ���ֵ
        uint toProcess;
        Debug.Log(async.progress * 100);

        // async.progress �����ڶ�ȡ�ĳ����Ľ���ֵ  0---0.9
        // �����ǰ�Ľ���С��0.9��˵������û�м�����ɣ���˵������������Ҫ�ƶ�
        // ��������������ݼ�����ϣ�async.progress ��ֵ�ͻ����0.9
        if (async.progress < 0.9f)
        {
            //  ����ֵ
            toProcess = (uint)(async.progress * 100);
        }
        // �����ִ�е����else��˵���Ѿ��������
        else
        {
            // �ֶ����ý���ֵΪ100
            toProcess = 100;
        }
        Debug.Log(toProcess);
        // ����������ĵ�ǰ���ȣ�С�ڣ���ǰ���س����ķ������صĽ���
        if (nowprocess < toProcess)
        {
            // ��ǰ�������Ľ��ȼ�һ
            nowprocess++;
            progress.text = nowprocess.ToString() + "%";
        }
        if (nowprocess <= 94)
        {
            p.transform.position = s.position - vector;
        }
       
        // ���û�������value
        mslider.value = nowprocess / 100f;
        //p.transform.position += new Vector3(1, 0,0)*Time.deltaTime* 0.08f * toProcess;
        // p.transform.position = Vector3.MoveTowards(p.transform.position, p.transform.position + new Vector3(0.08f*toProcess,0,0),toProcess/100);
        // �����������ֵ����100��˵���������
        if (nowprocess == 100)
        {
            // ����Ϊtrue��ʱ������������ݼ�����ϣ��Ϳ����Զ���ת����
            async.allowSceneActivation = true;
        }
    }
}
