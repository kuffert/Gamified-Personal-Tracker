using UnityEngine;
using System.Collections;

public class Login : MonoBehaviour {

	// The ultimate login checker:
	private string superSecretPassword = "password";

	// Label GUIStyle:
	private GUIStyle labelStyle = new GUIStyle ();

	// TextField Strings:
	private string userNameString = "Username";
	private string userPasswordString = "Password";
	private string userMajorString = "Major";

	// Window for GUI:
	private Rect windowRect = new Rect(0, 0, Screen.width, Screen.height);

	// Use this for initialization
	void Start () {
		AppController.appController.Load ();
        if (AppController.appController.getLoggedIn())
        {
            Application.LoadLevel("Profile");
        }
		labelStyle.alignment = TextAnchor.MiddleCenter;
		labelStyle.fontSize = 16;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		GUI.Window (0, windowRect, windowFunciton, "Log-In");
	}

	private void windowFunciton (int windowID) {
		// Labels:

		GUI.Label (new Rect (Screen.width / 4, 0.15f * Screen.height / 5f, Screen.width / 2, Screen.height / 10), "Enter a Username:", labelStyle);
		GUI.Label (new Rect (Screen.width / 4, 1.15f * Screen.height / 5f, Screen.width / 2, Screen.height / 10), "Enter the Password:", labelStyle);
		GUI.Label (new Rect (Screen.width / 4, 2.15f * Screen.height / 5f, Screen.width / 2, Screen.height / 10), "Enter a Major:", labelStyle);
		GUI.Label (new Rect (Screen.width / 4, 3.15f * Screen.height / 5f, Screen.width / 2, Screen.height / 10), "Choose a Year:", labelStyle);

		// TextFields:
		userNameString		= GUI.TextField(new Rect(Screen.width/3, 0.5f * Screen.height/5f, Screen.width/3, Screen.height/10), userNameString, 20);
		userPasswordString	= GUI.PasswordField(new Rect(Screen.width/3, 1.5f * Screen.height/5, Screen.width/3, Screen.height/10), userPasswordString, "*"[0], 10);
		userMajorString		= GUI.TextField(new Rect(Screen.width/3, 2.5f * Screen.height/5f, Screen.width/3, Screen.height/10), userMajorString, 50);

		// Buttons for Year:
		for (int i = 0; i < 5; i++) {
			string yearString = (i + 2017).ToString();
			if (GUI.Button(new Rect((float)i * Screen.width/4f, 3.5f * Screen.height/5f, Screen.width/4, Screen.height/12), yearString)) {
				Debug.Log (yearString);
				AppController.appController.setYear (int.Parse(yearString));
			}
		}

		// Button go "GO!"
		if (GUI.Button (new Rect(Screen.width/3, 4.25f * Screen.height/5, Screen.width/3, Screen.height/10), "GO!")) {
			if (userPasswordString == superSecretPassword) {
				AppController.appController.setUsername (userNameString);
				if (userMajorString.Length > 8) {
					userMajorString = userMajorString.Substring(0, 8) + "...";
				}
				AppController.appController.setMajor (userMajorString);
				AppController.appController.Save ();
				Debug.Log ("Setting Data and Logging In...");
                AppController.appController.setLoggedIn(true);
                AppController.appController.Save();
				Application.LoadLevel ("Profile");
			}
			else {
				Debug.LogWarning ("WRONG PASSWORD!");
			}
		}
	}
}
