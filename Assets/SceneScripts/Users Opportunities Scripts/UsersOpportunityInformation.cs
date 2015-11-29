using UnityEngine;
using System.Collections.Generic;

// Author: Chris Kuffert
// Date: 11/22/15
public class UsersOpportunityInformation : UsersOpportunities {

    GameObject completeText;
    GameObject removeText;
    Opportunity opportunity;
    
	void Start () {
        AppController.appController.Load();
        int loadIndex = AppController.appController.getUsersSelectedOpportunityIndex();
        opportunity = AppController.appController.getUsersSelectedOpportunities()[loadIndex]; 
        ApplicationView.applicationView.currentScreenText = opportunity.Title;

        completeText = new GameObject();
        completeText.AddComponent<TextMesh>();
        TextMesh completeTextMesh = completeText.GetComponent<TextMesh>();
        completeTextMesh.characterSize = .025f;
        completeTextMesh.fontSize = 200;
        completeTextMesh.text = "Complete";
        completeTextMesh.anchor = TextAnchor.MiddleCenter;
        completeText.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(.25f, .15f, 10f));
        completeText.GetComponent<MeshRenderer>().sortingOrder = 4;
        completeText.AddComponent<BoxCollider>();

        removeText = new GameObject();
        removeText.AddComponent<TextMesh>();
        TextMesh removeTextMesh = removeText.GetComponent<TextMesh>();
        removeTextMesh.characterSize = .025f;
        removeTextMesh.fontSize = 200;
        removeTextMesh.text = "Remove";
        removeTextMesh.anchor = TextAnchor.MiddleCenter;
        removeText.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(.75f, .15f, 10f));
        removeText.GetComponent<MeshRenderer>().sortingOrder = 4;
        removeText.AddComponent<BoxCollider>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (completeText.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
            {
                // @Yvette or @Craig, add the level up code here!

                List<Opportunity> completedOppList = AppController.appController.getUsersCompletedOpportunities();
                completedOppList.Add(opportunity);
                AppController.appController.setUsersCompletedOpportunities(completedOppList);
                List<Opportunity> selectedOppList = AppController.appController.getUsersSelectedOpportunities();
                selectedOppList.Remove(opportunity);
                AppController.appController.setUsersSelectedOpportunities(selectedOppList);
                AppController.appController.Save();
                Application.LoadLevel("Profile");
            }

            if (removeText.GetComponent<Collider>().Raycast(ray, out hit, 100.0f))
            {
                List<Opportunity> oppList = AppController.appController.getUsersSelectedOpportunities();
                oppList.RemoveAt(AppController.appController.getUsersSelectedOpportunityIndex());
                AppController.appController.setUsersSelectedOpportunities(oppList);
                AppController.appController.Save();
                Application.LoadLevel("UsersOpportunities");
            }
        }
    }
}
