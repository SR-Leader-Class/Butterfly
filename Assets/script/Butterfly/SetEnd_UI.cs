using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetEnd_UI : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text distance;
    [SerializeField] TMP_Text bestScore;
    [SerializeField] TMP_Text bestDistance;
    public void SetUI()
    {
        score.text = GameController.data.score.ToString();
        distance.text = GameController.data.moveDis.ToString();
        bestScore.text = CheckID.bestScore.ToString();
        bestDistance.text = CheckID.bestDis.ToString();
    }
}
