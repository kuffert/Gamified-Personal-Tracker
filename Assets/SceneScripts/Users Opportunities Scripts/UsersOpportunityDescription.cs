using UnityEngine;
using System.Collections;

public class UsersOpportunityDescription : UsersOpportunities {
    
    public Sprite buttonSprite;
    private GameObject infoText;

    // Use this for initialization
    void Start () {
        AppController.appController.Load();
        int loadIndex = AppController.appController.getUsersSelectedOpportunityIndex();
        Opportunity opportunity = AppController.appController.getUsersSelectedOpportunities()[loadIndex];
        ApplicationView.applicationView.getSceneText().GetComponent<TextMesh>().text = opportunity.Title;

        displayOpportunityDescription(opportunity);

        infoText = new GameObject();
        infoText.AddComponent<TextMesh>();
        TextMesh infoTextMesh = infoText.GetComponent<TextMesh>();
        infoTextMesh.characterSize = .025f;
        infoTextMesh.fontSize = 150;
        infoTextMesh.text = "Info";
        infoTextMesh.anchor = TextAnchor.MiddleCenter;
        infoTextMesh.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(.5f, .15f, 10f));
        infoText.GetComponent<MeshRenderer>().sortingOrder = 4;
        infoText.AddComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (infoText.GetComponent<Collider>().Raycast(ray, out hit, 100.0f))
            {
                Application.LoadLevel("UsersOpportunityInformation");
            }
        }
    }
}
