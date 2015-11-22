using UnityEngine;
using System.Collections;

public class OpportunityInformation : OpportunityFeed {

	// Use this for initialization
	void Start () {
        AppController.appController.Load();
        int loadIndex = AppController.appController.getOpportunityFeedIndex();
        Opportunity opportunity = AppController.appController.getAllOpportunities()[loadIndex];
        ApplicationView.applicationView.currentScreenText = opportunity.Title;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
