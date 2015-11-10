using UnityEngine;
using System.Collections.Generic;


// Author: Chris Kuffert
// Date: 11/9/2015
public class CurrentActivitiesFeed : MonoBehaviour {

    public int amountOfDisplayedActivities;
    private float fractionOfScreenPerActivity;

    // Use this for initialization
    void Start () {
        saveUserTestData();
        AppController.appController.Load();
        List<Activity> usersActivities = AppController.appController.getCurrentActivities();

        fractionOfScreenPerActivity = (1 - (2 * StaticFeatures.staticFeatures.getTaskbarFractionOfScreen()/ 100f)) / amountOfDisplayedActivities;

        for (int i = 0; i < usersActivities.Count; i++)
        {
            generateActivity(i, usersActivities[i]);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private GameObject generateActivity(int activityNumber, Activity activity) 
    {
        GameObject newActivity = new GameObject();
        
        newActivity.AddComponent<TextMesh>();
        TextMesh newText = newActivity.GetComponent<TextMesh>();
        newText.text = activity.getTitle();
        newText.anchor = TextAnchor.UpperLeft;
        newText.characterSize = .025f;
        newText.fontSize = 200;
        newText.color = Color.black;
        
        newActivity.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f - (StaticFeatures.staticFeatures.getTaskbarFractionOfScreen() / 100f + activityNumber * fractionOfScreenPerActivity), 10));
        newActivity.GetComponent<MeshRenderer>().sortingOrder = 4;

        newActivity.AddComponent<BoxCollider>();

        return newActivity;
    }

    // Saves some test data for the user's activities
    private void saveUserTestData()
    {
        List<Activity> testActivities = new List<Activity>();

        Activity testActivityOne = new Activity("test one");
        Activity testActivityTwo = new Activity("test two");
        Activity testActivityThree = new Activity("test three");
        Activity testActivityFour = new Activity("test four");
        Activity testActivityFive = new Activity("test five");
        Activity testActivitySix = new Activity("test six");
        Activity testActivitySeven = new Activity("test seven");
        Activity testActivityEight = new Activity("test eight");
        Activity testActivityNine = new Activity("test nine");
        Activity testActivityTen = new Activity("test ten");

        testActivities.Add(testActivityOne);
        testActivities.Add(testActivityTwo);
        testActivities.Add(testActivityThree);
        testActivities.Add(testActivityFour);
        testActivities.Add(testActivityFive);
        testActivities.Add(testActivitySix);
        testActivities.Add(testActivitySeven);
        testActivities.Add(testActivityEight);
        testActivities.Add(testActivityNine);
        testActivities.Add(testActivityTen);

        AppController.appController.setCurrentActivities(testActivities);
        AppController.appController.Save();
    }
}
