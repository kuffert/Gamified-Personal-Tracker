using UnityEngine;
using System.Collections;

// Author: Chris Kuffert
// Date: 11/1/2015
public class StaticFeatures : MonoBehaviour {

    public Sprite backgroundImage;
    public Sprite activityFeedIcon;
    public Sprite currentActivitiesIcon;
    public Sprite settingsIcon;
    public Sprite profileIcon;
    public string currentScreenText;

    public int numberOfButtons;
    public int taskbarFractionOfScreen;

    private int screenHeight;
    private int screenWidth;
    private float orthographicScreenHeight;
    private float orthographicScreenWidth;
    private GameObject background;
    private GameObject profileButton;
    private GameObject activityFeedButton;
    private GameObject currentActivitiesButton;
    private GameObject settingsButton;
    private GameObject sceneText;
    
    // Anything in here will be run, created, instantiated, etc. immediately as the application starts.
	void Start () {
        
        Camera camera = Camera.main;
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        orthographicScreenHeight = Camera.main.orthographicSize * 2;
        orthographicScreenWidth = orthographicScreenHeight * screenWidth / screenHeight;

        // Creates the generic screen objects; background, and each navigation button.
        background = generateApplicationBackground();
        profileButton = generateNavigationButton(profileIcon);
        activityFeedButton = generateNavigationButton(activityFeedIcon);
        currentActivitiesButton = generateNavigationButton(currentActivitiesIcon);
        settingsButton = generateNavigationButton(settingsIcon);

        // Display the text, assigning its string value depending on the current scene.
        sceneText = generateSceneText();
        sceneText.transform.position = camera.ViewportToWorldPoint(new Vector3(.5f, .95f, 10));
        sceneText.GetComponent<MeshRenderer>().sortingOrder = 3;

        // Determines the x-shift and y-shift of the buttons. (pivot-point is bound to middle of the sprite, we have to adjust accordingly)
        float xOffset = 1f / (numberOfButtons * 2);
        float yOffset = 1f / (taskbarFractionOfScreen * 2);

        // Assigns the relative screen location based on the number of buttons.
        // Screen location ranges from 0->1 on both x and y. ViewportToWorldPoint will convert the screen location into the world location.
        float buttonPlacementX = 1f / numberOfButtons;
        float buttonPlacementY = 1f / taskbarFractionOfScreen;

        // Places each screen object at its correct location.
        profileButton.transform.position = camera.ViewportToWorldPoint(new Vector3(xOffset, buttonPlacementY - yOffset, 10));
        activityFeedButton.transform.position = camera.ViewportToWorldPoint(new Vector3(xOffset + buttonPlacementX, buttonPlacementY - yOffset, 10));
        currentActivitiesButton.transform.position = camera.ViewportToWorldPoint(new Vector3(xOffset + buttonPlacementX * 2, buttonPlacementY - yOffset, 10));
        settingsButton.transform.position = camera.ViewportToWorldPoint(new Vector3(xOffset + buttonPlacementX * 3, buttonPlacementY - yOffset, 10));
    }
	
    // Anything in here will be run every tick.
	void Update () {
        delegateNavigationFromTouch();
	}

    // Constructs the application's static backdrop.
    private GameObject generateApplicationBackground()
    {
        GameObject background = new GameObject();

        float backgroundSpriteWidth = calculateSpriteUnitWidth(backgroundImage);
        float backgroundSpriteHeight = calculateSpriteUnitHeight(backgroundImage);

        // This will apply the background image to the gameObject. 
        background.AddComponent<SpriteRenderer>();
        SpriteRenderer backgroundSpriteRenderer = background.GetComponent<SpriteRenderer>();
        backgroundSpriteRenderer.sprite = backgroundImage;
        backgroundSpriteRenderer.sortingOrder = 1;
        backgroundSpriteRenderer.transform.localScale = new Vector3(orthographicScreenWidth / backgroundSpriteWidth, orthographicScreenHeight / backgroundSpriteHeight);

        return background;
    }

    // Constructs a generic navigation button with a sprite image.
    private GameObject generateNavigationButton(Sprite buttonSprite)
    {
        GameObject navigationButton = new GameObject();

        float navigationSpriteWidth = calculateSpriteUnitWidth(buttonSprite);
        float navigationSpriteHeight = calculateSpriteUnitHeight(buttonSprite);
        Vector3 newNavigationButtonScale = new Vector3(orthographicScreenWidth / navigationSpriteWidth / numberOfButtons, orthographicScreenHeight / navigationSpriteHeight / taskbarFractionOfScreen);

        // This will apply the corresponding button icon to the navigation button. 
        navigationButton.AddComponent<SpriteRenderer>();
        SpriteRenderer navigationButtonSpriteRenderer = navigationButton.GetComponent<SpriteRenderer>();
        navigationButtonSpriteRenderer.sprite = buttonSprite;
        navigationButtonSpriteRenderer.sortingOrder = 3;
        navigationButtonSpriteRenderer.transform.localScale = newNavigationButtonScale; // This just scales it to fit the screen properly. But we need to make individual icons that are a proper size.

        // This will allow the Button to register touch input.
        navigationButton.AddComponent<BoxCollider>();

        return navigationButton;
    }

    private float calculateSpriteUnitWidth(Sprite sprite)
    {
        return sprite.textureRect.width / sprite.pixelsPerUnit;
    }

    private float calculateSpriteUnitHeight(Sprite sprite)
    {
        return sprite.textureRect.height / sprite.pixelsPerUnit;
    }

    // Determines how far the location of each sprite should be offset. Sprites normally display 
    private Vector3 calculateSpriteOffset(GameObject button)
    {
        float xOffset = button.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
        float yOffset = button.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;

        return new Vector3(xOffset, yOffset);
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
        sceneTextMesh.fontSize = 200;
        sceneTextMesh.color = Color.black;

        return sceneText;
    }

    // Handles touching of each button:
    private void delegateNavigationFromTouch()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (profileButton.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
                {
                    Application.LoadLevel("Profile");
                }

                if (activityFeedButton.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
                {
                    Application.LoadLevel("Activities Feed");
                }

                if (currentActivitiesButton.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
                {
                    Application.LoadLevel("Current Activities");
                }

                if (settingsButton.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
                {
                    Application.LoadLevel("Settings");
                }
            }
        }
    }
}
