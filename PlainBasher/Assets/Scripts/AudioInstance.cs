﻿using UnityEngine;
using System.Collections;

public class AudioInstance : MonoBehaviour {
	private float defaultVolume;
	private AudioSource source;
	public AudioManager.AudioTag tag = AudioManager.AudioTag.Default;
	public float volume = 1f;
/*	public bool loop = false;
	public int loopTimes = 0;
	public bool destroy = false;
	public float length = 0f;*/

	private void Awake() {
		defaultVolume = volume;
		source = gameObject.GetComponent<AudioSource>();
		AudioManager.ChangeVolume += ChangeVolume;
	}

	public void SetDefaultVolume(float value) {
		defaultVolume = value;
	}
	
	public void SetClip(AudioClip clip) {
		source.clip = clip;
	}
	
	public void Play() {
		if (source.isPlaying)
			source.Stop();
		source.Play();
	}
	public void Stop() {
		//source.Stop();
		StartCoroutine(FadeOut());
	}

	private void ChangeVolume(string tag, float vol) {
		if (tag == gameObject.tag) {
			volume = vol * defaultVolume;
			source.volume = volume;
		}
	}


	private IEnumerator FadeOut() {
		bool breakThis = false;
		while (source.isPlaying && !breakThis) {
			source.volume -= 0.2f;
			yield return new WaitForSeconds(0.1f);
			if (source.volume <= 0.21f) {
				breakThis = true;
				source.Stop();
				source.volume = defaultVolume;
			}
		}
	}
}