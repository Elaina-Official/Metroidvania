using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class UI_MainScene : MonoBehaviour, ISavesManager
//�˽ű�����Canvas�ϣ����ڿ��Ƹ�UI���л�
{
    public static UI_MainScene instance;

    #region ToolTip
    [Header("ToolTip")]
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
    [Header("Menus")]
    //��¼��UI���Ա�ʹ�ð����л�
    public GameObject inGameUI;
    public GameObject characterUI;
    [SerializeField] private GameObject skillsUI;
    [SerializeField] private GameObject optionsUI;
    public GameObject cdPlayerUI;
    #endregion

    #region FadeScreen
    [Header("FadeScreen")]
    public GameObject fadeScreen;
    public GameObject deathText;
    public GameObject reSpawnButton;
    #endregion

    #region Settings
    [Header("Settings")]
    //����������С���õ��б�
    [SerializeField] private UI_VolumeSlider[] volumeSettings;
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
        //��Ϸ��������bgm
        AudioManager.instance.isPlayBGM = true;
        AudioManager.instance.bgmIndex = 0;

        //��ʼ״̬�򿪵�����Ϸ�ڽ���UI
        SwitchToUI(inGameUI);

        //��ֹ�ڲ���Ҫ��ʱ���
        statToolTip.gameObject.SetActive(false);
        itemToolTip.gameObject.SetActive(false);
        deathText.gameObject.SetActive(false);
        reSpawnButton.gameObject.SetActive(false);

        //���ſ�ʼʱ��Ľ��붯�����Լ���֤��������ļ���״̬
        fadeScreen.SetActive(true);
        fadeScreen.GetComponent<UI_FadeScreen>().FadeIn();
    }

    private void Update()
    {
        //��������UI�ļ��
        UIWithKeyController();
        //������ʾUI�ļ��
        CheckWhtherShowInteractToolTip();
    }

    #region UIControl
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
            //��֤������ʾUI����������ʾ��Ҫ��ֹfadeScreen��ֱ�ӹرն����ܼ�����Ļfade����ض���
            if (transform.GetChild(i).gameObject != interactToolTipUI && transform.GetChild(i).gameObject != fadeScreen)
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
            AudioManager.instance.PlaySFX(8, null);
        }

        #region GamePause
        //��UIʱ��ͣ��Ϸ
        if(GameManager.instance != null)
        {
            if (_menu == inGameUI)
                GameManager.instance.PauseGame(false);
            else
                GameManager.instance.PauseGame(true);
        }
        #endregion
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
            //��ⲻ�����������ƶ�������Ķ�����UI
            if (transform.GetChild(i).gameObject == characterUI || transform.GetChild(i).gameObject == skillsUI || transform.GetChild(i).gameObject == optionsUI || transform.GetChild(i).gameObject == cdPlayerUI)
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

    #region Death
    public void PlayDeathText()
    {
        StartCoroutine(DeathScreenAnimation());
    }
    IEnumerator DeathScreenAnimation()
    {
        //������ʾ
        yield return new WaitForSeconds(1.5f);
        deathText.SetActive(true);
        //������ť
        yield return new WaitForSeconds(2.5f);
        reSpawnButton.SetActive(true);
    }
    //���¼��س����ĺ�����������
    public void ReStartGame() => GameManager.instance.RestartScene();
    #endregion

    #region SwitchScene
    //�л�����Ϸ��ʼ����
    public void SwitchToMainMenu() => GameManager.instance.SwitchToMainMenu();
    #endregion

    #region ISaveManager
    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, float> _pair in _data.volumeSettings)
        {
            foreach (UI_VolumeSlider _slider in volumeSettings)
            {
                if (_slider.parameter == _pair.Key)
                    _slider.LoadSlider(_pair.Value);
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.volumeSettings.Clear();

        foreach (UI_VolumeSlider _slider in volumeSettings)
        {
            _data.volumeSettings.Add(_slider.parameter, _slider.slider.value);
        }    
    }
    #endregion
}