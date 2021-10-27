using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MAG.Model;
using TMPro;

public class LeaderboardEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI positionText;  
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI scoreText;


    public void Setup(LeaderboardEntryModel model, int position)
    {
        positionText.text = string.Concat(position.ToString(), ".");
        nameText.text = model.Name;
        scoreText.text = model.Score.ToString();
    }
}
