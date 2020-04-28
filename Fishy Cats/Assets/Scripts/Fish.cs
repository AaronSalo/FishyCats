using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] string fishName;
    [SerializeField] int rarity = 1; //from 1-5
    [SerializeField] double value;
    [SerializeField] double weight;
    [SerializeField] double length;

    [Header("Min/Max")]
    [SerializeField] double weightMin = 1.0D;
    [SerializeField] double weightMax = 1.0D;
    [SerializeField] double lengthMin = 1.0D;
    [SerializeField] double lengthMax = 1.0D;


    private double weightMult = 2.4; //the cost multiplier for weight
    private double lengthMult = 1.33;


    // Start is called before the first frame update
    void Start()
    {
        generateStats();
    }//start


    //generate the statistics for a fish. Really should only be done once
    public void generateStats() {
        //generate weight
        weight = Random.Range((float) weightMin, (float) weightMax); //get the next catch time
        //generate length
        length = Random.Range((float) lengthMin, (float) lengthMax); //get the next catch time
        value = weight*weightMult + length*lengthMult;
    }//generate stats


    public int getRarity() { return rarity; }

    public string getName() {return fishName; }
}//end of class