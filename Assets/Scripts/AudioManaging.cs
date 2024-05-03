using UnityEngine;

public class AudioManaging : MonoBehaviour
{
    [Header("----------Audio Sources----------")]
    [SerializeField] AudioSource backgroundSource;
    [SerializeField] AudioSource sfxSource;

    [Header("----------General Audio Clips----------")]
    public AudioClip background;
    public AudioClip jump;
    public AudioClip land;
    public AudioClip inspect;
    
    [Header("----------Puzzle 1 Audio Clips----------")]

    public AudioClip puzzle1Complete;
    public AudioClip glassSlide;
    public AudioClip tileSlide;

    [Header("----------Puzzle 2 Audio Clips----------")]
    public AudioClip melody1;

    public AudioClip puzzle2Complete;

    public AudioClip[] switchSounds;

    


    private void Start(){

    backgroundSource.clip = background;
    
    backgroundSource.loop = true;
    backgroundSource.Play();
}

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void StopBackgroundSound()
        {
            if (backgroundSource.isPlaying)
            {
                backgroundSource.Stop();
            }
        }



}