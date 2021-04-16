using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powersA_util_musicController : MonoBehaviour
{
    public powersA_game_gameManager gameManager;
    [Space(10)]
    
    [Tooltip("The music used for the main menu.")]
    public AudioClip menuMusic;
    [Tooltip("The music used for the death screen.")]
    public AudioClip deathMusic;
    [Tooltip("The music used during gameplay.")]
    public AudioClip gameplayMusic;

    private AudioSource audSource;
    private float defaultVolume;

    private float currentVolume;
    private AudioClip currentMusic;
    private AudioClip prevMusic;
    private bool changingMusic;
    
    // Start is called before the first frame update
    void Start()
    {
        audSource = GetComponent<AudioSource>(); //Get audio source to play music
        audSource.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        defaultVolume = PlayerPrefs.GetInt("Music Volume", 10)/10f; //Set default volume based on the music volume setting

        //Set music based on current conditions in the game
        if (gameManager.uiManager.inGame) currentMusic = menuMusic;
        else if (gameManager.playerScript.isDead) currentMusic = deathMusic;
        else currentMusic = gameplayMusic;

        if (currentMusic != prevMusic) changingMusic = true; //If the music has changed, set script to change the music

        if (changingMusic)
        {
            currentVolume = powersA_util_animMath.Slide(currentVolume, 0, 0.01f); //Fade out volume
            if(currentVolume < 0.01f) //Once volume is down...
            {
                currentVolume = 0; //Turn of the volume
                audSource.Stop(); //Stop the music playing
                audSource.clip = currentMusic; //Switch the audio to the current music to be played
                audSource.Play(); //Play the music

                changingMusic = false; //Changing music complete
            }
        }
        else currentVolume = powersA_util_animMath.Slide(currentVolume, defaultVolume, 0.01f); //Fade out volume

        audSource.volume = currentVolume; //Set the volume for the music
        prevMusic = currentMusic; //Set music clip of prev frame to current music clip
    }
}
