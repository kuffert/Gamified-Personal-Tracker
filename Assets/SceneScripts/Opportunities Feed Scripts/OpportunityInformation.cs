using UnityEngine;
using System.Collections.Generic;

// Author: Chris Kuffert
// Date: 11/22/15
public class OpportunityInformation : OpportunityFeed {
    
    private GameObject acceptTextButton;
    private Opportunity opportunity;
    private GameObject detailsTextButton;
    
    // Use this for initialization
    void Start () {
        AppController.appController.Load();
        int loadIndex = AppController.appController.getOpportunityFeedIndex();
        opportunity = AppController.appController.getAllOpportunities()[loadIndex];
        ApplicationView.applicationView.currentScreenText = truncateTitle(opportunity, numberOfVisibleCharacters, opportunity.Title.Length);
        ApplicationView.applicationView.currentScreenTextSize = 200 - opportunity.Title.Length;

        displayOpportunityMetadata(opportunity);

        GameObject acceptText = generateTextOverlay(.25f, "Accept");
        acceptTextButton = generateMetaDataNavigationButton(.25f, 2);

        generateTextOverlay(.75f, "Details");
        detailsTextButton = generateMetaDataNavigationButton(.75f, 2);

        if (containsOpportunityId(AppController.appController.getUsersSelectedOpportunities(), opportunity.Id))
        {
            acceptText.GetComponent<TextMesh>().text = "Accepted";
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (acceptTextButton.GetComponent<Collider>().Raycast(ray, out hit, 100.0F) && !containsOpportunityId(AppController.appController.getUsersSelectedOpportunities(), opportunity.Id))
            {
                List<Opportunity> allOppList = AppController.appController.getUsersSelectedOpportunities();
                allOppList.Add(opportunity);
                AppController.appController.setUsersSelectedOpportunities(allOppList);
                AppController.appController.Save();
                Application.LoadLevel("UsersOpportunities");
            }

            if (detailsTextButton.GetComponent<Collider>().Raycast(ray, out hit, 100.0f))
            {
                Application.LoadLevel("OpportunityDescription");
            }
        }
    }
}
