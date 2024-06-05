using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Spawner
    [Header("Spawner Settings")]
    //���ɹ���Ļ���Ч��
    [SerializeField] private ParticleSystem spawnerFX;
    //�Ƿ�������ɹ���
    [SerializeField] private bool randomSpawn = true;
    //����������������ʲô���͹���
    [SerializeField] private EnemyType spawnType;
    //���ɹ������ȴ
    [SerializeField] private float spawnCooldown = 15f;
    //һ�������ɵ�����
    [SerializeField] private int spawnAmount = 1;

    //�Ƿ������ˢ������Χ
    private bool isEnterSpawner;
    //�Ƿ���Լ������ɹ���
    private bool canSpawn;

    //���ɹ��������
    //[SerializeField] private int amountLimit;
    //��¼���ɵĹ���
    //private GameObject[] spawnedEnemyList;
    #endregion

    private void Start()
    {
        canSpawn = true;
    }

    private void Update()
    {
        //ˢ�ֿ���
        SpawnController();

        //����������������
        //SpawnedEnemyAmountLimiter();
    }

    #region OnTrigger
    private void OnTriggerEnter2D(Collider2D collision)
    //������ײ�䣨������ˢ�ֵ����򣩻ᴥ�����ˢ��
    {
        //��������ң����Ǳ��ʲô���ﶼ�ܴ���
        if (collision.GetComponent<Player>() != null)
        {
            //���������ˢ�ַ�Χ��
            isEnterSpawner = true;

            //����ˢ�ֵ�����Ч��
            spawnerFX.gameObject.SetActive(true);
            spawnerFX.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            //�����뿪��ˢ�ַ�Χ
            isEnterSpawner = false;

            //�ر�Ч��
            spawnerFX.gameObject.SetActive(false);
            spawnerFX.Stop();
        }
    }
    #endregion

    #region Spawn
    public void SpawnController()
    {
        //����ˢ������Χ����ˢ����ȴ����ʱ����ˢ��
        if(isEnterSpawner && canSpawn)
        {
            if (randomSpawn)
            {
                SpawnRandomEnemy();
            }
            else
            {
                SpawnEnemy();
            }
            //����ͣ�������ɣ�Ȼ��10s��ָ�����ֹһֱ��Update���ã���������
            canSpawn = false;
            Invoke("ReturnToCanSpawn", spawnCooldown);
        }
    }
    public bool ReturnToCanSpawn() => canSpawn = true;
    public void SpawnEnemy() => EnemyManager.instance.SpawnEnemy(spawnType, this.transform.position, spawnAmount);
    public void SpawnRandomEnemy() => EnemyManager.instance.SpawnRandomEnemy(this.transform.position, spawnAmount);
    #endregion

    #region AmountLimiter
    /*private void SpawnedEnemyAmountLimiter()
    {
        if(spawnedEnemyList.Length >= amountLimit)
        {
            canSpawn = false;
        }
    }*/
    #endregion
}
