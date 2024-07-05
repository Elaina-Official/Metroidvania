using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallSkill : PlayerSkill
{
    #region IceBallSkillInfo
    [Header("IceBall Skill Info")]
    //Prefab�Ĵ���
    [SerializeField] private GameObject iceballPrefab;
    //��ǰ���ƶ����ٶ�
    public float moveSpeed = 12f;
    #endregion

    public void CreateIceBall(Vector3 _position, int _dir)
    {
        //���ɱ���
        GameObject _newBall = Instantiate(iceballPrefab, _position, transform.rotation);
        //ˢ����ȴ
        RefreshCooldown();

        //���ӵ�������
        IceBall_Controller _control = _newBall.GetComponent<IceBall_Controller>();
        //��ʼ�����䷽��
        _control.SetupIceBall(_dir);
        //��¼һ�£���ֹ��ҿ����������ɼ�����
        PlayerSkillManager.instance.AssignNewIceBall(_newBall);

        //ʩ����Ч
        AudioManager.instance.PlaySFX(14, null);
    }
}
