using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }
        SceneManager.Instance.currentScene = CurrentScene.Main;
        SceneManager.Instance.ChangeScene("MiddleScene");

    }
}
