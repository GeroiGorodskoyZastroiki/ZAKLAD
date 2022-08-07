using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{
    Animator animator;
    Image splashImage;

    [SerializeField]
    Sprite companySplash, endGameSplash;

    public static Action onSplashEnd;

    void Awake()
    {
        animator = GetComponent<Animator>();
        splashImage = transform.Find("Image").gameObject.GetComponent<Image>();
    }

    public IEnumerator SplashCompany()
    {
        gameObject.SetActive(true);
        splashImage.sprite = companySplash;
        animator.Play("Wait");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        animator.Play("FadeOut");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        onSplashEnd.Invoke();
        gameObject.SetActive(false);
        yield break;
    }

    public IEnumerator SplashEndGame()
    {
        gameObject.SetActive(true);
        splashImage.sprite = endGameSplash;
        animator.Play("FadeIn");
        yield break;
    }
}