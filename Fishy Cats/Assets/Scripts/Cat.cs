using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cat : MonoBehaviour
{
    [SerializeField] string catName = "name not set";
    [SerializeField] int level = 1; // all cats start at level 1

    [Header("Catch Speed")]
    [SerializeField] float catchSpeedMin = 1.0f; //min time to catch a fish (seconds)
    [SerializeField] float catchSpeedMax = 3.0f; //max time to catch a fish
    private float nextCatchTime; //time remaining till we catch another fish

    [Header("Bucket Stuff")]
    [SerializeField] int bucketSize = 5; //the amount of fish the cat can hold in his bucket
    [SerializeField] GameObject[] bucket; //fish this cat is holding
    public int fishInBucket = 0; //starts at no fish
    public GameObject bucketFullNotif = null; //this contains the sprite and position and stuff
    private SpriteRenderer notif;
    [SerializeField] GameObject barrel = null;

    [SerializeField] GameObject[] availableFish = null;
    private GameObject[] rarity1Fish;
    private GameObject[] rarity2Fish;
    private GameObject[] rarity3Fish;
    private GameObject[] rarity4Fish;
    private GameObject[] rarity5Fish;
    [SerializeField] private Vector3 fishSpawn; //place to spawn the fish off screen

    [Header("Button Logistics")]
    [SerializeField] GameObject emptyButtonPrefab;
    [SerializeField] GameObject canvas = null;
    protected Button emptyBucketButton;


    void Start() {
        if(nextCatchTime <= 0){
            nextCatchTime = Random.Range(catchSpeedMin, catchSpeedMax); //get the next catch time
        }
        notif = bucketFullNotif.GetComponent<SpriteRenderer>(); //get the spriteRenderer

        rarity1Fish = sortFish(1);
        rarity2Fish = sortFish(2);
        rarity3Fish = sortFish(3);
        rarity4Fish = sortFish(4);
        rarity5Fish = sortFish(5);
            
        //set up the button for emptying the cats bucket
        setupEmptyBucket();
    }

    // Update is called once per frame
    void Update()
    {
        if(fishInBucket < bucketSize) { //if we have room in our bucket
            goFish(); //try to catch a fish
            notif.enabled = false; //remove the full bucket notif
        } else {
            notif.enabled = true; //show a notif that the bucket is full
        }
        fishery();
    }//update


    //perform the fishing action
    private void goFish() {
        if(nextCatchTime <= 0){
            GameObject fish = fishery(); //get a fish from the fishery
            if(fish != null) {
                Debug.Log(this.catName + " caught a " + fish.GetComponent<Fish>().getName() + "!!");
                bucket[fishInBucket] = fish;
                fishInBucket++; //WE CAUGHT A FISH
            } else {
                Debug.Log("Line snapped! :(");
            }
            nextCatchTime = Random.Range(catchSpeedMin, catchSpeedMax); //get the next catch time
        } else {
            nextCatchTime -= Time.deltaTime;
        }

    }//goFish

    /**emptyBucket
    empty the bucket, return how many fish we got
    */
    public int emptyBucket() {
        int temp = fishInBucket;
        fishInBucket = 0;
        GameObject[] remainder = barrel.GetComponent<Barrel>().fillBarrel(bucket);
        bucket = new GameObject[bucketSize];

        //place the remainder back into the cats bucket
        if(remainder != null) {
            foreach(GameObject f in remainder) {
                if(f != null) {
                    bucket[fishInBucket] = f;
                    fishInBucket ++;
                }
            }//foreach
        } //else continue on

        //maybe play a sound here?
        return temp;
    }//emptyBucket


    //get a fish from the current available fish
    //returns a GameObject with a fish component
    private GameObject fishery() {

        //percent chance to catch each fish, should add up to 100
        int rarity1Prob = 50;
        int rarity2Prob = 25;
        int rarity3Prob = 10;
        int rarity4Prob = 8;
        //rarity 5 is the remainder

        int rand = (int) Random.Range(1, 100);
        GameObject fish = null;

        //TODO: add probability for line to snap? Decreases as cat levels up

        //get the fish based on the given rarity
        if((rarity1Fish.Length > 0) && (rand < rarity1Prob)) {
            //get a rarity 1 fish
            rand = Random.Range(0, rarity1Fish.Length );
            fish = rarity1Fish[rand];

        } else if ((rarity2Fish.Length > 0) && (rand < rarity1Prob + rarity2Prob)) {
            //get rarity 2
            Random.Range(0, rarity2Fish.Length );
            rand = Random.Range(0, rarity2Fish.Length );
            fish = rarity2Fish[rand];

        } else if ((rarity3Fish.Length > 0 ) && (rand < rarity1Prob + rarity2Prob + rarity3Prob) ) {
            //get rarity 3
            Random.Range(0, rarity3Fish.Length );
            rand = Random.Range(0, rarity3Fish.Length );
            fish = rarity3Fish[rand];

        } else if((rarity4Fish.Length > 0) && (rand < rarity1Prob + rarity2Prob + rarity3Prob + rarity4Prob) ) {
            //get rarity 4
            Random.Range(0, rarity4Fish.Length );
            rand = Random.Range(0, rarity4Fish.Length );
            fish = rarity4Fish[rand];

        } else if (rarity5Fish.Length > 0 ) {
            //get rarity 5
            Random.Range(0, rarity5Fish.Length );
            rand = Random.Range(0, rarity5Fish.Length );
            fish = rarity5Fish[rand];
        } //else if
        
        fish.GetComponent<Fish>().generateStats();
        //Debug.Log("got fish at index " + rand + " and got a " + fish.GetComponent<Fish>().getName());   
        return Instantiate(fish, fishSpawn, fish.transform.rotation); //instantiate to create a new fish
    }//fishery


    public string getName() { return this.catName; }

/////SETUP FUNCTS

    private GameObject[] sortFish(int rarity) {
        GameObject[] temp = new GameObject[10];
        int numElements = 0; //count how many fish of a specific rarity there are

        foreach (GameObject f in availableFish) {
            if(f.GetComponent<Fish>().getRarity() == rarity) {
                temp[numElements] = f;
                numElements ++;
            }//if
        }

        //make an array of the correct size and copy to it
        GameObject[] arr = new GameObject[numElements]; 
        for(int i = 0; i < numElements; i++) {
            arr[i] = temp[i];
        }
        
        return arr;
    }//sortFish

    //this function sets up the button so we can empty a button
    //allows GameManager to create new cats without worrying about button placement and other stuff
    //only should be called on creation
    private void setupEmptyBucket() {
        //initialize the bucket
        bucket = new GameObject[bucketSize];

        //set up the button for emptying the cats bucket
        emptyButtonPrefab = Instantiate(emptyButtonPrefab); //create the button
        emptyButtonPrefab.transform.SetParent(canvas.transform, false); //make the canvas the parent of the button
        emptyButtonPrefab.transform.position = bucketFullNotif.transform.position; //set the position
        emptyBucketButton = emptyButtonPrefab.GetComponent<Button>(); //instantiate the button with the cat
        emptyBucketButton.onClick.AddListener( delegate { emptyBucket(); }); //create the onClick event
    } //setupEmptyBucket


    

    ///GETTERS/SETTERS/////
    public GameObject[] getBucket() { return this.bucket; }

    public int getBucketSize() { return bucketSize; }

}//end of class