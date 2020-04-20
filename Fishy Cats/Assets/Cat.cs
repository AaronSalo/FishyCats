using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{

    [SerializeField] int level = 1; // all cats start at level 1

    [Header("Catch Speed")]
    [SerializeField] float catchSpeedMin = 1.0f; //min time to catch a fish (seconds)
    [SerializeField] float catchSpeedMax = 3.0f; //max time to catch a fish
    private float nextCatchTime; //time remaining till we catch another fish

    [Header("Bucket Stuff")]
    [SerializeField] int bucketSize = 5; //the amount of fish the cat can hold in his bucket
    public int fishInBucket = 0; //starts at no fish
    public GameObject bucketFullNotif;

    void Start() {
        if(nextCatchTime <= 0){
            nextCatchTime = Random.Range(catchSpeedMin, catchSpeedMax); //get the next catch time
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(fishInBucket < bucketSize) { //if we have room in our bucket
            goFish(); //try to catch a fish
        } else {
            bucketFullNotif.GetComponent<SpriteRenderer>().enabled = true; //show a notif that the bucket is full
        }
    }//update

    private void goFish() {
        if(nextCatchTime <= 0){
            fishInBucket++; //WE CAUGHT A FISH
            nextCatchTime = Random.Range(catchSpeedMin, catchSpeedMax); //get the next catch time
        } else {
            nextCatchTime -= Time.deltaTime;
        }

    }
}//end of class
