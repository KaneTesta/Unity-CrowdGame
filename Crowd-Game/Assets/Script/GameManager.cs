using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject SurferScript;
    public Text ScoreText;
    public GameObject GameOverText;
    public GameObject[] skulls = new GameObject[3];
    int l = 3;
    int score = 0;
    bool updateLives = false;

    void Start() {
        SetScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        //Get Lives from SurferSpawn GameObject
        if (l!=SurferScript.GetComponent<Spawn>().lives){
            l = SurferScript.GetComponent<Spawn>().lives;
            updateLives = true;
        }
        score = SurferScript.GetComponent<Spawn>().surfCount; //CHANGE THIS FOR THE ACTUAL SCORE FIELD

        if (updateLives){
            if (l == 0){
                GameOver();
                skulls[0].SetActive(false);
            } else if (l == 1){
                skulls[1].SetActive(false);
            } else if (l == 2){
                skulls[2].SetActive(false);
            }
            updateLives = false;
        }
        SetScoreText();
    }

    void SetScoreText() {
        ScoreText.text = score.ToString();
    }

    void GameOver() {
        GameOverText.SetActive(true);
        Invoke("Restart", 5f);
    }

    void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameOverText.SetActive(false);
        score = 0;
        l = 0;
    }

}
