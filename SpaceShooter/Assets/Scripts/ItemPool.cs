using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour {
    [SerializeField]
    private GameObject[] item;

    private List<GameObject>[] itemList;

    // Use this for initialization
    void Start()
    {
        itemList = new List<GameObject>[item.Length];
        for (int i = 0; i < itemList.Length; i++)
        {
            itemList[i] = new List<GameObject>();
        }
    }

    public GameObject GetFromPool(eItemType input)
    {
        int index = (int)input;
        for (int i = 0; i < itemList[index].Count; i++)
        {
            if (!itemList[index][i].gameObject.activeInHierarchy)
            {
                return itemList[index][i];
            }
        }
        GameObject temp = Instantiate(item[index]);
        itemList[index].Add(temp);
        return temp;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
