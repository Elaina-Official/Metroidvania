using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ʹ�ø���ɱ���⵽
[System.Serializable]
public class Buff_Ignited : Buff
{
    //��������״̬ʱ��ÿ���೤ʱ���ܵ�һ�������˺�
    public float damageCooldown = 1f;
    //ÿ���ܵ������˺����ٷ�֮����Ѫ��
    public float burnHealthPercentage = 0.03f;
}
