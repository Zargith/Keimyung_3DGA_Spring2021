using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OnAwakeGetSpecificVolumeChangeText : MonoBehaviour
{
	[SerializeField] AudioMixerGroup audioMixerGroup;
	[SerializeField] Text _text;

	void Awake()
	{
		AudioMixer masterMixer = audioMixerGroup.audioMixer;
		string groupName = audioMixerGroup.name;
		float volume;
		bool res = masterMixer.GetFloat(groupName + "Vol", out volume);

		if (!res)
			volume = 0f;
		volume = (volume + 80) * (5/4);
		_text.text = groupName + ": " + volume.ToString() + "%";

	}
}
