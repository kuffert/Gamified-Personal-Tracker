﻿using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class AppController : MonoBehaviour {

	public static AppController appController;

	string username;
	string major;
	int year;

	int IAlevel;
	int GAlevel;
	int SClevel;
	int PPElevel;
	int WBlevel;

	int IAexp;
	int GAexp;
	int SCexp;
	int PPEexp;
	int WBexp;

	ArrayList currentActivities;

	void Awake () {
		if (appController == null) {
			DontDestroyOnLoad (gameObject);
			appController = this;
		} 
		else if (appController != this) {
			Destroy (appController);
		}
	}
	
	public void Save() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "userData.dat", FileMode.Open);
		UserData data = new UserData ();
        data.saveUserData();
		bf.Serialize (file, data);
		file.Close();
	}

	public void Load() {
		if (File.Exists (Application.persistentDataPath + "userData.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "userData.dat");
			UserData data = (UserData)bf.Deserialize(file);
			file.Close();
            data.loadUserData();
		}
	}
	
	[Serializable]
	class UserData {

		private string username;
		private string major;
		private int year;
		
		private int IAlevel;
		private int GAlevel;
		private int SClevel;
		private int PPElevel;
		private int WBlevel;
		
		private int IAexp;
		private int GAexp;
		private int SCexp;
		private int PPEexp;
		private int WBexp;

		private ArrayList currentActivities;

		public UserData() {
			currentActivities = new ArrayList();
		}

        string getUsername() { return username; }
        string getMajor() { return major; }
        int getYear() { return year; }
        int getIALevel() { return IAlevel; }
        int getGALevel() { return GAlevel; }
        int getSCLevel() { return SClevel; }
        int getPPELevel() { return PPElevel; }
        int getWBLevel() { return WBlevel; }
        int getIAExp() { return IAexp; }
        int getGAExp() { return GAexp; }
        int getSCExp() { return SCexp; }
        int getPPEExp() { return PPEexp; }
        int getWBexp() { return WBexp; }
        ArrayList getCurrentActivities() { return currentActivities; }

        void setUsername(string username) { this.username = username; }
        void setMajor(string major) { this.major = major; }
        void setYear(int year) { this.year = year; }
        void setIALevel(int level) { this.IAlevel = level; }
        void setGALevel(int level) { this.GAlevel = level; }
        void setSCLevel(int level) { this.SClevel = level; }
        void setPPELevel(int level) { this.PPElevel = level; }
        void setWBLevel(int level) { this.WBlevel = level; }
        void setIAExp(int exp) { this.IAexp = exp; }
        void setGAExp(int exp) { this.GAexp = exp; }
        void setSCExp(int exp) { this.SCexp = exp; }
        void setPPEExp(int exp) { this.PPEexp = exp; }
        void setWBExp(int exp) { this.WBexp = exp; }
        void setCurrentActivities(ArrayList currentActivities) { this.currentActivities = currentActivities; }

        public void saveUserData()
        {
            setUsername(username);
            setMajor(major);
            setYear(year);
            setIALevel(IAlevel);
            setGALevel(GAlevel);
            setSCLevel(SClevel);
            setPPELevel(PPElevel);
            setWBLevel(WBlevel);
            setIAExp(IAexp);
            setGAExp(GAexp);
            setSCExp(SCexp);
            setPPEExp(PPEexp);
            setWBExp(WBexp);
            setCurrentActivities(currentActivities);
        }

        public void loadUserData()
        {
            username = getUsername();
            major = getMajor();
            year = getYear();
            IAlevel = getIALevel();
            GAlevel = getGALevel();
            SClevel = getSCLevel();
            PPElevel = getPPELevel();
            WBlevel = getWBLevel();
            IAexp = getIAExp();
            GAexp = getGAExp();
            SCexp = getSCExp();
            PPEexp = getPPEExp();
            WBexp = getWBexp();
            currentActivities = getCurrentActivities();
        }
    }
}