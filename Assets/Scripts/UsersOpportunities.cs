using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;

// Author: Chris Kuffert
// Date: 11/9/2015
public class UsersOpportunities : MonoBehaviour {

	public Font font;
    public int amountOfDisplayedActivities;
    private float fractionOfScreenPerActivity;
    private const string baseUrl = "https://api.mongolab.com/api/1";
    private const string database = "softdevfall15";
    private const string activitiesCollection = "opportunities";
    private const string urlKeyEnd = "?apiKey=ZMZOg1DKKoow4p8XCzVGfX-k8P6szwZj";
    private const string url = baseUrl + "/databases/" + database + "/collections/" + activitiesCollection + urlKeyEnd;
    private int numberOfVisibleCharacters = 35;
	private Color NURed = new Color(204.0f, 0.0f, 0.0f);

    // Use this for initialization
    void Start () {
        saveUserTestData();
        AppController.appController.Load();
        List<Opportunity> usersActivities = AppController.appController.getCurrentOpportunities();

        fractionOfScreenPerActivity = (1 - (2 * StaticFeatures.staticFeatures.getTaskbarFractionOfScreen() / 100f)) / amountOfDisplayedActivities;

        for (int i = 0; i < usersActivities.Count; i++)
        {
            generateActivity(i, usersActivities[i]);
        }
	}
	
	void Update () {
        // Eventually will need delegate navigation based on touch.
	}

    private GameObject generateActivity(int activityNumber, Opportunity activity) 
    {
        GameObject newActivity = new GameObject();
        
        newActivity.AddComponent<TextMesh>();
        TextMesh newText = newActivity.GetComponent<TextMesh>();
        int titleLength = activity.getTitle().Length;
        Debug.Log(titleLength);
        newText.text = titleLength < numberOfVisibleCharacters? activity.getTitle() : activity.getTitle().Substring(0, (titleLength < numberOfVisibleCharacters ? titleLength : numberOfVisibleCharacters)) + "...";
        newText.anchor = TextAnchor.UpperLeft;
        newText.characterSize = .025f;
        newText.fontSize = 200 - titleLength;
		newText.color = NURed;
		//newText.font = font; This is broken at the moment, its causing a ton of problems
        
        newActivity.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f - (StaticFeatures.staticFeatures.getTaskbarFractionOfScreen() / 100f + activityNumber * fractionOfScreenPerActivity), 10));
        newActivity.GetComponent<MeshRenderer>().sortingOrder = 4;

        newActivity.AddComponent<BoxCollider>();
        newActivity.GetComponent<BoxCollider>().size = new Vector3(StaticFeatures.staticFeatures.getOrthographicScreenWidth(), StaticFeatures.staticFeatures.getOrthographicScreenHeight() * fractionOfScreenPerActivity, 0);
        newActivity.GetComponent<BoxCollider>().center = new Vector3(StaticFeatures.staticFeatures.getOrthographicScreenWidth() * .5f, newActivity.GetComponent<BoxCollider>().size.y /-2f, 0f);
        return newActivity;
    }

    // Saves some test data for the user's activities
    private void saveUserTestData()
    {
        List<Opportunity> testActivities = getActivities();

        AppController.appController.setCurrentOpportunities(testActivities);
        AppController.appController.Save();
    }

    private List<Opportunity> getActivities()
    {
        List<Opportunity> activities = new List<Opportunity>();

        // Grab data
        WWW www = new WWW(url);
        while (!www.isDone) {/*do nothing, does not work otherwise*/}
        var N = JSON.Parse(www.text);

        // Fill activities
        Opportunity temp;
        while(N.Count > 0)
        {
            Debug.Log(N.Count);
            temp = new Opportunity(N[0]["title"]); // for now just putting in titles
            activities.Add(temp);
            N.Remove(0);
        }
        Debug.Log(activities.Count);
        return activities;
    }
}
