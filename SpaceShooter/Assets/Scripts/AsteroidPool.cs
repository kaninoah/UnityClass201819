using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPool : MonoBehaviour {

    [SerializeField]
    private AsteroidMovement[] asteroid;

    private List<AsteroidMovement>[] asteroidList;
    
    // Use this for initialization
    void Start()
    {
        asteroidList = new List<AsteroidMovement>[asteroid.Length];
        for (int i = 0; i < asteroidList.Length; i++)
        {
            asteroidList[i] = new List<AsteroidMovement>();
        }
    }

    public AsteroidMovement GetFromPool(int index)
    {
        for (int i = 0; i < asteroidList[index].Count; i++)
        {
            if (!asteroidList[index][i].gameObject.activeInHierarchy)
            {
                return asteroidList[index][i];
            }
        }
        AsteroidMovement temp = Instantiate(asteroid[index]);
        asteroidList[index].Add(temp);
        return temp;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
