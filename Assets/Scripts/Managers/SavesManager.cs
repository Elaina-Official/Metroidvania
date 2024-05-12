using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ע�����using
using System.Linq;
using System.IO;

public class SavesManager : MonoBehaviour
{
    public static SavesManager instance;

    //��Ϸ����
    private GameData gameData;

    //����������ؽӿڣ�������������ע���б���List<DataType>��ʽ�����������飨��DataType[]��ʽ������������ǰ�߿ɶ�Ԫ�ؽ�����ɾ������
    public List<ISavesManager> savesManagers;
    //�浵������
    private FileDataHandler dataHandler;
    //�浵����
    [SerializeField] private string saveFileName;

    //ʹ�����ǿ�����Unity��SaveManager�ű����Ҽ�ѡ��"Delete Saved File"����浵�����ڲ���
    [ContextMenu("Delete Saved File")]
    public void DeleteSavedGameDate()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, saveFileName);
        dataHandler.DeleteSavedGameData();
    }

    private void Awake()
    {
        //ȷ������������һ��
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

        //����浵�ļ�·����浵�ļ�����Application.persistentDataPath��Ӧ��ͬϵͳ�Ĳ�ͬ·����windows����AppData\Localow\DefaultCompany\xxxxx��
        dataHandler = new FileDataHandler(Application.persistentDataPath, saveFileName);

        //��¼���нӿ�
        savesManagers = FindAllSavesManagers();

        //��ʼ��ʱ�������Ϸ�浵����û�У����½���Ϸ�浵
        LoadGame();
    }

    #region BasicSavesManagement
    public void NewGame()
    //�½���Ϸ����ζ���½�һ���ɶ�ȡ�Ĵ浵����
    {
        //�½�һ����Ϸ�浵������
        gameData = new GameData();
    }
    public void LoadGame()
    {
        //��ȡ����
        gameData = dataHandler.LoadGameData();

        if(this.gameData == null)
        {
            Debug.Log("No Saved Data Found!");
            
            //��������Ϸ
            NewGame();
        }

        if(this.gameData != null)
        {
            foreach (ISavesManager _savesManager in savesManagers)
            {
                //�˴�ֻ�������ݱ���ȡ���ɣ�����Ҫ��������
                _savesManager.LoadData(gameData);
            }
        }
    }
    public void SaveGame()
    //������Ϸ
    {
        foreach(ISavesManager _savesManager in savesManagers)
        //���洢�ڴ�Manager�ڵ�gameData���������õ��浵�ӿڵ�����
        {
            //�������ã�����ʹ�ö����ܱ��޸�
            _savesManager.SaveData(ref gameData);
        }

        //��ѭ��֮��洢�浵����
        dataHandler.SaveGameData(gameData);

        Debug.Log("Game Saved!");
    }
    private void OnApplicationQuit()
    //�����˳������ʱ���Զ��洢һ����Ϸ���ݣ���ֹ���ݶ�ʧ
    {
        SaveGame();
    }
    #endregion

    #region Interfaces
    private List<ISavesManager> FindAllSavesManagers()
    {
        //�ҵ����м̳���MonoBehaviour��ISavesManager����
        IEnumerable<ISavesManager> _savesManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISavesManager>();

        //�����ҵ�������ʹ���˴浵�ӿڵ��ࣻע�ⷵ�ص���new�����ĵ�һ���б�
        return new List<ISavesManager>(_savesManagers);
    }
    #endregion

    public bool WhetherHasSavedGameData()
    //����Ƿ����ѱ������Ϸ����
    {
        if(dataHandler.LoadGameData() != null)
        {
            return true;
        }

        return false;
    }
}
