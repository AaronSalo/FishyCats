using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    public GameObject[] cats;

    //CAT UI DETAILS
    [SerializeField] GameObject catUI = null;
    private Cat currCat;
    private Text catNameUI;
    private Text catLvlUI;
    private GameObject inventory;

    //FISH INSPECTOR DETAILS
    private GameObject fishInspector;
    private GameObject fishInInspector; //the fish being inspected
    private Text fishNameUI;
    private Image fishInspectorImage;
    private GameObject fishInspectorDetails;


    //called before the first frame
    void Start() {

        //set up references to the cat UI
        catNameUI = catUI.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
        inventory = catUI.transform.GetChild(2).gameObject;
        currCat = cats[0].GetComponent<Cat>(); //set it to the first cat by default

        //set up references to the fish inspector
        fishInspector = catUI.transform.GetChild(4).gameObject;
        fishNameUI = fishInspector.transform.GetChild(0).gameObject.GetComponent<Text>();
        fishInspectorImage = fishInspector.transform.GetChild(1).gameObject.GetComponent<Image>();
        fishInspectorDetails = fishInspector.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(catUI.activeSelf == true) {
            updateInventory(currCat.GetComponent<Cat>());
        }
    }

    //displayt the cat UI for the given cat
    public void displayCatUI(GameObject cat) {
        Debug.Log("Displaying Cat Info UI...");
        catUI.SetActive(true); //enable the ui display

        currCat = cat.GetComponent<Cat>(); //update the curr cat

        for(int i = 0; i < currCat.getBucketSize(); i++) {
            inventory.transform.GetChild(i).gameObject.SetActive(true);
        }

        currCat = cat.GetComponent<Cat>(); 
        catNameUI.text = currCat.getName();

        //update the inventory
        updateInventory(currCat);

    }//displayCatUI


    //set up the fish inspector from the slot
    public void displayFishInspector(GameObject slot) {
        Fish fish = slot.transform.GetChild(1).gameObject.GetComponent<Fish>();

        Debug.Log("displaying " + slot.name);
        fishNameUI.text = fish.getName(); //set the name
        fishInspectorImage.sprite = fish.GetComponent<SpriteRenderer>().sprite; //set the image

        //set the details up
        fishInspectorDetails.transform.GetChild(0).GetComponent<Text>().text = "Length: " + fish.getLength().ToString("#.##");
        fishInspectorDetails.transform.GetChild(1).GetComponent<Text>().text = "Weight: " + fish.getWeight().ToString("#.##");
        fishInspectorDetails.transform.GetChild(2).GetComponent<Text>().text = "Approx. Value: " + fish.getValue().ToString("#.##");
        fishInspectorDetails.transform.GetChild(3).GetComponent<Text>().text = "Rarity: " + fish.getRarity();
    }



    //update the inventory so we can see what fish are caught live
    private void updateInventory(Cat cat) {

        GameObject[] bucket = cat.getBucket();

        //iterate through the slots, inserting the fish from the cat's bucket
        for(int i = 0; i < bucket.Length; i++) {

            GameObject button = inventory.transform.GetChild(i).gameObject;
            GameObject slot = inventory.transform.GetChild(i).GetChild(0).gameObject;
            if(bucket[i] != null ) {
                button.GetComponent<Button>().enabled = true;
                slot.SetActive(true);
                slot.GetComponent<Image>().sprite = bucket[i].GetComponent<SpriteRenderer>().sprite; //set the icon picture

                //set up the fish reference
                GameObject fishComponent = inventory.transform.GetChild(i).GetChild(1).gameObject; //get a reference to the fish component of the icon
                fishComponent.GetComponent<Fish>().setupFish( bucket[i].GetComponent<Fish>() ); //set a reference to the fish
                fishComponent.GetComponent<SpriteRenderer>().sprite = bucket[i].GetComponent<SpriteRenderer>().sprite; //set the sprite
            } else {
                slot.SetActive(false);
                button.GetComponent<Button>().enabled = false;
            }
        } //for
    }

    
    public void disableCatUI() {
        catUI.SetActive(false);
    }


    //used for debugging
    public void buttonTest() {
        Debug.Log("Test was successful");
    }

}
