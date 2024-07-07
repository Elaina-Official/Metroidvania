using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    //������ײ��
    protected BoxCollider2D cd;
    //�����˺���С
    [SerializeField] protected int trapDamage;

    virtual protected void Awake()
    {
        cd = GetComponent<BoxCollider2D>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //����Ӧ��������ʵ�嶼�ܴ����ģ�������Һ͵���
        if (collision.GetComponent<Entity>() != null)
        {
            //���崥����Ч��
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Entity>() != null)
        {
            //�뿪�����Ч��
        }
    }
}
