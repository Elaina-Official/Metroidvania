using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
//����̳е���MonoBehaviour������Updateһֱ��ˢ��
{
    public static PlayerSkillManager instance {  get; private set; }
    //����Ͳ���Ҫ����Player�����ˣ���Ϊ����ֱ����PlayerManager�������������

    #region Skills
    [Header("Skill List")]
    public DashSkill dashSkill;
    public SwordSkill swordSkill;
    public FireBallSkill fireballSkill;
    public IceBallSkill iceballSkill;
    public CloneSkill cloneSkill;
    public BlackholeSkill blackholeSkill;
    #endregion

    #region SkillObject
    [Header("Skill Object")]
    //��¼�Ƿ��Ѿ�Ͷ����ȥ�˽�����ֹ����Ͷ������GroundedState�У���Ͷ����������ڴ�������Ƿ��Ѿ���������Prefab
    //�����player.assignedSword���Ե�boolֵʹ��
    public GameObject assignedSword;
    public GameObject assignedFireBall;
    public GameObject assignedIceBall;
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
        fireballSkill = GetComponent<FireBallSkill>();
        iceballSkill = GetComponent<IceBallSkill>();
        cloneSkill = GetComponent<CloneSkill>();
        blackholeSkill = GetComponent<BlackholeSkill>();
        #endregion
    }

    #region ObjectLife
    public void AssignNewSword(GameObject _newObject)
    {
        //��¼һ���½���һ����Prefab����CreateSword()�����б�����һ��
        assignedSword = _newObject;
    }
    public void AssignNewFireBall(GameObject _newObject)
    {
        assignedFireBall = _newObject;
    }
    public void AssignNewIceBall(GameObject _newObject)
    {
        assignedIceBall = _newObject;
    }
    public void ClearAssignedSword()
    {
        //���ٶ���Ľ�Prefab
        Destroy(assignedSword);
    }
    public void ClearAssignedFireBall()
    {
        Destroy(assignedFireBall);
    }
    public void ClearAssignedIceBall()
    {
        Destroy(assignedIceBall);
    }
    #endregion
}
