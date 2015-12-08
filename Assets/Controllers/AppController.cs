using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

// Author: Chris Kuffert
// Date: 11/8/2015
public class AppController : MonoBehaviour {

	public static AppController appController;

	string username;
	string major;
	int year;
    bool loggedIn;
    bool createdAccount;

	int IAExp;
	int GAExp;
	int SCExp;
	int PPExp;
	int WBExp;

	List<Opportunity> allOpportunities;
    List<Opportunity> usersSelectedOpportunities;
    List<Opportunity> usersCompletedOpportunities;
   
    int opportunityIndex;
    int usersSelectedOpportunityIndex;
    int usersCompletedOpportunityIndex;

    int opportunityFeedPageNumber;
    int usersOpportunitiesPageNumber;

    public string getUsername() { return username; }
    public string getMajor() { return major; }
    public int getYear() { return year; }
    public bool getLoggedIn() { return loggedIn; }
    public bool getCreatedAccount() { return createdAccount; }
    public int getIAExp() { return IAExp; }
    public int getGAExp() { return GAExp; }
    public int getSCExp() { return SCExp; }
    public int getPPExp() { return PPExp; }
    public int getWBExp() { return WBExp; }
    public List<Opportunity> getAllOpportunities() { return allOpportunities; }
    public List<Opportunity> getUsersSelectedOpportunities() { return usersSelectedOpportunities; }
    public List<Opportunity> getUsersCompletedOpportunities() { return usersCompletedOpportunities; }
    public int getOpportunityFeedIndex() { return opportunityIndex; }
    public int getUsersSelectedOpportunityIndex() { return usersSelectedOpportunityIndex; }
    public int getUsersCompletedOpportunityIndex() { return usersCompletedOpportunityIndex; }
    public int getOpportunityFeedPageNumber() { return opportunityFeedPageNumber; }
    public int getUsersOpportunitiesPageNumber() { return usersOpportunitiesPageNumber; }
    
    public void setUsername(string username) { this.username = username; }
    public void setMajor(string major) { this.major = major; }
    public void setYear(int year) { this.year = year; }
    public void setLoggedIn(bool loggedIn) { this.loggedIn = loggedIn; }
    public void setCreatedAccount(bool createdAccount) { this.createdAccount = createdAccount; }
    public void setIAExp(int exp) { this.IAExp = exp; }
    public void setGAExp(int exp) { this.GAExp = exp; }
    public void setSCExp(int exp) { this.SCExp = exp; }
    public void setPPExp(int exp) { this.PPExp = exp; }
    public void setWBExp(int exp) { this.WBExp = exp; }
    public void setAllOpportunities(List<Opportunity> allOpportunities) { this.allOpportunities = allOpportunities; }
    public void setUsersSelectedOpportunities(List<Opportunity> usersSelectedOpportunities) { this.usersSelectedOpportunities = usersSelectedOpportunities; }
    public void setUsersCompletedOpportunities(List<Opportunity> usersCompletedOpportunities) { this.usersCompletedOpportunities = usersCompletedOpportunities; }
    public void setOpportunityFeedIndex(int opportunityIndex) { this.opportunityIndex = opportunityIndex; }
    public void setUsersSelectedOpportunityIndex(int usersOpportunityIndex) { this.usersSelectedOpportunityIndex = usersOpportunityIndex; }
    public void setUsersCompletedOpportunityIndex(int usersCompletedOpportunityIndex) { this.usersCompletedOpportunityIndex = usersCompletedOpportunityIndex; }
    public void setOpportunityFeedPageNumber(int opportunityFeedPageNumber) { this.opportunityFeedPageNumber = opportunityFeedPageNumber; }
    public void setUsersOpportunitiesPageNumber(int usersOpportunitiesPageNumber) { this.usersOpportunitiesPageNumber = usersOpportunitiesPageNumber; }

    void Awake () {
        if (appController == null)
        {
            DontDestroyOnLoad(gameObject);
            setAllOpportunities(new List<Opportunity>());
            setUsersSelectedOpportunities(new List<Opportunity>());
            setUsersCompletedOpportunities(new List<Opportunity>());
            setIAExp(0);
            setGAExp(0);
            setSCExp(0);
            setPPExp(0);
            setWBExp(0);
            appController = this;
        }
        else if (appController != this)
        {
            Destroy(gameObject);
        }
	}
	
	public void Save() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/userData.dat");
		UserData data = new UserData ();
        data.saveUserData(this);
		bf.Serialize (file, data);
		file.Close();
	}

	public void Load() {
        if (File.Exists (Application.persistentDataPath + "/userData.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/userData.dat", FileMode.Open);
			UserData data = (UserData)bf.Deserialize(file);
			file.Close();
            data.loadUserData(this);
		}
	}
	
    // Class that contains the same data as AppController. Allows saving locally without interfering with Monobehaviour.
	[Serializable]
	class UserData {

		private string username;
		private string major;
		private int year;
        private bool loggedIn;
        private bool createdAccount;
		
		private int IAExp;
		private int GAExp;
		private int SCExp;
		private int PPExp;
		private int WBExp;

        List<Opportunity> allOpportunities;
        List<Opportunity> usersSelectedOpportunities;
        List<Opportunity> usersCompletedOpportunities;

        int opportunityIndex;
        int usersSelectedOpportunityIndex;
        int usersCompletedOpportunityIndex;

        int opportunityFeedPageNumber;
        int usersOpportunitiesPageNumber;

        public UserData() {
			allOpportunities = new List<Opportunity>();
            usersSelectedOpportunities = new List<Opportunity>();
            usersCompletedOpportunities = new List<Opportunity>();
		}

        string getUsername() { return username; }
        string getMajor() { return major; }
        int getYear() { return year; }
        bool getLoggedIn() { return loggedIn; }
        bool getCreatedAccount() { return createdAccount; }
        int getIAExp() { return IAExp; }
        int getGAExp() { return GAExp; }
        int getSCExp() { return SCExp; }
        int getPPEExp() { return PPExp; }
        int getWBexp() { return WBExp; }
        public List<Opportunity> getAllOpportunities() { return allOpportunities; }
        public List<Opportunity> getUsersSelectedOpportunities() { return usersSelectedOpportunities; }
        public List<Opportunity> getUsersCompletedOpportunities() { return usersCompletedOpportunities; }
        public int getOpportunityIndex() { return opportunityIndex; }
        public int getUsersSelectedOpportunityIndex() { return usersSelectedOpportunityIndex; }
        public int getUsersCompletedOpportunityIndex() { return usersCompletedOpportunityIndex; }
        public int getOpportunityFeedPageNumber() { return opportunityFeedPageNumber; }
        public int getUsersOpportunitiesPageNumber() { return usersOpportunitiesPageNumber; }

        void setUsername(string username) { this.username = username; }
        void setMajor(string major) { this.major = major; }
        void setYear(int year) { this.year = year; }
        void setLoggedIn(bool loggedIn) { this.loggedIn = loggedIn; }
        void setCreatedAccount(bool createdAccount) { this.createdAccount = createdAccount; }
        void setIAExp(int exp) { this.IAExp = exp; }
        void setGAExp(int exp) { this.GAExp = exp; }
        void setSCExp(int exp) { this.SCExp = exp; }
        void setPPEExp(int exp) { this.PPExp = exp; }
        void setWBExp(int exp) { this.WBExp = exp; }
        void setAllOpportunities(List<Opportunity> allOpportunities) { this.allOpportunities = allOpportunities; }
        void setUsersSelectedOpportunities(List<Opportunity> usersSelectedOpportunities) { this.usersSelectedOpportunities = usersSelectedOpportunities; }
        void setUsersCompletedOpportunities(List<Opportunity> usersCompletedOpportunities) { this.usersCompletedOpportunities = usersCompletedOpportunities; }
        void setOpportunityIndex(int opportunityIndex) { this.opportunityIndex = opportunityIndex; }
        void setUsersSelectedOpportunityIndex(int usersOpportunityIndex) { this.usersSelectedOpportunityIndex = usersOpportunityIndex; }
        void setUsersCompletedOpportunityIndex(int usersCompletedOpportunityIndex) { this.usersCompletedOpportunityIndex = usersCompletedOpportunityIndex; }
        public void setOpportunityFeedPageNumber(int opportunityFeedPageNumber) { this.opportunityFeedPageNumber = opportunityFeedPageNumber; }
        public void setUsersOpportunitiesPageNumber(int usersOpportunitiesPageNumber) { this.usersOpportunitiesPageNumber = usersOpportunitiesPageNumber; }

        public void saveUserData(AppController appController)
        {
            setUsername(appController.getUsername());
            setMajor(appController.getMajor());
            setYear(appController.getYear());
            setLoggedIn(appController.getLoggedIn());
            setCreatedAccount(appController.getCreatedAccount());
            setIAExp(appController.getIAExp());
            setGAExp(appController.getGAExp());
            setSCExp(appController.getSCExp());
            setPPEExp(appController.getPPExp());
            setWBExp(appController.getWBExp());
            setAllOpportunities(appController.getAllOpportunities());
            setUsersSelectedOpportunities(appController.getUsersSelectedOpportunities());
            setUsersCompletedOpportunities(appController.getUsersCompletedOpportunities());
            setOpportunityIndex(appController.getOpportunityFeedIndex());
            setUsersSelectedOpportunityIndex(appController.getUsersSelectedOpportunityIndex());
            setUsersCompletedOpportunityIndex(appController.getUsersCompletedOpportunityIndex());
            setOpportunityFeedPageNumber(appController.getOpportunityFeedPageNumber());
            setUsersOpportunitiesPageNumber(appController.getUsersOpportunitiesPageNumber());
        }

        public void loadUserData(AppController appController)
        {
            appController.setUsername(getUsername());
            appController.setMajor(getMajor());
            appController.setYear(getYear());
            appController.setLoggedIn(getLoggedIn());
            appController.setCreatedAccount(getCreatedAccount());
            appController.setIAExp(getIAExp());
            appController.setGAExp(getGAExp());
            appController.setSCExp(getSCExp());
            appController.setPPExp(getPPEExp());
            appController.setWBExp(getWBexp());
            appController.setAllOpportunities(getAllOpportunities());
            appController.setUsersSelectedOpportunities(getUsersSelectedOpportunities());
            appController.setUsersCompletedOpportunities(getUsersCompletedOpportunities());
            appController.setOpportunityFeedIndex(getOpportunityIndex());
            appController.setUsersSelectedOpportunityIndex(getUsersSelectedOpportunityIndex());
            appController.setUsersCompletedOpportunityIndex(getUsersCompletedOpportunityIndex());
            appController.setOpportunityFeedPageNumber(getOpportunityFeedPageNumber());
            appController.setUsersOpportunitiesPageNumber(getUsersOpportunitiesPageNumber());
        }
    }
}
