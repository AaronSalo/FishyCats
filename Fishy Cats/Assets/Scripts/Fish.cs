using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private string fishName = "ERR: no name assigned";
    [SerializeField] private int rarity = 1; //from 1-5
    [SerializeField] private double value;
    [SerializeField] private double weight;
    [SerializeField] private double length;

    [Header("Min/Max")]
    [SerializeField] double weightMin = 1.0D;
    [SerializeField] double weightMax = 1.0D;
    [SerializeField] double lengthMin = 1.0D;
    [SerializeField] double lengthMax = 1.0D;


    private double weightMult = 2.4; //the cost multiplier for weight
    private double lengthMult = 1.33;


    //generate the statistics for a fish. Really should only be done once
    public void generateStats() {
        //generate weight
        this.weight = Random.Range((float) weightMin, (float) weightMax); //get the next catch time
        //generate length
        this.length = Random.Range((float) lengthMin, (float) lengthMax); //get the next catch time
        this.value = (this.weight*weightMult + this.length*lengthMult) * this.rarity;
    }//generate stats

    //set this fish to another fish
    public void setupFish(Fish otherFish) {
        this.fishName = otherFish.getName();
        this.rarity = otherFish.getRarity();
        this.value = otherFish.getValue();
        this.weight = otherFish.getWeight();
        this.length = otherFish.getLength();
    }//setupFish

    public int getRarity() { return rarity; }

    public string getName() {return fishName; }

    public double getWeight() { return weight; }
    
    public double getLength() { return length; }

    public double getValue() { return value; }
}//end of class