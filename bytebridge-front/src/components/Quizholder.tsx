import { useEffect, useRef, useState } from "react";
import { LocalProfile, Questionnaire } from "@/types";
import { Button } from "@nextui-org/react";
import {
	AnimatePresence,
	motion,
	useAnimationControls,
	useMotionValue,
} from "framer-motion";
import { Roboto_Mono } from "next/font/google";
import Question from "./Question";
import { Question as Question_type } from "@/types";
import { complete_questionnaire } from "@/apis";

const roboto = Roboto_Mono({ weight: "400", subsets: ["latin"] });
export default function Quizholder({
	questionnaire,
	nic,
}: {
	questionnaire: Questionnaire;
	nic: string;
}) {
	const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0);
	const [questionState, setQuestionState] = useState(
		questionnaire.questions[currentQuestionIndex]
	);
	const [submitAnswer, setSubmitAnswer] = useState(false);
	const [questionnaireState, setQuestionnaire] =
		useState<Questionnaire>(questionnaire);
	// const [answeredIndex, setAnsweredIndex] = useState<number | undefined>(
	// 	undefined
	// );
	const toastText = useMotionValue("");
	const toastControls = useAnimationControls();
	const fadeoutTimeout = useRef<NodeJS.Timeout | null>(null);

	useEffect(() => {
		setQuestionState(questionnaire.questions[currentQuestionIndex]);
	}, [currentQuestionIndex]);

	function removeSpecificFeedback(questions: Question_type[]): Question_type[] {
		return questions.map(({ specificFeedback, ...question }) => question);
	}
	const finish = async () => {
		let newQuestions = removeSpecificFeedback(questionnaireState.questions);
		const test_profile: LocalProfile = {
			nic: nic,
			questions: newQuestions,
		};
		const response = await complete_questionnaire(test_profile);
		console.log(response.data.marks);
	};
	const spring = {
		type: "spring",
		stiffness: 80,
		damping: 12,
	};
	return (
		<AnimatePresence>
			<motion.div className="w-screen overflow-auto  grid grid-flow-row" layout>
				<div className="text-4xl text-black pl-5 py-5">
					Question {currentQuestionIndex + 1}
				</div>
				<AnimatePresence mode="wait">
					<Question
						questionState={questionState}
						setQuestionnaire={setQuestionnaire}
						currentQuestionIndex={currentQuestionIndex}
						key={currentQuestionIndex}
						mode="question"
						submitAnswer={submitAnswer}
					/>
				</AnimatePresence>
				<div className="flex  justify-end mx-10 items-center my-5 z-10">
					{submitAnswer ? (
						<Button
							color={
								currentQuestionIndex == questionnaire.questions.length - 1
									? "success"
									: "primary"
							}
							size="lg"
							onClick={async () => {
								if (
									currentQuestionIndex ==
									questionnaire.questions.length - 1
								) {
									await finish();
								}
								if (currentQuestionIndex < questionnaire.questions.length - 1) {
									toastText.set(questionState.genericFeedback);
									setCurrentQuestionIndex((prev) => prev + 1);
									setSubmitAnswer(false);
									await toastControls.start({
										opacity: 1,
										scale: 1,
										// y: -150,
										filter: "blur(0px)",
										zIndex: 100,
									});
									if (fadeoutTimeout.current !== null) {
										clearTimeout(fadeoutTimeout.current);
									}

									fadeoutTimeout.current = setTimeout(async () => {
										await toastControls.start({
											opacity: 0,
											// y: 100,
											filter: "blur(10px)",
											zIndex: 0,
											scale: 1.05,
										});
									}, 10000);
								}
							}}
						>
							{currentQuestionIndex == questionnaire.questions.length - 1
								? "Finish"
								: "Next"}
						</Button>
					) : (
						<Button
							color={
								questionState.answeredIndex !== undefined
									? "success"
									: "default"
							}
							size="lg"
							onClick={() => {
								if (questionState.answeredIndex !== undefined) {
									setSubmitAnswer(true);
								}
							}}
						>
							Submit Answer
						</Button>
					)}
				</div>
			</motion.div>
			<motion.div
				initial={{ opacity: 0 }}
				className="absolute bottom-10 w-screen flex justify-center flex-col text-2xl"
				// style={{ filter: "blur(10px)" }}
				animate={toastControls}
			>
				<div
					className=" bg-black rounded-md py-3 px-5 md:mx-28 mx-10 text-white"
					style={roboto.style}
				>
					<div>
						<div
							onClick={async () => {
								if (fadeoutTimeout.current !== null) {
									clearTimeout(fadeoutTimeout.current);
								}
								await toastControls.start({
									opacity: 0,
									// y: 100,
									filter: "blur(10px)",
									zIndex: 0,
									scale: 1.05,
								});
							}}
							className="text-right mb-3 font-bold underline absolute top-[-30px] bg-red-500 cursor-pointer hover:scale-105"
						>
							Close [X]
						</div>
						{toastText.get()}
					</div>
				</div>
			</motion.div>
		</AnimatePresence>
	);
}
