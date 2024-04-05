using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private TMP_Text highScoreText;

    public void UpdateUI(int currentHighScore)
    {
        highScoreText.text = currentHighScore.ToString();
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
        AndroidJavaObject activity = 
            new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call<bool>("moveTaskToBack", true);
#elif UNITY_STANDALONE_WIN
        Application.Quit();  
#endif
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
