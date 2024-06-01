using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Animator anim;

    //�����ض��浵��ı�ʶ
    public string id;

    //��¼����浵���Ƿ񱻼����
    public bool isActive;

    //�������ÿ�ε��ö�������һ���µ�ID������ֻ��Ҫ����ContextMenu����һ��
    //��Unity�ڸýű��������Ҽ��ýű���������"Generate CheckPoint ID"���ɵ��ô˺���
    [ContextMenu("Generate CheckPoint ID")]
    private void GenerateID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            //�Ӵ���Ѵ�����ѵļ���״̬����ʾ�����㼤��
            ActivateCheckPoint();
        }
    }

    public void ActivateCheckPoint()
    {
        //�������Ч
        //AudioManager.instance.PlaySFX()
        
        //��¼�����״̬
        isActive = true;

        //����Ķ���
        anim.SetBool("active", true);        
    }
}
