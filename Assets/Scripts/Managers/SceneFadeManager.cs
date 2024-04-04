using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFadeManager : MonoBehaviour
{

    public static SceneFadeManager instance;

    [SerializeField] private Image _fadeOutImage;
    [Range(0.1f, 10f), SerializeField] private float _fadeOutSpeed = 5f;
    [Range(0.1f, 10f), SerializeField] private float _fadeInSpeed = 5f;

    [SerializeField] private Color _fadeOutStartColor;    

    public bool isFadingOut { get; private set; }
    public bool isFadingIn { get; private set; }
    // Start is called before the first frame update
    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }

        _fadeOutStartColor.a = 0f;
    }

    private void Update()
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

    private void FadeOut()
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

    private void FadeIn()
    {
        
        if (_fadeOutImage.color.a > 0f)
        {
            _fadeOutStartColor.a -= Time.deltaTime * _fadeInSpeed;
            _fadeOutImage.color = _fadeOutStartColor;
        }
        else
        {
            isFadingIn = false;
        }
    }

    public void StartFadeOut()
    {
        _fadeOutImage.color = _fadeOutStartColor;
        isFadingOut = true;
    }

    public void StartFadeIn()
    {
        if(_fadeOutImage.color.a >= 1f)
        {
            _fadeOutImage.color = _fadeOutStartColor;
            isFadingIn = true;
        }
        
    }
}
