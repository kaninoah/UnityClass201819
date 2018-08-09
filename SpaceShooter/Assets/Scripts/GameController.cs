using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    [SerializeField]
    private AsteroidPool AsteroidP;

    [SerializeField]
    private EnemyPool EnemyP;
    [SerializeField]
    private BossPool BossP;
    private bool IsBossAlive;

  
    private BossController boss;


    [SerializeField]
    private EffectPool EffectP;

    [SerializeField]
    private ItemPool ItemP;

    [SerializeField]
    private int Score;

    private MainUIController ui;

    private Coroutine hazardRoutine;

    [SerializeField]
    private BGScroll[] BGs;
    private bool IsGameOver;

    private PlayerController player;

    [SerializeField]
    private int DefaultPlayerLife;
    private int PlayerLife;
    private readonly bool IsPlayerDead;

    [SerializeField]
    private int BossStageGap;
    private int CurrentStageNumber;
    
	// Use this for initialization
	void Start () {
        IsGameOver = false;
        IsBossAlive = false;
        // IsPlayerDead = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PlayerLife = DefaultPlayerLife - 1;
        hazardRoutine = StartCoroutine(Hazards());
        Debug.Log("coroutine1");

        for (int i = 0; i < BGs.Length; i++)
        {
            BGs[i].StartScroll();
        }
        Score = 0;
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<MainUIController>();
        ui.SetScore(Score);
        ui.SetPlayerLife(PlayerLife);
        CurrentStageNumber = 1;
	}
	
    public GameObject GetEffect(eEffectType input)
    {
        return EffectP.GetFromPool(input);
    }

    public void GameOver()
    {
        //StopCoroutine(hazardRoutine);
        Debug.Log("stop coroutine1");


        Invoke("WaitGameOver", 5); 
    }

    private void WaitGameOver()
    {
        
        if (PlayerLife > 0)
        {
            PlayerLife--;
            ui.SetPlayerLife(PlayerLife);
            player.transform.position = Vector3.zero;
            player.gameObject.SetActive(true);
            //hazardRoutine = StartCoroutine(Hazards());
            Debug.Log("new coroutine");
        }
        else
        {
            for (int i = 0; i < BGs.Length; i++)
            {
                BGs[i].StopScroll();
            }
            ui.SetGameOver();
            IsGameOver = true;
        }
       
    }

    public void GameRestart()
    {
        //SceneManager.LoadScene(0);
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        PlayerLife = DefaultPlayerLife - 1; 
        ui.SetPlayerLife(PlayerLife);

        Score = 0;
        ui.SetScore(Score);
        ui.HideGameOver();

        hazardRoutine = StartCoroutine(Hazards());
        Debug.Log("coroutine2");

        for (int i = 0; i < BGs.Length; i++)
        {
            BGs[i].StartScroll();
        }
    }

    public void AddScore(int value)
    {
        Score += value;
        ui.SetScore(Score);
    }

    public void AddLife()
    {
        PlayerLife++;
        ui.SetPlayerLife(PlayerLife);
    }

    public void PowerUP()
    {
        player.PowerUP();

    }

    private void SpawnAsteroid()
    {
        AsteroidMovement temp = AsteroidP.GetFromPool(Random.Range(0, 3));
        temp.transform.position = new Vector3(Random.Range(-5f, 5f), 0, 16);
        temp.gameObject.SetActive(true);
    }
    private void SpawnEnemy()
    {
        EnemyController temp = EnemyP.GetFromPool();
        temp.transform.position = new Vector3(Random.Range(-5f, 5f), 0, 16);
        temp.gameObject.SetActive(true);
    }

    private IEnumerator Hazards()
    {
        WaitForSeconds threeSec = new WaitForSeconds(3);
        WaitForSeconds pointTwoSec = new WaitForSeconds(.2f);


        while (true)
        {
            yield return threeSec;
            int asteroidSpawnCount = 10, enemySpawnCount = 3;

            while (true)
            {
                if (asteroidSpawnCount > 0 && enemySpawnCount > 0)
                {
                    int randValue = Random.Range(0, 2);
                    if (randValue == 1)
                    {
                        SpawnAsteroid();
                        Debug.Log("aste1");

                        asteroidSpawnCount--;
                        yield return pointTwoSec;
                    }
                    else
                    {
                        SpawnEnemy();
                        Debug.Log("enemy1");

                        enemySpawnCount--;
                        yield return pointTwoSec;
                    }
                }
                else if (enemySpawnCount > 0)
                {
                    for (int i = 0; i < enemySpawnCount; i++)
                    {
                        SpawnEnemy();
                        Debug.Log("enemy2");

                        yield return pointTwoSec;
                    }
                    break;
                }
                else if (asteroidSpawnCount > 0)
                {
                    for (int i = 0; i < asteroidSpawnCount; i++)
                    {
                        SpawnAsteroid();
                        Debug.Log("aste2");

                        yield return pointTwoSec;
                    }
                    break;
                }
                else
                {
                    break;
                }
            }
            CurrentStageNumber++;
            for (int i = 0; i < 3; i++)
            {
                GameObject item = ItemP.GetFromPool((eItemType)Random.Range(0, 2));
                item.transform.position = new Vector3(Random.Range(-5f, 5f), 0, 16);
                item.gameObject.SetActive(true);
            }


            if (CurrentStageNumber % BossStageGap == 0)
            {
                yield return threeSec;
                BossController boss = BossP.GetFromPool();
                boss.transform.position = new Vector3(0, 0, 19);
                boss.gameObject.SetActive(true);
                IsBossAlive = true;
                CurrentStageNumber++;

            }
            while (IsBossAlive)
            {
                yield return threeSec;
                Debug.Log("boss1");
                


            }

            //TODO 보스전 중 죽었을시 동작 pause 
            //while (player.CurrentHP <= 0)
            //{
            //    yield return null;
            //    Debug.Log("player dead");

            //    IsBossAlive = false;
            //    hazardRoutine = StartCoroutine(Hazards());
            //}
        }
    }
        

    public void BossDead()
    {
        IsBossAlive = false;
    }

	// Update is called once per frame
	void Update () {
		if (IsGameOver && Input.GetKeyDown(KeyCode.R))
        {
            GameRestart();
        }
	}
}
