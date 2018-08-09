using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltPool : MonoBehaviour {

    [SerializeField]
    private Bolt bolt;

    private List<Bolt> boltlist;    

    // Use this for initialization
    void Start () {
        boltlist = new List<Bolt>();
	}
	
    public Bolt GetFromPool()
    {
        for(int i = 0; i <boltlist.Count; i++)
        {
            if (!boltlist[i].gameObject.activeInHierarchy)
            {
                return boltlist[i];
            }
        }
        Bolt temp = Instantiate(bolt);
        boltlist.Add(temp);
        return temp;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
