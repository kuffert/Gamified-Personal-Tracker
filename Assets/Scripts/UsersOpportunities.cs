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
        List<Opportunity> usersOpportunities = AppController.appController.getUsersSelectedOpportunities();

        fractionOfScreenPerOpportunity = (1 - (2 * ApplicationView.applicationView.getTaskbarFractionOfScreen() / 100f)) / amountOfDisplayedOpportunties;

        for (int i = 0; i < usersOpportunities.Count; i++)
        {
            OpportunityController.generateOpportunity(i, usersOpportunities[i], numberOfVisibleCharacters, fractionOfScreenPerOpportunity);
        }
    }
	
	void Update () {
        // Eventually will need delegate navigation based on touch.
	}

    // Saves some test data for the user's activities
    private void saveLocalTestData()
    {
        List<Opportunity> testOpportunities = new List<Opportunity>();
        testOpportunities.Add(OpportunityController.getOpportunities()[1]);
        testOpportunities.Add(OpportunityController.getOpportunities()[2]);

        AppController.appController.setUsersSelectedOpportunities(testOpportunities);
        AppController.appController.Save();
    }
}
