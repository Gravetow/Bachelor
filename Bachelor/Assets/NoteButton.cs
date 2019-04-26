using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoteButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Sprite[] sprites = new Sprite[5];

    private Image image;

    private int currentSpriteNumber = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentSpriteNumber >= sprites.Length)
        {
            currentSpriteNumber = 0;
        }
        image.sprite = sprites[currentSpriteNumber];
        currentSpriteNumber++;
    }

    private void Awake()
    {
        image = GetComponent<Image>();
    }
}