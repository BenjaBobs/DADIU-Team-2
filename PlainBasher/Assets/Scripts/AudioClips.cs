using UnityEngine;
using System.Collections;

public class AudioClips : MonoBehaviour {

	public static AudioClips instance;

	public AudioClip gameOver;
	public AudioClip music;
	public AudioClip musicLoop;
	public AudioClip musicSplash;

	public AudioClip button;
	public AudioClip loseLife;
	public AudioClip pointCount;

	public AudioClip destroyElektro;
	public AudioClip destroyExplosion;
	public AudioClip destroyFreeze;
	public AudioClip holdFreeze;
	public AudioClip tapBigJelly;
	public AudioClip tapSmallJelly;
	public AudioClip destroyJelly;

	public AudioClip tapGround;

	public AudioClip iceAppear;
	public AudioClip tapIce1;
	public AudioClip tapIce2;
	public AudioClip tapIce3;

	private void Awake() {
		instance = this;
	}
}