using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class answerController : MonoBehaviour
{
    private Color orgColor;
    private GameObject activityManager;
    void Start()
    {
        orgColor = this.gameObject.GetComponent<Renderer>().material.color;
    }

    private void OnMouseOver()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.green;

        if (Input.GetMouseButtonDown(0)) {
            StartCoroutine(this.gameObject.GetComponentInParent<activityTrigger>().checkAnswer(this.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text));
        }
    }

    private void OnMouseExit() {

        this.gameObject.GetComponent<Renderer>().material.color = orgColor;
    }
}
