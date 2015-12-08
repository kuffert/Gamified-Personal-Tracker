using UnityEngine;
using System.Collections;

// Author: Chris Kuffert
// Date: 11/1/2015
public class ApplicationView : MonoBehaviour {

    public static ApplicationView applicationView;

    public Sprite backgroundImage;
    public Sprite opportunityFeedIcon;
    public Sprite youOpportunitiesIcon;
    public Sprite settingsIcon;
    public Sprite profileIcon;
    public Sprite taskbar;
    public string currentScreenText;
    public int currentScreenTextSize = 200;

    public int numberOfButtons;
    public int taskbarFractionOfScreen;

	public Font font;

    public static float orthographicScreenHeight;
    public static float orthographicScreenWidth;

    private GameObject background;
    private GameObject topTaskbar;
    private GameObject bottomTaskbar;
    private GameObject profileButton;
    private GameObject opportunityFeedButton;
    private GameObject yourOpportunitiesButton;
    private GameObject settingsButton;
    private GameObject sceneText;

    public int getNumberOfButtons() { return numberOfButtons; }
    public int getTaskbarFractionOfScreen() { return taskbarFractionOfScreen; }

    public float getOrthographicScreenHeight() { return orthographicScreenHeight; }
    public float getOrthographicScreenWidth() { return orthographicScreenWidth; }

    // Used for validation testing purposes
    public GameObject getBackground() { return background; }
    public GameObject getTopTaskbar() { return topTaskbar; }
    public GameObject getBottomTaskBar() { return bottomTaskbar; }
    public GameObject getProfileButton() { return profileButton; }
    public GameObject getOpportunityFeedButton() { return opportunityFeedButton; }
    public GameObject getYourOpportunitiesButton() { return yourOpportunitiesButton; }
    public GameObject getSettingsButton() { return settingsButton; }
    public GameObject getSceneText() { return sceneText; }

    void Awake()
    {
        applicationView = this;

        // Determines the screensize we will need to scale the UI elements.
        orthographicScreenHeight = Camera.main.orthographicSize * 2;
        orthographicScreenWidth = orthographicScreenHeight * Screen.width / Screen.height;
    }

    // Anything in here will be run, created, instantiated, etc. immediately as the application starts.
    void Start () {
        Camera camera = Camera.main;

        // Creates the generic screen objects; background, taskbars, and each navigation button.
        background = generateBackground(backgroundImage);
        topTaskbar = generateTaskbar(taskbar);
        bottomTaskbar = generateTaskbar(taskbar);
        profileButton = generateDynamicElement(profileIcon);
        opportunityFeedButton = generateDynamicElement(opportunityFeedIcon);
        yourOpportunitiesButton = generateDynamicElement(youOpportunitiesIcon);
        settingsButton = generateDynamicElement(settingsIcon);

        // Display the text, assigning its string value depending on the current scene.
        sceneText = generateSceneText();
        sceneText.transform.position = camera.ViewportToWorldPoint(new Vector3(.5f, .95f, 10));
        sceneText.GetComponent<MeshRenderer>().sortingOrder = 3;

        // Determines the x-shift and y-offsets of the buttons, based on the #of buttons and how 
        // much screen real estate the taskbars get.
        float buttonOffsetX = 1f / (numberOfButtons * 2);
        float buttonOffsetY = 1f / (taskbarFractionOfScreen * 2);

        // Assigns the relative screen location based on the number of buttons.
        // Screen location ranges from 0->1 on both x and y. ViewportToWorldPoint will convert the screen location into the world location.
        float buttonPlacementX = 1f / numberOfButtons;
        float buttonPlacementY = 1f / taskbarFractionOfScreen;

        // Places each screen object at its correct location.
        topTaskbar.transform.position = camera.ViewportToWorldPoint(new Vector3(.5f, 1 - buttonOffsetY, 10));
        bottomTaskbar.transform.position = camera.ViewportToWorldPoint(new Vector3(.5f, buttonOffsetY, 10));
        profileButton.transform.position = camera.ViewportToWorldPoint(new Vector3(buttonOffsetX, buttonPlacementY - buttonOffsetY, 10));
        opportunityFeedButton.transform.position = camera.ViewportToWorldPoint(new Vector3(buttonOffsetX + buttonPlacementX, buttonPlacementY - buttonOffsetY, 10));
        yourOpportunitiesButton.transform.position = camera.ViewportToWorldPoint(new Vector3(buttonOffsetX + buttonPlacementX * 2, buttonPlacementY - buttonOffsetY, 10));
        settingsButton.transform.position = camera.ViewportToWorldPoint(new Vector3(buttonOffsetX + buttonPlacementX * 3, buttonPlacementY - buttonOffsetY, 10));
    }
	
    // Anything in here will be run every tick.
	void Update () {
        sceneText.GetComponent<TextMesh>().text = currentScreenText;
        sceneText.GetComponent<TextMesh>().fontSize = currentScreenTextSize;
        delegateNavigationFromTouch();
    }

    // Constructs a static element of the application.
    private GameObject generateBackground(Sprite sprite)
    {
        GameObject background = new GameObject();

        // This will apply the background image to the gameObject. 
        background.AddComponent<SpriteRenderer>();
        SpriteRenderer backgroundSpriteRenderer = background.GetComponent<SpriteRenderer>();
        backgroundSpriteRenderer.sprite = sprite;
        backgroundSpriteRenderer.sortingOrder = 1;

        // This will dynamically scale the size of the background to fit the screen.
        float backgroundSpriteWidth = calculateSpriteUnitWidth(sprite);
        float backgroundSpriteHeight = calculateSpriteUnitHeight(sprite);
        backgroundSpriteRenderer.transform.localScale = new Vector3(orthographicScreenWidth / backgroundSpriteWidth, orthographicScreenHeight / backgroundSpriteHeight);

        return background;
    }

    // Constructs a static taskbar.
    private GameObject generateTaskbar(Sprite sprite)
    {
        GameObject taskbar = new GameObject();

        // This will apply the taskbar image to the gameObject.
        taskbar.AddComponent<SpriteRenderer>();
        SpriteRenderer taskbarSpriteRenderer = taskbar.GetComponent<SpriteRenderer>();
        taskbarSpriteRenderer.sprite = sprite;
        taskbarSpriteRenderer.sortingOrder = 2;

        // This will dynamically scale the size of the taskbar to fit the screen.
        float taskbarSpriteWidth = calculateSpriteUnitWidth(sprite);
        float taskbarSpriteHeight = calculateSpriteUnitHeight(sprite);
        taskbarSpriteRenderer.transform.localScale = new Vector3(orthographicScreenWidth / taskbarSpriteWidth, orthographicScreenHeight / taskbarSpriteHeight / taskbarFractionOfScreen);

        return taskbar;
    }

    // Constructs an interactable dynamic element of the application.
    private GameObject generateDynamicElement(Sprite sprite)
    {
        GameObject navigationButton = new GameObject();

        // This will apply the corresponding button icon to the navigation button. 
        navigationButton.AddComponent<SpriteRenderer>();
        SpriteRenderer navigationButtonSpriteRenderer = navigationButton.GetComponent<SpriteRenderer>();
        navigationButtonSpriteRenderer.sprite = sprite;
        navigationButtonSpriteRenderer.sortingOrder = 3;

        // Dynamically scales the size of the navigation button to fit the screen.
        float navigationSpriteWidth = calculateSpriteUnitWidth(sprite);
        float navigationSpriteHeight = calculateSpriteUnitHeight(sprite);
        Vector3 newNavigationButtonScale = new Vector3(orthographicScreenWidth / navigationSpriteWidth / numberOfButtons, orthographicScreenHeight / navigationSpriteHeight / taskbarFractionOfScreen);
        navigationButtonSpriteRenderer.transform.localScale = newNavigationButtonScale; 

        // This will allow the Button to register touch input.
        navigationButton.AddComponent<BoxCollider>();

        return navigationButton;
    }

    // Generates the current scenes text
    private GameObject generateSceneText()
    {
        GameObject sceneText = new GameObject();
        sceneText.AddComponent<TextMesh>();
        TextMesh sceneTextMesh = sceneText.GetComponent<TextMesh>();
        sceneTextMesh.text = currentScreenText;
        sceneTextMesh.anchor = TextAnchor.MiddleCenter;
        sceneTextMesh.characterSize = .025f;
        sceneTextMesh.fontSize = currentScreenTextSize;
        Color NURed = new Color(204.0f, 0.0f, 0.0f);
        sceneTextMesh.color = NURed;
        return sceneText;
    }

    // Helper for dynamically scaling the buttons (x)
    // This calculates the Sprites "Unit" width, which is the the unit of measurement in Unity.
    public static float calculateSpriteUnitWidth(Sprite sprite)
    {
        return sprite.textureRect.width / sprite.pixelsPerUnit;
    }

    // Helper for dynamically scaling the buttons (y)
    // This calculates the Sprites "Unit" height, which is the the unit of measurement in Unity.
    public static float calculateSpriteUnitHeight(Sprite sprite)
    {
        return sprite.textureRect.height / sprite.pixelsPerUnit;
    }

    // Determines how far the location of each sprite should be offset. Gameobjects have pivot points
    // at their centers, so we have to offset the location we place them at.
    private Vector3 calculateSpriteOffset(GameObject button)
    {
        float xOffset = button.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
        float yOffset = button.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;

        return new Vector3(xOffset, yOffset);
    }

    // Handles touching of each button:
    private void delegateNavigationFromTouch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (profileButton.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
            {
                Application.LoadLevel("Profile");
            }

            if (opportunityFeedButton.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
            {
                Application.LoadLevel("GeneralOpportunityFeed");
            }

            if (yourOpportunitiesButton.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
            {
                Application.LoadLevel("UsersOpportunities");
            }

            if (settingsButton.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
            {
                Application.LoadLevel("Settings");
            }
        }
    }
}
