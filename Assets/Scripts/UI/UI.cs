using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
//�˽ű�����Canvas�ϣ����ڿ��Ƹ�UI���л�
{
    public void SwitchToUI(GameObject _menu)
    {
        //����Canvas���Ӷ���
        for (int i = 0; i < transform.childCount; i++)
        {
            //�ر������Ӷ���
            transform.GetChild(i).gameObject.SetActive(false);
        }
        
        //������Ҫת�����ķǿն���
        if(_menu != null)
            _menu.SetActive(true);
    }
}
