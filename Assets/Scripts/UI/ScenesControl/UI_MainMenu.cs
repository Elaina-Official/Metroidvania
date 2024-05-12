using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//��������using
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "MainScene";

    //���ڴ���Continue Game�İ�ť����û�д浵��ʱ�����������ť
    [SerializeField] private GameObject continueButton;

    //�������ӵ����������������ڵ�����ؽ��뽥������
    [SerializeField] UI_FadeScreen fadeScreen;

    private void Start()
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
        SceneManager.LoadScene(nextSceneName);
    }

    public void NewGame()
    //��ʼ�µ���Ϸ
    {
        //ɾ���浵
        SavesManager.instance.DeleteSavedGameDate();

        //������Ϸ����
        SceneManager.LoadScene(nextSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Game Exited");
        //Application.Quit();
    }
}
