using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ע��˴�using
using System;
using System.IO;

public class FileDataHandler
{
    //��ȡ�ļ�����������Ҫ�ļ���·��
    private string dataFileDirPath = "";
    //�Լ��ļ�������
    private string dateFileName = "";

    public FileDataHandler(string _dataFileDirPath, string _dateFileName)
    //Ĭ�Ϲ��캯��
    {
        this.dataFileDirPath = _dataFileDirPath;
        this.dateFileName = _dateFileName;
    }

    public GameData LoadGameData()
    {
        //����·��
        string _fullPath = Path.Combine(dataFileDirPath, dateFileName);

        //�ڴ����µĴ浵���ݲ���ֵ����֮ǰ��������Ϊnull
        GameData _loadData = null;

        if(File.Exists(_fullPath))
        {
            try
            {
                string _dataToLoad = "";

                using(FileStream _stream = new FileStream(_fullPath, FileMode.Open))
                {
                    using(StreamReader _reader = new StreamReader(_stream))
                    {
                        _dataToLoad = _reader.ReadToEnd();
                    }
                }

                _loadData = JsonUtility.FromJson<GameData>(_dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Error On Trying To Load GameData From File " + _fullPath + "\n" + e);
            }
        }

        return _loadData;
    }

    public void SaveGameData(GameData _gameData)
    {
        //����·��
        string _fullPath = Path.Combine(dataFileDirPath, dateFileName);
    
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_fullPath));
        
            //�ڶ�������true��ʾ����
            string _dataToStore = JsonUtility.ToJson(_gameData, true);

            //����
            using (FileStream _stream = new FileStream(_fullPath, FileMode.Create))
            {
                using (StreamWriter _writer = new StreamWriter(_stream))
                {
                    _writer.Write(_dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Error On Trying To Save GameData To File " + _fullPath + "\n" + e);
        }
    }

    public void DeleteSavedGameData()
    {
        string _fullPath = Path.Combine(dataFileDirPath, dateFileName);

        if(File.Exists(_fullPath))
        {
            File.Delete(_fullPath);
        }
    }
}
