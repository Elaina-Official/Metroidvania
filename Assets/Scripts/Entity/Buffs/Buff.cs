using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ʹ�ø���ɱ���⵽
[System.Serializable]
public class Buff
{
    //��Buff�ļ���״̬
    [SerializeField] protected bool status;
    //��Buff�ĳ���ʱ��
    public float duration = 5f;

    public virtual bool GetStatus() => status;

    public virtual void SetStatus(bool _bool) => status = _bool;
}
