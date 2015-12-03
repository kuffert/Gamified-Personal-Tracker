using UnityEngine;
using System.Collections.Generic;

// Author: Chris Kuffert
// Date: 11/22/15
public class OpportunityInformation : OpportunityFeed {

    public Sprite buttonSprite;
    private GameObject acceptText;
    private Opportunity opportunity;
    private GameObject detailsText;
    
    // Use this for initialization
    void Start () {
        AppController.appController.Load();
        int loadIndex = AppController.appController.getOpportunityFeedIndex();
        opportunity = AppController.appController.getAllOpportunities()[loadIndex];
        ApplicationView.applicationView.currentScreenText = opportunity.Title;

        displayOpportunityMetadata(opportunity);

        acceptText = new GameObject();
        acceptText.AddComponent<TextMesh>();
        TextMesh acceptTextMesh = acceptText.GetComponent<TextMesh>();
        acceptTextMesh.characterSize = .025f;
        acceptTextMesh.fontSize = 150;
        acceptTextMesh.text = "Accept";
        acceptTextMesh.anchor = TextAnchor.MiddleCenter;
        acceptText.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(.25f, .15f, 10f));
        acceptText.GetComponent<MeshRenderer>().sortingOrder = 4;
        acceptText.AddComponent<BoxCollider>();

        detailsText = new GameObject();
        detailsText.AddComponent<TextMesh>();
        TextMesh descriptionTextMesh = detailsText.GetComponent<TextMesh>();
        descriptionTextMesh.characterSize = .025f;
        descriptionTextMesh.fontSize = 150;
        descriptionTextMesh.text = "Details";
        descriptionTextMesh.anchor = TextAnchor.MiddleCenter;
        descriptionTextMesh.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(.75f, .15f, 10f));
        detailsText.GetComponent<MeshRenderer>().sortingOrder = 4;
        detailsText.AddComponent<BoxCollider>();

        if (containsOpportunityId(AppController.appController.getUsersSelectedOpportunities(), opportunity.Id))
        {
            acceptTextMesh.text = "Accepted";
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (acceptText.GetComponent<Collider>().Raycast(ray, out hit, 100.0F) && !containsOpportunityId(AppController.appController.getUsersSelectedOpportunities(), opportunity.Id))
            {
                List<Opportunity> allOppList = AppController.appController.getUsersSelectedOpportunities();
                allOppList.Add(opportunity);
                AppController.appController.setUsersSelectedOpportunities(allOppList);
                AppController.appController.Save();
                Application.LoadLevel("UsersOpportunities");
            }

            if (detailsText.GetComponent<Collider>().Raycast(ray, out hit, 100.0f))
            {
                Application.LoadLevel("OpportunityDescription");
            }
        }
    }
}
