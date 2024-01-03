using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class activityTrigger : MonoBehaviour
{
    public string desiredIndicator;
    public TextMeshProUGUI indicator;
    public GameObject viewCamera;
    public GameObject mainCamera;
    public GameObject activityObject;
    public GameObject JSONreader;
    public GameObject gemReward;
    public GameObject rewardSpawn;
    public Button exitButton;
    public GameObject VFX;
    public TextMeshProUGUI currentlevel;
    public TextMeshProUGUI currentRewardCount;
    public statsController sC;
    private int questionIndex;
    private JSONreader jR;
    private bool activityStarted = false;

    void Start()
    {
        indicator.text = "";

        for (int i = 0; i < activityObject.gameObject.transform.childCount-1; i++) {
            activityObject.gameObject.transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(0, 0f);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                JSONreader.SetActive(true);
                jR = JSONreader.GetComponent<JSONreader>();
                StartCoroutine(loadActivity());
            }

            if (!activityStarted)
            {
                indicator.text = desiredIndicator;
            }
            else
            {
                indicator.text = "";
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

    public IEnumerator loadActivity()
    {
        activityStarted = true;
        Cursor.lockState = CursorLockMode.None;
        exitButton.gameObject.SetActive(true);
        exitButton.onClick.AddListener(() =>
        {
            StartCoroutine(unloadActivity());

        });
        mainCamera.transform.position = viewCamera.transform.position;
        mainCamera.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        viewCamera.gameObject.SetActive(false);
        mainCamera.gameObject.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        mainCamera.transform.DOMove(new Vector3(viewCamera.transform.position.x, viewCamera.transform.position.y+0.5f, viewCamera.transform.position.z - 4f), 1f);
        yield return new WaitForSeconds(1f);
        activityObject.transform.DOLocalMoveZ(1.25f, 1f);
        yield return new WaitForSeconds(1f);
        StartCoroutine(loadQuestion());
    }

    public IEnumerator unloadActivity() {

        for (int i = 0; i < activityObject.gameObject.transform.childCount - 1; i++)
        {
            activityObject.gameObject.transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(0, 1f);
        }
        yield return new WaitForSeconds(1f);
        activityObject.transform.DOLocalMoveZ(-1f, 1f);
        yield return new WaitForSeconds(1f);
        mainCamera.transform.DOMove(new Vector3(viewCamera.transform.position.x, viewCamera.transform.position.y - 0.5f, viewCamera.transform.position.z + 4f), 1f);
        yield return new WaitForSeconds(1f);
        mainCamera.gameObject.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;
        viewCamera.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        exitButton.gameObject.SetActive(false);
        JSONreader.SetActive(false);
        jR = null;
        activityStarted = false;
    }

    public IEnumerator loadQuestion() {

        for (int i = 0; i < activityObject.gameObject.transform.childCount-1; i++)
        {
            activityObject.gameObject.transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(0, 1f);
        }
        
        yield return new WaitForSeconds(1f);

        questionIndex = Random.Range(0, jR.quesList.math.Length);
        activityObject.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshPro>().text = jR.quesList.math[questionIndex].question;
        
        for (int i = 1; i < activityObject.transform.childCount-1; i++) {
            if(activityObject.gameObject.transform.GetChild(i).name != "line")
            activityObject.gameObject.transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshPro>().text = jR.quesList.math[questionIndex].answers[i-1];
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < activityObject.gameObject.transform.childCount-1; i++)
        {
            activityObject.gameObject.transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(1, 1f);
        }
    }

    public IEnumerator checkAnswer(string answer) {
        
        GameObject targetVFX;
        
        if (answer == jR.quesList.math[questionIndex].correctAnswer)
        {
            targetVFX = VFX.transform.GetChild(0).gameObject;
            sC.IncreaseXP();
            spawnGem();
            spawnGem();
            spawnGem();

        }
        else {
            targetVFX = VFX.transform.GetChild(1).gameObject;
            sC.DecreaseXP();
        }


        for (int i = 0; i < targetVFX.transform.childCount; i++) {

            targetVFX.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
        }

        yield return new WaitForSeconds(1.25f);

        StartCoroutine(loadQuestion());
    
    }

    public void spawnGem() {
        GameObject gem = Instantiate(gemReward, rewardSpawn.transform.position, Quaternion.identity);
        Vector3 target = GameObject.Find("Player").transform.position;
        gem.transform.DOMove(new Vector3(target.x, target.y + 5f, target.z), 1.5f).OnComplete(() => {
            int count = System.Convert.ToInt32(currentRewardCount.text + "");
            count++;
            currentRewardCount.text = count + "";
            Destroy(gem, 0.25f);
        });
    }


}