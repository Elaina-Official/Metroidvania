using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FadeScreen : MonoBehaviour
{
    [SerializeField] private Animator anim;

    //����fadeOut��fadeIn��Animator�ڶ�Ӧ��parameter��Trigger���͵�
    public void FadeOut() => anim.SetTrigger("fadeOut");

    public void FadeIn() => anim.SetTrigger("fadeIn");
}
