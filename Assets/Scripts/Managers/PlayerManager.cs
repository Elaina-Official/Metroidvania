using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    canWallSlide,
    CanDash,
    CanDoubleJump,
    CanThrowSword,
    CanFireBall,
    CanIceBall
}

public class PlayerManager : MonoBehaviour, ISavesManager
//����̳е���MonoBehaviour������Updateһֱ��ˢ��
{
    #region Interface
    //����ͨ��PlayerManager.instance��������������ڵĳ�Ա��������player��Ҫ��PlayerManager.instance.player.position������PlayerManager.instance.position������ı��ٸ���ˣ�
    //�����ֻ����һ�����󣬲�Ȼ�������
    public static PlayerManager instance {  get; private set; }

    //����ͨ��PlayerManager.instance.player��������ط����ʵ�player��������Ҫ��GameObject.Find("Player")���ַ�ʽ
    //��Ҫ�����player�󶨵���Manager����Ҫ��Unity�ڴ���һ��PlayerManager���󣬽�Player����󶨵�����ű����ű��󶨵�Manager���󣩵Ĵ˳�Ա������
    public Player player;
    #endregion

    #region SaveData
    [Header("Currency")]
    //��ҳ��еĻ���
    public int currency;
    [Header("Ability")]
    //����Ƿ��ܻ�ǽ
    public bool ability_CanWallSlide;
    //����Ƿ��ܳ��
    public bool ability_CanDash;
    //����Ƿ��ܶ�����
    public bool ability_CanDoubleJump;
    //����Ƿ���Ͷ����
    public bool ability_CanThrowSword;
    //����Ƿ����ӻ���
    public bool ability_CanFireBall;
    //����Ƿ����ӱ���
    public bool ability_CanIceBall;
    #endregion

    private void Awake()
    {
        //ȷ��ֻ��һ��instance�ڹ�������ֹ������
        if (instance != null)
        {
            //ɾ�����������Ľű�
            //Destroy(instance);
            //ֱ��ɾ���������ű����ڵĶ���
            Destroy(instance.gameObject);
            Debug.Log("Invalid GameObject Containing PlayerManager's Instance DESTROYED");
        }
        else
            instance = this;
    }

    #region ISavesManager
    public void LoadData(GameData _data)
    //������Ϸʱ��ִ�еĲ���
    {
        //��ȡ��������
        this.currency = _data.currency;
        //��ȡ�������
        ability_CanWallSlide = _data.canWallSlide;
        ability_CanDash = _data.canDash;
        ability_CanDoubleJump = _data.canDoubleJump;
        ability_CanThrowSword = _data.canThrowSword;
        ability_CanFireBall = _data.canFireBall;
        ability_CanIceBall = _data.canIceBall;
    }

    public void SaveData(ref GameData _data)
    {
        //�洢��������
        _data.currency = this.currency;
        //�洢�������
        _data.canWallSlide = ability_CanWallSlide;
        _data.canDash = ability_CanDash;
        _data.canDoubleJump = ability_CanDoubleJump;
        _data.canThrowSword = ability_CanThrowSword;
        _data.canFireBall = ability_CanFireBall;
        _data.canIceBall = ability_CanIceBall;
    }
    #endregion
}