using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONreader : MonoBehaviour
{
    public TextAsset MathSheet;
    //public TextAsset ScienceSheet;

    [System.Serializable]
    public class Math {

        public string question;
        public string[] answers;
        public string correctAnswer;
    
    }
    [System.Serializable]
    public class QuesList
    {
        public Math[] math;
        
    }

    public QuesList quesList = new QuesList();

    void Start()
    {
        quesList = JsonUtility.FromJson<QuesList>(MathSheet.text);
    }
}