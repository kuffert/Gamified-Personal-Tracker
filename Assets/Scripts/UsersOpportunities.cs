using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;

// Author: Chris Kuffert
// Date: 11/9/2015
public class UsersOpportunities : MonoBehaviour {

	public Font font;
    public int amountOfDisplayedOpportunties;
    private float fractionOfScreenPerOpportunity;
    private int numberOfVisibleCharacters = 35;
	private Color NURed = new Color(204.0f, 0.0f, 0.0f);

    // Use this for initialization
    void Start () {
        saveUserTestData();
        AppController.appController.Load();
        List<Opportunity> usersActivities = AppController.appController.getCurrentOpportunities();

        fractionOfScreenPerOpportunity = (1 - (2 * StaticFeatures.staticFeatures.getTaskbarFractionOfScreen() / 100f)) / amountOfDisplayedOpportunties;
	}
	
	void Update () {
        // Eventually will need delegate navigation based on touch.
	}
	

    // Saves some test data for the user's activities
    private void saveUserTestData()
    {
        List<Opportunity> testActivities = OpportunityController.getActivities();

        AppController.appController.setCurrentOpportunities(testActivities);
        AppController.appController.Save();
    }
}
