//��ɾ�����˲���Ҫ��using
using UnityEngine;

//ʹ�ÿ�����Unity���Ҽ���Ӧ��·���ڣ��������������ض����ļ�
[CreateAssetMenu(fileName ="New Item Data", menuName ="Item Data/New Item")]

public class ItemData : ScriptableObject
//�̳���ScriptableObject����࣬��һ�ֺܺ��õ�ģ��
{
    //��Ʒ������
    public string itemName;
    //��Ʒ������
    public string itemType;
    //��Ʒ������
    [TextArea]
    public string itemDescription;
    //��Ʒ����ͼ
    public Sprite itemIcon;
}
