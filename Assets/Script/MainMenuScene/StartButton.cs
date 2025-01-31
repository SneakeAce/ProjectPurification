using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] private float _timeBeforeStartGame;
    private Coroutine _startGameCoroutine;

    public void StartGame()
    {
        _startGameCoroutine = StartCoroutine(StartGameJob());
    }

    private IEnumerator StartGameJob()
    {
        yield return new WaitForSeconds(_timeBeforeStartGame);

        SceneManager.LoadScene("TrainingRoom");

        Time.timeScale = 1.0f;

        StopCoroutine(_startGameCoroutine);
        _startGameCoroutine = null;
    }
}
