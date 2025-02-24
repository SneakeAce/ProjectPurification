using System.Collections;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    [SerializeField] private GameObject _playerUI;
    [SerializeField] private GameObject _trainingText;
    [SerializeField] private float _timeBetweenStartLevel;

    [SerializeField] private Bootstrap _bootstrap;

    private Coroutine _startLevelCoroutine;

    // Добавить анимации появления интерфейса игрока и сокрытие интерфейса обучения.

    private void Start()
    {
        _bootstrap.Initialization();
    }

    public void StartLvl()
    {
        //_startLevelCoroutine = StartCoroutine(StartLevelCoroutine());
    }

    private IEnumerator StartLevelCoroutine()
    {
        _bootstrap.Initialization();

        yield return new WaitForSeconds(_timeBetweenStartLevel);

        _playerUI.SetActive(true);

        _trainingText.SetActive(false);

        StopCoroutine(_startLevelCoroutine);
        _startLevelCoroutine = null;
    }
}
