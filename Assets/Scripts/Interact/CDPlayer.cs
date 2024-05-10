using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CDPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    //������볪Ƭ������ײ��Ӵ�ʱִ�е����
    {
        //��������ң����Ǳ��ʲô���ﶼ�ܴ���
        if(collision.GetComponent<Player>()  != null)
        {
            //��ʾ���ﴦ�ڿɴ�����������������ڣ���ʾ������ʾ
            UI.instance.SetWhetherShowInteractToolTip(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    //������뿪��Ƭ������ײ�䷶Χʱִ�е����
    {
        if (collision.GetComponent<Player>() != null)
        {
            //�رհ�����ʾ
            UI.instance.SetWhetherShowInteractToolTip(false);

            //���뿪ʱ��Ƭ��UI�ǿ����ģ���ر�
            if (UI.instance.cdPlayerUI.activeSelf)
                UI.instance.SwitchToUI(UI.instance.inGameUI);
        }
    }
}