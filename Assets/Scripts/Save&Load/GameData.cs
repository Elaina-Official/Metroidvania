using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    #region Currency
    //��ҳ��еĻ���
    public int currency;
    #endregion

    #region Inventory
    //��ҵ���Ʒ����ʹ���Զ���Ŀ����л��ֵ�
    public SerializableDictionary<string, int> inventory;
    #endregion

    #region CheckPoints
    //���漤��Ĵ浵����ֵ�
    public SerializableDictionary<string, bool> checkpointsDict;
    //�������������Ĵ浵��id
    public string closestCheckPointID;
    #endregion

    #region Settings
    //�洢���֣�bgm��cd������Ч��sfx��������С���õ��ֵ�
    public SerializableDictionary<string, float> volumeSettings; 
    #endregion

    public GameData()
    //���캯��
    {
        //������Ϸ�浵ʱ��Ĭ�ϻ���Ϊ��0
        this.currency = 0;
        this.inventory = new SerializableDictionary<string, int>();
        this.checkpointsDict = new SerializableDictionary<string, bool>();
        this.closestCheckPointID = string.Empty;
        this.volumeSettings = new SerializableDictionary<string, float>();
    }
}
