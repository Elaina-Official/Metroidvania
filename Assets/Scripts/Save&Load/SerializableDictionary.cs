using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<Tkey, TValue> : Dictionary<Tkey, TValue>, ISerializationCallbackReceiver
//���ڴ�����Ʒ�����ֵ�����
{
    [SerializeField] List<Tkey> keys = new List<Tkey>();
    [SerializeField] List<TValue> values = new List<TValue>();


    public void OnBeforeSerialize()
    {
        //���ԭ�������ݣ���Ϊ��Ʒ���ֵ��е�ӳ����ܻᷢ���仯
        keys.Clear();
        values.Clear();

        //д���µ���Ʒ������
        foreach (KeyValuePair<Tkey, TValue> _pair in this)
        {
            keys.Add(_pair.Key);
            values.Add(_pair.Value);
        }
    }
    public void OnAfterDeserialize()
    {
        this.Clear();

        if(keys.Count != values.Count)
        {
            //Debug.Log("Keys Count Not Equals to Values Count");
        }

        for (int i = 0; i < keys.Count; i++)
        {
            this.Add(keys[i], values[i]);
        }
    }
}
