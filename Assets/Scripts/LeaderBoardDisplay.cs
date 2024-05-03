using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class LeaderBoardDisplay : MonoBehaviour
{
    public int score1;
    public int score2;
    public int score3;
    public int score4;
    public int score5;
    public int score6;
    public int score7;
    public int score8;
    public int score9;
    public int score10;
    public int add1;
    public int add2;
    public int add3;
    public int add4;
    public int add5;
    public int add6;
    public int add7;
    public int add8;
    public int add9;
    public int add10;
    public Text scoreText1;
    public Text scoreText2;
    public Text scoreText3;
    public Text scoreText4;
    public Text scoreText5;
    public Text scoreText6;
    public Text scoreText7;
    public Text scoreText8;
    public Text scoreText9;
    public Text scoreText10;
    float scoreCD = 5;
    float addCD;
    // Start is called before the first frame update
    void Start()
    {
        addCD = scoreCD;
    }

    // Update is called once per frame
    void Update()
    {
        AddScore();
        scoreText1.text = score1.ToString();
        scoreText2.text = score2.ToString();
        scoreText3.text = score3.ToString();
        scoreText4.text = score4.ToString();
        scoreText5.text = score5.ToString();
        scoreText6.text = score6.ToString();
        scoreText7.text = score7.ToString();
        scoreText8.text = score8.ToString();
        scoreText9.text = score9.ToString();
        scoreText10.text = score10.ToString();
    }

    void AddScore()
    {
        if (addCD <= 0)
        {
            score1 += Random.Range(add1, add1 + 2) * 10;
            score2 += Random.Range(add2, add2 + 2) * 10;
            score3 += Random.Range(add3, add3 + 2) * 10;
            score4 += Random.Range(add4, add4 + 2) * 10;
            score5 += Random.Range(add5, add5 + 2) * 10;
            score6 += Random.Range(add6, add6 + 2) * 10;
            score7 += Random.Range(add7, add7 + 2) * 10;
            score8 += Random.Range(add8, add8 + 2) * 10;
            score9 += Random.Range(add9, add9 + 2) * 10;
            score10 += Random.Range(add10, add10 + 2) * 10;
            addCD = scoreCD;
            return;
        }
        addCD -= Time.deltaTime;
    }

}
