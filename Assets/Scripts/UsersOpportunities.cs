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
        saveLocalTestData();
        AppController.appController.Load();
        List<Opportunity> usersActivities = AppController.appController.getUsersSelectedOpportunities();

        fractionOfScreenPerOpportunity = (1 - (2 * StaticFeatures.staticFeatures.getTaskbarFractionOfScreen() / 100f)) / amountOfDisplayedOpportunties;

        for (int i = 0; i < usersActivities.Count; i++)
        {
            OpportunityController.generateOpportunity(i, usersActivities[i], numberOfVisibleCharacters, fractionOfScreenPerOpportunity);
        }
    }
	
	void Update () {
        // Eventually will need delegate navigation based on touch.
	}
	

    // Saves some test data for the user's activities
    private void saveLocalTestData()
    {
        List<Opportunity> testActivities = new List<Opportunity>();
        testActivities.Add(OpportunityController.getActivities()[1]);
        testActivities.Add(OpportunityController.getActivities()[2]);

        AppController.appController.setUsersSelectedOpportunities(testActivities);
        AppController.appController.Save();
    }
}
