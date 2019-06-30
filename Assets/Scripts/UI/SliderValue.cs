using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour {

    public Text ValueText;
    public  GameObject Camera;

    private Slider slider;
    private AudioSource music;
	// Use this for initialization
	void Start () {

        slider = GetComponent<Slider>();
        music = Camera.GetComponent<AudioSource>();
        slider.value = music.volume * 100;    
        ShowSliderValue();
    }

    public void ShowSliderValue () {

        string newtext = slider.value + " %";
        ValueText.text = newtext;
	}

    public void ChangeAudioVolume()
    {
        float volume = slider.value / 100.0f;
        music.volume = volume;
    }
}
