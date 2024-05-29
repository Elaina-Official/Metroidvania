using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ע��
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISavesManager
{
    public static GameManager instance;
    //����浵����б�
    [SerializeField] private CheckPoint[] checkpointsList;

    private void Awake()
    {
        //ȷ������������һ��
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

        //�����ֶ��϶���ֵ����Ϊ�浵���¼������ڴ浵����֮ǰ��ɣ���Ȼ�޷���ȡ
        //��ȡ���У���ǰ�����еģ����浵��
        //checkpointsList = FindObjectsOfType<CheckPoint>();
    }

    #region Scenes
    public void RestartScene()
    {
        //�Զ�����
        SavesManager.instance.SaveGame();

        //����������Ч
        AudioManager.instance.StopSFX(10);

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

    public void SwitchToMainScene()
    {
        //�Զ�����һ�£��������ݻ᲻һ��
        SavesManager.instance.SaveGame();

        //������Ϸ��ʼ����
        SceneManager.LoadScene("MainScene");
    }
    #endregion

    #region ISaveManager
    public void LoadData(GameData _data)
    {
        foreach(KeyValuePair<string, bool> _pair in _data.checkpointsDict)
        {
            foreach(CheckPoint _checkpoint in checkpointsList)
            {
                if(_checkpoint.ID == _pair.Key && _pair.Value == true)
                    _checkpoint.ActivateCheckPoint();
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        //�Է���һ��������ٴ洢
        _data.checkpointsDict.Clear();

        foreach(CheckPoint _checkpoint in checkpointsList)
        {
            _data.checkpointsDict.Add(_checkpoint.ID, _checkpoint.isActive);
        }
    }
    #endregion
}
