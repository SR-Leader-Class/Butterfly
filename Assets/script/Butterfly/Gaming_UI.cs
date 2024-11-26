using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gaming_UI : MonoBehaviour
{
    [SerializeField] TMP_Text Score;
    [SerializeField] TMP_Text Time;
    float lessTime;
    float lessMin;
    float lessSec;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = GameController.data.score.ToString();
        lessTime = GameController.playTime - UnityEngine.Time.time + GameController.startTime;
        lessMin = (int)lessTime / 60;
        lessSec = (int)(lessTime - lessMin*60);
        Time.text = string.Format("{0}:{1}", lessMin, lessSec);
    }
}
