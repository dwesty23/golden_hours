using System.Collections;
using UnityEngine;

public class PuzzleObject : MonoBehaviour
{
    [SerializeField] private int puzzleNumber;
    [SerializeField] private GameObject puzzleSprite;

    private void Start()
    {
        StartCoroutine(CheckPuzzleCompletionAfterStart());
    }

    private IEnumerator CheckPuzzleCompletionAfterStart()
    {
        yield return new WaitForEndOfFrame();
        CheckPuzzleCompletion();
    }

    public void CheckPuzzleCompletion()
    {
        switch (puzzleNumber)
        {
            case 1:
                puzzleSprite.SetActive(Scenes.Instance.puzzle1Finished);
                break;
            case 2:
                puzzleSprite.SetActive(Scenes.Instance.puzzle2Finished);
                break;
            case 3:
                puzzleSprite.SetActive(Scenes.Instance.puzzle3Finished);
                break;
        }
    }
}