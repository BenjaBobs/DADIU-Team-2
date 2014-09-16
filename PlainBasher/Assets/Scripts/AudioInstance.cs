using UnityEngine;
using System.Collections;

public class AudioInstance : MonoBehaviour {
	private float defaultVolume;
	private AudioSource source;
	private AudioLowPassFilter lowPass;
	private AudioClip nextClip;
	private bool loopNext = false;
	public AudioManager.AudioTag AudioTag = AudioManager.AudioTag.Effect;
	public float volume = 1f;
	private bool ice = false;
/*	public bool loop = false;
	public int loopTimes = 0;
	public bool destroy = false;
	public float length = 0f;*/

	private void Awake() {
		defaultVolume = volume;
		source = gameObject.GetComponent<AudioSource>();
		AudioManager.ChangeVolume += ChangeVolume;
		AudioManager.IceEvent += IceEvent;
	}
	
	private void Start() {
		volume = AudioTag == AudioManager.AudioTag.Music ? defaultVolume * AudioManager.effectVolume : defaultVolume * AudioManager.musicVolume;
		source.volume = volume;
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
	public void Stop(bool fade = true) {
		if (!fade)
			source.Stop();
		else
			StartCoroutine(FadeOut());
	}

	public void SetIce() {
		ice = true;
	}

	public void ChangeClipWhenDone(AudioClip clip,  bool loop = false) {
		nextClip = clip;
		loopNext = loop;
	}
	private void Update() {
		if (nextClip != null && !source.isPlaying) {
			source.clip = nextClip;
			source.Play();
			source.loop = loopNext;
			nextClip = null;
		}
	}

	private void ChangeVolume(string tag, float vol) {
		if (tag == gameObject.tag) {
			volume = vol * defaultVolume;
			source.volume = volume;
		}
	}

	private void IceEvent(bool on) {
		if (!ice) {
			if (on) {
				if (gameObject.GetComponent<AudioLowPassFilter>() == null)
					gameObject.AddComponent<AudioLowPassFilter>().cutoffFrequency = 500f;
				else
					gameObject.GetComponent<AudioLowPassFilter>().enabled = true;
			}
			else if (gameObject.GetComponent<AudioLowPassFilter>() != null) {
				gameObject.GetComponent<AudioLowPassFilter>().enabled = false;
			}
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