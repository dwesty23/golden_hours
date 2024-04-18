using UnityEngine;

public class AudioManaging : MonoBehaviour
{
    [Header("----------Audio Sources----------")]
    [SerializeField] AudioSource backgroundSource;
    [SerializeField] AudioSource sfxSource;

    [Header("----------Audio Clips----------")]
    public AudioClip wind;
    public AudioClip jump;
    public AudioClip land;

    private void Start()
{
    backgroundSource.clip = wind;
    backgroundSource.loop = true;
    backgroundSource.Play();
}

public void PlaySFX(AudioClip clip)
{
    sfxSource.PlayOneShot(clip);
}

}