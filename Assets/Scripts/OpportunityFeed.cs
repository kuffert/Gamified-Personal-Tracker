using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;

// Author: Craig Hammond
// Date: 11/11/2015
public class OpportunityFeed : MonoBehaviour {
	
	public Font font;
	public int amountOfDisplayedOpportunties;
	private float fractionOfScreenPerOpportunity;
	private int numberOfVisibleCharacters = 30;
	private Color NURed = new Color(204.0f, 0.0f, 0.0f);
	
	// Use this for initialization
	void Start () {
		saveAllOpportunitiesToLocalFile();
		AppController.appController.Load();
		List<Opportunity> usersActivities = AppController.appController.getAllOpportunities();
		
		fractionOfScreenPerOpportunity = (1 - (2 * ApplicationView.applicationView.getTaskbarFractionOfScreen() / 100f)) / amountOfDisplayedOpportunties;
		
		for (int i = 0; i < usersActivities.Count; i++)
		{
			OpportunityController.generateOpportunity(i, usersActivities[i], numberOfVisibleCharacters, fractionOfScreenPerOpportunity);
		}
	}
	
	void Update () {
		// Eventually will need delegate navigation based on touch.
	}
	
	// Saves some test data for the user's activities
	private void saveAllOpportunitiesToLocalFile()
	{
		List<Opportunity> testActivities = OpportunityController.getOpportunities();
		AppController.appController.setAllOpportunities(testActivities);
		AppController.appController.Save();
	}
}
