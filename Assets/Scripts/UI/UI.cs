using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
//�˽ű�����Canvas�ϣ����ڿ��Ƹ�UI���л�
{
    //�������Ե���ϸ��Ϣ����
    public UI_StatToolTip statToolTip;

    #region UIMenus
    //��¼��UI���Ա�ʹ�ð����л�
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject skillsUI;
    [SerializeField] private GameObject optionsUI;
    #endregion

    private void Start()
    {
        //��ʼ״̬�򿪵�����Ϸ�ڽ���UI
        SwitchToUI(inGameUI);

        //��ֹTooTip�ڲ���Ҫ��ʱ���
        statToolTip.gameObject.SetActive(false);
    }

    private void Update()
    {
        //��������UI�ļ��
        UIWithKeyController();
    }

    #region UIController
    public void UIWithKeyController()
    //�ۺϵİ�������
    {
        if (Input.GetKeyDown(KeyCode.I))
            SwitchWithKeyToUI(characterUI);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //�����������水ESCӦ���˳����л�����Ϸ��UI
            if (characterUI.activeSelf || skillsUI.activeSelf)
            {
                SwitchToUI(inGameUI);
                //��������
                return;
            }

            //�����������������
            SwitchWithKeyToUI(optionsUI);
        }
    }
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
    public void SwitchWithKeyToUI(GameObject _menu)
    //ʹ�ð������ƵĽ�������˳�UI����
    {
        //��������UI�����Ǽ���״̬�ģ���رմ˽���
        if (_menu != null && _menu.activeSelf)
        {
            //����UI���͵�ǰUI�ص������л�����Ϸ��UI
            SwitchToUI(inGameUI);
            //�����˺���
            return;
        }

        //����򿪴˴�����棨���ر��������棩
        SwitchToUI(_menu);
    }
    #endregion
}
