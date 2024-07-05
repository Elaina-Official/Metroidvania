using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; }
    //�洢���״̬������չʾ�Ķ���״̬��ʲô��{ get; private set; }��ʾ����������ⲿ��ֻ�ɶ������ɸ��ĵ�

    public void Initialize(EnemyState _startState)
    {
        //�趨���״̬���ĳ�ʼ״̬���������״̬
        this.currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(EnemyState _newState)
    {
        //�˳���һ��״̬�������õ�ǰ״̬Ϊ�����״̬��Ȼ������״̬
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
