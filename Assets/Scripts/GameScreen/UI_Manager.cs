using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _restartText;
    void Start()
    {
        _scoreText.text = "Score: 0";
        _livesImage.sprite = _liveSprites[3];
        _gameOver.gameObject.SetActive(false);
    }
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }
    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }
    private void GameOverSequence()
    {
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOver());
    }
    IEnumerator GameOver()
    {
        while (1==1)
        {
            _gameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(.5f);
            _gameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(.5f);
        }
    }
}
