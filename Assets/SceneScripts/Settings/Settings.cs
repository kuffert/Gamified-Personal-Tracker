using UnityEngine;
using System.Collections;

// Author: Jack Kelly
public class Settings : MonoBehaviour {
	
	// Label GUIStyle:
	private GUIStyle labelStyleText = new GUIStyle ();
    private GUIStyle labelStyleInput = new GUIStyle();
	
	// TextField Strings:
	private string userNameString = string.Empty;
	private string userMajorString = string.Empty;

    // Text Strings:
    private string enterUserNameString = string.Empty;
    private string enterMajorString = string.Empty;
	
	// Button Sprites:
	public Sprite userNameUpdateSprite;
	public Sprite userMajorUpdateSprite;
	public Sprite userLogOutSprite;
	
	// Objects to update Uses's Name, Major, and Year:
	private GameObject userNameUpdateObject;
	private GameObject userMajorUpdateObject;
	private GameObject userLogOutObject;
	
	// UI Dynamic Scaling vars:
	private float orthographicScreenHeight;
	private float orthographicScreenWidth;
	
	// Use this for initialization
	void Start () {
		AppController.appController.Load ();
		userNameString = AppController.appController.getUsername () == null? "" : AppController.appController.getUsername();
		userMajorString = AppController.appController.getMajor () == null? "" : AppController.appController.getMajor();
        enterUserNameString = "Update Username";
        enterMajorString = "Update Major";
        orthographicScreenHeight = Camera.main.orthographicSize * 2;
		orthographicScreenWidth = orthographicScreenHeight * Screen.width / Screen.height;
		
		labelStyleText.alignment = TextAnchor.MiddleLeft;
		labelStyleText.normal.textColor = Color.white;
		labelStyleText.fontSize = Screen.width / 15;

        labelStyleInput.alignment = TextAnchor.MiddleLeft;
		labelStyleInput.normal.textColor = Color.red;
        labelStyleInput.fontSize = Screen.width / 18;
		
		// Generate Objects to update Uses's Name, Major, and to Logout:
		generateSettingsObjects (userNameUpdateSprite, userMajorUpdateSprite, userLogOutSprite);
	}
	
	// Update is called once per frame
	void Update () {
		delegateNavigationFromTouch ();
	}
	
//	// Create Buttons for the User to update their data with:
	private void generateSettingsObjects (Sprite userNameSprite, Sprite userMajorSprite, Sprite logOutSprite) {
		Camera camera = Camera.main;
		
		// Generate the User's Update Name Button:
		userNameUpdateObject = new GameObject ();
		userNameUpdateObject.name = "Update UserName Button";
		Vector3 updateNameLoc = new Vector3 (0.8f, .80f, 10);
		userNameUpdateObject.transform.position = camera.ViewportToWorldPoint(updateNameLoc);
		userNameUpdateObject.AddComponent<SpriteRenderer> ();
		SpriteRenderer userNameUpdateSpriteRenderer = userNameUpdateObject.GetComponent<SpriteRenderer> ();
		userNameUpdateSpriteRenderer.sprite = userNameSprite;
		userNameUpdateSpriteRenderer.sortingOrder = 3;
		userNameUpdateObject.AddComponent<BoxCollider> ();
		float updateNameButtonSpriteWidth = calculateSpriteUnitWidth(userNameSprite);
		float updateNameButtonSpriteHeight = calculateSpriteUnitHeight(userNameSprite);
		Vector3 screenUpdateUserNameScale = new Vector3(orthographicScreenWidth / updateNameButtonSpriteWidth / 3.25f, orthographicScreenHeight / updateNameButtonSpriteHeight / 10);
		userNameUpdateSpriteRenderer.transform.localScale = screenUpdateUserNameScale;
		
		// Generate the User's Update Major Button:
		userMajorUpdateObject = new GameObject ();
		userMajorUpdateObject.name = "Update UserMajor Button";
		Vector3 updateMajorLoc = new Vector3 (0.8f, .6f, 10);
		userMajorUpdateObject.transform.position = camera.ViewportToWorldPoint(updateMajorLoc);
		userMajorUpdateObject.AddComponent<SpriteRenderer> ();
		SpriteRenderer userMajorUpdateSpriteRenderer = userMajorUpdateObject.GetComponent<SpriteRenderer> ();
		userMajorUpdateSpriteRenderer.sprite = userMajorSprite;
		userMajorUpdateSpriteRenderer.sortingOrder = 3;
		userMajorUpdateObject.AddComponent<BoxCollider> ();
		float updateMajorButtonSpriteWidth = calculateSpriteUnitWidth(userMajorSprite);
		float updateMajorButtonSpriteHeight = calculateSpriteUnitHeight(userMajorSprite);
		Vector3 screenUpateUserMajorScale = new Vector3(orthographicScreenWidth / updateMajorButtonSpriteWidth / 3.25f, orthographicScreenHeight / updateMajorButtonSpriteHeight / 10);
		userMajorUpdateSpriteRenderer.transform.localScale = screenUpateUserMajorScale;
		
		// Generate the User's Update Major Button:
		userLogOutObject = new GameObject ();
		userLogOutObject.name = "Update UserMajor Button";
		Vector3 logOutLoc = new Vector3 (0.5f, 0.25f, 10);
		userLogOutObject.transform.position = camera.ViewportToWorldPoint(logOutLoc);
		userLogOutObject.AddComponent<SpriteRenderer> ();
		SpriteRenderer logOutSpriteRenderer = userLogOutObject.GetComponent<SpriteRenderer> ();
		logOutSpriteRenderer.sprite = logOutSprite;
		logOutSpriteRenderer.sortingOrder = 3;
		userLogOutObject.AddComponent<BoxCollider> ();
		float logOutrButtonSpriteWidth = calculateSpriteUnitWidth(logOutSprite);
		float logOutButtonSpriteHeight = calculateSpriteUnitHeight(logOutSprite);
		Vector3 screenLogOutButtonScale = new Vector3(orthographicScreenWidth / logOutrButtonSpriteWidth / 2.6f, orthographicScreenHeight / logOutButtonSpriteHeight / 10);
		logOutSpriteRenderer.transform.localScale = screenLogOutButtonScale;
	}
	
		void OnGUI () {
			// Labels:
			GUI.Label (new Rect (Screen.width/8, 0.4f * Screen.height / 5, Screen.width / 2, Screen.height / 10),enterUserNameString, labelStyleText);
			GUI.Label (new Rect (Screen.width/8, 1.6f * Screen.height / 5, Screen.width / 2, Screen.height / 10), enterMajorString, labelStyleText);
	
			// TextFields:
			userNameString = GUI.TextField(new Rect(Screen.width/8, 0.75f * Screen.height/5f, Screen.width/2.5f, Screen.height/10), userNameString, 20, labelStyleInput);
			userMajorString	= GUI.TextField(new Rect(Screen.width/8, 2.0f * Screen.height/5f, Screen.width/2.5f, Screen.height/10), userMajorString, 50, labelStyleInput);
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
			
			GameObject tempIA = (GameObject)userNameUpdateObject;
			if (tempIA.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
			{
				Debug.Log("User Updated UserName!");
                enterUserNameString = "Username Updated";
				AppController.appController.setUsername(userNameString);
				AppController.appController.Save ();
			}
			
			GameObject tempGA = (GameObject)userMajorUpdateObject;
			if (tempGA.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
			{
				Debug.Log("User Updated UserMajor!");
                enterMajorString = "Major Updated";
				AppController.appController.setMajor(userMajorString);
				AppController.appController.Save ();
			}
			
			GameObject tempSC = (GameObject)userLogOutObject;
			if (tempSC.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
			{
				Debug.Log ("Logging out");
				AppController.appController.setLoggedIn (false);
				AppController.appController.Save ();
				Application.LoadLevel("Log In");
			}
		}
	}
}
