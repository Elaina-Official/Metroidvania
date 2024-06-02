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

        //���ſ�ʼʱ��Ľ��붯�����Լ���֤��������ļ���״̬
        fadeScreen.SetActive(true);
        fadeScreen.GetComponent<UI_FadeScreen>().FadeIn();

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
}
