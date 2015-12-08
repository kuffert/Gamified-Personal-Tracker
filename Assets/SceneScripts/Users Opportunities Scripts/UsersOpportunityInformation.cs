using UnityEngine;
using System.Collections.Generic;

// Author: Chris Kuffert
// Date: 11/22/15
public class UsersOpportunityInformation : UsersOpportunities {
    
    private GameObject completeTextButton;
    private GameObject removeTextButton;
    private GameObject detailsTextButton;
    private Opportunity opportunity;
    
	void Start () {
        AppController.appController.Load();
        int loadIndex = AppController.appController.getUsersSelectedOpportunityIndex();
        opportunity = AppController.appController.getUsersSelectedOpportunities()[loadIndex]; 
        ApplicationView.applicationView.currentScreenText = truncateTitle (opportunity, numberOfVisibleCharacters, opportunity.Title.Length);
        ApplicationView.applicationView.currentScreenTextSize = 200 - opportunity.Title.Length;

        displayOpportunityMetadata(opportunity);

        generateTextOverlay(.17f, "Complete");
        completeTextButton = generateMetaDataNavigationButton(.17f, 3);

        generateTextOverlay(.5f, "Details");
        detailsTextButton = generateMetaDataNavigationButton(.5f, 3);

        generateTextOverlay(.83f, "Remove");
        removeTextButton = generateMetaDataNavigationButton(.83f, 3);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (completeTextButton.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
            {
                AddExp();
                List<Opportunity> completedOppList = AppController.appController.getUsersCompletedOpportunities();
                completedOppList.Add(opportunity);
                AppController.appController.setUsersCompletedOpportunities(completedOppList);
                List<Opportunity> selectedOppList = AppController.appController.getUsersSelectedOpportunities();
                selectedOppList.Remove(opportunity);
                AppController.appController.setUsersSelectedOpportunities(selectedOppList);
                AppController.appController.Save();
                Application.LoadLevel("Profile");
            }

            if (removeTextButton.GetComponent<Collider>().Raycast(ray, out hit, 100.0f))
            {
                List<Opportunity> oppList = AppController.appController.getUsersSelectedOpportunities();
                oppList.RemoveAt(AppController.appController.getUsersSelectedOpportunityIndex());
                AppController.appController.setUsersSelectedOpportunities(oppList);
                AppController.appController.Save();
                Application.LoadLevel("UsersOpportunities");
            }

            if (detailsTextButton.GetComponent<Collider>().Raycast(ray, out hit, 100.0f))
            {
                Application.LoadLevel("UsersOpportunityDescription");
            }
        }
    }

    private void AddExp()
    {
        Experience toAdd = this.opportunity.EXP();

        int add = 0;

        toAdd.totals.TryGetValue(((Dimension)0).GetDescription(), out add);
        AppController.appController.setIAExp(AppController.appController.getIAExp() + add);
        add = 0;

        toAdd.totals.TryGetValue(((Dimension)1).GetDescription(), out add);
        AppController.appController.setGAExp(AppController.appController.getGAExp() + add);
        add = 0;

        toAdd.totals.TryGetValue(((Dimension)2).GetDescription(), out add);
        AppController.appController.setSCExp(AppController.appController.getSCExp() + add);
        add = 0;

        toAdd.totals.TryGetValue(((Dimension)3).GetDescription(), out add);
        AppController.appController.setPPExp(AppController.appController.getPPExp() + add);
        add = 0;

        toAdd.totals.TryGetValue(((Dimension)4).GetDescription(), out add);
        AppController.appController.setWBExp(AppController.appController.getWBExp() + add);

        AppController.appController.Save();

    }

}
