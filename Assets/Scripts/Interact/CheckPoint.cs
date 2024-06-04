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
        //��������ң����Ǳ��ʲô���ﶼ�ܴ���
        if (collision.GetComponent<Player>() != null)
        {
            //��ʾ���ﴦ�ڿɴ�����������������ڣ���ʾ������ʾ
            UI_MainScene.instance.SetWhetherShowInteractToolTip(true);
            //��ʾ���ڽӴ��Ŀɽ�������������
            UI_MainScene.instance.isAtCheckPoint = true;
            //�����Լ��ı����Ա������
            UI_MainScene.instance.touchedCheckPoint = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            //�رհ�����ʾ
            UI_MainScene.instance.SetWhetherShowInteractToolTip(false);
            UI_MainScene.instance.isAtCheckPoint = false;
            //���ٹ��ڶԷ������Լ���Ȩ��
            UI_MainScene.instance.touchedCheckPoint = null;
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
