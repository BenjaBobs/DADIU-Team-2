using UnityEngine;
using System.Collections;

public static class AudioManager {
	public enum AudioTag {
		DefaultSound,
		BackgroundSound,
		EffectSound,
		Music
	};

	private static string GiveTag(AudioTag tag) {
		switch (tag) {
		case AudioTag.BackgroundSound:
			return "Audio_Background";
		case AudioTag.EffectSound:
			return "Audio_Effect";
		case AudioTag.Music:
			return "Audio_Music";
		default:
			return "Audio_Default";
		}
	}
	
	// Transform
	public static AudioSource Play(AudioClip clip, Transform emitter, AudioTag tag = AudioTag.DefaultSound, float volume = 1f, bool loop = false, int loopTimes = 0, bool destroy = false, float length = 0f) {
		GameObject go = new GameObject("Audio: " + clip.name);
		go.transform.parent = emitter;
		go.transform.position = emitter.position;
		go.tag = GiveTag(tag);
		AudioSource source = go.AddComponent<AudioSource>();
		source.clip = clip;
		source.volume = volume;
		source.loop = loop;
		source.Play();
		if (destroy)
			MonoBehaviour.Destroy(go, length > 0f ? length : (loop ? clip.length * (loopTimes == 0 ? 1 : loopTimes) : clip.length));
		return source;
	}
	public static AudioSource Play(AudioClip clip, Transform emitter, bool destroy, AudioTag tag = AudioTag.DefaultSound, float length = 0f) {
		return Play(clip, emitter, tag, 1f, false, 0, destroy, length);
	}

	/// <summary>
	/// Plays a sound at the camera's position
	/// </summary>
	/// <param name="clip">Clip.</param>
	public static AudioSource Play(AudioClip clip, AudioTag tag = AudioTag.DefaultSound) {
		return Play(clip, Camera.mainCamera.transform, tag);
	}
}