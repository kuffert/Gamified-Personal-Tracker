using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;

// Author: Yvette Kim
// Date: 11/11/2015
public class OpportunityController: MonoBehaviour {
	
	public static OpportunityController opportunityController;
	
	private const string baseUrl = "https://api.mongolab.com/api/1";
	private const string database = "softdevfall15";
	private const string activitiesCollection = "opportunities";
	private const string urlKeyEnd = "?apiKey=ZMZOg1DKKoow4p8XCzVGfX-k8P6szwZj";
	private const string url = baseUrl + "/databases/" + database + "/collections/" + activitiesCollection + urlKeyEnd;
	
	public static List<Opportunity> getActivities()
	{
		List<Opportunity> activities = new List<Opportunity>();
		
		// Grab data
		WWW www = new WWW(url);
		while (!www.isDone) {/*do nothing, does not work otherwise*/}
		var N = JSON.Parse(www.text);
		
		// Fill activities
		Opportunity temp;
		while(N.Count > 0)
		{
			Debug.Log(N.Count);
			temp = mapOpportunity(N[0]);
			activities.Add(temp);
			N.Remove(0);
		}
		Debug.Log(activities.Count);
		return activities;
	}
	
	private static Opportunity mapOpportunity(JSONNode node)
	{
		Opportunity opportunity = new Opportunity();

		opportunity.Id = node["_id"]["$oid"];
		opportunity.Title = node["title"];
		opportunity.Format = node["format"];
		//opportunity.TopicsOfInterest = convertToStringArray(node["topicsOfInterest"]);
		opportunity.Description = node["description"];
		opportunity.StartDate = node["startDate"];
		opportunity.EndDate = node["endDate"];
		opportunity.JoinAnytime = node["joinAnytime"].AsBool;
		opportunity.LengthOfEngagement = node["lengthOfEngagement"].AsInt;
		opportunity.Location = node["location"];
		opportunity.Recurrence = convertToStringArray(node["recurrence"]);
		opportunity.Coop = node["coop"].AsBool;
		opportunity.AcademicStanding = convertToIntArray(node["academicStanding"]);
		opportunity.Major = node["major"];
		opportunity.Resident = node["resident"].AsBool;
		opportunity.Sponsor = node["sponsor"];
		opportunity.ContactName = node["contactName"];
		opportunity.ContactOffice = node["contactOffice"];
		opportunity.ContactEmail = node["contactEmail"];
		opportunity.LearningOutcomes = convertToStringArray(node["learningOutcomes"]);
		//opportunity.Skills = node["skills"]; needs to pull and convert Skills, then convert this JSONNode to List<Skills>
		opportunity.Engagement = node["engagement"];

		return opportunity;
	}

	private static int[] convertToIntArray(JSONNode node)
	{
		int[] result = new int[node.Count];

		for (int i = 0; i < node.Count; i++) 
		{
			result[i] = node[i].AsInt;
		}

		return result;
	}

	private static string[] convertToStringArray(JSONNode node)
	{
		string[] result = new string[node.Count];
		
		for (int i = 0; i < node.Count; i++) 
		{
			result[i] = node[i];
		}
		
		return result;
	}
}