using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{

    [SerializeField] int barrelSize = 10;
    [SerializeField] int numFishInBarrel = 0;
    [SerializeField] GameObject[] barrel;

    [SerializeField] GameObject barrelFullNotif = null;

    // Start is called before the first frame update
    void Start()
    {
        barrel = new GameObject[barrelSize];
        barrelFullNotif.GetComponent<SpriteRenderer>().enabled = false;
    }



    public bool barrelFull() { return numFishInBarrel >= barrel.Length; }



    //add the given fish into the barrel
    //returns the remainder of fish that cannot fit into the barrel
    //if there is no remainder, return null
    public GameObject[] fillBarrel(GameObject[] fish) {

        GameObject[] remainder = new GameObject[fish.Length];
        int rPos = 0; //remainder arrPos

        //insert as many fish into the barrel, the rest into remainder
        foreach(GameObject f in fish) {
            if(numFishInBarrel >= barrel.Length){
                remainder[rPos] = f;
                rPos ++;
            } else if( f != null) {
                barrel[numFishInBarrel] = f;
                numFishInBarrel ++;
            }
        } //foreach

        //update the full barrel notification
        if(barrelFull())
            barrelFullNotif.GetComponent<SpriteRenderer>().enabled = true;
        else
            barrelFullNotif.GetComponent<SpriteRenderer>().enabled = false;

        //do we need to return a remainder?
        if(rPos == 0)
            return null;
        else
            return remainder;
    }//fillBarrel

}//class
