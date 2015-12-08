using UnityEngine;
using System.Collections;

public class UsersOpportunityDescription : UsersOpportunities {
    
    private GameObject infoTextButton;

    // Use this for initialization
    void Start () {
        AppController.appController.Load();
        int loadIndex = AppController.appController.getUsersSelectedOpportunityIndex();
        Opportunity opportunity = AppController.appController.getUsersSelectedOpportunities()[loadIndex];
        ApplicationView.applicationView.currentScreenText = truncateTitle(opportunity, numberOfVisibleCharacters, opportunity.Title.Length);
        ApplicationView.applicationView.currentScreenTextSize = 200 - opportunity.Title.Length;

        displayOpportunityDescription(opportunity);

        generateTextOverlay(.5f, "Info");
        infoTextButton = generateMetaDataNavigationButton(.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (infoTextButton.GetComponent<Collider>().Raycast(ray, out hit, 100.0f))
            {
                Application.LoadLevel("UsersOpportunityInformation");
            }
        }
    }
}
