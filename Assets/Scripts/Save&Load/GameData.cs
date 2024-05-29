using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //��ҳ��еĻ���
    public int currency;

    //��ҵ���Ʒ����ʹ���Զ���Ŀ����л��ֵ�
    public SerializableDictionary<string, int> inventory;

    //���漤��Ĵ浵����ֵ�
    public SerializableDictionary<string, bool> checkpointsDict;

    public GameData()
    //���캯��
    {
        //������Ϸ�浵ʱ��Ĭ�ϻ���Ϊ��0
        this.currency = 0;

        this.inventory = new SerializableDictionary<string, int>();
        this.checkpointsDict = new SerializableDictionary<string, bool>();
    }
}
