using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UI_CDPlayer : MonoBehaviour
{
    public void ClickToPlayCD(int _cdIndex)
    {
        //��Ƭ����ť����Ч
        AudioManager.instance.PlaySFX(7, null);
        //������cd
        AudioManager.instance.isPlayCD = true;
        //����ָ����cd
        AudioManager.instance.PlayCD(_cdIndex);
    }

    public void ClickToReturnToBGM()
    {
        //����cd�����������ʱ��֪������ж�cd�������ˣ�������һ��ť�ֶ��ָ�����bgm
        AudioManager.instance.StopAllCD();
        AudioManager.instance.isPlayCD = false;
        AudioManager.instance.isPlayBGM = true;
    }
}