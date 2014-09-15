using UnityEngine;
using System.Collections;

public static class AudioManager {
	public delegate void ChangeVolumeEvent(string tag, float volume);
	public static event ChangeVolumeEvent ChangeVolume;

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
	

	private static AudioInstance Play(AudioClip clip, Transform emitter, AudioTag tag = AudioTag.Default, float volume = 1f, bool loop = false, int loopTimes = 0, bool destroy = false, float length = 0f) {
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
	private static AudioInstance Play(AudioClip clip, AudioTag tag = AudioTag.Default, float volume = 1f) {
		return Play(clip, Camera.main.transform, tag, volume);
	}


	// Music
	private static AudioInstance aiGameOver;
	/// <summary>
	/// Music for game over screen, play instantly when dead
	/// </summary>
	public static void PlayGameOver() {
		if (aiGameOver == null)
			aiGameOver = Play (MonoBehaviour.Instantiate (Resources.Load ("Music/BlobBashermusic_game over plus taunting laugh", typeof(AudioClip))) as AudioClip, AudioTag.Music);
		else
			aiGameOver.Play();
	}
	private static AudioInstance aiMusic;
	/// <summary>
	/// Background music for gameplay(do NOT loop this)
	/// </summary>
	public static void PlayMusic() {
/*		if (aiMusic == null)
			aiMusic = Play (MonoBehaviour.Instantiate (Resources.Load ("Music/BlobBashermusic_in game music", typeof(AudioClip))) as AudioClip, AudioTag.Music);
		else
			aiMusic.Play();
*/	}
	/// <summary>
	/// Short loop of background music to be looped when the original background music stops, after 5:19min into the gameplay
	/// </summary>
	public static void PlayMusicLoop() {
		if (aiMusic == null)
			aiMusic = Play (MonoBehaviour.Instantiate (Resources.Load ("Music/BlobBashermusic_in game_shortloop", typeof(AudioClip))) as AudioClip, AudioTag.Music);
		else
			aiMusic.Play();
	}
	private static AudioInstance aiSplashMusic;
	/// <summary>
	/// Theme music for splash screen
	/// </summary>
	public static void PlaySplashMusic() {
		if (aiSplashMusic == null)
			aiSplashMusic = Play (MonoBehaviour.Instantiate (Resources.Load ("Music/BlobBashermusic_splash screen music", typeof(AudioClip))) as AudioClip, AudioTag.Music);
		else
			aiSplashMusic.Play();
	}


	// Sfx
	private static AudioInstance aiButton;
	/// <summary>
	/// Sound for all button clicks in the game
	/// </summary>
	public static void PlayButton() {
		if (aiButton == null)
			aiButton = Play (MonoBehaviour.Instantiate (Resources.Load ("Sound/Sound effects/BlobBashersfx_Click sound for ALL buttons", typeof(AudioClip))) as AudioClip, AudioTag.Effect);
		else
			aiButton.Play();
	}
	private static AudioInstance aiLoseLife;
	/// <summary>
	/// In game sound for lost life (heart)
	/// </summary>
	public static void PlayLoseLife() {
		if (aiLoseLife == null)
			aiLoseLife = Play (MonoBehaviour.Instantiate (Resources.Load ("Sound/Sound effects/BlobBashersfx_lose life(heart)", typeof(AudioClip))) as AudioClip, AudioTag.Effect);
		else
			aiLoseLife.Play();
	}
	private static AudioInstance aiPointCount;
	/// <summary>
	/// Short sound to be linked with each point count in highscore screenPlaies the point count.
	/// </summary>
	public static void PlayPointCount() {
		if (aiPointCount == null)
			aiPointCount = Play (MonoBehaviour.Instantiate (Resources.Load ("Sound/Sound effects/BlobBashersfx_Point-Count", typeof(AudioClip))) as AudioClip, AudioTag.Effect);
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
			aiDestroyElektro = Play (MonoBehaviour.Instantiate (Resources.Load ("Sound/Sound effects/enemy destruction/BlobBashersfx_electricity bolt enemy", typeof(AudioClip))) as AudioClip, AudioTag.Effect);
		else
			aiDestroyElektro.Play();
	}
	private static AudioInstance aiDestroyExplosion;
	/// <summary>
	/// Play when Explosion destroys
	/// </summary>
	public static void PlayDestroyExplosion() {
		if (aiDestroyExplosion == null)
			aiDestroyExplosion = Play (MonoBehaviour.Instantiate (Resources.Load ("Sound/Sound effects/enemy destruction/Blobbashersfx_explosion enemy", typeof(AudioClip))) as AudioClip, AudioTag.Effect);
		else
			aiDestroyExplosion.Play();
	}
	private static AudioInstance aiDestroyFreeze;
	/// <summary>
	/// Play when Freeze destroys
	/// </summary>
	public static void PlayDestroyFreeze() {
		if (aiDestroyFreeze == null)
			aiDestroyFreeze = Play (MonoBehaviour.Instantiate (Resources.Load ("Sound/Sound effects/enemy destruction/Blobbashersfx_ice break enemy", typeof(AudioClip))) as AudioClip, AudioTag.Effect);
		else
			aiDestroyFreeze.Play();
	}
	private static AudioInstance aiFreeze;
	/// <summary>
	/// Play when Freeze is getting hold
	/// </summary>
	public static void PlayHoldFreeze() {
		if (aiFreeze == null)
			aiFreeze = Play (MonoBehaviour.Instantiate (Resources.Load ("Sound/Sound effects/enemy destruction/Blobbashersfx_ice break enemy_holding sound", typeof(AudioClip))) as AudioClip, AudioTag.Effect);
		else
			aiFreeze.Play();
	}
	/// <summary>
	/// Stops the hold freeze
	/// </summary>
	public static void StopHoldFreeze() {
		aiFreeze.Stop();
	}
	private static AudioInstance aiTapBigJelly;
	/// <summary>
	/// Play when big Jelly gets tapped
	/// </summary>
	public static void PlayTapBigJelly() {
		if (aiTapBigJelly == null)
			aiTapBigJelly = Play (MonoBehaviour.Instantiate (Resources.Load ("Sound/Sound effects/enemy destruction/Blobbashersfx_jelly enemy deflate big", typeof(AudioClip))) as AudioClip, AudioTag.Effect);
		else
			aiTapBigJelly.Play();
	}
	private static AudioInstance aiTapSmallJelly;
	/// <summary>
	/// Play when small Jelly gets tapped
	/// </summary>
	public static void PlayTapSmallJelly() {
		if (aiTapSmallJelly == null)
			aiTapSmallJelly = Play (MonoBehaviour.Instantiate (Resources.Load ("Sound/Sound effects/enemy destruction/Blobbashersfx_jelly enemy deflate small", typeof(AudioClip))) as AudioClip, AudioTag.Effect);
		else
			aiTapSmallJelly.Play();
	}
	private static AudioInstance aiDestroyJelly;
	/// <summary>
	/// Play when Jelly destroys
	/// </summary>
	public static void PlayDestroyJelly() {
		if (aiDestroyJelly == null)
			aiDestroyJelly = Play (MonoBehaviour.Instantiate (Resources.Load ("Sound/Sound effects/enemy destruction/Blobbashersfx_jelly splat", typeof(AudioClip))) as AudioClip, AudioTag.Effect);
		else
			aiDestroyJelly.Play();
	}

	// Ground
	private static AudioInstance aiTapGround;
	/// <summary>
	/// Play when player taps the ground and is not hitting enemy
	/// </summary>
	public static void PlayTapGround() {
		if (aiTapGround == null)
			aiTapGround = Play (MonoBehaviour.Instantiate (Resources.Load ("Audio/Blobbashersfx_ground hit", typeof(AudioClip))) as AudioClip, AudioTag.Effect);
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
			aiIceAppear = Play (MonoBehaviour.Instantiate (Resources.Load ("Sound/Sound effects/gameboard ground/Blobbashersfx_ice screen block", typeof(AudioClip))) as AudioClip, AudioTag.Effect);
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
		if (aiTapGround == null)
			aiTapGround = Play (MonoBehaviour.Instantiate (Resources.Load ("Sound/Sound effects/ice screen block/Blobbashersfx_ice screen 1st pick", typeof(AudioClip))) as AudioClip, AudioTag.Effect);
		else {
			switch (iceTapCounter) {
			case 1:
				aiTapGround.SetClip(MonoBehaviour.Instantiate (Resources.Load ("Sound/Sound effects/ice screen block/Blobbashersfx_ice screen 1st pick", typeof(AudioClip))) as AudioClip);
				break;
			case 2:
				aiTapGround.SetClip(MonoBehaviour.Instantiate (Resources.Load ("Sound/Sound effects/ice screen block/Blobbashersfx_ice screen 2nd pick", typeof(AudioClip))) as AudioClip);
				break;
			default:
				iceTapCounter = 0;
				aiTapGround.SetClip(MonoBehaviour.Instantiate (Resources.Load ("Sound/Sound effects/ice screen block/BlobBashersfx_ice screen 3rd pick and breaking", typeof(AudioClip))) as AudioClip);
				break;
			}
			aiTapGround.Play();
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
}