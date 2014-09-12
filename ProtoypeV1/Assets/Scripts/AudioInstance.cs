using UnityEngine;
using System.Collections;

public class AudioInstance : MonoBehaviour {
	public AudioManager.AudioTag tag = AudioManager.AudioTag.DefaultSound;
	public float volume = 1f;
	public bool loop = false;
	public int loopTimes = 0;
	public bool destroy = false;
	public float length = 0f;

	private void Start () {

	}
	
	private void Update () {
	
	}
}