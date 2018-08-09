using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSoundEffectClip {
    shotPlayer,
    shotEnemy,
    expPlayer,
    expEnemy,
    expAsteroid        
};

public class SoundController : MonoBehaviour {
    [SerializeField]
    private AudioSource BGM, Effect;
    [SerializeField]
    private AudioClip[] EffectClips;
    [SerializeField]
    private AudioClip BGMClip;
    // Use this for initialization


    void Start () {
        BGM.clip = BGMClip;
        BGM.Play();        
	}
    public void PlayEffectSound(eSoundEffectClip index)
    {
        Effect.PlayOneShot(EffectClips[(int)index]);
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
