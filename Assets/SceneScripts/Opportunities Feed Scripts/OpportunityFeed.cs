using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;

// Author: Craig Hammond
// Date: 11/11/2015
public class OpportunityFeed : OpportunityController {

	void Start () {
        wipeAllOpportunityGameObjects();
		saveAllOpportunitiesToLocalFile();
		AppController.appController.Load();
		List<Opportunity> usersOpportunities = AppController.appController.getAllOpportunities();
		
		float fractionOfScreenPerOpportunity = (1 - (2 * ApplicationView.applicationView.getTaskbarFractionOfScreen() / 100f)) / numberOfDisplayedOpportunities;
		
		for (int i = 0; i < usersOpportunities.Count && i < numberOfDisplayedOpportunities; i++)
		{
            opportunityGameObjects.Add(generateOpportunity(i, usersOpportunities[i], numberOfVisibleCharacters, fractionOfScreenPerOpportunity));
		}
	}
	
	void Update () {
        navigateToOpportunityInformation();
	}
	
	// Saves some test data for the user's activities
	private void saveAllOpportunitiesToLocalFile()
	{
		List<Opportunity> dbOpportunties = getOpportunities();
		AppController.appController.setAllOpportunities(dbOpportunties);
		AppController.appController.Save();
	}

    // Navigate to the Opportunity's information scene when an opportunity is touched. Saves the index of the 
    // opportunity to be displayed so that it can be loaded upon changing scenes. The index is calculated
    // by multiplying the current page number (starting at zero) by the amount of opportunities 
    // displayed on each page, then adding the index of it on the list of game objects. 
    public void navigateToOpportunityInformation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            foreach (GameObject opportunityObject in opportunityGameObjects)
            {
                if (opportunityObject.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
                {
                    int saveIndex = (AppController.appController.getOpportunityFeedPageNumber() * numberOfDisplayedOpportunities) + opportunityGameObjects.IndexOf(opportunityObject);
                    AppController.appController.setOpportunityFeedIndex(saveIndex);
                    AppController.appController.Save();
                    Application.LoadLevel("OpportunityInformation"); 
                }
            }
        }
    }
}
