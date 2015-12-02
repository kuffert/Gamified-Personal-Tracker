using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// Authors: Chris Kuffert, Craig Hammond, John Kelly, Yvette Kim, Zhenhuan Wu
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


        // TESTS FOR THE APPLICATION VIEW //
        // Author: Chris Kuffert
        // Date: 12/1/15
        // 4 - 11
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

        // 12. Test getting opportunities from db
        outputTestResult(opportunities != null);

        // 13. Test getting skills from db, should be exactly 50 at all times
        outputTestResult(skills.Count == 50);

        // 14 - 16 
        // Spot test opportunity mapping for string, string[], List<Skill>
        outputTestResult(opportunities.First().Id != null);
        outputTestResult(opportunities.First().AcademicStanding != null);
        outputTestResult(opportunities.First().Skills != null);

        // 17 - 21 
        // Test Skills
        Skill skill = skills.Find(item => item.SkillName == "Advocacy");

        outputTestResult(skill.SkillName == "Advocacy");
        outputTestResult(skill.Dimensions.Contains("Social Consciousness & Interpersonal Commitment"));
        outputTestResult(skill.Dimensions.Length == 1);
        outputTestResult(skill.Id == "563f9e95c58aed0304213e5b");

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
