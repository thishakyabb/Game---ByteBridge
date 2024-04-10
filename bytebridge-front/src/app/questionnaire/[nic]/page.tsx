"use client";
import { isAuthorizedForQuestionnare } from "@/apis";
import Quizholder from "@/components/Quizholder";
import questions from "@/questions";
import { AnimatePresence, motion } from "framer-motion";
import { useParams, useRouter } from "next/navigation";
import { useEffect, useState } from "react";

export default function Questionnaire() {
	const [intro, setIntro] = useState(true);
	const [promptText, setPromptText] = useState("Lets play a game...");
	const router = useRouter();
	const params = useParams();
	useEffect(() => {
		// let userNic = localStorage? localStorage.getItem("nic"):null
		const nic = params.nic;

		//@ts-ignore
		isAuthorizedForQuestionnare(nic).then((e) => {
			console.log(e.data);

			if (e.data) {
				//user is authorized to take the quiz
				setTimeout(() => {
					setIntro(false);
				}, 2500);
			} else {
				setPromptText("You are not authorized to retake the quiz");
			}
		});
	}, []);
	return (
		<div
			style={{
				background: "radial-gradient(circle, white 0%, #999999 100%)",
			}}
			className="overflow-auto  min-h-screen w-screen flex"
		>
			<AnimatePresence>
				{intro && (
					<motion.div
						layout
						initial={{ opacity: 0, y: 100 }}
						animate={{ opacity: 1, y: 0 }}
						exit={{ opacity: 0 }}
					>
						<div className="text-6xl text-black h-screen w-screen flex justify-center items-center ">
							{promptText}
						</div>
					</motion.div>
				)}
				{!intro && (
					<Quizholder
						//@ts-ignore
						nic={params.nic}
						questionnaire={{
							questions: questions,
						}}
					/>
				)}
			</AnimatePresence>
		</div>
	);
}
