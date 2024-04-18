using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
//����̳е���MonoBehaviour������Updateһֱ��ˢ��
{
    //����ͨ��PlayerManager.instance��������������ڵĳ�Ա��������player��Ҫ��PlayerManager.instance.player.position������PlayerManager.instance.position������ı��ٸ���ˣ�
    //�����ֻ����һ�����󣬲�Ȼ�������
    public static PlayerManager instance {  get; private set; }

    //����ͨ��PlayerManager.instance.player��������ط����ʵ�player��������Ҫ��GameObject.Find("Player")���ַ�ʽ
    //��Ҫ�����player�󶨵���Manager����Ҫ��Unity�ڴ���һ��PlayerManager���󣬽�Player����󶨵�����ű����ű��󶨵�Manager���󣩵Ĵ˳�Ա������
    public Player player;

    private void Awake()
    {
        //ȷ��ֻ��һ��instance�ڹ�������ֹ������
        if (instance != null)
        {
            //ɾ�����������Ľű�
            //Destroy(instance);
            //ֱ��ɾ���������ű����ڵĶ���
            Destroy(instance.gameObject);
            Debug.Log("Invalid GameObject containing PlayerManager's instance DESTROYED");
        }
        else
            instance = this;
    }
}