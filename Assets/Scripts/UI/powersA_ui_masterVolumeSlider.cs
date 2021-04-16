using UnityEngine;
using UnityEngine.UI;

public class powersA_ui_masterVolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetInt("Master Volume", 10); //Set master volume to the current saved value for volume. If there is none, set it to 1.
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("Master Volume", (int)volumeSlider.value); //Save the current master volume
        AudioListener.volume = volumeSlider.value / 10; //Set the current master volume to the slider value
    }
}
