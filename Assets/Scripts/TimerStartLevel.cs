using UnityEngine;
using TMPro;
using System.Collections;

public class TimerStartLevel : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreboard;
    [SerializeField] private float _delayStart;
    [SerializeField] private TimeSceler _timeSceler;

    private void Start()
    {
        StartCoroutine(Сountdown());
    }

    private IEnumerator Сountdown() 
    {
        _timeSceler.PauseGame();

        while (_delayStart>0)
        {
            _delayStart -= Time.unscaledDeltaTime;
            _scoreboard.text= ((int)_delayStart).ToString();
            yield return new WaitForEndOfFrame();
        }

        _scoreboard.enabled = false;
        _timeSceler.PlayGame();
    }
}
