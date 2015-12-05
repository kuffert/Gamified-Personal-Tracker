using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;
using System.Linq;

// Author: Yvette Kim
// Date: 11/11/2015
public class OpportunityController: MonoBehaviour {
    
    private const string baseUrl = "https://api.mongolab.com/api/1";
	private const string database = "softdevfall15";
	private const string opportunityCollection = "opportunities";
	private const string skillCollection = "skills";
	private const string urlKeyEnd = "?apiKey=ZMZOg1DKKoow4p8XCzVGfX-k8P6szwZj";
	private const string opportunityUrl = baseUrl + "/databases/" + database + "/collections/" + opportunityCollection + urlKeyEnd;
	private const string skillUrl = baseUrl + "/databases/" + database + "/collections/" + skillCollection + urlKeyEnd;
    
    public int numberOfDisplayedOpportunities;
    public Sprite buttonSprite;
    protected int numberOfVisibleCharacters = 33;
    protected List<GameObject> opportunityButtonObjects = new List<GameObject>();
    protected List<GameObject> opportunityTextObjects = new List<GameObject>();

    private float oppMetaDataYLoc = .9f;
    private float gapBetweenMetaData = .04f;
    private int maxCharactersPerLine = 41;

    // Grab Opportunities from DB, return list of fully populated Opportunity objects
    // Should only be used once, on log in
    public static List<Opportunity> getOpportunities()
	{
		List<Opportunity> opportunities = new List<Opportunity>();
        List<Skill> allSkills = getSkills();

		// Grab data
		WWW www = new WWW(opportunityUrl);
		while (!www.isDone) {/*do nothing, does not work otherwise*/}
		var N = JSON.Parse(www.text);
		
		// Fill Opportunities
		Opportunity temp;
		while(N.Count > 0)
		{
			temp = mapOpportunity(N[0], allSkills);
			opportunities.Add(temp);
			N.Remove(0);
		}
		return opportunities;
	}

    // Grab Skills from DB, return list of Skill objects
    // Already used in pulling opportunities
	public static List<Skill> getSkills()
	{
		List<Skill> skills = new List<Skill>();
		
		// Grab data
		WWW wwwSkill = new WWW(skillUrl);
		while (!wwwSkill.isDone) {/*do nothing, does not work otherwise*/}
		var S = JSON.Parse(wwwSkill.text);
		
		// Fill Skills
		Skill temp = new Skill();
		while(S.Count > 0)
		{
			temp = mapSkill(S[0]);
			skills.Add(temp);
			S.Remove(0);
		}
		return skills;
	}

    // Map JSONNode to Opportunity objects
    private static Opportunity mapOpportunity(JSONNode node, List<Skill> skills)
	{
		Opportunity opportunity = new Opportunity();

		opportunity.Id = node["_id"]["$oid"];
		opportunity.Title = node["title"];
		opportunity.Format = node["format"];
		opportunity.TopicsOfInterest = convertToStringArray(node["topicsOfInterest"]);
		opportunity.Description = node["description"];
		opportunity.StartDate = node["begin_date"];
		opportunity.EndDate = node["end_date"];
		opportunity.JoinAnytime = node["joinAnytime"].AsBool;
		opportunity.LengthOfEngagement = node["length"].AsInt;
		opportunity.Location = node["location"];
		opportunity.Recurrence = convertToStringArray(node["recurrence"]);
		opportunity.Coop = node["co-op"].AsBool;
		opportunity.AcademicStanding = convertToStringArray(node["academic_standing"]);
		opportunity.Major = node["major"];
		opportunity.Resident = node["resident"].AsBool;
		opportunity.Sponsor = node["sponsor"];
		opportunity.ContactName = node["contact_name"];
		opportunity.ContactOffice = node["contact_office"];
		opportunity.ContactEmail = node["contact_email"];
		opportunity.LearningOutcomes = convertToStringArray(node["learning_outcomes"]);
		opportunity.Skills = convertToSkills(convertToStringArray(node["skills"]), skills);
		opportunity.Engagement = node["engagement"];

		return opportunity;
	}

    // Map JSONNode to Skill objects
	private static Skill mapSkill(JSONNode node)
	{
		Skill skill = new Skill();
		
		skill.Id = node["_id"]["$oid"];
		skill.SkillName = node["skill"];
		skill.Dimensions = convertToStringArray(node["dimensions"]);
		
		return skill;
	}

    // convert JSONNode into int[]
	private static int[] convertToIntArray(JSONNode node)
	{
		int[] result = new int[node.Count];

		for (int i = 0; i < node.Count; i++) 
		{
			result[i] = node[i].AsInt;
		}
		return result;
	}

    // convert JSONNode into string[]
    private static string[] convertToStringArray(JSONNode node)
	{
		string[] result = new string[node.Count];
		
		for (int i = 0; i < node.Count; i++) 
		{
			result[i] = node[i];
		}
		return result;
	}

    // Convert strings into Skills
	private static List<Skill> convertToSkills(string[] ids, List<Skill> skills)
	{
        List<Skill> result = new List<Skill>();
		
		for (int p = 0; p < ids.Length; p++) 
		{
            result.Add(skills.First(item => item.Id == ids[p]));
   		}
		return result;
	}

    // Generates an a text object and an interactable button to allow navigation when an opportunity is selected.
    public void generateOpportunity(int opportunityNumber, Opportunity opportunity, int numberOfVisibleCharacters, float fractionOfScreenPerOpportunity)
    {
        float newOpportunitySpriteWidth = ApplicationView.calculateSpriteUnitWidth(buttonSprite);
        float newOpportunitySpriteHeight = ApplicationView.calculateSpriteUnitHeight(buttonSprite);
        float opportunityPositionY = 1f - (ApplicationView.applicationView.getTaskbarFractionOfScreen() / 100f + opportunityNumber * fractionOfScreenPerOpportunity) - .043f;

        GameObject newOpportunityText = new GameObject();
        newOpportunityText.AddComponent<TextMesh>();
        TextMesh newText = newOpportunityText.GetComponent<TextMesh>();
        int titleLength = opportunity.Title.Length;
        newText.text = truncateTitle(opportunity, numberOfVisibleCharacters, titleLength);
        newText.anchor = TextAnchor.MiddleLeft;
        newText.characterSize = .025f;
        newText.fontSize = 200 - titleLength;
        newText.color = Color.red;
        newOpportunityText.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0f, opportunityPositionY, 10f));
        newOpportunityText.GetComponent<MeshRenderer>().sortingOrder = 5;
        opportunityTextObjects.Add(newOpportunityText);

        GameObject newOpportunityButton = new GameObject();
        newOpportunityButton.AddComponent<SpriteRenderer>().sprite = buttonSprite;
        newOpportunityButton.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(.5f, opportunityPositionY, 10f));
        newOpportunityButton.GetComponent<SpriteRenderer>().sortingOrder = 4;
        
        newOpportunityButton.GetComponent<SpriteRenderer>().transform.localScale = new Vector3(ApplicationView.orthographicScreenWidth / newOpportunitySpriteWidth, (ApplicationView.orthographicScreenHeight - newOpportunitySpriteHeight*2) / newOpportunitySpriteHeight / ApplicationView.applicationView.taskbarFractionOfScreen);
        newOpportunityButton.AddComponent<BoxCollider>();
        opportunityButtonObjects.Add(newOpportunityButton);
    }

    // Wipe all opportunityGameObjects
    public void wipeAllOpportunityGameObjects()
    {
        foreach (GameObject opportunityButton in opportunityButtonObjects)
        {
            Destroy(opportunityButton);
        }
        opportunityButtonObjects.Clear();

        foreach(GameObject opportunityText in opportunityTextObjects)
        {
            Destroy(opportunityText);
        }
        opportunityTextObjects.Clear();
    }

    // Checks if a list of opportunities contains an opportunity with the given id
    public static bool containsOpportunityId(List<Opportunity> oppList, string id)
    {
        foreach (Opportunity opp in oppList)
        {
            if (opp.Id.Equals(id))
            {
                return true;
            }
        }
        return false;
    }

    // Truncated a title down to size to fit on the screen. 
    protected string truncateTitle (Opportunity opportunity, int numberOfVisibleCharacters, int titleLength)
    {
        return titleLength < numberOfVisibleCharacters ? opportunity.Title : opportunity.Title.Substring(0, (titleLength < numberOfVisibleCharacters ? titleLength : numberOfVisibleCharacters)) + "...";
    }

    // Shows all of the Opportunity's metadata when it is selected
    protected void displayOpportunityMetadata(Opportunity opportunity)
    {
        generateMetaDataText("TITLE: " + opportunity.Title);
        
        generateMetaDataText("DATE RANGE: " + opportunity.StartDate + " - " + opportunity.EndDate);

        generateMetaDataText("LOCATION: " + opportunity.Location);

        generateMetaDataText("CONTACT: " + opportunity.ContactName);

        generateMetaDataText("EMAIL: " + opportunity.ContactEmail);

        generateMetaDataText("OFFICE: " + opportunity.ContactOffice);

        string skillsText = "SKILLS: ";
        foreach(Skill skill in opportunity.Skills)
        {
            skillsText += skill.SkillName + ", ";
        }
        skillsText = skillsText.Substring(0, skillsText.Length - 2);

        //This is odd. Some opportunities have like 7 of the same skill?
        generateMetaDataText(skillsText); 
    }

    // Shows the opportunity's description
    protected void displayOpportunityDescription(Opportunity opportunity)
    {
        generateMetaDataText("DESCRIPTION: " + opportunity.Description);
    }

    // Generates the gameobject that will show a particular piece of opportunity metadata
    private GameObject generateMetaDataText(string metaDataText)
    {
        GameObject newMetaDataObject = new GameObject();
        newMetaDataObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0f, oppMetaDataYLoc, 10f));
        newMetaDataObject.AddComponent<TextMesh>();
        TextMesh metaDataTextMesh = newMetaDataObject.GetComponent<TextMesh>();
        metaDataTextMesh.anchor = TextAnchor.UpperLeft;
        metaDataTextMesh.fontSize = 100;
        metaDataTextMesh.characterSize = .025f;
        metaDataTextMesh.text = fitTextToScreenWidth(metaDataText);

        newMetaDataObject.GetComponent<MeshRenderer>().sortingOrder = 4;
        
        return newMetaDataObject;
    }

    // Adds new line characters at predetermined intervals so the text will fit the screen
    private string fitTextToScreenWidth (string text)
    {
        string outcomeString = "";
        oppMetaDataYLoc -= gapBetweenMetaData;
        if (text.Length < maxCharactersPerLine)
        {
            return text;
        }
        else
        {
            string modifiedText = text;
            while (modifiedText.Length > maxCharactersPerLine)
            {
                int indexOfLastSpace = findIndexOfLastSpace(modifiedText);
                outcomeString += modifiedText.Substring(0, indexOfLastSpace) + "\n";
                modifiedText = modifiedText.Substring(indexOfLastSpace, modifiedText.Length - indexOfLastSpace);
                oppMetaDataYLoc -= gapBetweenMetaData -.01f;
            }
            outcomeString += modifiedText;
            return outcomeString;
        }
    }

    // Returns the index of the last space in a string
    private int findIndexOfLastSpace(string text)
    {
        int length = text.Length >= maxCharactersPerLine ? maxCharactersPerLine : text.Length;

        for (int i = length; i > 0;  i--)
        {
            if (text[i].Equals(' '))
            {
                return i;
            }
        }
        return 0;
    }

    // Generates a text object to overlay a button on the opportunity information and description scenes
    protected GameObject generateTextOverlay(float xLoc, string text)
    {
        GameObject textOverlay = new GameObject();
        textOverlay.AddComponent<TextMesh>();
        TextMesh acceptTextMesh = textOverlay.GetComponent<TextMesh>();
        acceptTextMesh.characterSize = .025f;
        acceptTextMesh.fontSize = 150;
        acceptTextMesh.text = text;
        acceptTextMesh.color = Color.black;
        acceptTextMesh.anchor = TextAnchor.MiddleCenter;
        textOverlay.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(xLoc, .15f, 10f));
        textOverlay.GetComponent<MeshRenderer>().sortingOrder = 5;

        return textOverlay;
    }

    // Generates a button object to allow navigation to opportunity metadata
    protected GameObject generateMetaDataNavigationButton(float xLoc, int buttonsOnScreen)
    {
        GameObject metaDataNavigationButton = new GameObject();
        metaDataNavigationButton.AddComponent<SpriteRenderer>().sprite = buttonSprite;
        metaDataNavigationButton.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(xLoc, .15f, 10f));
        metaDataNavigationButton.GetComponent<SpriteRenderer>().sortingOrder = 4;
        float completeTextButtonWidth = ApplicationView.calculateSpriteUnitWidth(buttonSprite);
        metaDataNavigationButton.GetComponent<SpriteRenderer>().transform.localScale = new Vector3(ApplicationView.orthographicScreenWidth / completeTextButtonWidth / buttonsOnScreen, 1f);
        metaDataNavigationButton.AddComponent<BoxCollider>();

        return metaDataNavigationButton;
    }
}