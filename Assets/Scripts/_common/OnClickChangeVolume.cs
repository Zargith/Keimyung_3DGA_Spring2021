using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OnClickChangeVolume : MonoBehaviour
{
	AudioMixer masterMixer;
	[SerializeField] AudioMixerGroup audioMixerGroup;
	[SerializeField] Text _text;
	[SerializeField] float amountToChange;

	void Start()
	{
		masterMixer = audioMixerGroup.audioMixer;
	}

	public void IncreaseVolume()
	{
		string groupName = audioMixerGroup.name;
		float volume;
		bool res = masterMixer.GetFloat(groupName + "Vol", out volume);

		if (!res)
			volume = 0f;
		float newVolume = volume + amountToChange;

		if ((newVolume + 80) * (5/4) > 100)
			return;

		masterMixer.SetFloat(groupName + "Vol", newVolume);
		PlayerPrefs.SetFloat(groupName + "Vol", newVolume);
		UpdateVolumeText();
	}

	public void DecreaseVolume()
	{
		string groupName = audioMixerGroup.name;
		float volume;
		bool res = masterMixer.GetFloat(groupName + "Vol", out volume);

		if (!res)
			volume = 0f;
		float newVolume = volume - amountToChange;

		if ((newVolume + 80) * (5/4) < 0)
			return;

		masterMixer.SetFloat(groupName + "Vol", newVolume);
		PlayerPrefs.SetFloat(groupName + "Vol", newVolume);
		UpdateVolumeText();
	}

	void UpdateVolumeText()
	{
		string groupName = audioMixerGroup.name;
		float volume;
		bool res = masterMixer.GetFloat(groupName + "Vol", out volume);

		if (!res)
			volume = 0f;
		volume = (volume + 80) * (5/4);
		_text.text = groupName + ": " + volume.ToString() + "%";
	}
}
