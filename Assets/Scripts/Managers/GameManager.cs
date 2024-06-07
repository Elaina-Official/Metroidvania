using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ע��
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISavesManager
{
    public static GameManager instance;

    #region CheckPoint
    [Header("CheckPoint")]
    //����浵����б�
    [SerializeField] private CheckPoint[] checkpointsList;
    //������һ����Ϣ�Ĵ浵��
    public string lastRestCheckPointID = "";
    #endregion

    #region AbilityActivator
    [Header("Ability Activator")]
    //����������������Ĺؼ�����
    [SerializeField] private GameObject wallslideActivator;
    [SerializeField] private GameObject dashActivator;
    [SerializeField] private GameObject throwswordActivator;
    [SerializeField] private GameObject fireballActivator;
    [SerializeField] private GameObject iceballActivator;
    #endregion

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

    private void Start()
    {
        #region CheckAbility
        //���ݽ�ɫ����������Ȩ�����ж���Щ���������Ĺؼ������Ƿ�Ӧ�ó���
        if (!PlayerManager.instance.ability_CanWallSlide) { wallslideActivator.gameObject.SetActive(true); } else { wallslideActivator.gameObject.SetActive(false);}
        if (!PlayerManager.instance.ability_CanDash) { dashActivator.gameObject.SetActive(true); } else { dashActivator.gameObject.SetActive(false);}
        if (!PlayerManager.instance.ability_CanThrowSword) { throwswordActivator.gameObject.SetActive(true); } else { throwswordActivator.gameObject.SetActive(false);}
        if (!PlayerManager.instance.ability_CanFireBall) { fireballActivator.gameObject.SetActive(true); } else { fireballActivator.gameObject.SetActive(false);}
        if (!PlayerManager.instance.ability_CanIceBall) { iceballActivator.gameObject.SetActive(true); } else { iceballActivator.gameObject.SetActive(false);}
        #endregion
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

    #region Pause
    public void PauseGame(bool _pause)
    //��ͣ��Ϸ
    {
        if (_pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    #endregion

    #region ClosestCP
    public CheckPoint FindClosestCheckPoint()
    //���غ���Ҿ���������Ѽ���浵��
    {
        float _closestDistance = Mathf.Infinity;
        CheckPoint _closestCP = null;

        foreach (var _cp in checkpointsList)
        {
            float _distanceToCP = Vector2.Distance(PlayerManager.instance.player.transform.position, _cp.transform.position);

            if (_distanceToCP < _closestDistance && _cp.isActive)
            {
                _closestDistance = _distanceToCP;
                _closestCP = _cp;
            }
        }
        return _closestCP;
    }
    #endregion

    #region ISaveManager
    public void LoadData(GameData _data)
    {
        #region CheckPoint
        //��ȡ�浵����б���������������Ϣ
        foreach (KeyValuePair<string, bool> _pair in _data.checkpointsDict)
        {
            foreach(CheckPoint _checkpoint in checkpointsList)
            {
                if(_checkpoint.id == _pair.Key && _pair.Value == true)
                    _checkpoint.ActivateCheckPoint();
            }
        }

        //��ȡ����ϴ���Ϣ�Ĵ浵��
        lastRestCheckPointID = _data.lastRestCPID;

        //ͨ���洢��id��������ϴ���Ϣ�Ĵ浵��
        foreach (CheckPoint _cp in checkpointsList)
        {
            //����Ҷ�λ�ڴ˴�
            if (_data.lastRestCPID == _cp.id)
            {
                //����λ�������Ϸ�һ�㣬��ֹ���ڵص���
                GameObject.Find("Player").transform.position = _cp.transform.position + new Vector3(0, 2, 0);
            }
        }

        /*//ͨ���洢��id�����������������Ѽ���浵��
        foreach(CheckPoint _cp in checkpointsList)
        {
            //����Ҷ�λ�ڴ˴�
            if(_data.closestCheckPointID == _cp.id)
            {
                //����λ�������Ϸ�һ�㣬��ֹ���ڵص���
                GameObject.Find("Player").transform.position = _cp.transform.position + new Vector3(0, 2, 0);
            }
        }*/
        #endregion
    }
    public void SaveData(ref GameData _data)
    {
        //�Է���һ��������ٴ洢
        _data.checkpointsDict.Clear();

        #region CheckPoint
        //����浵����б���������������Ϣ
        foreach (CheckPoint _checkpoint in checkpointsList)
        {
            _data.checkpointsDict.Add(_checkpoint.id, _checkpoint.isActive);
        }
        //������һ����Ϣ�Ĵ浵��
        if (lastRestCheckPointID != null)
        {
            _data.lastRestCPID = lastRestCheckPointID;
            //Debug.Log("Save lastRestCPID As " + lastRestCheckPointID);
        }
        if (lastRestCheckPointID == null)
            _data.lastRestCPID = "";

        /*//���汣����Ϸ��ʱ��������������Ѽ���浵�㣬������ֱ�Ӵ洢һ��CheckPoint���͵����ݣ�ֻ���ַ����Ȼ����������Ͳ��ܱ��洢�ڴ浵�ı��ļ���
        if (FindClosestCheckPoint() != null)
            _data.closestCheckPointID = FindClosestCheckPoint().id;
        if (FindClosestCheckPoint() == null)
            _data.closestCheckPointID = "";*/
        #endregion
    }
    #endregion
}
