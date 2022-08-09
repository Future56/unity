using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Transform m_Player;
    private Vector3 m_v3ReleativePos;
    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        //根据向量减法，获得一个恒定的向量
        m_v3ReleativePos = transform.position - m_Player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_Player.position + m_v3ReleativePos;
    }
}
