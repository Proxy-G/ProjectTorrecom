using UnityEngine;
using UnityEngine.UI;

public class powersA_ui_musicVolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetInt("Music Volume", 10); //Set music volume to the current saved value for volume. If there is none, set it to 1.
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("Music Volume", (int)volumeSlider.value); //Save the current music volume
    }
}
