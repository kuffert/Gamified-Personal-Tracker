using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;

// Author: Chris Kuffert
// Date: 11/9/2015
public class CurrentActivitiesFeed : MonoBehaviour {

    public int amountOfDisplayedActivities;
    private float fractionOfScreenPerActivity;
    private const string baseUrl = "https://api.mongolab.com/api/1";
    private const string database = "softdevfall15";
    private const string activitiesCollection = "opportunities";
    private const string urlKeyEnd = "?apiKey=ZMZOg1DKKoow4p8XCzVGfX-k8P6szwZj";
    private const string url = baseUrl + "/databases/" + database + "/collections/" + activitiesCollection + urlKeyEnd;

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
        List<Activity> testActivities = getActivities();

        AppController.appController.setCurrentActivities(testActivities);
        AppController.appController.Save();
    }

    private List<Activity> getActivities()
    {
        List<Activity> activities = new List<Activity>();

        // Grab data
        WWW www = new WWW(url);
        while (!www.isDone) {/*do nothing, does not work otherwise*/}
        var N = JSON.Parse(www.text);

        // Fill activities
        Activity temp;
        while(N.Count > 0)
        {
            Debug.Log(N.Count);
            temp = new Activity(N[0]["title"]); // for now just putting in titles
            activities.Add(temp);
            N.Remove(0);
        }
        Debug.Log(activities.Count);
        return activities;
    }
}
