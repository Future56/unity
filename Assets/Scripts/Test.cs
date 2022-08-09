using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform c;
    public Transform s;
    private Vector3 vector;
    // Start is called before the first frame update
    void Start()
    {
        vector = c.position - s.position;
    }

    // Update is called once per frame
    void Update()
    {
        c.position += transform.forward * Time.deltaTime * 8f;
        s.position = c.position - vector;
    }
}
