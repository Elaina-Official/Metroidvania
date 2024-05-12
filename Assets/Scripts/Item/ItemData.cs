//��ɾ�����˲���Ҫ��using
using UnityEditor;
using UnityEngine;

//ʹ�ÿ�����Unity���Ҽ���Ӧ��·���ڣ��������������ض����ļ�
[CreateAssetMenu(fileName ="New Item Data", menuName ="Item Data/New Item")]

public class ItemData : ScriptableObject
//�̳���ScriptableObject����࣬��һ�ֺܺ��õ�ģ��
{
    //��Ʒ������
    public string itemName;
    //��Ʒ������
    public ItemType itemType;
    //��Ʒ������
    [TextArea]
    public string itemDescription;
    //��Ʒ����ͼ
    public Sprite itemIcon;

    //ÿ����Ʒ���еĵ�id���У����ڴ浵��¼��Ʒ������Ʒ��ع���
    public string itemID;

    private void OnValidate()
    {
#if UNITY_EDITOR
        string _path = AssetDatabase.GetAssetPath(this);
        itemID = AssetDatabase.AssetPathToGUID(_path);
#endif
    }
}

public enum ItemType
{
    Weapon,
    Potion,
    CD
}
