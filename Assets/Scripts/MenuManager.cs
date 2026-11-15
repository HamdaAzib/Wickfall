using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public Slider volumeSlider;

    void Start()
    {
        Time.timeScale = 1f;

        float saved = PlayerPrefs.GetFloat("volume", 1f);
        AudioListener.volume = saved;
        if (volumeSlider != null) volumeSlider.SetValueWithoutNotify(saved);
        if (optionsPanel != null) optionsPanel.SetActive(false);
    }

    public void Play() => SceneManager.LoadScene("GameScene");
    public void OpenOptions() => optionsPanel.SetActive(true);
    public void CloseOptions() => optionsPanel.SetActive(false);

    public void SetVolume(float v)
    {
        AudioListener.volume = v;
        PlayerPrefs.SetFloat("volume", v);
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}