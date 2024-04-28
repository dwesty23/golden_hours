using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlider : MonoBehaviour
{
    public Image[] defaultImages; // Array of default images (length 3)
    public Sprite[] upgradedImages; // Array of sprites for upgraded images (length 3)
    public float slideDuration = 1.0f; // Duration of the slide

    public int currentIndex = 0; // To keep track of the current pair of images

    // sound stuff
    AudioManaging audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManaging>();    
    }


    // Call this method when the puzzle is completed
   // Call this method when the puzzle is completed

    public void UpgradeImage()
    {
        if (currentIndex >= defaultImages.Length)
        {
            Debug.LogError("All images have been upgraded!");
            return;
        }

        StartCoroutine(SlideOutIn(currentIndex));
    }

    private IEnumerator SlideOutIn(int index)
    {
        Image defaultImage = defaultImages[index];
        Sprite upgradedSprite = upgradedImages[index];

        Image upgradedImage = Instantiate(defaultImage, defaultImage.transform.parent);
        upgradedImage.sprite = upgradedSprite;
        upgradedImage.rectTransform.anchoredPosition = defaultImage.rectTransform.anchoredPosition;
        upgradedImage.gameObject.SetActive(true);

        defaultImage.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        audioManager.PlaySFX(audioManager.glassSlide);

        Vector2 startPosition = upgradedImage.rectTransform.anchoredPosition;
        Vector2 endPosition = startPosition + Vector2.right * 700;

        float time = 0;
        while (time <= slideDuration)
        {
            time += Time.deltaTime;
            float t = time / slideDuration;
            upgradedImage.rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        upgradedImage.gameObject.SetActive(false);

        // Ensure the default image for the next round is updated if it's re-instantiated.
        if (currentIndex + 1 < defaultImages.Length)
        {
            Image nextDefaultImage = Instantiate(defaultImages[currentIndex + 1], defaultImages[currentIndex + 1].transform.parent);
            defaultImages[currentIndex + 1] = nextDefaultImage;
        }

        currentIndex++;

        StartCoroutine(SlideInNextImage(index + 1));
    }

    private IEnumerator SlideInNextImage(int index)
    {
        if (index >= defaultImages.Length)
        {
            Debug.LogError("All images have been upgraded!");
            yield break;
        }

        Image nextImage = defaultImages[index]; // This now references the newly instantiated image.
        nextImage.rectTransform.anchoredPosition -= Vector2.right * 700;
        nextImage.gameObject.SetActive(true);

        Vector2 startPosition = nextImage.rectTransform.anchoredPosition;
        Vector2 endPosition = startPosition + Vector2.right * 700;

        float time = 0;
        while (time <= slideDuration)
        {
            time += Time.deltaTime;
            float t = time / slideDuration;
            nextImage.rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
            yield return null;
        }
    }


}
