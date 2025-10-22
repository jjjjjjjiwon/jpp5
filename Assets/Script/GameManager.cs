using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    const int SCORE_SPAWN = 5;
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public GameObject GameOverText;
    public bool isGameActive;
    public Button reStartButton;
    public GameObject titleScreen;

    private float spawnRate = 1.0f;
    private int score;

    void Start()
    {
        GameOver(false);
        score = 0;
        UpdateScore(0);
    }
    
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = $"Score : {score}";
    }

    public void GameOver(bool value)
    {
        isGameActive = !value;
        GameOverText.SetActive(value);
        reStartButton.gameObject.SetActive(value);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void StartGame(int difficulty)
    {
        spawnRate = difficulty;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        titleScreen.SetActive(false);
    }
}
