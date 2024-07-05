using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    bringer,
    slime_Big,
    slime_Medium,
    slime_Small
}

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [Header("Enemy Prefab")]
    //ʹ���б��¼����Ԥ����
    [SerializeField] private GameObject[] enemyPrefabList;

    private void Awake()
    {
        //ȷ������������һ��
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public void SpawnEnemy(EnemyType _enemy, Vector3 _position, int _count)
    {
        //����ָ������
        for (int i = 0; i < _count; i++)
        {
            //�����ˮƽλ��
            float _xPos = UnityEngine.Random.Range(-5f, 5f);

            //��ѡ����λ������ѡ���Ĺ���
            GameObject _newSlime = Instantiate(EnemyTypeMapping(_enemy), _position + new Vector3(_xPos, 0), Quaternion.identity);
        }
    }

    public void SpawnRandomEnemy(Vector3 _position, int _count)
    {
        //����ָ������
        for (int i = 0; i < _count; i++)
        {
            //���ѡȡ�б��ڵ�һ������
            int _random = UnityEngine.Random.Range(0, enemyPrefabList.Length);

            //���ɹ���
            SpawnEnemy(enemyPrefabList[_random].GetComponent<Enemy>().enemyType, _position, _count);
        }
    }

    public GameObject EnemyTypeMapping(EnemyType _enemy)
    {
        if (_enemy == EnemyType.bringer) { return enemyPrefabList[0]; }
        if (_enemy == EnemyType.slime_Big) { return enemyPrefabList[1]; }
        if (_enemy == EnemyType.slime_Medium) { return enemyPrefabList[2]; }
        if (_enemy == EnemyType.slime_Small) { return enemyPrefabList[3]; }
        return null;
    }
}
