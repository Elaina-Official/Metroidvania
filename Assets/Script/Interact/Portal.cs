using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private CapsuleCollider2D cd;
    private Animator anim;
    //����һ����������Ϊ����Ŀ�ĵ�
    public Portal teleportTarget;
    //�Ƿ��ڿɴ������͵�״̬����ֹ���͵�Ŀ�ĵصĴ����ź�������ô�������ײ�󴥷����ͣ��������޴���
    public bool canTeleport;

    private void OnValidate()
    //�˺�����Unity��Hierarchy�ڽ��и��ֶ���Ĳ���ʱ���ͻ���е��ã������õȵ���ʼ������Ϸʱ�Ž��и��£�����Start�����ڸ��£�
    {
        //�������������໥�󶨵�
        if (teleportTarget != null)
            teleportTarget.teleportTarget = this;
    }

    private void Start()
    {
        cd = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();

        //��ʼ����ʱ�����ÿ��Դ�������
        canTeleport = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���ɴ����򲻷�������ļ��
        if (!canTeleport)
            return;

        if (collision.GetComponent<Player>() != null)
        {
            //��ʱȡ��Ŀ��Ĵ�����ɣ���ֹһ��ȥ�ͱ�������
            teleportTarget.canTeleport = false;

            //��������
            GameObject.Find("Player").transform.position = teleportTarget.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            //�ָ�����Ĵ������
            canTeleport = true;
        }
    }
}