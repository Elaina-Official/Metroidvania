//��ɾ�����˲���Ҫ��using
using UnityEngine;

//ʹ�ÿ�����Unity���Ҽ���Ӧ��·���ڣ��������������ض����ļ�
[CreateAssetMenu(fileName ="New Item Data", menuName ="Item Data/New Item")]

public class ItemData : ScriptableObject
//�̳���ScriptableObject����࣬��һ�ֺܺ��õ�ģ��
{
    //��Ʒ������
    public string itemName;
    //��Ʒ����ͼ
    public Sprite itemIcon;
}
