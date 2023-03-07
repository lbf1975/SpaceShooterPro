using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] Sprite[] _liveSprites;
    [SerializeField] Text _scoreText;
    [SerializeField] Image _livesImg;
    [SerializeField] Text _gameOverText;
    [SerializeField] bool _gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];

        if (currentLives == 0)
        {
            _gameOver = true;
            StartCoroutine(GameOverFlicker());
        }
    }

   private IEnumerator GameOverFlicker()
    {
        while(_gameOver == true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);

            if (Input.GetKey("r"))
            {
                Restart();
            }

        }

    }
    //void Update()
    //{
    //    if(Input.GetKey("r"))
    //    {
    //        Restart();
    //    }
    //}

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}


