using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class indicatorTigger : MonoBehaviour
{

    public string desiredIndicator;
    public TextMeshProUGUI indicator;
    public Vector3 teleportPos;
    public Image transition;
    public GameObject firstCamera;
    public GameObject secondCamera;
    private GameObject player;

    void Start()
    {
        indicator.text = "";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) {
            indicator.text = desiredIndicator;
            if (Input.GetKey(KeyCode.E)) {
                StartCoroutine(teleport());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            indicator.text = "";
        }
    }

    public IEnumerator teleport() {
        player = GameObject.Find("Player");
        transition.transform.DOScale(50f, 2f);
        yield return new WaitForSeconds(2f);
        player.GetComponent<PlayerController>().teleport(teleportPos);
        player.GetComponent<PlayerController>().teleport(teleportPos);
        player.GetComponent<PlayerController>().teleport(teleportPos);
        player.GetComponent<PlayerController>().teleport(teleportPos);
        player.GetComponent<PlayerController>().teleport(teleportPos);
        player.GetComponent<PlayerController>().teleport(teleportPos);
        firstCamera.SetActive(false);
        secondCamera.SetActive(true);
        yield return new WaitForSeconds(1f);
        transition.transform.DOScale(0f, 1f);
    }
}
