using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class selectionMenuCont : MonoBehaviour
{
    public Button selectionButton;

    public Sprite[] characters;

    private int selectionIndex;

    private int previousIndex;

    public GameObject player;

    public Image selection;

    void Start()
    {
        selectionButton.interactable = false;
        selectionIndex = 0;
        previousIndex = 0;
    }

    public void nextSelection() {
        
        if (selectionIndex < characters.Length-1) {
            selectionIndex++;
            selection.sprite = characters[selectionIndex];
        }
        
    }

    public void previousSelection() {
        if (selectionIndex > 0)
        {
            selectionIndex--;
            selection.sprite = characters[selectionIndex];
        }

    }

    public void stretch() {
        this.transform.DOScale(0.9f, 1f);
        selectionButton.interactable = true;
    }

    public void compress() {
        this.transform.DOScale(0f, 1f);
        selectionButton.interactable = false;
    }

    public void select() {

        for (int i = 0; i < player.transform.childCount; i++) {
            if (player.transform.GetChild(i).gameObject.activeInHierarchy) {
                player.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        player.transform.GetChild(selectionIndex).gameObject.SetActive(true);
        player.GetComponent<PlayerController>().setAnimator(selectionIndex);

        this.transform.DOScale(0f, 1.25f);

    }
}
