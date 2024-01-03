using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class profileMenuCont : MonoBehaviour
{
    [SerializeField]
    private float finalWidth;

    [SerializeField]
    private float finalPosX;

    [SerializeField]
    private float finalScale;

    [SerializeField]
    private Button button;

    private float orgWidth;

    private float orgPosX;

    private float orgScale;
    
    void Start()
    {
        orgWidth = this.GetComponent<RectTransform>().rect.width;
        orgPosX = this.transform.position.x;
        orgScale = this.transform.localScale.x;
    }


    public void stretch() {


        if (this.transform.localScale.x < 1)
        {
            this.GetComponent<RectTransform>().DOSizeDelta(new Vector2(finalWidth, 100f), 1f);
            this.transform.DOMoveX(finalPosX + 961f, 1f);
            this.transform.DOScale(finalScale, 1f);

            for (int i = 0; i < this.transform.childCount; i++) {
                this.transform.GetChild(i).GetComponent<Image>().DOFade(1f, 1f);
            }
            
        }
        else {

            this.GetComponent<RectTransform>().DOSizeDelta(new Vector2(orgWidth, 100f), 1f);
            this.transform.DOMoveX(orgPosX, 1f);
            this.transform.DOScale(orgScale, 1f);

            for (int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).GetComponent<Image>().DOFade(0f, 0.2f);
            }

        }

        StartCoroutine(timeout());
    }

    public IEnumerator timeout() {
        button.interactable = false;
        yield return new WaitForSeconds(1.3f);
        button.interactable = true;
    }

}
