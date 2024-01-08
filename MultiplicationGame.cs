using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class MultiplicationGame : MonoBehaviour
{
    public GameObject multiplicationPopup;
    public TMP_Text questionTMP_Text;
    public TMP_InputField answerInput;

    private int correctAnswer;
    private float timer = 13f;

    private void Start()
    {
        multiplicationPopup.SetActive(false);
        InvokeRepeating("ShowMultiplicationPopup", 0f, 13f);
    }

    private void ShowMultiplicationPopup()
    {
        multiplicationPopup.SetActive(true);
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
        correctAnswer = num1 * num2;

        questionTMP_Text.text = $"{num1} x {num2} = ?";
        answerInput.text = "";

        Invoke("HideMultiplicationPopup", timer);
    }

    public void CheckAnswer()
    {
        int userAnswer;
        if (int.TryParse(answerInput.text, out userAnswer))
        {
            if (userAnswer == correctAnswer)
            {
                multiplicationPopup.SetActive(false);
            }
            else
            {
                RestartGame();
            }
        }
        else
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        // Логика перезапуска игры
        // Например, перезагрузка сцены
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void HideMultiplicationPopup()
    {
        multiplicationPopup.SetActive(false);
    }
}
//InputField