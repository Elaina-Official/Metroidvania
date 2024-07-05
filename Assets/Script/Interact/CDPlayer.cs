using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CDPlayer : MonoBehaviour
{
    #region Components
    private BoxCollider2D cd;
    private Rigidbody rb;
    #endregion

    private void Awake()
    {
        cd = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    //������볪Ƭ������ײ��Ӵ�ʱִ�е����
    {
        //��������ң����Ǳ��ʲô���ﶼ�ܴ���
        if (collision.GetComponent<Player>()  != null)
        {
            //��ʾ���ﴦ�ڿɴ�����������������ڣ���ʾ������ʾ
            UI_MainScene.instance.SetWhetherShowInteractToolTip(true);
            //��ʾ���ڽӴ��Ŀɽ������ǳ�Ƭ��
            UI_MainScene.instance.isAtCDPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    //������뿪��Ƭ������ײ�䷶Χʱִ�е����
    {
        if (collision.GetComponent<Player>() != null)
        {
            //�رհ�����ʾ
            UI_MainScene.instance.SetWhetherShowInteractToolTip(false);
            UI_MainScene.instance.isAtCDPlayer = false;

            //���뿪ʱ��Ƭ��UI�ǿ����ģ���ر�
            //�˴��е�Ī�������bug...?
            if (UI_MainScene.instance.cdPlayerUI != null)
            {
                if (UI_MainScene.instance.cdPlayerUI.activeSelf)
                    UI_MainScene.instance.SwitchToUI(UI_MainScene.instance.inGameUI);
            }
        }
    }
}