using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eItemType
{
    LifeUP, PowerUP
}

public class Item : MonoBehaviour {

    private Rigidbody rb;

    [SerializeField]
    eItemType itemType;



    [SerializeField]
    private float angularSpeed, Speed;

    private GameController control;
    internal static int Length;

    //private SoundController soundControl;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        control = GameObject.FindGameObjectWithTag("GameController")
                                                        .GetComponent<GameController>();


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
            switch (itemType)
            {
                case eItemType.LifeUP:
                    control.AddLife();
                    break;
                case eItemType.PowerUP:
                    control.PowerUP();
                    break;
                default:
                    Debug.Log("Item Type error");
                    break;
            }







            //soundControl.PlayEffectSound(eSoundEffectClip.expAsteroid);
            //GameObject effect = control.GetEffect(eEffectType.expAsteroid);
            //effect.transform.position = transform.position;
            //effect.SetActive(true);
        }      
    }

    // Update is called once per frame
    void Update () {
		
	}
}
