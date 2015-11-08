using UnityEngine;
using System.Collections.Generic;

public class Activity {

	string title;
	string format;
	List<string> topicsOfInterest;
	string description;
	string startDate; // needs a proper date format
	string endDate;   // needs a proper date format
	bool joinAnytime;
	int lengthOfEngagement; // May need to make a seperate enum for this. day, week, month, semester, year, summer
	string location;
	List<string> recurrence;
	bool coop;
	List<int> academicStanding; // May need to make this an enum as well. year 1,2,3,4,5,NU IN, last year or last semester
	string major;
	bool resident;
	string sponsor;
	string contactName;
	string contactOffice;
	string contactEmail;
	List<string> learningOutcomes;
	List<Skill> skills;
	string engagement;

    Activity()
    {
    }
}
