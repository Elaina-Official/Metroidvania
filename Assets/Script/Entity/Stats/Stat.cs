using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ʹ�ø���ɱ���⵽
[System.Serializable]
public class Stat
//�޼̳У�����һ��������ʾһ���Զ�����ֵ����
{
    //����ֵ�Ļ�����ֵ
    [SerializeField] private int baseValue = 0;

    #region GetValue
    public int GetValue()
    //�����ṩ�ӿڣ�ʹ���Ի�ȡ�����������ֵ
    {
        //finalValue�Ǳ�Stat���ձ�ɵ�ֵ����û���κμӳɵ������Ĭ��ΪbaseValue
        int _finalValue = baseValue;
        return _finalValue;
    }
    #endregion

    #region SetValue
    public void SetValue(int _value)
    {
        baseValue = _value;
    }
    #endregion
}
