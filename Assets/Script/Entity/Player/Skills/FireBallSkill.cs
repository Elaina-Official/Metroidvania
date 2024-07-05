using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSkill : PlayerSkill
{
    #region FireBallSkillInfo
    [Header("FireBall Skill Info")]
    //Prefab�Ĵ���
    [SerializeField] private GameObject fireballPrefab;
    //��ǰ���ƶ����ٶ�
    public float moveSpeed = 12f;
    #endregion

    public void CreateFireBall(Vector3 _position, int _dir)
    {
        //���ɻ���
        GameObject _newBall = Instantiate(fireballPrefab, _position, transform.rotation);
        //ˢ����ȴ
        RefreshCooldown();

        //���ӵ�������
        FireBall_Controller _control = _newBall.GetComponent<FireBall_Controller>();
        //��ʼ�����䷽��
        _control.SetupFireBall(_dir);
        //��¼һ�£���ֹ��ҿ����������ɼ�����
        PlayerSkillManager.instance.AssignNewFireBall(_newBall);

        //ʩ����Ч
        AudioManager.instance.PlaySFX(14, null);
    }
}
