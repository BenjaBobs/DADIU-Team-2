using UnityEngine;
using System.Collections;

public static class AudioManager {
	public delegate void ChangeVolumeEvent(string tag, float volume);
	public static event ChangeVolumeEvent ChangeVolume;

	private static AudioInstance aiExplosion;
	private static AudioInstance aiFreeze;

	private static float effectVolume = 1f;
	private static float musicVolume = 1f;

	public enum AudioTag {
		Default,
		Effect,
		Music
	};

	private static string GetTag(AudioTag tag) {
		switch (tag) {
		case AudioTag.Music:
			return "Audio_Music";
		case AudioTag.Effect:
		default:
			return "Audio_Effect";
		}
	}
	

	public static AudioInstance Play(AudioClip clip, Transform emitter, AudioTag tag = AudioTag.Default, float volume = 1f, bool loop = false, int loopTimes = 0, bool destroy = false, float length = 0f) {
		GameObject go = new GameObject("Audio: " + clip.name);
		go.transform.parent = emitter;
		go.transform.position = emitter.position;
		go.tag = GetTag(tag);
		AudioSource source = go.AddComponent<AudioSource>();
		AudioInstance audioInstance = go.AddComponent<AudioInstance>();
		if (volume < 1f)
			audioInstance.SetDefaultVolume(volume);
		source.clip = clip;
		source.volume = volume;
		source.loop = loop;
		audioInstance.Play();
		if (destroy)
			MonoBehaviour.Destroy(go, length > 0f ? length : (loop ? clip.length * (loopTimes == 0 ? 1 : loopTimes) : clip.length));
		return audioInstance;
	}
	public static AudioInstance Play(AudioClip clip, AudioTag tag = AudioTag.Default, float volume = 1f) {
		return Play(clip, Camera.main.transform, tag, volume);
	}


	public static void PlayExplosion() {
		if (aiExplosion == null)
			aiExplosion = Play (MonoBehaviour.Instantiate (Resources.Load ("Audio/Placeholder", typeof(AudioClip))) as AudioClip, AudioTag.Effect);
		else
			aiExplosion.Play();
	}
	public static void PlayFreeze() {
		if (aiFreeze == null)
			aiFreeze = Play(MonoBehaviour.Instantiate (Resources.Load ("Audio/Placeholder", typeof(AudioClip))) as AudioClip, AudioTag.Effect);
		else
			aiFreeze.Play();
	}
	public static void StopFreeze() {
		aiFreeze.Stop();
	}

	public static void ToggleEffects() {
		effectVolume = effectVolume == 1f ? 0f : 1f;
		ChangeVolume(GetTag(AudioTag.Effect), effectVolume);
	}
	public static void ToggleMusic() {
		musicVolume = musicVolume == 1f ? 0f : 1f;
		ChangeVolume(GetTag(AudioTag.Music), musicVolume);
	}
}