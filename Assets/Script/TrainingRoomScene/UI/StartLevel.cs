using System.Collections;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    [SerializeField] private GameObject _playerUI;
    [SerializeField] private GameObject _trainingText;
    [SerializeField] private float _timeBetweenStartLevel;

    private Coroutine _startLevelCoroutine;

    // �������� �������� ��������� ���������� ������ � �������� ���������� ��������.

    public void StartLvl()
    {
        _startLevelCoroutine = StartCoroutine(StartLevelCoroutine());
    }

    private IEnumerator StartLevelCoroutine()
    {
        yield return new WaitForSeconds(_timeBetweenStartLevel);

        _playerUI.SetActive(true);

        _trainingText.SetActive(false);

        StopCoroutine(_startLevelCoroutine);
        _startLevelCoroutine = null;
    }
}
