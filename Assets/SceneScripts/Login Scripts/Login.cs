using UnityEngine;
using System.Collections;

public class Login : MonoBehaviour {

	// The ultimate login checker:
	private string superSecretPassword = "password";

	// TextField Strings:
	private string userNameString = "Username";
	private string userPasswordString = "Password";
	private string userMajorString = "Major";
	private string userYearString = "Year";

	// Window for GUI:
	private Rect windowRect = new Rect(0, 0, Screen.width, Screen.height);

	// Use this for initialization
	void Start () {
		AppController.appController.Load ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		GUI.Window (0, windowRect, windowFunciton, "Log-In");
	}

	private void windowFunciton (int windowID) {
		// Labels:
		GUI.Label (new Rect (Screen.width / 3, 0.25f * Screen.height / 5f, Screen.width / 2, Screen.height / 10), "Enter a Username:");
		GUI.Label (new Rect (Screen.width / 3, 1.25f * Screen.height / 5f, Screen.width / 2, Screen.height / 10), "Enter the Password:");
		GUI.Label (new Rect (Screen.width / 3, 2.25f * Screen.height / 5f, Screen.width / 2, Screen.height / 10), "Enter a Major:");
		GUI.Label (new Rect (Screen.width / 3, 3.25f * Screen.height / 5f, Screen.width / 2, Screen.height / 10), "Enter a Year:");

		// TextFields:
		userNameString		= GUI.TextField(new Rect(Screen.width/3, 0.5f * Screen.height/5f, Screen.width/3, Screen.height/10), userNameString, 10);
		userPasswordString	= GUI.PasswordField(new Rect(Screen.width/3, 1.5f * Screen.height/5, Screen.width/3, Screen.height/10), userPasswordString, "*"[0], 10);
		userMajorString		= GUI.TextField(new Rect(Screen.width/3, 2.5f * Screen.height/5f, Screen.width/3, Screen.height/10), userMajorString, 10);
		userYearString		= GUI.TextField(new Rect(Screen.width/3, 3.5f * Screen.height/5f, Screen.width/3, Screen.height/10), userYearString, 10);

		if (GUI.Button (new Rect(Screen.width/3, 4.25f * Screen.height/5, Screen.width/3, Screen.height/10), "GO!")) {
			if (userPasswordString == superSecretPassword) {
				Debug.Log ("Setting Data and Logging In...");
				AppController.appController.setUsername (userNameString);
				AppController.appController.setMajor (userMajorString);
				AppController.appController.setYear (int.Parse(userYearString));
				AppController.appController.Save ();
				Application.LoadLevel ("Profile");
			}
			else {
				Debug.LogWarning ("WRONG PASSWORD!");
			}
		}
	}
}
