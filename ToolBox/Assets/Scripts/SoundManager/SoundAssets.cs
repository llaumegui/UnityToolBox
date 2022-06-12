using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAssets : MonoBehaviour
{

	static SoundAssets _i;
	public static SoundAssets i
	{
		get
		{
			if (_i == null)
			{
				_i = Instantiate(Resources.Load<SoundAssets>("Prefabs/SoundAssets"));
			}
			return _i;
		}
	}

	public string Hello()
	{
		return "Hello World";
	}

	[System.Serializable]
	public class SoundAudioClip
	{
		public Sound sound;
		public AudioClip[] audioClips;
	}

	public SoundAudioClip[] soundAudioClips;

}
