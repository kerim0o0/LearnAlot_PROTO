using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    public void startIntro() {

        StartCoroutine(intro());
    }

    private IEnumerator intro() {
        yield return new WaitForSeconds(0.25f);
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.Find("mainMenuCanvas").SetActive(false);
        //GameObject.Find("profileMenu").SetActive(false);
        yield return new WaitForSeconds(0.25f);
        this.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(2.25f);
        this.GetComponent<Animator>().enabled = false;
        this.transform.DOMove(GameObject.Find("Player").transform.position, 3f);
        yield return new WaitForSeconds(2.5f);
        this.GetComponent<CinemachineBrain>().enabled = true;
    }
}
