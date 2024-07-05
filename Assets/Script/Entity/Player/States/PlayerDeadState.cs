using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    //��ֹEnter�������޴�������
    int xxx = 1;

    public override void Enter()
    //ע��˴�������û��ת����ȥ����״̬������������Exitһֱ���ᴥ������Enter�ᱻ��������
    {
        base.Enter();

        if(xxx == 1)
        {
            //ֹͣbgm
            AudioManager.instance.isPlayBGM = false;
            //������Ч
            AudioManager.instance.PlaySFX(10, null);

            //������������
            UI_MainScene.instance.PlayDeathText();

            //��ֹEnter�������޴�������
            xxx++;
        }

        //����������Ļ�Ľ�������֪Ϊɶ�������������������棬�������FadeOut��ں�����±�͸��
        UI_MainScene.instance.fadeScreen.GetComponent<UI_FadeScreen>().FadeOut();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //���˾Ͳ��ܶ���
        player.SetVelocity(0, 0);
    }
}
