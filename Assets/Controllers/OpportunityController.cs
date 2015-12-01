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
    protected int numberOfVisibleCharacters = 35;
    protected List<GameObject> opportunityGameObjects = new List<GameObject>();

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
			Debug.Log(S.Count);
			temp = mapSkill(S[0]);
			skills.Add(temp);
			S.Remove(0);
		}
		Debug.Log(skills.Count);
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
		opportunity.AcademicStanding = convertToIntArray(node["academic_standing"]);
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
		skill.Dimensions = convertToStringArray(node["opportunities"]);
		
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
			result[i] = node[1];
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

    // Generates an opportunity gameObject based on opporuntity given.
    public GameObject generateOpportunity(int opportunityNumber, Opportunity opportunity, int numberOfVisibleCharacters, float fractionOfScreenPerOpportunity)
    {
        GameObject newOpportunity = new GameObject();

        newOpportunity.AddComponent<TextMesh>();
        TextMesh newText = newOpportunity.GetComponent<TextMesh>();
        int titleLength = opportunity.Title.Length;
        newText.text = titleLength < numberOfVisibleCharacters ? opportunity.Title : opportunity.Title.Substring(0, (titleLength < numberOfVisibleCharacters ? titleLength : numberOfVisibleCharacters)) + "...";
        newText.anchor = TextAnchor.UpperLeft;
        newText.characterSize = .025f;
        newText.fontSize = 200 - titleLength;
        newText.color = Color.red;

        newOpportunity.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f - (ApplicationView.applicationView.getTaskbarFractionOfScreen() / 100f + opportunityNumber * fractionOfScreenPerOpportunity), 10));
        newOpportunity.GetComponent<MeshRenderer>().sortingOrder = 4;

        newOpportunity.AddComponent<BoxCollider>();

        return newOpportunity;
    }

    // Wipe all opportunityGameObjects
    public void wipeAllOpportunityGameObjects()
    {
        foreach (GameObject opportunityObject in opportunityGameObjects)
        {
            Destroy(opportunityObject);
        }
        opportunityGameObjects.Clear();
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
}