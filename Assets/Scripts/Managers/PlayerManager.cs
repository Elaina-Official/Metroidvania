using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region AbilityType
public enum AbilityType
{
    canWallSlide,
    CanDash,
    CanDoubleJump,
    CanThrowSword,
    CanFireBall,
    CanIceBall
}
#endregion

public class PlayerManager : MonoBehaviour, ISavesManager
//����̳е���MonoBehaviour������Updateһֱ��ˢ��
{
    #region Components
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

    #region Ability
    public void ActivateAbility(AbilityType _type)
    //��������Ȩ��
    {
        if (_type == AbilityType.canWallSlide) { ability_CanWallSlide = true; }
        if (_type == AbilityType.CanDash) { ability_CanDash = true; }
        if (_type == AbilityType.CanDoubleJump) {  ability_CanDoubleJump = true; }
        if (_type == AbilityType.CanThrowSword) {  ability_CanThrowSword = true; }
        if (_type == AbilityType.CanFireBall) {  ability_CanFireBall = true; }
        if (_type == AbilityType.CanIceBall) {  ability_CanIceBall = true; }
    }
    public void DeactivateAbility(AbilityType _type)
    //�ر�����Ȩ��
    {
        if (_type == AbilityType.canWallSlide) { ability_CanWallSlide = false; }
        if (_type == AbilityType.CanDash) { ability_CanDash = false; }
        if (_type == AbilityType.CanDoubleJump) { ability_CanDoubleJump = false; }
        if (_type == AbilityType.CanThrowSword) { ability_CanThrowSword = false; }
        if (_type == AbilityType.CanFireBall) { ability_CanFireBall = false; }
        if (_type == AbilityType.CanIceBall) { ability_CanIceBall = false; }
    }
    #endregion

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