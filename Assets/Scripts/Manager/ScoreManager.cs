using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddScore(int value)
    {
        score = score + value;
        Debug.Log("Target has been hit. You have a score of " + score);
    }
}
