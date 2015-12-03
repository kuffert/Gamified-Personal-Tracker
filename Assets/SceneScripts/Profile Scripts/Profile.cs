using UnityEngine;
using System.Collections;

public class Profile : MonoBehaviour {

	// Button Sprites:
	public Sprite IAButtonSprite;
	public Sprite GAButtonSprite;
	public Sprite SCButtonSprite;
	public Sprite PPButtonSprite;
	public Sprite WBButtonSprite;

	// Bubble Sprites:
	public Sprite IABubbleSprite;
	public Sprite GABubbleSprite;
	public Sprite SCBubbleSprite;
	public Sprite PPBubbleSprite;
	public Sprite WBBubbleSprite;

	// UI Dynamic Scaling vars:
	private float orthographicScreenHeight;
	private float orthographicScreenWidth;
		
	// Hashtables for Dimension-Filter Buttons, string/BubbleObject and string/TextObject for XP:
	private Hashtable stringObjectDimensionFiltersHashTable;
	private Hashtable stringObjectXPBubbleHashTable;
	private Hashtable stringObjectXPTextHashTable;

	// Use this for initialization
	void Start () {
		// Initializations:
		AppController.appController.Load ();
		stringObjectDimensionFiltersHashTable = new Hashtable ();
		stringObjectXPBubbleHashTable = new Hashtable ();
		stringObjectXPTextHashTable = new Hashtable ();

		// Determines the screensize we will need to scale the UI elements.
//		Camera camera = Camera.main;
		orthographicScreenHeight = Camera.main.orthographicSize * 2;
		orthographicScreenWidth = orthographicScreenHeight * Screen.width / Screen.height;

		// Generate the Dimension-Filter Toolbar:
		generateDimensionToolbarButton (0.5f, IAButtonSprite, "IA");
		generateDimensionToolbarButton (1.5f, GAButtonSprite, "GA");
		generateDimensionToolbarButton (2.5f, SCButtonSprite, "SC");
		generateDimensionToolbarButton (3.5f, PPButtonSprite, "PP");
		generateDimensionToolbarButton (4.5f, WBButtonSprite, "WB");

		// Generate the XP Bubbles Objects:
		generateXPBubbleObject (IABubbleSprite, "IA");
		generateXPBubbleObject (GABubbleSprite, "GA");
		generateXPBubbleObject (SCBubbleSprite, "SC");
		generateXPBubbleObject (PPBubbleSprite, "PP");
		generateXPBubbleObject (WBBubbleSprite, "WB");

		// Generate the XP Text Objects:
		generateXPTextObject (AppController.appController.getIAExp(), "IA");
		generateXPTextObject (AppController.appController.getGAExp(), "GA");
		generateXPTextObject (AppController.appController.getSCExp(), "SC");
		generateXPTextObject (AppController.appController.getPPExp(), "PP");
		generateXPTextObject (AppController.appController.getWBExp(), "WB");

	}
	
	// Update is called once per frame
	void Update () {
		delegateNavigationFromTouch ();
	}

	// Method to generate a Dimension-Filter Toolbar Button
	private void generateDimensionToolbarButton(float buttonNumber, Sprite sprite, string name) {
		Camera camera = Camera.main;

		// Create the GameObject, name it, and add it to the correct Hashtable:
		GameObject dimensionFilterButton = new GameObject ();
		dimensionFilterButton.name = name + " Dimension-Filter Button";
		stringObjectDimensionFiltersHashTable.Add (name, dimensionFilterButton);

		// Set the Button's location:
		Vector3 buttonLocation = new Vector3 (buttonNumber * 0.2f, 0.15f, 10.0f);
		dimensionFilterButton.transform.position = camera.ViewportToWorldPoint(buttonLocation);
		
		// Add the SpriteRenderer Component:
		dimensionFilterButton.AddComponent<SpriteRenderer> ();
		SpriteRenderer dimensionFilterButtonSpriteRenderer = dimensionFilterButton.GetComponent<SpriteRenderer> ();
		dimensionFilterButtonSpriteRenderer.sprite = sprite;
		dimensionFilterButtonSpriteRenderer.sortingOrder = 3;

		// Dynamically scales the size of the navigation button to fit the screen.
		float dimensionButtonSpriteWidth = calculateSpriteUnitWidth(sprite);
		float dimensionButtonSpriteHeight = calculateSpriteUnitHeight(sprite);
		Vector3 newNavigationButtonScale = new Vector3(orthographicScreenWidth / dimensionButtonSpriteWidth / 5, orthographicScreenHeight / dimensionButtonSpriteHeight / 10);
		dimensionFilterButtonSpriteRenderer.transform.localScale = newNavigationButtonScale; 

		// Add the BoxCollider Component:
		dimensionFilterButton.AddComponent<BoxCollider>();

		Debug.Log (name + "Dimension-Filter Button Created!");
	}

	// Method to generate an XP's Bubble Object:
	private void generateXPBubbleObject(Sprite sprite, string name) {
		Camera camera = Camera.main;

		// Create the GameObject, name it, and add it to the correct Hashtable:
		GameObject XPBubbleObject = new GameObject ();
		XPBubbleObject.name = name + " Bubble Object";
		stringObjectXPBubbleHashTable.Add (name, XPBubbleObject);

		// Set the Bubble's location:
		Vector3 bubbleLocation = new Vector3 (0.5f, .40f, 10);
		XPBubbleObject.transform.position = camera.ViewportToWorldPoint(bubbleLocation);

		// Add the SpriteRenderer Component:
		XPBubbleObject.AddComponent<SpriteRenderer> ();
		SpriteRenderer xpBubbleSpriteRenderer = XPBubbleObject.GetComponent<SpriteRenderer> ();
		xpBubbleSpriteRenderer.sprite = sprite;
		xpBubbleSpriteRenderer.sortingOrder = 3;

		// Dynamically scales the size of the XP Bubble to fit the screen:
		float xpBubbleSpriteWidth = calculateSpriteUnitWidth(sprite);
		float xpBubbleSpriteHeight = calculateSpriteUnitHeight(sprite);
		Vector3 screenXPBubbleScale = new Vector3(orthographicScreenWidth / xpBubbleSpriteWidth, orthographicScreenHeight / xpBubbleSpriteHeight);
		xpBubbleSpriteRenderer.transform.localScale = screenXPBubbleScale;

		// Dynamically sacle the size of XP Bubble by how much XP the user has:
		Vector3 userXPBubbleScale = new Vector3 ();
		switch (name) {
			case "IA":
				userXPBubbleScale = new Vector3(0.1f * AppController.appController.getIAExp(), 0.1f * AppController.appController.getIAExp());
				xpBubbleSpriteRenderer.transform.localScale = userXPBubbleScale;
				break;

			case "GA":
				userXPBubbleScale = new Vector3(0.1f * AppController.appController.getGAExp(), 0.1f * AppController.appController.getGAExp());
				xpBubbleSpriteRenderer.transform.localScale = userXPBubbleScale;	
				break;

			case "SC":
				userXPBubbleScale = new Vector3(0.1f * AppController.appController.getSCExp(), 0.1f * AppController.appController.getSCExp());
				xpBubbleSpriteRenderer.transform.localScale = userXPBubbleScale;	
				break;

			case "PP":
				userXPBubbleScale = new Vector3(0.1f * AppController.appController.getPPExp(), 0.1f * AppController.appController.getPPExp());
				xpBubbleSpriteRenderer.transform.localScale = userXPBubbleScale;	
				break;

			case "WB":
				userXPBubbleScale = new Vector3(0.1f * AppController.appController.getWBExp(), 0.1f * AppController.appController.getWBExp());
				xpBubbleSpriteRenderer.transform.localScale = userXPBubbleScale;	
				break;
		}

		// Set the Bubble Object to be Disabled:
		XPBubbleObject.SetActive (false);
	}

	// Method to generate an XP's Text Object:
	private void generateXPTextObject(int xp, string name) {
		Camera camera = Camera.main;

		// Create the GameObject, name it, and add it to the correct Hashtable:
		GameObject XPTextObject = new GameObject ();
		XPTextObject.name = name + " Text Object";
		stringObjectXPTextHashTable.Add (name, XPTextObject);
		
		// Set the Bubble's location:
		Vector3 textLocation = new Vector3 (0.5f, .40f, 10);
		XPTextObject.transform.position = camera.ViewportToWorldPoint(textLocation);

		// Add the TextMesh Component:
		XPTextObject.AddComponent<TextMesh> ();
		TextMesh XPTextObjectTextMesh = XPTextObject.GetComponent<TextMesh> ();
		XPTextObjectTextMesh.text = xp.ToString ();
		XPTextObjectTextMesh.anchor = TextAnchor.MiddleCenter;
		XPTextObjectTextMesh.characterSize = .025f;
		XPTextObjectTextMesh.fontSize = 400;
		XPTextObject.GetComponent<MeshRenderer> ().sortingOrder = 3;

		Debug.Log (name + "Text Object Created!");

		// Set the Bubble Object to be Disabled:
		XPTextObject.SetActive (false);
	}

	// Hiders:
	private void hideAllXPBubbles() {
		foreach (GameObject obj in stringObjectXPBubbleHashTable.Values) {
			obj.SetActive (false);
		}
	}

	private void hideAllXPTexts() {
		foreach (GameObject obj in stringObjectXPTextHashTable.Values) {
			obj.SetActive (false);
		}
	}

	// Showers:
	private void showXPBubble(string key) {
		GameObject temp = (GameObject)stringObjectXPBubbleHashTable [key];
		temp.SetActive (true);
	}

	private void showXPText(string key) {
		GameObject temp = (GameObject)stringObjectXPTextHashTable [key];
		temp.SetActive (true);
	}

	// Helper for dynamically scaling the buttons (x)
	// This calculates the Sprites "Unit" width, which is the the unit of measurement in Unity.
	private float calculateSpriteUnitWidth(Sprite sprite)
	{
		return sprite.textureRect.width / sprite.pixelsPerUnit;
	}
	
	// Helper for dynamically scaling the buttons (y)
	// This calculates the Sprites "Unit" height, which is the the unit of measurement in Unity.
	private float calculateSpriteUnitHeight(Sprite sprite)
	{
		return sprite.textureRect.height / sprite.pixelsPerUnit;
	}

	// Handles touching of each button:
	private void delegateNavigationFromTouch()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			GameObject tempIA = (GameObject)stringObjectDimensionFiltersHashTable["IA"];
			if (tempIA.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
			{
				// Hide all, show the correct XP Bubble:
				hideAllXPBubbles();
				showXPBubble("IA");
				// Hide all, show the correct XP Text:
				hideAllXPTexts();
				showXPText("IA");
			}

			GameObject tempGA = (GameObject)stringObjectDimensionFiltersHashTable["GA"];
			if (tempGA.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
			{
				// Hide all, show the correct XP Bubble:
				hideAllXPBubbles();
				showXPBubble("GA");
				// Hide all, show the correct XP Text:
				hideAllXPTexts();
				showXPText("GA");
			}

			GameObject tempSC = (GameObject)stringObjectDimensionFiltersHashTable["SC"];
			if (tempSC.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
			{
				// Hide all, show the correct XP Bubble:
				hideAllXPBubbles();
				showXPBubble("SC");
				// Hide all, show the correct XP Text:
				hideAllXPTexts();
				showXPText("SC");
			}

			GameObject tempPP = (GameObject)stringObjectDimensionFiltersHashTable["PP"];
			if (tempPP.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
			{
				// Hide all, show the correct XP Bubble:
				hideAllXPBubbles();
				showXPBubble("PP");
				// Hide all, show the correct XP Text:
				hideAllXPTexts();
				showXPText("PP");
			}

			GameObject tempWB = (GameObject)stringObjectDimensionFiltersHashTable["WB"];
			if (tempWB.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
			{
				// Hide all, show the correct XP Bubble:
				hideAllXPBubbles();
				showXPBubble("WB");
				// Hide all, show the correct XP Text:
				hideAllXPTexts();
				showXPText("WB");
			}
		}
	}
}
