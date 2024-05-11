using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI : MonoBehaviour
//�˽ű�����Canvas�ϣ����ڿ��Ƹ�UI���л�
{
    public static UI instance;

    #region ToolTip
    //��Ʒ����Ʒ����ϸ��Ϣ����
    public UI_ItemToolTip itemToolTip;
    //�������Ե���ϸ��Ϣ����
    public UI_StatToolTip statToolTip;
    //������ʾ
    [SerializeField] private GameObject interactToolTipUI;
    //�Ƿ���ʾ������ʾ
    private bool isShowInteractToolTip; 
    #endregion

    #region UIMenus
    //��¼��UI���Ա�ʹ�ð����л�
    public GameObject inGameUI;
    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject skillsUI;
    [SerializeField] private GameObject optionsUI;
    public GameObject cdPlayerUI;
    #endregion

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
    private void Start()
    {
        //��ʼ״̬�򿪵�����Ϸ�ڽ���UI
        SwitchToUI(inGameUI);

        //��ֹTooTip�ڲ���Ҫ��ʱ���
        statToolTip.gameObject.SetActive(false);
        itemToolTip.gameObject.SetActive(false);
    }

    private void Update()
    {
        //��������UI�ļ��
        UIWithKeyController();
        //������ʾUI�ļ��
        CheckWhtherShowInteractToolTip();
    }

    #region UIController
    public void UIWithKeyController()
    //�ۺϵİ�������
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SwitchWithKeyToUI(characterUI);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //���⼸�����水ESCӦ���˳����л�����Ϸ��UI
            if (characterUI.activeSelf || skillsUI.activeSelf || cdPlayerUI.activeSelf)
            {
                SwitchToUI(inGameUI);
                //��������
                return;
            }

            //�����������������
            SwitchWithKeyToUI(optionsUI);
        }

        if (Input.GetKeyDown(KeyCode.E) && isShowInteractToolTip)
        //����ʾ�˰�����ʾUIʱ����E�򿪻�رճ�Ƭ������UI
        {
            SwitchWithKeyToUI(cdPlayerUI);
        }
    }
    public void SwitchToUI(GameObject _menu)
    {
        //����Canvas���Ӷ���
        for (int i = 0; i < transform.childCount; i++)
        {
            //��֤������ʾUI����������ʾ
            if(transform.GetChild(i).gameObject != interactToolTipUI)
            {
                //�ر������Ӷ���
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        
        //������Ҫת�����ķǿն���
        if(_menu != null)
        {
            _menu.SetActive(true);
            //UI�л�����Ч
            Audio_Manager.instance.PlaySFX(8, null);
        }
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

    #region StateOfUI
    public bool ActivatedStateOfMainUIs()
    //������ҪUI�ļ�����������ڱ���˺�������Ϊ��ʱ�������������ȶ���
    {
        //����Canvas���Ӷ���
        for (int i = 0; i < transform.childCount; i++)
        {
            //������inGameUI�Ͱ�����ʾUI֮���UI
            if (transform.GetChild(i).gameObject != interactToolTipUI && transform.GetChild(i).gameObject != inGameUI)
            {
                if(transform.GetChild(i).gameObject.activeSelf)
                    return true;
            }
        }
        return false;
    }
    #endregion

    #region Interact
    public void SetWhetherShowInteractToolTip(bool _bool)
    //�˺������ھ����Ƿ���ʾ������ʾUI
    {
        isShowInteractToolTip = _bool;
    }
    public void CheckWhtherShowInteractToolTip()
    {
        //�Ƿ���ʾ������ʾUI
        interactToolTipUI.SetActive(isShowInteractToolTip);
    }
    #endregion
}