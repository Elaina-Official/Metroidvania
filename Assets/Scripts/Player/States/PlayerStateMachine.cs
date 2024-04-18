using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
//�����������һ�����߼��ٿ�һ����������ж�����PlayerState��֮����໥ת�����˴�����Ҫ�̳�MonoBehaviour
{
    public PlayerState currentState { get; private set; }
    //�洢���״̬������չʾ�Ķ���״̬��ʲô��{ get; private set; }��ʾ����������ⲿ��ֻ�ɶ������ɸ��ĵ�

    public void Initialize(PlayerState _startState)
    {
        //�趨���״̬���ĳ�ʼ״̬���������״̬
        this.currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState _newState)
    {
        //�˳���һ��״̬�������õ�ǰ״̬Ϊ�����״̬��Ȼ������״̬
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
