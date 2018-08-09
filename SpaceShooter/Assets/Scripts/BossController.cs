using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {
    private Rigidbody rb;

    [SerializeField]
    private Transform BoltPos;
    [SerializeField]
    private BoltPool BoltP;
    [SerializeField]
    private float Speed;

    [SerializeField]
    private int MaxHP;
    private int CurrentHP;

    private GameController control;
    private SoundController soundControl;
    private MainUIController UIControl;

    [SerializeField]
    private int ScoreValue;

    private bool AttackStart;
     
    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        soundControl = GameObject.FindGameObjectWithTag("SoundController")
                                                .GetComponent<SoundController>();
        control = GameObject.FindGameObjectWithTag("GameController")
                                                .GetComponent<GameController>();
        //BoltP = GameObject.FindGameObjectWithTag("EnemyBoltPool").
        //                                GetComponent<BoltPool>();     
        UIControl = GameObject.FindGameObjectWithTag("UI")
                                                .GetComponent<MainUIController>();    
    }

    public void SetBoltPool(BoltPool boltP)
    {
        BoltP = boltP;
    }

    private void OnEnable()
    {        
        AttackStart = false;
        CurrentHP = MaxHP;
        UIControl.SetHP((float)CurrentHP / MaxHP);
        StartCoroutine(BossPhase());
    }

    private IEnumerator BossPhase()
    {
        rb.velocity = Vector3.back * Speed;

        WaitForSeconds pointOne = new WaitForSeconds(.1f);
        WaitForSeconds pointFive = new WaitForSeconds(.5f);
        WaitForSeconds One = new WaitForSeconds(1);
        WaitForSeconds Twopointfive = new WaitForSeconds(2.5f);
        WaitForSeconds Five = new WaitForSeconds(5);
        Coroutine AutoShot;

        while (rb.position.z > 10.2f)
        {
            yield return pointOne;
        }
        rb.velocity = Vector3.zero;
        yield return One;
        AttackStart = true;

        while (true)
        {
            rb.velocity = Vector3.left * Speed;
            AutoShot = StartCoroutine(AutoShoot());
            yield return Twopointfive;
            StopCoroutine(AutoShot);
            rb.velocity = Vector3.zero;
            yield return pointFive;

            rb.velocity = Vector3.right * Speed;
            AutoShot = StartCoroutine(AutoShoot());
            yield return Five;
            StopCoroutine(AutoShot);
            rb.velocity = Vector3.zero;
            yield return pointFive;

            rb.velocity = Vector3.left * Speed;
            AutoShot = StartCoroutine(AutoShoot());
            yield return Twopointfive;
            StopCoroutine(AutoShot);
            rb.velocity = Vector3.zero;            
            yield return One;
        }

    }

    private IEnumerator AutoShoot()
    {
        WaitForSeconds pointFive = new WaitForSeconds(.5f);

        while (true)
        {
            yield return pointFive;
            Bolt bullet = BoltP.GetFromPool();
            bullet.transform.position = BoltPos.position;
            bullet.transform.rotation = BoltPos.rotation;
            bullet.gameObject.SetActive(true);
            soundControl.PlayEffectSound(eSoundEffectClip.shotEnemy);
        }
    }

    public void Bomb()
    {
        CurrentHP /= 2;
        UIControl.SetHP((float)CurrentHP / MaxHP);
    }

    private void OnTriggerEnter(Collider other)
    {       
        if (other.gameObject.CompareTag("PlayerBolt"))
        {
            other.gameObject.SetActive(false);
                       

            if(AttackStart)
            {
                if (CurrentHP > 0)
                {
                    CurrentHP--;
                }
                else
                {
                    control.AddScore(ScoreValue);
                    control.BossDead();
                    gameObject.SetActive(false);

                    soundControl.PlayEffectSound(eSoundEffectClip.expEnemy);
                    GameObject effect = control.GetEffect(eEffectType.expEnemy);
                    effect.transform.position = transform.position;
                    effect.SetActive(true);
                }
                UIControl.SetHP((float)CurrentHP / MaxHP);
            } 
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
