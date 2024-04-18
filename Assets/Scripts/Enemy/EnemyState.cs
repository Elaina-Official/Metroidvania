using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    #region Initialize
    //���ĸ�״̬������
    protected EnemyStateMachine stateMachine;
    //���������ƣ������������Animator���������boolֵ��״̬�жϣ�
    private string animBoolName;
    //������������
    protected Enemy enemyBase;
    //���ｫ��rb��Enemy.cs��rb�ȼ�����������ʹ��EnemyState�����е���rb���ӿ�ݣ���д�������룩
    protected Rigidbody2D rb;
    #endregion

    //ÿ��״̬���Զ�Ӧ�е�һ����ʱ��
    protected float stateTimer;
    //ÿ��״̬���е�һ��״̬������¼��
    protected bool stateActionFinished;

    //����Ѿ�����Ҫ�ˣ�ֱ����PlayerManager.instance.transform.position����
    //��¼��ҵ�λ�ã�ע����Transform�������ͣ�
    //public Transform playerPos;

    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        //�����Ļ���EnemyState�������п���ֱ���ñ���rb.velocity.x����ˮƽ�ٶ������Ϣ����������enemy.rb.velocity.x
        rb = enemyBase.rb;

        //��ֵ��������ļ���״̬Ϊ��
        enemyBase.anim.SetBool(animBoolName, true);

        //ÿ�ν����µ�״̬ʱ����ֵ�������Ϊ��
        stateActionFinished = false;
    }

    public virtual void Update()
    {
        //ÿ1s�ݼ�1��λ��ֵ
        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        //��ֵ��������ļ���״̬Ϊ��
        enemyBase.anim.SetBool(animBoolName, false);
    }

    public virtual void TriggerWhenAnimationFinished()
    {
        //���������¼����ʹ��attackStateת�Ƶ�ĳ��״̬
        stateActionFinished = true;
    }
}