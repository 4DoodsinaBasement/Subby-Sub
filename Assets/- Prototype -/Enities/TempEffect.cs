using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEffect : MonoBehaviour
{
	AudioSource source;

	void Start()
	{
		source = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (source.isPlaying != true)
		{
			Destroy(this.gameObject);
		}
	}
}
