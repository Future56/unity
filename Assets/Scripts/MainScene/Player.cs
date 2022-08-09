using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Player : MonoBehaviour
{
    public enum emMoveType
    {
        Mathy,
        CharacterController,
        Rigidbody
    }
    public enum emMathfSubtype
    {
           Mathf,
           Translate,
           MoveTowards
    }
    public enum emCharacterControllerSubType
    {
        Move,
        Simple
    }
    public enum emRigidbodySubType
    {
        Velocity,
        AddForce,
        MovePosition,
    }
    public float mfRotateSpeed;
    public float mfMoveSpeed;
    public float mAddForceFactor;
    [SerializeField]
    private emMoveType m_emMoveType=emMoveType.Mathy;
    public emMathfSubtype m_emMathfSubtype=emMathfSubtype.Mathf;
    public emCharacterControllerSubType m_emCharacterControllerSubType=emCharacterControllerSubType.Move;
    public emRigidbodySubType m_emRigidbodySubType=emRigidbodySubType.AddForce;
    private CharacterController m_characterController;
    private Rigidbody m_rigidbody;
    private CapsuleCollider c;
    private Animator m_anim;

    private NavMeshAgent m_agent;
    private bool m_isAutoMove;//判断Npc此时是否在走动寻路中
    private Vector3 M_v3Target;
    // Start is called before the first frame update
    void Start()
    {
        m_anim = transform.Find("Anim").GetComponent<Animator>();
        m_agent = transform.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_emMoveType == emMoveType.CharacterController)
        {

            if (m_rigidbody != null)
            {
                Destroy(m_rigidbody);
                Destroy(c);
            }
            if (m_characterController == null)
            {
                m_characterController = transform.gameObject.AddComponent<CharacterController>();
            }
            m_characterController.center = new Vector3(0, 0.66f, 0);
            m_characterController.radius = 0.5f;
            m_characterController.height = 1.6f;

        }
        else if (m_emMoveType == emMoveType.Rigidbody)
        {

            if (m_characterController != null)
            {
                Destroy(m_characterController);
            }
            if (m_rigidbody == null)
            {
               m_rigidbody = transform.gameObject.AddComponent<Rigidbody>();
               c= transform.gameObject.AddComponent<CapsuleCollider>();
            }
            m_rigidbody.constraints = RigidbodyConstraints.FreezePositionY|RigidbodyConstraints.FreezeRotation;
            
            c.center= new Vector3(0, 0.66f, 0);
            c.height = 1.6f;
            c.radius = 0.5f;
        }
        else
        {
            Destroy(m_characterController);
            Destroy(m_rigidbody);
            Destroy(c);
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        m_anim.SetBool("bMove", h != 0 || v != 0 ||m_isAutoMove==true);
        //如果按了键盘或者到达目的地
        if (h != 0 || v != 0 ||Vector3.Distance(transform.position,M_v3Target)<=3f)
        {
            m_isAutoMove = false;
            m_agent.isStopped = true;
            M_v3Target = Vector3.zero;
        }
        if (h != 0 || v != 0)
        {
            
            Vector3 dir = new Vector3(h, 0, v);
            dir = -dir;
            Quaternion targetQ = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetQ, Time.deltaTime * mfRotateSpeed);
            switch (m_emMoveType)
            {
                case emMoveType.Mathy:
                    if (m_emMathfSubtype == emMathfSubtype.Mathf)
                    {
                        //transform.position += dir*Time.deltaTime*mfMoveSpeed;
                        transform.position += transform.forward * Time.deltaTime * mfMoveSpeed;
                    }
                    else if (m_emMathfSubtype == emMathfSubtype.Translate)
                    {
                        transform.Translate(dir * Time.deltaTime * mfMoveSpeed, Space.World);
                    }
                    else if (m_emMathfSubtype == emMathfSubtype.MoveTowards)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward * Time.deltaTime * mfMoveSpeed, Time.deltaTime * mfMoveSpeed);
                    }
                    break;
                case emMoveType.CharacterController:
                    if (m_emCharacterControllerSubType == emCharacterControllerSubType.Move)
                    {
                        //m_characterController.Move(-transform.up * Time.deltaTime * mfMoveSpeed);
                        m_characterController.Move(dir * Time.deltaTime * mfMoveSpeed);
                    }
                    else if (m_emCharacterControllerSubType == emCharacterControllerSubType.Simple)
                    {
                        //simpleMove是按秒移动,且能模拟出重力效果
                        m_characterController.SimpleMove(transform.forward * mfMoveSpeed);
                    }
                    break;
                case emMoveType.Rigidbody:
                    //velocity和AddForce不需要乘Time.deltaTime
                    if (m_emRigidbodySubType == emRigidbodySubType.Velocity)
                    {
                        m_rigidbody.velocity = transform.forward * mfMoveSpeed;
                    }
                    else if (m_emRigidbodySubType == emRigidbodySubType.AddForce)
                    {
                        m_rigidbody.AddForce(dir * mfMoveSpeed * mAddForceFactor);
                    }
                    else if (m_emRigidbodySubType == emRigidbodySubType.MovePosition)
                    {
                        m_rigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * mfMoveSpeed);
                    }
                    break;
            }
        }
        else
        {

        }
    }
    public void AutoPath(Vector3 pos)
    {
        m_isAutoMove = true;
        M_v3Target = pos;
        m_agent.isStopped = false;
        m_agent.destination = pos;
        m_agent.speed = mfMoveSpeed;
    }
}
