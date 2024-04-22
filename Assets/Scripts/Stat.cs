using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ʹ�ø���ɱ���⵽
[System.Serializable]
public class Stat
//�޼̳У�����һ��������ʾһ���Զ�����ֵ����
{
    //����ֵ�Ļ�����ֵ
    [SerializeField] private int baseValue;

    //һ��int���飬��¼��Stat�ڲ�ͬ����µĲ�ͬ��ֵ
    public List<int> modifiers;

    public int GetValue()
    //�����ṩ�ӿڣ�ʹ���Ի�ȡ�����������ֵ
    {
        //finalValue�Ǳ�Stat���ձ�ɵ�ֵ����û���κμӳɵ������Ĭ��ΪbaseValue
        int finalValue = baseValue;

        //�������װ����һ������������������˺��ļӳ�/��������ֵ�����modifiers������
        foreach (int modifier in modifiers)
        {
            //������мӳ�/�����õ������˺�
            finalValue += modifier;
        }

        return finalValue;
    }

    #region EditModifiers
    public void AddModifier(int _modifier)
    {
        //���Ԫ��
        modifiers.Add(_modifier);
    }
    public void RemoveModifier(int _modifier)
    {
        //ɾ��Ԫ��
        modifiers.Remove(_modifier);
    }
    #endregion
}
