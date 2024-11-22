using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private FMODUnity.EventReference musicReference;
    private FMOD.Studio.EventInstance musicInstance;

    private FMOD.Studio.VCA masterVCA;
    private FMOD.Studio.VCA musicVCA;
    private FMOD.Studio.VCA sfxVCA;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        CreateInstance();
        SetupVCAs();
    }

    private void CreateInstance()
    {
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicReference);
        musicInstance.start();
    }

    private void SetupVCAs()
    {
        masterVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Master");
        musicVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Music");
        sfxVCA = FMODUnity.RuntimeManager.GetVCA("vca:/SFX");
    }

    public void SetMasterVolume(float volume)
    {
        masterVCA.setVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVCA.setVolume(volume);
    }

    public void SetEffectVolume(float volume)
    {
        sfxVCA.setVolume(volume);
    }

    public void SetAreaParameter(AreaType areaType)
    {
        musicInstance.setParameterByName("Area", (float)areaType);
    }

    public void TransitionMusic(MusicState musicState)
    {
        musicInstance.setParameterByName("State", (float)musicState);
    }
}
