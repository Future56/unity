using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damage(float danage)
    {
        BattleManage.Instance.Damage(danage);
    }
    public void ResetMoveEvent()
    {
        BattleManage.Instance.ResetMoveEvent(); 
    }
}
