using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//��������using
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    //���ڴ���Continue Game�İ�ť����û�д浵��ʱ�����������ť
    [SerializeField] private GameObject continueButton;

    //�������ӵ����������������ڵ�����ؽ��뽥������
    [SerializeField] GameObject fadeScreen;

    private void Start()
    {
        //��ʼ�˵��ĳ���bgm
        AudioManager.instance.isPlayBGM = true;
        AudioManager.instance.bgmIndex = 0;

        /*��������ӳ���MainScene��ͨ���˳���Ϸ��ť���뵽MainMenuʱ�����������bug
         * 1. MainMenu��ť����Ч�޷��������ţ����ǳ�����Ч����ť���������ܶ������������ǳ�����bgmȴ������������
         * 2. fadeScreen��FadeIn�����޷��������ţ����ǻῨס������һ�º������������䶯�����ŵĵ�һ֡��
         */

        //�ڽ��볡��0.1��󼤷��������ݻ�fadeScreen��bug������ע�����ǻ���֮��
        Invoke("ActivateFadeScreen", 0.1f);

        //û�д浵��ʱ�����ؼ�����Ϸ�İ�ť
        CheckButtons();
    }

    #region FadeScreen
    private void ActivateFadeScreen()
    {
        //���ſ�ʼʱ��Ľ��붯�����Լ���֤��������ļ���״̬
        fadeScreen.SetActive(true);
        fadeScreen.GetComponent<UI_FadeScreen>().FadeIn();
    }
    #endregion

    #region Buttons
    private void CheckButtons()
    {
        if (SavesManager.instance.WhetherHasSavedGameData() == false)
        {
            //û�д浵��ʱ�����ؼ�����Ϸ�İ�ť
            continueButton.SetActive(false);
        }
        else
        {
            continueButton.SetActive(true);
        }
    }
    public void ContinueGame()
    //������Ϸ�ĺ��������ñ���õĴ浵���������ڰ󶨸�Button
    {
        //������Ϸ����
        SceneManager.LoadScene("MainScene");
    }

    public void NewGame()
    //��ʼ�µ���Ϸ
    {
        //ɾ���浵
        SavesManager.instance.DeleteSavedGameDate();

        //������Ϸ����
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        Debug.Log("Game Exited");
        //Application.Quit();
    }
    #endregion
}
