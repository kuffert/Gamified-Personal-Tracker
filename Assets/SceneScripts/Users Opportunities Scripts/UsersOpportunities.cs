using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;

// Author: Chris Kuffert
// Date: 11/9/2015
public class UsersOpportunities : OpportunityController {

    void Start () {
        wipeAllOpportunityGameObjects();
        AppController.appController.Load();
        setAllSkills();
        List<Opportunity> usersOpportunities = AppController.appController.getUsersSelectedOpportunities();

        float fractionOfScreenPerOpportunity = (1 - (2 * ApplicationView.applicationView.getTaskbarFractionOfScreen() / 100f)) / numberOfDisplayedOpportunities;

        for (int i = 0; i < usersOpportunities.Count && i < numberOfDisplayedOpportunities; i++)
        {
            opportunityGameObjects.Add(generateOpportunity(i, usersOpportunities[i], numberOfVisibleCharacters, fractionOfScreenPerOpportunity));
        }
    }
	
	void Update () {
        navigateToOpportunityInformation();
	}

    // Navigate to the UsersOpportunitiesScene when an opportunity is touched. Saves the index of the 
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
                    int saveIndex = (AppController.appController.getUsersOpportunitiesPageNumber() * numberOfDisplayedOpportunities) + opportunityGameObjects.IndexOf(opportunityObject);
                    AppController.appController.setUsersSelectedOpportunityIndex(saveIndex);
                    AppController.appController.Save();
                    Application.LoadLevel("UsersOpportunityInformation");
                }
            }
        }
    }
}
