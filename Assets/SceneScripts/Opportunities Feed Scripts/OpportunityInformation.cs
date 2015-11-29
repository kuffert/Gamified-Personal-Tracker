using UnityEngine;
using System.Collections.Generic;

// Author: Chris Kuffert
// Date: 11/22/15
public class OpportunityInformation : OpportunityFeed {

    GameObject acceptText;
    Opportunity opportunity;

	// Use this for initialization
	void Start () {
        AppController.appController.Load();
        int loadIndex = AppController.appController.getOpportunityFeedIndex();
        opportunity = AppController.appController.getAllOpportunities()[loadIndex];
        ApplicationView.applicationView.currentScreenText = opportunity.Title;

        acceptText = new GameObject();
        acceptText.AddComponent<TextMesh>();
        TextMesh acceptTextMesh = acceptText.GetComponent<TextMesh>();
        acceptTextMesh.characterSize = .025f;
        acceptTextMesh.fontSize = 200;
        acceptTextMesh.text = "Accept";
        acceptTextMesh.anchor = TextAnchor.MiddleCenter;
        acceptText.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(.5f, .15f, 10f));
        acceptText.GetComponent<MeshRenderer>().sortingOrder = 4;
        acceptText.AddComponent<BoxCollider>();

        if (containsOpportunityId(AppController.appController.getUsersSelectedOpportunities(), opportunity.Id))
        {
            acceptTextMesh.text = "Accepted";
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && !containsOpportunityId(AppController.appController.getUsersSelectedOpportunities(), opportunity.Id))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (acceptText.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
            {
                List<Opportunity> allOppList = AppController.appController.getUsersSelectedOpportunities();
                allOppList.Add(opportunity);
                AppController.appController.setUsersSelectedOpportunities(allOppList);
                AppController.appController.Save();
                Application.LoadLevel("UsersOpportunities");
            }
        }
    }
}
