using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour {

    private Rigidbody rb;

    [SerializeField]
    private float angularSpeed, Speed;

    [SerializeField]
    private int ScoreValue;

    private GameController control;
    private SoundController soundControl;

    // Use this for initialization
    void Awake () {
        rb = GetComponent<Rigidbody>();
        control = GameObject.FindGameObjectWithTag("GameController")
                                                        .GetComponent<GameController>();
        soundControl = GameObject.FindGameObjectWithTag("SoundController")
                                                        .GetComponent<SoundController>();
        
    }

    private void OnEnable()
    {
        rb.angularVelocity = Random.onUnitSphere * angularSpeed;
        rb.velocity = Vector3.back * Speed;
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            soundControl.PlayEffectSound(eSoundEffectClip.expAsteroid);
            GameObject effect = control.GetEffect(eEffectType.expAsteroid);
            effect.transform.position = transform.position;
            effect.SetActive(true);
        }
        if (other.gameObject.CompareTag("PlayerBolt"))
        {
            other.gameObject.SetActive(false);        
            control.AddScore(ScoreValue);
            gameObject.SetActive(false);
                      
            soundControl.PlayEffectSound(eSoundEffectClip.expAsteroid);
            GameObject effect = control.GetEffect(eEffectType.expAsteroid);
            effect.transform.position = transform.position;
            effect.SetActive(true);
        }
    }

    public void Bomb()
    {
        control.AddScore(ScoreValue);
        gameObject.SetActive(false);

        soundControl.PlayEffectSound(eSoundEffectClip.expEnemy);
        GameObject effect = control.GetEffect(eEffectType.expEnemy);
        effect.transform.position = transform.position;
        effect.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
