using UnityEngine;
using System.Collections;
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

	int IAexp;
	int GAexp;
	int SCexp;
	int PPEexp;
	int WBexp;

	List<Opportunity> allOpportunities;
    List<Opportunity> usersSelectedOpportunities;
    List<Opportunity> usersCompletedOpportunities;

    string getUsername() { return username; }
    string getMajor() { return major; }
    int getYear() { return year; }
    int getIAExp() { return IAexp; }
    int getGAExp() { return GAexp; }
    int getSCExp() { return SCexp; }
    int getPPEExp() { return PPEexp; }
    int getWBexp() { return WBexp; }
    public List<Opportunity> getAllOpportunities() { return allOpportunities; }
    public List<Opportunity> getUsersSelectedOpportunities() { return usersSelectedOpportunities; }
    public List<Opportunity> getUsersCompletedOpportunities() { return usersCompletedOpportunities; }

    void setUsername(string username) { this.username = username; }
    void setMajor(string major) { this.major = major; }
    void setYear(int year) { this.year = year; }
    void setIAExp(int exp) { this.IAexp = exp; }
    void setGAExp(int exp) { this.GAexp = exp; }
    void setSCExp(int exp) { this.SCexp = exp; }
    void setPPEExp(int exp) { this.PPEexp = exp; }
    void setWBExp(int exp) { this.WBexp = exp; }
    public void setAllOpportunities(List<Opportunity> allOpportunities) { this.allOpportunities = allOpportunities; }
    public void setUsersSelectedOpportunities(List<Opportunity> usersSelectedOpportunities) { this.usersSelectedOpportunities = usersSelectedOpportunities; }
    public void setUsersCompletedOpportunities(List<Opportunity> usersCompletedOpportunities) { this.usersCompletedOpportunities = usersCompletedOpportunities; }

    void Awake () {
        if (appController == null)
        {
            DontDestroyOnLoad(gameObject);
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
        data.saveUserData();
		bf.Serialize (file, data);
		file.Close();
	}

	public void Load() {
		if (File.Exists (Application.persistentDataPath + "userData.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/userData.dat", FileMode.Open);
			UserData data = (UserData)bf.Deserialize(file);
			file.Close();
            data.loadUserData();
		}
	}
	
    // Class that contains the same data as AppController. Allows saving to DB without interfering with Monobehaviour.
	[Serializable]
	class UserData {

		private string username;
		private string major;
		private int year;
		
		private int IAexp;
		private int GAexp;
		private int SCexp;
		private int PPEexp;
		private int WBexp;

		private List<Opportunity> allOpportunities;
        private List<Opportunity> usersSelectedOpportunities;
        private List<Opportunity> usersCompletedOpportunities;

        public UserData() {
			allOpportunities = new List<Opportunity>();
		}

        string getUsername() { return username; }
        string getMajor() { return major; }
        int getYear() { return year; }
        int getIAExp() { return IAexp; }
        int getGAExp() { return GAexp; }
        int getSCExp() { return SCexp; }
        int getPPEExp() { return PPEexp; }
        int getWBexp() { return WBexp; }
        List<Opportunity> getAllOpportunities() { return allOpportunities; }
        List<Opportunity> getUsersSelectedOpportunities() { return usersSelectedOpportunities; }
        List<Opportunity> getUsersCompletedOpportunities() { return usersCompletedOpportunities; }

        void setUsername(string username) { this.username = username; }
        void setMajor(string major) { this.major = major; }
        void setYear(int year) { this.year = year; }
        void setIAExp(int exp) { this.IAexp = exp; }
        void setGAExp(int exp) { this.GAexp = exp; }
        void setSCExp(int exp) { this.SCexp = exp; }
        void setPPEExp(int exp) { this.PPEexp = exp; }
        void setWBExp(int exp) { this.WBexp = exp; }
        void setAllOpportunities(List<Opportunity> allOpportunities) { this.allOpportunities = allOpportunities; }
        void setUsersSelectedOpportunities(List<Opportunity> usersSelectedOpportunities) { this.usersSelectedOpportunities = usersSelectedOpportunities; }
        void setUsersCompletedOpportunities(List<Opportunity> usersCompletedOpportunities) { this.usersCompletedOpportunities = usersCompletedOpportunities; }

        public void saveUserData()
        {
            setUsername(username);
            setMajor(major);
            setYear(year);
            setIAExp(IAexp);
            setGAExp(GAexp);
            setSCExp(SCexp);
            setPPEExp(PPEexp);
            setWBExp(WBexp);
            setAllOpportunities(allOpportunities);
            setUsersSelectedOpportunities(usersSelectedOpportunities);
            setUsersCompletedOpportunities(usersCompletedOpportunities);
        }

        public void loadUserData()
        {
            username = getUsername();
            major = getMajor();
            year = getYear();
            IAexp = getIAExp();
            GAexp = getGAExp();
            SCexp = getSCExp();
            PPEexp = getPPEExp();
            WBexp = getWBexp();
            allOpportunities = getAllOpportunities();
            usersSelectedOpportunities = getUsersSelectedOpportunities();
            usersCompletedOpportunities = getUsersCompletedOpportunities();
        }
    }
}
