using System;
using System.Reflection;
using System.ComponentModel;

// Author: Yvette Kim
// Date: 11/16
public enum OpportunityFormat
{
	[Description("Special Topics Discussion")]
	SpecialTopicsDiscussion,
	[Description("Event")]
	EventEnum,
	[Description("Alternative Spring Break")]
	AlternativeSpringBreak,
	[Description("Education, Training, and Skills Development")]
	EducationTrainingSkillsDevelopment,
	[Description("Peer Mentee")]
	PeerMentee,
	[Description("Peer Mentor")]
	PeerMentor,
	[Description("Leadership Role")]
	LeadershipRole,
	[Description("Program Facilitator")]
	ProgramFacilitator,
	Teaching,
	Officer,
	Conferences,
	Retreats,
	[Description("Student Clubs and Organizations")]
	StudentClubsOrganizations,
	Project,
	Activity,
	[Description("Club Sports")]           
	ClubSports,
	Intramurals,
	Series,
	Course,
	[Description("Training Development Program")]
	TrainingDevelopmentProgram,
	Other
}