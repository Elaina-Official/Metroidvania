using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UI_CDPlayer : MonoBehaviour
{
    public void ClickToPlayCD(int _cdIndex)
    {
        //��Ƭ����ť����Ч
        Audio_Manager.instance.PlaySFX(7, null);
        //������cd
        Audio_Manager.instance.isPlayCD = true;
        //����ָ����cd
        Audio_Manager.instance.PlayCD(_cdIndex);
    }

    public void ClickToReturnToBGM()
    {
        //����cd�����������ʱ��֪������ж�cd�������ˣ�������һ��ť�ֶ��ָ�����bgm
        Audio_Manager.instance.StopAllCD();
        Audio_Manager.instance.isPlayCD = false;
        Audio_Manager.instance.isPlayBGM = true;
    }
}