using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMute : MonoBehaviour {

	public AudioSource audio;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

 	public void muteMusic(){
		audio.mute = !audio.mute;
	}

}
