export type Question = {
	question: string;
	answers: string[];
	correctAnswerIndex: number;
	answeredIndex: number | undefined;
	genericFeedback: string;
	specificFeedback?: string[];
};
export type Questionnaire = {
	questions: Question[];
};

export type UserProfile = {
	firstname: string;
	lastname: string;
	username: string;
	nic: string;
	phoneNumber: string;
	email: string;
	profilePictureUrl: string;
};
export type LocalProfile = {
	id?: number | undefined;
	nic: string;
	marks?: number;
	questions: Question[];
};
// export type PlayerList = {

// }
