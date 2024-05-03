using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class SceneFadeManager
{
    private static Image _fadeOutImage;
    private static float _fadeOutSpeed = 5f;
    private static float _fadeInSpeed = 5f;
    private static Color _fadeOutStartColor;
    public static bool isFadingOut { get; private set; }
    public static bool isFadingIn { get; private set; }

    // Initialize the static class
    static SceneFadeManager()
    {
        GameObject fadeImageObject = new GameObject("FadeImage");
        _fadeOutImage = fadeImageObject.AddComponent<Image>();
        _fadeOutImage.color = _fadeOutStartColor;
    }

    private static void Update()
    {
        if (isFadingOut)
        {
            FadeOut();
        }
        if (isFadingIn)
        {
            FadeIn();
        }
    }

    private static void FadeOut()
    {
        if (_fadeOutImage.color.a < 1f)
        {
            _fadeOutStartColor.a += Time.deltaTime * _fadeOutSpeed;
            _fadeOutImage.color = _fadeOutStartColor;
        }
        else
        {
            isFadingOut = false;
        }
    }

    // private void FadeIn()
    // {
        
    //     if (_fadeOutImage.color.a > 0f)
    //     {
    //         _fadeOutStartColor.a -= Time.deltaTime * _fadeInSpeed;
    //         _fadeOutImage.color = _fadeOutStartColor;
    //     }
    //     else
    //     {
    //         isFadingIn = false;
    //     }
    // }

    // public void StartFadeOut()
    // {
    //     _fadeOutImage.color = _fadeOutStartColor;
    //     isFadingOut = true;
    // }

    // public void StartFadeIn()
    // {
    //     if(_fadeOutImage.color.a >= 1f)
    //     {
    //         _fadeOutImage.color = _fadeOutStartColor;
    //         isFadingIn = true;
    //     }
        
    // } 

    private static IEnumerator FadeIn()
    {
        while (_fadeOutImage.color.a > 0f)
        {
            _fadeOutStartColor.a -= Time.deltaTime * _fadeInSpeed;
            _fadeOutImage.color = _fadeOutStartColor;
            yield return null;
        }
        isFadingIn = false;
    }

    public static IEnumerator StartFadeOut()
    {
        _fadeOutImage.color = _fadeOutStartColor;
        isFadingOut = true;
        while (_fadeOutImage.color.a < 1f)
        {
            _fadeOutStartColor.a += Time.deltaTime * _fadeOutSpeed;
            _fadeOutImage.color = _fadeOutStartColor;
            yield return null;
        }
    }

    public static IEnumerator StartFadeIn()
    {
        if(_fadeOutImage.color.a >= 1f)
        {
            _fadeOutImage.color = _fadeOutStartColor;
            isFadingIn = true;
            while (_fadeOutImage.color.a > 0f)
            {
                _fadeOutStartColor.a -= Time.deltaTime * _fadeInSpeed;
                _fadeOutImage.color = _fadeOutStartColor;
                yield return null;
            }
            isFadingIn = false;
        }
    }
}