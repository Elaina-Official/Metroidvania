using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatToolTip : MonoBehaviour
//�˽ű�����CharacterUI����������ͣ��Stats�ϵĲ�����������Ϣ�ı�����
{
    //ʹ�����ļ��е�TextMeshProUGUI�������˱�����ֵ
    [SerializeField] private TextMeshProUGUI statDescription;

    public void ShowStatToolTipAs(string _content)
    //��ʾ������������
    {
        //����������Ϣ�Ծ�����ı�
        statDescription.text = _content;

        //�����������
        gameObject.SetActive(true);
    }

    public void HideStatToolTip()
    //�ر��������
    {
        //����ı�
        statDescription.text = "";
        gameObject.SetActive(false);
    }
}
