using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	// Audio players components.
	public AudioSource[] EffectsSource;
	public AudioSource[] MusicSource;

	// Random pitch adjustment range.
	public float LowPitchRange = .95f;
	public float HighPitchRange = 1.05f;

	// Singleton instance.
	public static SoundManager Instance = null;

	// Initialize the singleton instance.
	private void Awake()
	{
		// If there is not already an instance of SoundManager, set it to this.
		if (Instance == null)
		{
			Instance = this;
		}
		//If an instance already exists, destroy whatever this object is to enforce the singleton.
		else if (Instance != this)
		{
			Destroy(gameObject);
		}

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad(gameObject);
	}

	// Play a single clip through the sound effects source.
	public void Play(AudioClip clip, int indexSound)
	{
		EffectsSource[indexSound].clip = clip;
		EffectsSource[indexSound].Play();
	}

	// Play a single clip through the music source.
	public void PlayMusic(AudioClip clip, int indexMusique)
	{
		MusicSource[indexMusique].clip = clip;
		MusicSource[indexMusique].Play();
	}

	
}
