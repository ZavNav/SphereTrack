using System.Collections;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class GameController : MonoBehaviour
{
    public static GameController Singleton;
    
    [SerializeField] private TextMeshProUGUI timerText;

    private Coroutine _timerFlowCoroutine;
    private int _seconds;
    private int _minutes;
    private bool _won;
    private bool _isRaceEnded = false;

    private bool _esqPressed;
    

    private void Awake()
    {
        Singleton = this;
        Time.timeScale = 0;
    }

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        
        PlayerPrefs.SetInt("played", 2);
        SceneManager.LoadScene(0);
    }

    public void StartRace(GameObject buttonItself)
    {
        Time.timeScale = 1;
        buttonItself.SetActive(false);
        //TimerFLow();
        StartCoroutine(TimerFlow());
    }

    private IEnumerator TimerFlow()
    {
        while (!_isRaceEnded)
        {
            yield return new WaitForSeconds(1);
            _seconds++;
            if (_seconds == 60)
            {
                _seconds = 0;
                _minutes++;
            }

            timerText.text = _minutes.ToString("D2") + " : " + _seconds.ToString("D2");
        }
    }
    private async void TimerFLowOld()
    {
        while (!_isRaceEnded)
        {
            await Task.Delay(1000);
            _seconds++;
            if (_seconds == 60)
            {
                _seconds = 0;
                _minutes++;
            }

            timerText.text = _minutes.ToString("D2") + " : " + _seconds.ToString("D2");
        }
    }
    public void OpenResults(bool won)
    {
        _isRaceEnded = true;
        _won = won;
        SaveResult();
        PlayerPrefs.SetInt("played", 1);
        SceneManager.LoadScene(0);
    }

    private void SaveResult()
    {
        var path = SavePathCreator.GetPath();
        var save = new SaveSample(_minutes, _seconds, _won);
        File.WriteAllText(path, JsonUtility.ToJson(save));
    }
}
