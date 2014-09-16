using UnityEngine;
using System.Collections;

public class AudioClips : MonoBehaviour {

	public static AudioClips instance;

	public AudioClip gameOver;
	public float gameOverVolume = 1f;
	public AudioClip music;
	public AudioClip musicLoop;
	public float musicVolume = 1f;
	public AudioClip musicSplash;
	public float musicSplashVolume = 1f;

	public AudioClip button;
	public float buttonVolume = 1f;
	public AudioClip loseLife;
	public float loseLifeVolume = 1f;
	public AudioClip pointCount;
	public float pointCountVolume = 1f;

	public AudioClip destroyElektro;
	public float destroyElektroVolume = 1f;
	public AudioClip destroyExplosion;
	public float destroyExplosionVolume = 1f;
	public AudioClip destroyFreeze;
	public float destroyFreezeVolume = 1f;
	public AudioClip holdFreeze;
	public float holdFreezeVolume = 1f;
	public AudioClip tapBigJelly;
	public float tapBigJellyVolume = 1f;
	public AudioClip tapSmallJelly;
	public float tapSmallJellyVolume = 1f;
	public AudioClip destroyJelly;
	public float destroyJellyVolume = 1f;

	public AudioClip tapGround;
	public float tapGroundVolume = 1f;

	public AudioClip iceAppear;
	public float iceAppearVolume = 1f;
	public AudioClip tapIce1;
	public AudioClip tapIce2;
	public AudioClip tapIce3;
	public float tapIceVolume = 1f;

	private void Start() {
		instance = this;
		//AudioManager.PlayMusic();
		AudioManager.PlaySplashMusic();
		ScoreManager.LoadTotalHighscore();
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.T))
			AudioManager.ToggleEffects();
		if (Input.GetKeyDown(KeyCode.Y))
			AudioManager.ToggleMusic();
	}
}