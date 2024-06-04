using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    //���ӵ�ʵ���Animator�ڵ���Ⱦ��Component
    private SpriteRenderer sr;

    #region PopUpText
    [Header("Pop Up Text")]
    //������Щʵ��������Ч�����������
    [SerializeField] private GameObject popUpTextPrefab;
    #endregion

    #region Attack
    [Header("Damaged Material")]
    //��¼��ʼ�Ĳ���
    private Material originMat;
    //��¼�����ܹ�������Ч���ĵĲ���
    [SerializeField] private Material flashHitMat;
    //��¼�����ܷ�����������Ч���Ĳ���
    [SerializeField] private Material magicalHitMat;
    //���ʸ�����ͣ��ʱ��
    [SerializeField] private float changeMatDuration = 0.1f;
    [Header("Hit FX")]
    [SerializeField] private GameObject hitFX00;
    [SerializeField] private GameObject hitFX01;
    #endregion

    #region Ailments
    [Header("Ailments Color")]
    [SerializeField] private Color ignitedColor;
    [SerializeField] private Color chilledColor;
    [SerializeField] private Color shockedColor;

    [Header("Ailments Particle")]
    [SerializeField] private ParticleSystem ignitedFX;
    [SerializeField] private ParticleSystem chilledFX;
    [SerializeField] private ParticleSystem shockedFX;
    #endregion

    private void Start()
    {
        //���ӵ�ʵ���Animator�ڵ���Ⱦ��Component
        sr = GetComponentInChildren<SpriteRenderer>();

        //��¼ԭʼ����
        originMat = sr.material;
    }

    #region PopUpText
    public void CreatPopUpText(string _text, Color _color)
    //���Ƶ����������Ч���ĺ�����������Ҫ���������ݼ�����ɫ
    {
        //��������Ч������ٻ��ߵ�����λ�ã��ڷ�Χ�����
        float _randomX = Random.Range(-1.5f, 1.5f);
        float _randomY = Random.Range(0.5f, 2f);
        Vector3 _positionOffset = new Vector3(_randomX, _randomY, 0);

        //����Ԥ����
        GameObject _newText = Instantiate(popUpTextPrefab, transform.position + _positionOffset, Quaternion.identity);

        _newText.GetComponent<TextMeshPro>().color = _color;
        _newText.GetComponent<TextMeshPro>().text = _text;
    }
    #endregion

    #region Clear
    private void CancelColorChange()
    //����ʾ��bringer.fx.Invoke("CancelColorChange", 0);��Ϊ�ӳ��������ô˺���
    {
        //�˺�������ȡ��MonoBehaviour�е�����InvokeRepeating�������Ǹ���Invoke��RedBlink����
        CancelInvoke();
        //��ȷ��������ɫ�ָ�Ϊ��ɫ
        sr.color = Color.white;

        //ȡ����������Ч��
        ignitedFX.gameObject.SetActive(false);
        chilledFX.gameObject.SetActive(false);
        shockedFX.gameObject.SetActive(false);
        ignitedFX.Stop();
        chilledFX.Stop();
        shockedFX.Stop();
    }
    #endregion

    #region HitFX
    public void CreatHitFX00(Transform _target)
    //�������λ�ã��ܻ�Ч�������ڵ�������
    {
        //�����λ������ת��ʹ��Ч����������һ��
        float _xPosition = UnityEngine.Random.Range(-0.5f, 0.5f);
        float _yPosition = UnityEngine.Random.Range(-0.5f, 0.5f);
        //float _zRotation = UnityEngine.Random.Range(-90, 90);

        //����Ԥ����
        GameObject _newHitFX = Instantiate(hitFX00, _target.position + new Vector3(_xPosition, _yPosition), Quaternion.identity);
        //_newHitFX.transform.Rotate(new Vector3(0, 0, _zRotation));

        //1s������
        Destroy(_newHitFX, 1f);
    }
    public void CreatHitFX01(Transform _target)
    //�������λ�ã��ܻ�Ч�������ڵ�������
    {
        //�����λ������ת��ʹ��Ч����������һ��
        float _xPosition = UnityEngine.Random.Range(-0.5f, 0.5f);
        float _yPosition = UnityEngine.Random.Range(-0.5f, 0.5f);
        //float _zRotation = UnityEngine.Random.Range(-90, 90);

        //����Ԥ����
        GameObject _newHitFX = Instantiate(hitFX01, _target.position + new Vector3(_xPosition, _yPosition), Quaternion.identity);
        //_newHitFX.transform.Rotate(new Vector3(0, 0, _zRotation));

        //1s������
        Destroy(_newHitFX, 0.5f);
    }
    #endregion

    #region DamagedFX
    private IEnumerator FlashHitFX()
    //���������Ҫʹ����fx.StartCoroutine("FlashHitFX");�����ã�������ֱ����fx.FlashHitFX()
    {
        //ʹ���ܻ�����
        sr.material = flashHitMat;
        //�ӳ�һ��ʱ��
        yield return new WaitForSeconds(changeMatDuration);
        //�ع�ԭ���Ĳ���
        sr.material = originMat;
    }

    private IEnumerator MagicalHitFX()
    //���������Ҫʹ����fx.StartCoroutine("MagicalHitFX");�����ã�������ֱ����fx.MagicalHitFX()
    {
        //ʹ���ܻ�����
        sr.material = magicalHitMat;
        //�ӳ�һ��ʱ��
        yield return new WaitForSeconds(changeMatDuration);
        //�ع�ԭ���Ĳ���
        sr.material = originMat;
    }

    private void RedBlink()
    //���÷���ʾ��bringer.fx.InvokeRepeating("RedBlink", 0, 0.1f);��Ϊ�ӳ��������0.1f��Ƶ�ʳ�������
    {
        //��Ч����������ʵ�屻����ѣ�κ��ڵ�״̬�н��к�ɫ����˸����ʵ���StunnedState�б�InvokeRepeating���ϵ���
        if(sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }
    #endregion

    #region AilmentsFX
    public void InvokeIgnitedFXFor(float _duration)
    //����ȼ��Ч���೤ʱ��
    {
        //��������Ч��
        ignitedFX.gameObject.SetActive(true);
        ignitedFX.Play();

        //������ɫЧ��
        sr.color = ignitedColor;
        //����_durationʱ��������Ч��
        Invoke("CancelColorChange", _duration);
    }
    public void InvokeChilledFXFor(float _duration)
    {
        //��������Ч��
        chilledFX.gameObject.SetActive(true);
        chilledFX.Play();

        //������ɫЧ��
        sr.color = chilledColor;
        //����_durationʱ��������Ч��
        Invoke("CancelColorChange", _duration);
    }
    public void InvokeShockedFXFor(float _duration)
    {
        //��������Ч��
        shockedFX.gameObject.SetActive(true);
        shockedFX.Play();

        //������ɫЧ��
        sr.color = shockedColor;
        //����_durationʱ��������Ч��
        Invoke("CancelColorChange", _duration);
    }
    #endregion
}
