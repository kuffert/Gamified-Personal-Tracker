﻿using UnityEngine;
using System.Collections.Generic;
using System;
// Author: Chris Kuffert
// Date: 11/8/2015

[Serializable]
public class Opportunity {

	private string id;
	private string title;
	private string format;
	private string[] topicsOfInterest;
	private string description;
	private string startDate; // needs a proper date format
	private string endDate;   // needs a proper date format
	private bool joinAnytime;
	private int lengthOfEngagement; // May need to make a seperate enum for this. day, week, month, semester, year, summer
	private string location;
	private string[] recurrence;
	private bool coop;
	private string[] academicStanding; // May need to make this an enum as well. year 1,2,3,4,5,NU IN, last year or last semester
	private string major;
	private bool resident;
	private string sponsor;
	private string contactName;
	private string contactOffice;
	private string contactEmail;
	private string[] learningOutcomes;
	private List<Skill> skills;
	private string engagement;

    public Opportunity()
    {
        this.id = string.Empty;
        this.title = string.Empty;
        this.format = string.Empty;
        this.topicsOfInterest = new string[0];
        this.description = string.Empty;
        this.startDate = string.Empty;
        this.endDate = string.Empty;
        this.joinAnytime = false;
        this.lengthOfEngagement = 0;
        this.location = string.Empty;
        this.recurrence = new string[0];
        this.coop = false;
        this.academicStanding = new string[0];
        this.major = string.Empty;
        this.resident = false;
        this.sponsor = string.Empty;
        this.contactName = string.Empty;
        this.contactOffice = string.Empty;
        this.contactEmail = string.Empty;
        this.learningOutcomes = new string[0];
        this.skills = new List<Skill>();
        this.engagement = string.Empty;
    }

	public string Id {
		get {
			return this.id;
		}
		set {
			id = value;
		}
	}

	public string Title {
		get {
			return this.title;
		}
		set {
			title = value;
		}
	}

	public string Format {
		get {
			return this.format;
		}
		set {
			format = value;
		}
	}

	public string[] TopicsOfInterest {
		get {
			return this.topicsOfInterest;
		}
		set {
			topicsOfInterest = value;
		}
	}

	public string Description {
		get {
			return this.description;
		}
		set {
			description = value;
		}
	}

	public string StartDate {
		get {
			return this.startDate;
		}
		set {
			startDate = value;
		}
	}

	public string EndDate {
		get {
			return this.endDate;
		}
		set {
			endDate = value;
		}
	}

	public bool JoinAnytime {
		get {
			return this.joinAnytime;
		}
		set {
			joinAnytime = value;
		}
	}

	public int LengthOfEngagement {
		get {
			return this.lengthOfEngagement;
		}
		set {
			lengthOfEngagement = value;
		}
	}

	public string Location {
		get {
			return this.location;
		}
		set {
			location = value;
		}
	}

	public string[] Recurrence {
		get {
			return this.recurrence;
		}
		set {
			recurrence = value;
		}
	}

	public bool Coop {
		get {
			return this.coop;
		}
		set {
			coop = value;
		}
	}
 
	public string[] AcademicStanding {
		get {
			return this.academicStanding;
		}
		set {
			academicStanding = value;
		}
	}

	public string Major {
		get {
			return this.major;
		}
		set {
			major = value;
		}
	}

	public bool Resident {
		get {
			return this.resident;
		}
		set {
			resident = value;
		}
	}

	public string Sponsor {
		get {
			return this.sponsor;
		}
		set {
			sponsor = value;
		}
	}

	public string ContactName {
		get {
			return this.contactName;
		}
		set {
			contactName = value;
		}
	}

	public string ContactOffice {
		get {
			return this.contactOffice;
		}
		set {
			contactOffice = value;
		}
	}

	public string ContactEmail {
		get {
			return this.contactEmail;
		}
		set {
			contactEmail = value;
		}
	}

	public string[] LearningOutcomes {
		get {
			return this.learningOutcomes;
		}
		set {
			learningOutcomes = value;
		}
	}

	public List<Skill> Skills {
		get {
			return this.skills;
		}
		set {
			skills = value;
		}
	}

	public string Engagement {
		get {
			return this.engagement;
		}
		set {
			engagement = value;
		}
	}

    // calculates the Experience values of this opportunity, right now at a weight of 1 per dimension listing
    // Craig Hammond
    public Experience EXP()
    {
        Experience exp = Experience.DefaultExperience();

        foreach (Skill skill in this.skills)
        {
            foreach(string dimension in skill.Dimensions)
            {
                if (exp.totals.ContainsKey(dimension))
                {
                    exp.totals[dimension] += 1;
                }
                else
                {
                    exp.add(dimension, 1);
                }
            }
        }

        return exp;
    }
}