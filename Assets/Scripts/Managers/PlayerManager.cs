using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ISavesManager
//����̳е���MonoBehaviour������Updateһֱ��ˢ��
{
    //����ͨ��PlayerManager.instance��������������ڵĳ�Ա��������player��Ҫ��PlayerManager.instance.player.position������PlayerManager.instance.position������ı��ٸ���ˣ�
    //�����ֻ����һ�����󣬲�Ȼ�������
    public static PlayerManager instance {  get; private set; }

    //����ͨ��PlayerManager.instance.player��������ط����ʵ�player��������Ҫ��GameObject.Find("Player")���ַ�ʽ
    //��Ҫ�����player�󶨵���Manager����Ҫ��Unity�ڴ���һ��PlayerManager���󣬽�Player����󶨵�����ű����ű��󶨵�Manager���󣩵Ĵ˳�Ա������
    public Player player;

    //��ҳ��еĻ���
    public int currency;

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

    //��ȡ��ҳ��еĻ�������
    public int GetCurrency() => currency;

    #region InterfacesOfSavesManager
    public void LoadData(GameData _data)
    //������Ϸʱ��ִ�еĲ���
    {
        //��ȡ��������
        this.currency = _data.currency;
    }

    public void SaveData(ref GameData _data)
    {
        //�洢��������
        _data.currency = this.currency;
    }
    #endregion
}