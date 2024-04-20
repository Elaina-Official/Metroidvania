using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
//�����������һ�����߼��ٿ�һ����������ж�����PlayerState��֮����໥ת�����˴�����Ҫ�̳�MonoBehaviour
{
    //�洢���״̬������չʾ�Ķ���״̬��ʲô��{ get; private set; }��ʾ����������ⲿ��ֻ�ɶ������ɸ��ĵ�
    public PlayerState currentState { get; private set; }

    //�洢״̬����һ��״̬��ʲô
    public PlayerState formerState { get; private set; }

    public void Initialize(PlayerState _startState)
    {
        //�趨���״̬���ĳ�ʼ״̬���������״̬
        this.currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState _newState)
    {
        //�˳���һ��״̬������������Ĳ�������Ϊfalse��
        currentState.Exit();
        //��ת��״̬֮ǰ����¼���Ǵ�ʲô״̬ת��������һ��״̬
        formerState = currentState;
        //���õ�ǰ״̬Ϊ�����״̬��Ȼ������״̬������������Ĳ�������Ϊtrue��
        currentState = _newState;
        currentState.Enter();
    }
}
