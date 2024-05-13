using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ע��
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        //ȷ������������һ��
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public void RestartScene()
    {
        //��ʼ��ʱ�������Ϸ�浵������SavesManager�����һֱ���ڵ�Prefab���������¼��س�����ʱ����Start�������ᱻ���ã�����Ҫ���������һ��
        SavesManager.instance.LoadGame();

        //��ȡ��ǰ����ĳ���
        Scene _scene = SceneManager.GetActiveScene();

        //���¼��ص�ǰ����
        SceneManager.LoadScene(_scene.name);
    }

    public void SwitchToMainMenu()
    {
        //�Զ�����һ�£��������ݻ᲻һ��
        SavesManager.instance.SaveGame();

        //������Ϸ��ʼ����
        SceneManager.LoadScene("MainMenu");
    }
}
