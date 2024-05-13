using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //��̬��ȫ�ֿɵ��õ�ʵ��
    public static AudioManager instance;

    #region SFX
    //������Ч��sound effect�����б�
    [SerializeField] AudioSource[] sfx;
    //��Ч���ŵļ��뾶��̫Զ����Ч���貥��
    [SerializeField] float sfxMinPlayRadius;
    //�Ƿ���Բ�����Ч
    private bool canPlaySFX;
    #endregion

    #region BGM
    //���汳�����ֵ��б�
    [SerializeField] AudioSource[] bgm;
    //�Ƿ�Ӧ������һ��ʱ����һֱ������ĳ��������
    public bool isPlayBGM;
    //���ŵ�bgm�ı��
    public int bgmIndex;
    #endregion

    #region CD
    //���泪Ƭ
    [SerializeField] AudioSource[] cds;
    //��ǰ�Ƿ��ڲ���cd
    public bool isPlayCD;
    #endregion

    private void Awake()
    {
        //ȷ������������һ��
        if(instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

        //�ڽ��볡��0.1������������Ч����ֹ��ʼʱ��SwitchToUI(ingameUI)����Ч�ڿ�ʼ�ͱ�����
        Invoke("AllowPlaySFX", 0.1f);
    }

    private void Update()
    {
        //��δ�ڲ���cd��ʱ����ܲ���bgm
        if (!isPlayCD)
        {
            //����bgm�Ĳ���
            if (!isPlayBGM)
                StopAllBGM();
            else
            {
                //��Ӧ�����ŵ�bgmû�в��ţ���ʼ���ţ�û�д�if�ᵼ��һֱ��ͷ��ʼ����Ŷ~��
                if (!bgm[bgmIndex].isPlaying)
                    PlayBGM(bgmIndex);
            }
        }
    }

    #region SFX
    public void PlaySFX(int _sfxIndex, Transform _sfxSource)
    //������Ч����Լ���Ч����Դλ�ã����ڶ�������null����������Ч���ŵ��������ϣ�����������Լ�����Ч
    {
        //���볡��ʱ��0.1������������Ч
        if (!canPlaySFX)
            return;

        //��������ͼ���ŵ���Ч��̫��ңԶ���򲻲���
        if (_sfxSource != null && Vector2.Distance(PlayerManager.instance.player.transform.position, _sfxSource.position) >= sfxMinPlayRadius)
            return;

        //����Ŵ������б��ڣ���Ŵ�0��ʼŶ��
        if(_sfxIndex < sfx.Length && sfx[_sfxIndex] != null)
        {
            //һ��Сtrick,���������Ŀ����Ч������
            //sfx[_sfxIndex].pitch = UnityEngine.Random.Range(0.85f, 1.1f);

            //������Ч
            sfx[_sfxIndex].Play();
        }
    }
    //ֹͣ��Ч
    public void StopSFX(int _sfxIndex) => sfx[_sfxIndex].Stop();
    //��������Ч
    public void AllowPlaySFX() => canPlaySFX = true;
    #endregion

    #region BGM
    public void PlayBGM(int _index)
    //������Ч���
    {
        //����Ŵ������б��ڣ���Ŵ�0��ʼŶ��
        if (_index < bgm.Length)
        {
            //bgmIndex��������ȷ����ǰ����bgm�ı���
            bgmIndex = _index;
            //����ǰӦ����ֹͣ�����������б�������
            StopAllBGM();
            //���ű�������
            bgm[bgmIndex].Play();
        }
    }
    public void PlayRandomBGM()
    //�������bgm
    {
        bgmIndex = UnityEngine.Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);
    }
    //�ر����б�������
    public void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
    #endregion

    #region CD
    public void PlayCD(int _cdIndex)
    //��ʼ����cd
    {
        if(_cdIndex < cds.Length)
        {
            //��������bgm
            isPlayBGM = false;
            //�ر�bgm
            StopAllBGM();
            //�ر�����cd
            StopAllCD();

            //����cd
            cds[_cdIndex].Play();
        }
    }
    public void PlayRandomCD()
    //��������cd
    {
        //��������bgm
        isPlayBGM = false;
        //�ر�bgm
        StopAllBGM();
        //�ر�����cd
        StopAllCD();

        //�����ȡcd��Ų�����
        int _cdIndex = UnityEngine.Random.Range(0, cds.Length);
        cds[_cdIndex].Play();
    }
    public void StopAllCD()
    //�ر�����cd��������֮ǰ��bgm
    {
        for (int i = 0; i < cds.Length; i++)
        {
            cds[i].Stop();
        }

        //��������bgm
        isPlayBGM = true;
    }
    #endregion
}