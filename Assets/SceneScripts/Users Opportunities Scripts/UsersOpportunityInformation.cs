using UnityEngine;
using System.Collections;

public class UsersOpportunityInformation : UsersOpportunities {
    
	void Start () {
        AppController.appController.Load();
        int loadIndex = AppController.appController.getUsersSelectedOpportunityIndex();
        Opportunity opportunity = AppController.appController.getUsersSelectedOpportunities()[loadIndex]; 
        ApplicationView.applicationView.currentScreenText = opportunity.Title;
	}
	
	void Update () {

    }
}
