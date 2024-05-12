using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
//����̳е���MonoBehaviour������Updateһֱ��ˢ��
{
    public static PlayerSkillManager instance {  get; private set; }
    //����Ͳ���Ҫ����Player�����ˣ���Ϊ����ֱ����PlayerManager�������������

    #region Skills
    public DashSkill dashSkill;
    public SwordSkill swordSkill;
    #endregion

    private void Awake()
    {
        //ȷ��ֻ��һ��instance�ڹ�������ֹ������
        if (instance != null)
        {
            //ֱ��ɾ���������ű����ڵĶ���
            Destroy(instance.gameObject);
            Debug.Log("Invalid PlayerManager Instance DESTROYED");
        }
        else
            instance = this;
    }

    private void Start()
    {
        #region Skills
        //�������dash��DashSkill�Ľű�;�Ա���κεط�ͨ����Manager����Player��DashSkill
        dashSkill = GetComponent<DashSkill>();
        swordSkill = GetComponent<SwordSkill>();
        #endregion
    }
}
