using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class MainMenuController : MonoBehaviour
{
    private SaveSample _save;
    [SerializeField] private GameObject resultsWindow;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI resultTimeText;

    private void Start()
    {
        LoadSave();
        SwitchWindows();
    }
    

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadSave()
    {
        var path = SavePathCreator.GetPath();
        
        if(!File.Exists(path)) return;
        _save = JsonUtility.FromJson<SaveSample>(File.ReadAllText(path));
    }

    private void SwitchWindows()
    {
        var howPlayerGetHere = PlayerPrefs.GetInt("played", 0); 
        // 0 - just started, 1 - just played, 2 - pressed esq
        if (howPlayerGetHere == 0)
        {
            resultText.text = "No data";
        }
        else if (howPlayerGetHere == 1)
        {
            resultsWindow.SetActive(true);
            DrawResults();
        }
        else
        {
            DrawResults();
        }
        PlayerPrefs.DeleteKey("played");
    }

    private void DrawResults()
    {
        resultText.text = _save.Won ? "You won!" : "You lose :(";
        resultTimeText.text = "You time: " 
                              + _save.Minutes.ToString("D2") + ":"
                              + _save.Seconds.ToString("D2");
    }
}
