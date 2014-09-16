using UnityEngine;
using System.Collections;

public static class AudioManager {
	public delegate void ChangeVolumeEvent(string tag, float volume);
	public static event ChangeVolumeEvent ChangeVolume;
	
	public delegate void OnIceEvent(bool on);
	public static event OnIceEvent IceEvent;
	
	public static float effectVolume = 1f;
	public static float musicVolume = 1f;

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
	

	private static AudioInstance Play(AudioClip clip, Transform emitter, AudioTag tag = AudioTag.Default, float volume = 1f, bool loop = false, int loopTimes = 0, bool destroy = false, float length = 0f) {
		GameObject go = new GameObject("Audio: " + (clip != null ? clip.name : ""));
		go.transform.parent = emitter;
		go.transform.position = emitter.position;
		go.tag = GetTag(tag);
		AudioSource source = go.AddComponent<AudioSource>();
		AudioInstance audioInstance = go.AddComponent<AudioInstance>();
		if (volume < 1f)
			audioInstance.SetDefaultVolume(volume);
		if (clip != null)
			source.clip = clip;
		source.volume = volume;
		source.loop = loop;
		if (clip != null)
			audioInstance.Play();
		if (destroy)
			MonoBehaviour.Destroy(go, length > 0f ? length : (loop ? clip.length * (loopTimes == 0 ? 1 : loopTimes) : clip.length));
		return audioInstance;
	}
	private static AudioInstance Play(AudioClip clip, AudioTag tag = AudioTag.Default, float volume = 1f, bool loop = false) {
		return Play(clip, Camera.main.transform, tag, volume, loop);
	}
	private static AudioInstance PlayIce(AudioClip clip, AudioTag tag, float volume = 1f) {
		AudioInstance ai = Play(clip, Camera.main.transform, tag, volume);
		ai.SetIce();
		return ai;
	}
	private static AudioInstance Play(AudioClip clip, AudioTag tag, AudioClip clip2, bool loop2 = false, float volume = 1f) {
		AudioInstance ai = Play(clip, Camera.main.transform, tag, volume);
		ai.ChangeClipWhenDone(clip2, loop2);
		return ai;
	}

	
	// Music
	private static AudioInstance aiGameOver;
	/// <summary>
	/// Music for game over screen, play instantly when dead
	/// </summary>
	public static void PlayGameOver() {
		if (aiGameOver == null)
			aiGameOver = Play (AudioClips.instance.gameOver, AudioTag.Music, AudioClips.instance.gameOverVolume);
		else
			aiGameOver.Play();
	}
	private static AudioInstance aiMusic;
	/// <summary>
	/// Background music for gameplay(do NOT loop this)
	/// </summary>
	public static void PlayMusic() {
		if (aiMusic == null)
			aiMusic = Play (AudioClips.instance.music, AudioTag.Music, AudioClips.instance.musicLoop, true, AudioClips.instance.musicVolume);
		else
			aiMusic.Play();
	}
	/// <summary>
	/// Short loop of background music to be looped when the original background music stops, after 5:19min into the gameplay
	/// </summary>
	/*public static void PlayMusicLoop() {
		if (aiMusic == null)
			aiMusic = Play (AudioClips.instance.musicLoop, AudioTag.Music, AudioClips.instance.musicVolume);
		else
			aiMusic.Play();
	}*/
	private static AudioInstance aiSplashMusic;
	/// <summary>
	/// Theme music for splash screen
	/// </summary>
	public static void PlaySplashMusic() {
		if (aiSplashMusic == null)
			aiSplashMusic = Play (AudioClips.instance.musicSplash, AudioTag.Music, AudioClips.instance.musicSplashVolume, true);
		else
			aiSplashMusic.Play();
	}
	public static void StopSplashMusic(bool fade = true) {
		aiSplashMusic.Stop(fade);
	}


	// Sfx
	private static AudioInstance aiButton;
	/// <summary>
	/// Sound for all button clicks in the game
	/// </summary>
	public static void PlayButton() {
		if (aiButton == null)
			aiButton = Play (AudioClips.instance.button, AudioTag.Effect, AudioClips.instance.buttonVolume);
		else
			aiButton.Play();
	}
	private static AudioInstance aiLoseLife;
	/// <summary>
	/// In game sound for lost life (heart)
	/// </summary>
	public static void PlayLoseLife() {
		if (aiLoseLife == null)
			aiLoseLife = Play (AudioClips.instance.loseLife, AudioTag.Effect, AudioClips.instance.loseLifeVolume);
		else
			aiLoseLife.Play();
	}
	private static AudioInstance aiPointCount;
	/// <summary>
	/// Short sound to be linked with each point count in highscore screenPlaies the point count.
	/// </summary>
	public static void PlayPointCount() {
		if (aiPointCount == null)
			aiPointCount = Play (AudioClips.instance.pointCount, AudioTag.Effect, AudioClips.instance.pointCountVolume);
		else
			aiPointCount.Play();
	}


	// Enemy destruction
	private static AudioInstance aiDestroyElektro;
	/// <summary>
	/// Play when Elektro destroys
	/// </summary>
	public static void PlayDestroyElektro() {
		if (aiDestroyElektro == null)
			aiDestroyElektro = Play (AudioClips.instance.destroyElektro, AudioTag.Effect, AudioClips.instance.destroyElektroVolume);
		else
			aiDestroyElektro.Play();
	}
	private static AudioInstance aiDestroyExplosion;
	/// <summary>
	/// Play when Explosion destroys
	/// </summary>
	public static void PlayDestroyExplosion() {
		if (aiDestroyExplosion == null)
			aiDestroyExplosion = Play (AudioClips.instance.destroyExplosion, AudioTag.Effect, AudioClips.instance.destroyExplosionVolume);
		else
			aiDestroyExplosion.Play();
	}
	private static AudioInstance aiDestroyFreeze;
	/// <summary>
	/// Play when Freeze destroys
	/// </summary>
	public static void PlayDestroyFreeze() {
		if (aiDestroyFreeze == null)
			aiDestroyFreeze = Play (AudioClips.instance.destroyFreeze, AudioTag.Effect, AudioClips.instance.destroyFreezeVolume);
		else
			aiDestroyFreeze.Play();
	}
	private static AudioInstance aiFreeze;
	/// <summary>
	/// Play when Freeze is getting hold
	/// </summary>
	public static void PlayHoldFreeze() {
		if (aiFreeze == null)
			aiFreeze = Play (AudioClips.instance.holdFreeze, AudioTag.Effect, AudioClips.instance.holdFreezeVolume);
		else
			aiFreeze.Play();
	}
	/// <summary>
	/// Stops the hold freeze
	/// </summary>
	public static void StopHoldFreeze(bool fade = true) {
		if (aiFreeze != null)
            aiFreeze.Stop(fade);
	}
	private static AudioInstance aiTapBigJelly;
	/// <summary>
	/// Play when big Jelly gets tapped
	/// </summary>
	public static void PlayTapBigJelly() {
		if (aiTapBigJelly == null)
			aiTapBigJelly = Play (AudioClips.instance.tapBigJelly, AudioTag.Effect, AudioClips.instance.tapBigJellyVolume);
		else
			aiTapBigJelly.Play();
	}
	private static AudioInstance aiTapSmallJelly;
	/// <summary>
	/// Play when small Jelly gets tapped
	/// </summary>
	public static void PlayTapSmallJelly() {
		if (aiTapSmallJelly == null)
			aiTapSmallJelly = Play (AudioClips.instance.tapSmallJelly, AudioTag.Effect, AudioClips.instance.tapSmallJellyVolume);
		else
			aiTapSmallJelly.Play();
	}
	private static AudioInstance aiDestroyJelly;
	private static int lastPlayed = 0;
	private static AudioClip GetJellySound() {
		int newId;
		do {
			newId = Random.Range(1,9);
		} while (newId == lastPlayed);
		lastPlayed = newId;

		switch(lastPlayed) {
		case 1:
			return AudioClips.instance.destroyJelly1;
		case 2:
			return AudioClips.instance.destroyJelly2;
		case 3:
			return AudioClips.instance.destroyJelly3;
		case 4:
			return AudioClips.instance.destroyJelly4;
		case 5:
			return AudioClips.instance.destroyJelly5;
		case 6:
			return AudioClips.instance.destroyJelly6;
		case 7:
			return AudioClips.instance.destroyJelly7;
		default:
			return AudioClips.instance.destroyJelly8;
		}
	}
	/// <summary>
	/// Play when Jelly destroys
	/// </summary>
	public static void PlayDestroyJelly() {
		if (aiDestroyJelly == null)
			aiDestroyJelly = Play (GetJellySound(), AudioTag.Effect, AudioClips.instance.destroyJellyVolume);
		else {
			aiDestroyJelly.SetClip(GetJellySound());
			aiDestroyJelly.Play();
		}
	}

	// Ground
	private static AudioInstance aiTapGround;
	/// <summary>
	/// Play when player taps the ground and is not hitting enemy
	/// </summary>
	public static void PlayTapGround() {
		if (aiTapGround == null)
			aiTapGround = Play (AudioClips.instance.tapGround, AudioTag.Effect, AudioClips.instance.tapGroundVolume);
		else
			aiTapGround.Play();
	}

	// Ice
	private static AudioInstance aiIceAppear;
	/// <summary>
	/// Play when ice block appears on screen and freezes the game
	/// </summary>
	public static void PlayIceAppear() {
		if (aiIceAppear == null)
			aiIceAppear = PlayIce (AudioClips.instance.iceAppear, AudioTag.Effect, AudioClips.instance.iceAppearVolume);
		else
			aiIceAppear.Play();
	}
	private static AudioInstance aiTapIce;
	private static int iceTapCounter = 0;
	/// <summary>
	/// Play when player taps ice block
	/// </summary>
	public static void PlayTapIce() {
		++iceTapCounter;
		if (aiTapIce == null)
			aiTapIce = PlayIce (AudioClips.instance.tapIce1, AudioTag.Effect, AudioClips.instance.tapIceVolume);
		else {
			switch (iceTapCounter) {
			case 1:
				aiTapIce.SetClip(AudioClips.instance.tapIce1);
				break;
			case 2:
				aiTapIce.SetClip(AudioClips.instance.tapIce2);
				break;
			default:
				iceTapCounter = 0;
				aiTapIce.SetClip(AudioClips.instance.tapIce3);
				break;
			}
			aiTapIce.Play();
		}
	}


	/// <summary>
	/// Toggle effect sounds
	/// </summary>
	public static void ToggleEffects() {
		effectVolume = effectVolume == 1f ? 0f : 1f;
		ChangeVolume(GetTag(AudioTag.Effect), effectVolume);
	}
	/// <summary>
	/// Toggles music sounds
	/// </summary>
	public static void ToggleMusic() {
		musicVolume = musicVolume == 1f ? 0f : 1f;
		ChangeVolume(GetTag(AudioTag.Music), musicVolume);
	}


	public static void LowPassFilter(bool on) {

		IceEvent(on);
	}
}