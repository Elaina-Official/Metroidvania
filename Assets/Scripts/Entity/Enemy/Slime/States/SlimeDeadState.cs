using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeadState : EnemyState
{
    private Slime slime;

    public SlimeDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Slime _slime) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.slime = _slime;
    }

    public override void Enter()
    {
        base.Enter();

        #region AbandonedDesign
        /*//��Ϊ����Ϸ��Ƶĵ�������û�����⶯�������ǲ�ȡ��������������ĵ���������������һ��Ȼ�������ͼ��Ч��
        //��������״̬ʱ��ǿ�ƽ���һ��״̬��AnimBoolName����Ϊ�棬���������ϸ�״̬�Ķ���
        slime.anim.SetBool(slime.lastAnimBoolName, true);
        
        //���ö����Ĳ����ٶ�Ϊ0����ֹͣ���ţ�������һ֡
        slime.anim.speed = 0;

        //�ر�ʵ�����ײ
        slime.cd.enabled = false;

        //����������һ�����ϵ��ٶȵĳ���ʱ�䣬�¼�������ͻ���Ϊ������׹��ȥ
        stateTimer = 0.1f;*/
        #endregion
    }

    public override void Update()
    {
        base.Update();

        //��
        slime.SetVelocity(0, 0);
    }
}
