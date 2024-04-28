using System;
using System.Collections;
using UnityEngine;

public class PuzzleObject : MonoBehaviour
{
    [SerializeField] private int puzzleNumber;
    [SerializeField] private GameObject[] puzzleSprite;
    [SerializeField] private GameObject[] disableSprite;

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
        bool puzzle;

        switch (puzzleNumber)
        {
            case 1:
                puzzle = Scenes.Instance.puzzle1Finished;
                // log if this is reached and print the value of puzzle
                Debug.Log("Puzzle 1: " + puzzle);
                break;
            case 2:
                puzzle = Scenes.Instance.puzzle2Finished;
                break;
            case 3:
                puzzle = Scenes.Instance.puzzle3Finished;
                break;
            default:
                puzzle = false;
                break;
        }

        for(int i = 0; i < puzzleSprite.Length; i++)
        {
            puzzleSprite[i].SetActive(puzzle);
        }

        for(int i = 0; i < disableSprite.Length; i++)
        {
            disableSprite[i].SetActive(!puzzle);
        }
    }
}