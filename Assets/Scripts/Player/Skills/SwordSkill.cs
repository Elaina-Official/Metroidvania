using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkill : PlayerSkill
{
    #region SwordSkillInfo
    [Header("Sword Skill Info")]
    //����Prefab�Ĵ���
    [SerializeField] private GameObject swordPrefab;
    //�����������ٶȱ���
    [SerializeField] private float swordGravity;
    //���ķ����ٶȴ�С�Ŀ�������
    [SerializeField] private Vector2 launchForce;
    //ʵʱ���µĽ���Ͷ����׼��������AimDirection()�������������ᱻȡ��һ��������ֵ��С������Ҫÿ�����launchDir����ֵ
    private Vector2 finalAimDir;
    #endregion

    #region AimDirectionDots
    [Header("AimDirection Dots")]
    //��׼�����ߵĵ�ĸ���
    [SerializeField] private int dotsNum = 20;
    //��֮��ļ��
    [SerializeField] private float spaceBetweenDots = 0.07f;
    //�����Ϸ�ڶ���
    [SerializeField] private GameObject dotPrefab;
    //������Щ��ĸ������λ�ã�������Unity�ڵ�Player���ϣ���һ��Empty���������ſ������е��λ�õĶ���
    [SerializeField] private Transform dotsParent;
    //����������
    private GameObject[] dotsArray;
    #endregion

    protected void Start()
    {
        //����һ�¹켣��Ҫ�õ��ĵ�
        GenerateDots();
    }

    protected override void Update()
    {
        base.Update();

        //ע������GetKey������GetKeyDown��������ʵʱ����finalAimDir
        //ʵʱ���µĽ���Ͷ����׼����ע�������ʵ�֣����Ե�������������������
        //normalized��һ������ʾȡ�������������һ�����ߣ��������죬��Ҫȡһ�������Եĵ��ֵ�������������������ԭ��Ͻ���һ�������Զ�������������˷���
        if (Input.GetKey(KeyCode.Mouse2))
            finalAimDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);

        //�ѵ㰲�����������ϲ�ͬʱ��㣨��(i * spaceBetweenDots)��ʾʱ��t���ĵȾ�λ��
        if (Input.GetKey(KeyCode.Mouse2))
        {
            for (int i = 0; i < dotsNum; i++)
            {
                dotsArray[i].transform.position = DotsPosition(i * spaceBetweenDots);
            }
        }
    }

    #region CreateSword
    public void CreateSword()
    //��ʼ�����ĺ���������Ϊ��PlayerAnimationTriggers�ű��ڵ�һ����������������playerThrowsSword������ĳ֡����
    //�������ж��ǽ���ʱ����ʱʹ�õı������󣬹������ﴴ���ֲ�����
    {
        //��ʼ������Unity�ڶ���λ�á���ת
        GameObject newSword = Instantiate(swordPrefab, PlayerManager.instance.player.transform.position, transform.rotation);
        //���ӵ����Ŀ�����
        Sword_Controller newSwordController = newSword.GetComponent<Sword_Controller>();
        //��ʼ�����ķ��䷽�򡢸�������
        newSwordController.SetupSword(finalAimDir, swordGravity);

        //�����˽�����֮�󣬹رո�����׼�켣����
        ActivateDots(false);
    }
    #endregion

    #region AimDirection
    public Vector2 AimDirection()
    //Ͷ��ǰ����׼����
    {
        //��¼���λ������
        Vector2 playerPos = PlayerManager.instance.player.transform.position;
        //��¼���λ������
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //����������Ϊ�����ָ������������
        Vector2 aimDirection = mousePos - playerPos;
        Debug.Log("Doing Aim Direction Detect");
        return aimDirection;
    }
    #endregion

    #region AimDirectionDots
    private void GenerateDots()
    //����������׼ʱԤ���Ͷ���켣��Ͷ����ȥ�����Ҫ��ʧ
    {
        //��ȷ�����鳤��
        dotsArray = new GameObject[dotsNum];
        //������Ԫ�ظ�ֵ��ʹ�䶼��Ӧ��һ��Unity�ڵ�Prefab����
        for (int dot = 0; dot < dotsNum; dot++)
        {
            //Quaternion.identity����ָQuaternion(0,0,0,0)������ÿ��תǰ�ĳ�ʼ�Ƕ�,��һ��ȷ�е�ֵ���ͣ���transform.rotation��ָ������ĽǶȣ���ֵ��ȷ�������Ա���
            //Instantiate(Unity�ڶ���Ԥ����, ��ʼ��λ��, ��ת(�˴���ʾ����ת), ����ĸ�����);
            dotsArray[dot] = Instantiate(dotPrefab, PlayerManager.instance.player.transform.position, Quaternion.identity, dotsParent);
            
            //�ڸ�ֵ��֮�����̰��������ص�����Ϊ������Ҫ����ActivateDots���������Ƶ�Ŀɼ���񣬱�����ֻ�ǰѶ����������������
            dotsArray[dot].SetActive(false);
        }
    }
    public void ActivateDots(bool _isActivate)
    //���ƹ켣��Ŀɼ����
    {
        for (int i = 0; i < dotsNum; i++)
        {
            dotsArray[i].SetActive(_isActivate);
        }
    }
    private Vector2 DotsPosition(float t)
    //�ð���Щ�������ʲôλ���أ����켣����������
    {
        //���ص�λ������������������Ӷ��ɣ���һ��Ϊ���λ�ã��ڶ���ΪfinalAimDir������ʱ��仯������Ӱ����״ֵ̬
        //finalAimDir���������ٶ�v��(Physics2D.gravity * swordGravity)���������ٶ�a�����������Ϊ����ʽĩ�ٶ�v'=v*t+1/2*a*t^2
        //��������׹ģ������Ч��������ĩ�ٶ�v'��y������ٶȣ����˴���x����Ҳ�����ˣ�����ν����Physics2D.gravityĬ��ֵΪ(0, -9.8)
        Vector2 pos = (Vector2)PlayerManager.instance.player.transform.position + finalAimDir * t + 0.5f * (t * t) * (Physics2D.gravity * swordGravity);
        
        return pos;
    }
    #endregion
}
