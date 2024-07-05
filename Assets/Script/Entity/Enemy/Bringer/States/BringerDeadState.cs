using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerDeadState : EnemyState
//��������ҪExit����ֻ��Enter��Update����
{
    private Bringer bringer;

    public BringerDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Bringer _bringer) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.bringer = _bringer;
    }

    public override void Enter()
    {
        base.Enter();

        #region AbandonedDesign
        /*//��Ϊ����Ϸ��Ƶĵ�������û�����⶯�������ǲ�ȡ��������������ĵ���������������һ��Ȼ�������ͼ��Ч��
        //��������״̬ʱ��ǿ�ƽ���һ��״̬��AnimBoolName����Ϊ�棬���������ϸ�״̬�Ķ���
        bringer.anim.SetBool(bringer.lastAnimBoolName, true);
        
        //���ö����Ĳ����ٶ�Ϊ0����ֹͣ���ţ�������һ֡
        bringer.anim.speed = 0;

        //�ر�ʵ�����ײ
        bringer.cd.enabled = false;

        //����������һ�����ϵ��ٶȵĳ���ʱ�䣬�¼�������ͻ���Ϊ������׹��ȥ
        stateTimer = 0.1f;*/
        #endregion
    }

    public override void Update()
    {
        base.Update();

        //��
        bringer.SetVelocity(0, 0);
    }
}
