using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlider : MonoBehaviour
{
    public Image[] defaultImages; // Array of default images (length 3)
    public Sprite[] upgradedImages; // Array of sprites for upgraded images (length 3)
    public float slideDuration = 1.0f; // Duration of the slide

    private int currentIndex = 0; // To keep track of the current pair of images

    // Call this method when the puzzle is completed
    public void UpgradeImage()
    {
        if (currentIndex >= defaultImages.Length)
        {
            Debug.LogError("All images have been upgraded!");
            return;
        }

        // Start sliding out the current default image and sliding in the upgraded image
        StartCoroutine(SlideOutIn(defaultImages[currentIndex], upgradedImages[currentIndex]));
    }

    private IEnumerator SlideOutIn(Image defaultImage, Sprite upgradedSprite)
    {
        // Create the upgraded image in the same position as the default one
        Image upgradedImage = Instantiate(defaultImage, defaultImage.transform.parent);
        upgradedImage.sprite = upgradedSprite;
        upgradedImage.rectTransform.anchoredPosition = defaultImage.rectTransform.anchoredPosition;
        upgradedImage.gameObject.SetActive(true);

        // Deactivate the default image
        defaultImage.gameObject.SetActive(false);

        // Wait for half a second before starting the slide
        yield return new WaitForSeconds(1f);

        // Calculate the end position to the right of the current position
        Vector2 startPosition = upgradedImage.rectTransform.anchoredPosition;
        Vector2 endPosition = startPosition + Vector2.right * 700; // Adjust this value to move the image off-screen

        // Start sliding the upgraded image to the right
        float time = 0;
        while (time <= slideDuration)
        {
            time += Time.deltaTime;
            float t = time / slideDuration;
            upgradedImage.rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        // Deactivate the upgraded image after sliding off the screen
        upgradedImage.gameObject.SetActive(false);

        // Optionally fade out the upgraded image here if you want a fading effect
        // StartCoroutine(FadeOutImage(upgradedImage));

        // Update the current index
        currentIndex++;

        // Optionally call a method to slide in the next default image
        StartCoroutine(SlideInNextImage());
    }

    private IEnumerator SlideInNextImage()
    {
        if (currentIndex >= defaultImages.Length)
        {
            Debug.LogError("All images have been upgraded!");
            yield break;
        }

        // Create the next default image off-screen to the left
        Image nextImage = Instantiate(defaultImages[currentIndex], defaultImages[currentIndex].transform.parent);
        nextImage.rectTransform.anchoredPosition = defaultImages[currentIndex].rectTransform.anchoredPosition - Vector2.right * 700; // Adjust this value to move the image off-screen
        nextImage.gameObject.SetActive(true);

        // Calculate the end position to the right of the current position
        Vector2 startPosition = nextImage.rectTransform.anchoredPosition;
        Vector2 endPosition = startPosition + Vector2.right * 700; // Adjust this value to move the image off-screen

        // Start sliding the next default image to the right
        float time = 0;
        while (time <= slideDuration)
        {
            time += Time.deltaTime;
            float t = time / slideDuration;
            nextImage.rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        // Optionally fade in the next default image here if you want a fading effect
        // StartCoroutine(FadeInImage(nextImage));
    }

    // get image function for the puzzle
    public Sprite GetImage()
    {
        return upgradedImages[currentIndex+1];

    }

}
