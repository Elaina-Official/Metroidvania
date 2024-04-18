using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
//����Ķ�����Ϊ��������ʾ����ĸ��ֶ�����ÿ���¶����Ľű���Ҫ����PlayerState���ͱ�����ͨ�����캯�������ʼ�����˴�����Ҫ�̳�MonoBehaviour
{
    #region Initialize
    //���ĸ�״̬������
    protected PlayerStateMachine stateMachine;
    //���������ƣ������������Animator���������boolֵ��״̬�жϣ�
    private string animBoolName;
    //������������
    protected Player player;
    //���ｫ��rb��Player.cs��rb�ȼ�����������ʹ��PlayerState�����е���rb���ӿ�ݣ���д�������룩
    protected Rigidbody2D rb;
    #endregion

    //ˮƽ�ٶ�����
    public float xInput {  get; private set; }
    //��ֱ�ٶ�����
    public float yInput {  get; private set; }

    //ÿ��״̬���Զ�Ӧ�е�һ����ʱ��
    protected float stateTimer;
    //ÿ��״̬���е�һ��״̬������¼��
    protected bool stateActionFinished;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    //PlayerState��Ĺ��캯����ָ����ȷ��һ������״̬��Ҫ������������������˭��Player�������ĸ�״̬�����ƣ����������Animator���������boolֵ
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        //�����Ļ���PlayerState�������п���ֱ���ñ���rb.velocity.x����ˮƽ�ٶ������Ϣ����������player.rb.velocity.x
        rb = player.rb;

        //��ֵ��������ļ���״̬Ϊ��
        player.anim.SetBool(animBoolName, true);

        //ÿ�ν����µ�״̬ʱ����ֵ�������Ϊ��
        stateActionFinished = false;
    }
  
    public virtual void Update()
    //��Player��ʹ����Update���ϵ��ô˺�����������PlayerState�̳�MonoBehavior�����������Update���̳�MonoBehavior����Խ��Խ�ã��������
    {
        //ÿ1s�ݼ�1��λ��ֵ
        stateTimer -= Time.deltaTime;
        //�������£���yVelocity������ֵΪ��ǰ����ֱ�ٶ�
        player.anim.SetFloat("yVelocity", rb.velocity.y);
        //��ˮƽ�ٶ���AD������λ�󶨣���ֱ�ٶ���WS����λ�󶨣��ƶ��ٶȿ��Ա�������������ʵ����ʶ��ŵ������н��ж���
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }
    
    public virtual void Exit()
    {
        //��ֵ��������ļ���״̬Ϊ��
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void TriggerWhenAnimationFinished()
    {
        //���������¼����ʹ��attackStateת�Ƶ�ĳ��״̬
        stateActionFinished = true;
    }
}
