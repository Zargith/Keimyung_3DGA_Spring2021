using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ResetMixerVolumeOnStartScene : MonoBehaviour
{

	[SerializeField] AudioMixer mixer;
	[SerializeField] string exposedGroupeNameMaster;
	[SerializeField] string exposedGroupeNameMusic;
	[SerializeField] string exposedGroupeNameSoundEffects;

	void Start()
	{
		float masterValume = PlayerPrefs.GetFloat(exposedGroupeNameMaster);
		mixer.SetFloat(exposedGroupeNameMaster, masterValume);
		float musicValume = PlayerPrefs.GetFloat(exposedGroupeNameMusic);
		mixer.SetFloat(exposedGroupeNameMusic, musicValume);
		float soundEffectsValume = PlayerPrefs.GetFloat(exposedGroupeNameSoundEffects);
		mixer.SetFloat(exposedGroupeNameSoundEffects, soundEffectsValume);
	}
}