using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class Activity {

	string title;
	ArrayList topicsOfInterest;
	string description;
	string startDate; // needs a proper date format
	string endDate;   // needs a proper date format
	bool joinAnytime;
	int lengthOfEngagement; // May need to make a seperate enum for this. day, week, month, semester, year, summer
	string location;
	string recurrence;
	int academicStanding; // May need to make this an enum as well. year 1,2,3,4,5
	string major; 

}
