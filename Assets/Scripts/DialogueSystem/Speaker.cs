#pragma warning disable 0649
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Speaker", menuName = "Dialogue/New Speaker")]
public class Speaker : ScriptableObject 
{
    [SerializeField] private string speakerName;
    [SerializeField] private Sprite speakerSprite;
    [SerializeField] private TMP_FontAsset speakerFont;

    public string GetName()
    {
        return speakerName;
    }

    public Sprite GetSprite()
    {
        return speakerSprite;
    }

    public TMP_FontAsset GetFont()
    {
        return speakerFont;
    }
}