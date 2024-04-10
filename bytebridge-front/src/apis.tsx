import axios from "axios";
import { LocalProfile } from "./types";

export function sleep(ms: number) {
	return new Promise((resolve) => setTimeout(resolve, ms));
}
const API_KEY = process.env.NEXT_PUBLIC_API_KEY;
const instance = axios.create({
	baseURL: "http://20.15.114.131:8080/api",
	timeout: 1000,
	headers: {
		Authorization: `Bearer ${API_KEY}`,
	},
});

const local_instance = axios.create({
	baseURL: "http://localhost:8080/api",
	timeout: 1000,
});
async function authenticate() {
	// const response = await instance.post("/login", {
	// 	apiKey: API_KEY,
	// });
	await sleep(1000);
	// return response.data.token;
	return "token";

	// response.data['token']
}
async function get_user_profile() {
	return await instance.get("/user/profile/view");
}

async function complete_questionnaire(data: LocalProfile) {
	return await local_instance.post("/profiles/create", data);
}
async function get_local_profile(nic: string) {
	return await local_instance.get(`/profiles/${nic}`);
}
async function isAuthorizedForQuestionnare(nic: string) {
	return await local_instance.get(
		`/profiles/authorizedforquestionnaire/${nic}`
	);
}

export default instance;
export {
	authenticate,
	get_user_profile,
	complete_questionnaire,
	get_local_profile,
	isAuthorizedForQuestionnare,
};
