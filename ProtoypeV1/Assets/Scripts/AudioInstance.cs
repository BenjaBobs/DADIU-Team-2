using UnityEngine;
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
		print (defaultVolume);
		source = gameObject.GetComponent<AudioSource>();
		AudioManager.ChangeVolume += ChangeVolume;
	}

	public void SetDefaultVolume(float value) {
		defaultVolume = value;
	}

	public void Play() {
		if (source.isPlaying)
			source.Stop();
		source.Play();
	}
	public void Stop() {
		source.Stop();
	}

	private void ChangeVolume(string tag, float volume) {
		if (tag == gameObject.tag)
			source.volume = volume * defaultVolume;
	}
}