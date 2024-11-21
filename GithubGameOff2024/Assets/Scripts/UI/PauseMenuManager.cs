using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement root;
    private VisualElement pausePanel;
    private VisualElement settingsPanel;

    private void Awake()
    {
        InitializePauseMenu();
    }

    private void InitializePauseMenu()
    {
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument component not found for Pause Menu.");
            return;
        }

        root = uiDocument.rootVisualElement;
        root.visible = false;

        pausePanel = root.Q("pause-panel");
        settingsPanel = root.Q("settings-panel");

        SetupButtons();
        SetupSliders();

        // Initially hide both panels
        pausePanel.AddToClassList("hide");
        settingsPanel.AddToClassList("hide");
    }

    private void SetupButtons()
    {
        root.Q<Button>("resume-button").clicked += () => UIManager.Instance.ClosePauseMenu();
        root.Q<Button>("settings-button").clicked += OpenSettings;
        root.Q<Button>("main-menu-button").clicked += LoadMainMenu;
        root.Q<Button>("quit-button").clicked += QuitGame;
        root.Q<Button>("settings-back-button").clicked += CloseSettings;
    }

    private void SetupSliders()
    {
        SetupSlider("master-volume", "MasterVolume", 100f);

    }

    private void SetupSlider(string sliderName, string prefName, float defaultValue)
    {
        var slider = root.Q<Slider>(sliderName);
        if (slider != null)
        {
            slider.value = PlayerPrefs.GetFloat(prefName, defaultValue);
            slider.RegisterValueChangedCallback(evt =>
            {
                PlayerPrefs.SetFloat(prefName, evt.newValue);
                PlayerPrefs.Save();
                Debug.Log($"{prefName} set to {evt.newValue}");
                OnSettingChanged(prefName, evt.newValue);
            });
        }
    }

    private void OnSettingChanged(string settingName, float value)
    {
        switch (settingName)
        {
            case "MasterVolume":
                // Implement master volume change
                break;
        }
    }

    public void OpenPauseMenu()
    {
        root.visible = true;
        Time.timeScale = 0f;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

        pausePanel.RemoveFromClassList("hide");
        pausePanel.AddToClassList("show");
        settingsPanel.AddToClassList("hide");
        settingsPanel.RemoveFromClassList("show");
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1f;
        pausePanel.RemoveFromClassList("show");
        pausePanel.AddToClassList("hide");
        settingsPanel.RemoveFromClassList("show");
        settingsPanel.AddToClassList("hide");

        root.visible = false;
    }

    private void OpenSettings()
    {
        pausePanel.RemoveFromClassList("show");
        pausePanel.AddToClassList("hide");
        settingsPanel.RemoveFromClassList("hide");
        settingsPanel.AddToClassList("show");
    }

    private void CloseSettings()
    {
        settingsPanel.RemoveFromClassList("show");
        settingsPanel.AddToClassList("hide");
        pausePanel.RemoveFromClassList("hide");
        pausePanel.AddToClassList("show");
    }

    private void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public bool IsPauseMenuOpen()
    {
        return root.visible;
    }
}