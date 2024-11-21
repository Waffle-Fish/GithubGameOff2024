using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private UIDocument _document;
    private Button _playButton;
    private Button _settingsButton;
    private Button _creditsButton;
    private Button _quitButton;
    private VisualElement _creditsPanel;
    private Button _backButton;
    private VisualElement _settingsPanel;
    private Button _settingsBackButton;
    private SliderInt _masterVolumeSlider;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        // Get references to all buttons
        var root = _document.rootVisualElement;
        _playButton = root.Q<Button>("play-button", "menu-button");
        _settingsButton = root.Q<Button>("settings-button", "menu-button");
        _creditsButton = root.Q<Button>("credits-button", "menu-button");
        _quitButton = root.Q<Button>("quit-button", "menu-button");

        // Get credits panel references
        _creditsPanel = root.Q<VisualElement>("credits-panel");
        _backButton = _creditsPanel.Q<Button>("back-button");

        // Get settings panel references
        _settingsPanel = root.Q<VisualElement>("settings-panel");
        _settingsBackButton = _settingsPanel.Q<Button>("settings-back-button");
        _masterVolumeSlider = _settingsPanel.Q<SliderInt>("masterVolume");

        // Add click event handlers
        _playButton.clicked += OnPlayClicked;
        _settingsButton.clicked += OnSettingsClicked;
        _creditsButton.clicked += OnCreditsClicked;
        _quitButton.clicked += OnQuitClicked;
        _backButton.clicked += OnBackClicked;
        _settingsBackButton.clicked += OnSettingsBackClicked;

        // Add slider value change handler
        if (_masterVolumeSlider != null)
        {
            _masterVolumeSlider.RegisterValueChangedCallback(OnMasterVolumeChanged);
        }
    }

    private void OnPlayClicked()
    {
        //!TODO: Implement play functionality
        Debug.Log("Play clicked");
        SceneManager.LoadScene("GameScene");
    }

    private void OnSettingsClicked()
    {
        _settingsPanel.style.display = DisplayStyle.Flex;
    }

    private void OnCreditsClicked()
    {
        _creditsPanel.style.display = DisplayStyle.Flex;
    }

    private void OnBackClicked()
    {
        _creditsPanel.style.display = DisplayStyle.None;
    }

    private void OnQuitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnSettingsBackClicked()
    {
        _settingsPanel.style.display = DisplayStyle.None;
    }

    private void OnMasterVolumeChanged(ChangeEvent<int> evt)
    {
        // Convert slider value (0-100) to audio volume (0-1)
        float volume = evt.newValue / 100f;
        Debug.Log("Master volume changed to " + volume);
    }
}