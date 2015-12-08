using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// Authors: Chris Kuffert, Craig Hammond, John Kelly, Yvette Kim
public class Testing : MonoBehaviour {

    public bool showAllTestOutputs;
    private int testNumber = 1;
    private int passedTests = 0;
    private int failedTests = 0;

	// All Tests should be written or called inside this function, that way they are only called once.
	void Start () {
        // TESTS FOR THE APP CONTROLLER //
        // Author: Chris Kuffert
        // Date: 12/1/15
        // 1. Should be no data starting out.
        wipeAllUserData();
        outputTestResult(AppController.appController.getUsername() == null);

        // 2. Should be no data stored locally yet.
        AppController.appController.Load();
        outputTestResult(AppController.appController.getUsername() == null);

        // 3. Test local data saving and loading.
        AppController.appController.setUsername("testUsername");
        AppController.appController.Save();
        AppController.appController.Load();
        string usernameFromLocal = AppController.appController.getUsername();
        outputTestResult(usernameFromLocal.Equals("testUsername"));

        // 4. Testing logging in being saved
        AppController.appController.setLoggedIn(true);
        AppController.appController.Save();
        AppController.appController.Load();
        outputTestResult(AppController.appController.getLoggedIn());

        // 5. Testing creating an account being saved
        AppController.appController.setCreatedAccount(true);
        AppController.appController.Save();
        AppController.appController.Load();
        outputTestResult(AppController.appController.getCreatedAccount());

        // TESTS FOR THE APPLICATION VIEW //
        // Author: Chris Kuffert
        // Date: 12/1/15
        // 6 - 13
        // NOTE: As this is the script for displaying components, 
        // these tests will determine whether the components were 
        // indeed created successfully. Upon application start,
        // all of these GameObjects are built inside the ApplicationView
        // script. Calling each generation function again would duplicate
        // what already exists. So, that script will run upon start, then 
        // these tests will run, determining whether or not they were instantiated.
        outputTestResult(ApplicationView.applicationView.getBackground() != null);
        outputTestResult(ApplicationView.applicationView.getTopTaskbar() != null);
        outputTestResult(ApplicationView.applicationView.getBottomTaskBar() != null);
        outputTestResult(ApplicationView.applicationView.getProfileButton() != null);
        outputTestResult(ApplicationView.applicationView.getOpportunityFeedButton() != null);
        outputTestResult(ApplicationView.applicationView.getYourOpportunitiesButton() != null);
        outputTestResult(ApplicationView.applicationView.getSettingsButton() != null);
        outputTestResult(ApplicationView.applicationView.getSceneText() != null);

        // TESTS FOR THE OPPORTUNITY CONTROLLER //
        // Author: Yvette Kim
        // Date: 12/2/15

        List<Opportunity> opportunities = OpportunityController.getOpportunities();
        List<Skill> skills = OpportunityController.getSkills();

        // 14. Test getting opportunities from db
        outputTestResult(opportunities != null);

        // 15. Test getting skills from db, should be exactly 50 at all times
        outputTestResult(skills.Count == 50);

        // 15 - 18 
        // Spot test opportunity mapping for string, string[], List<Skill>
        outputTestResult(opportunities.First().Id != null);
        outputTestResult(opportunities.First().AcademicStanding != null);
        outputTestResult(opportunities.First().Skills != null);

        // 19 - 22
        // Test Skills
        Skill skill = skills.Find(item => item.SkillName == "Advocacy");

        outputTestResult(skill.SkillName == "Advocacy");
        outputTestResult(skill.Dimensions.Contains("Social Consciousness & Interpersonal Commitment"));
        outputTestResult(skill.Dimensions.Length == 1);
        outputTestResult(skill.Id == "563f9e95c58aed0304213e5b");

        // TESTS FOR THE EXPERIENCE CLASS //
        // 23 - 28
        Experience testEXP = Experience.DefaultExperience();
        int temp = 0;
        outputTestResult(testEXP.totals.Values.Count == 5);
        outputTestResult(testEXP.totals.TryGetValue("Intellectual Agility", out temp));
        outputTestResult(testEXP.totals.TryGetValue("Global Awareness", out temp));
        outputTestResult(testEXP.totals.TryGetValue("Social Consciousness & Interpersonal Commitment", out temp));
        outputTestResult(testEXP.totals.TryGetValue("Professional and Personal Effectiveness", out temp));
        outputTestResult(testEXP.totals.TryGetValue("Well-Being", out temp));

		// TESTS FOR THE LOGIN SCENE //
		// Author: Jack Kelly
		// Date: 12/6/15

		// 29) Test Get/SET Major
		AppController.appController.setMajor("testMajor");
		AppController.appController.Save();
		AppController.appController.Load();
		string userMajorFromLocal = AppController.appController.getMajor();
		outputTestResult(userMajorFromLocal.Equals("testMajor"));

		// 30) Test Get/SET Year
		AppController.appController.setYear(1234);
		AppController.appController.Save();
		AppController.appController.Load();
		int userYearFromLocal = AppController.appController.getYear();
		outputTestResult(userYearFromLocal == 1234);

        outputFinalResult();
	}

    private void outputTestResult(bool testResult)
    {
        if (testResult) { passedTests++; } else { failedTests++; }
        string output = testResult ? "Passed" : "Failed";
        if (showAllTestOutputs)
        {
            Debug.Log("Test " + testNumber++ + ": " + output);
        }
    }

    private void outputFinalResult()
    {
        Debug.Log(failedTests == 0 ? "All " + passedTests + " Tests Passed." : passedTests + " Tests Passed, " + failedTests + " Tests Failed.");
    }

    private void wipeAllUserData()
    {
        AppController.appController.Load();
        AppController.appController.setUsername(null);
        AppController.appController.setMajor(null);
        AppController.appController.setLoggedIn(false);
        AppController.appController.setCreatedAccount(false);
        AppController.appController.setYear(0);
        AppController.appController.setIAExp(0);
        AppController.appController.setGAExp(0);
        AppController.appController.setSCExp(0);
        AppController.appController.setPPExp(0);
        AppController.appController.setWBExp(0);
        AppController.appController.setAllOpportunities(null);
        AppController.appController.setUsersSelectedOpportunities(null);
        AppController.appController.setUsersCompletedOpportunities(null);
        AppController.appController.setOpportunityFeedIndex(0);
        AppController.appController.setUsersSelectedOpportunityIndex(0);
        AppController.appController.setUsersCompletedOpportunityIndex(0);
        AppController.appController.setOpportunityFeedPageNumber(0);
        AppController.appController.setUsersOpportunitiesPageNumber(0);
        AppController.appController.Save();
        AppController.appController.Load();
    }
}
