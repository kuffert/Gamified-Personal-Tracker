using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;

// Author: Craig Hammond
// Date: 11/11/2015
public class OpportunityFeed : MonoBehaviour {
	
	public Font font;
	public int amountOfDisplayedOpportunties;
	private float fractionOfScreenPerOpportunity;
	private int numberOfVisibleCharacters = 35;
	private Color NURed = new Color(204.0f, 0.0f, 0.0f);
	
	// Use this for initialization
	void Start () {
		saveUserTestData();
		AppController.appController.Load();
		List<Opportunity> usersActivities = AppController.appController.getCurrentOpportunities();
		
		fractionOfScreenPerOpportunity = (1 - (2 * StaticFeatures.staticFeatures.getTaskbarFractionOfScreen() / 100f)) / amountOfDisplayedOpportunties;
		
		for (int i = 0; i < usersActivities.Count; i++)
		{
			generateOpportunity(i, usersActivities[i]);
		}
	}
	
	void Update () {
		// Eventually will need delegate navigation based on touch.
	}
	
	private GameObject generateOpportunity(int opportunityNumber, Opportunity opportunity) 
	{
		GameObject newOpportunity = new GameObject();
		
		newOpportunity.AddComponent<TextMesh>();
		TextMesh newText = newOpportunity.GetComponent<TextMesh>();
		int titleLength = opportunity.Title.Length;
		Debug.Log(titleLength);
		newText.text = titleLength < numberOfVisibleCharacters? opportunity.Title : opportunity.Title.Substring(0, (titleLength < numberOfVisibleCharacters ? titleLength : numberOfVisibleCharacters)) + "...";
		newText.anchor = TextAnchor.UpperLeft;
		newText.characterSize = .025f;
		newText.fontSize = 200 - titleLength;
		newText.color = NURed;
		//newText.font = font; This is broken at the moment, its causing a ton of problems
		
		newOpportunity.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f - (StaticFeatures.staticFeatures.getTaskbarFractionOfScreen() / 100f + opportunityNumber * fractionOfScreenPerOpportunity), 10));
		newOpportunity.GetComponent<MeshRenderer>().sortingOrder = 4;
		
		newOpportunity.AddComponent<BoxCollider>();
		newOpportunity.GetComponent<BoxCollider>().size = new Vector3(StaticFeatures.staticFeatures.getOrthographicScreenWidth(), StaticFeatures.staticFeatures.getOrthographicScreenHeight() * fractionOfScreenPerOpportunity, 0);
		newOpportunity.GetComponent<BoxCollider>().center = new Vector3(StaticFeatures.staticFeatures.getOrthographicScreenWidth() * .5f, newOpportunity.GetComponent<BoxCollider>().size.y /-2f, 0f);
		return newOpportunity;
	}
	
	// Saves some test data for the user's activities
	private void saveUserTestData()
	{
		List<Opportunity> testActivities = OpportunityController.getActivities();
		
		AppController.appController.setCurrentOpportunities(testActivities);
		AppController.appController.Save();
	}
}
