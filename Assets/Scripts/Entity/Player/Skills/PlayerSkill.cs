using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
//����̳е���MonoBehaviour������Updateһֱ��ˢ��
{
    #region Cooldown
    [Header("Skill Cooldown")]
    //ÿ�����������ȴʱ��
    public float cooldown;
    //������ȴ�ļ�ʱ��
    protected float cooldownTimer;
    #endregion

    protected virtual void Update()
    {
        //��ʱ��ݼ���ÿ���1��λ
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if(cooldownTimer < 0)
        {            
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void RefreshCooldown()
    {
        //�ָ���ȴʱ��
        cooldownTimer = cooldown;
    }
}
