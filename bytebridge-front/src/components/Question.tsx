import { motion } from "framer-motion";
import { Roboto_Mono } from "next/font/google";
const roboto = Roboto_Mono({ weight: "400", subsets: ["latin"] });
import { Question, Questionnaire } from "@/types";
export default function Question({
	questionState,
	setQuestionnaire,
	currentQuestionIndex,
	mode,
	index,
	submitAnswer,
	finish,
}: {
	questionState: Question;
	setQuestionnaire?: any | null;
	currentQuestionIndex?: number | null;
	mode: "question" | "answer";
	index?: number | undefined;
	submitAnswer?: boolean | undefined;
	finish?: () => void;
}) {
	function generate_classnames(index: number) {
		const base = "border-1  text-2xl px-5 py-3 hover:cursor-pointer";
		if (mode == "question") {
			if (questionState.answeredIndex == index) {
				return `${base} bg-black text-white border-black`;
			} else {
				return `${base} text-black border-black`;
			}
		}
		if (mode == "answer") {
			if (questionState.answeredIndex == index) {
				if (questionState.answeredIndex == questionState.correctAnswerIndex) {
					return `${base} bg-green-500 text-white border-green-500`;
				}
				return `${base} bg-black text-red border-red-500 border-5`;
			} else if (questionState.correctAnswerIndex == index) {
				return `${base} bg-green-500 text-white border-green-500`;
			}

			return `${base} text-black border-black border-black`;
		}
	}
	return (
		<motion.div
			layout
			className="flex justify-center items-center flex-col"
			initial={{ opacity: 0, y: 0 }}
			animate={{ opacity: 1 }}
			exit={{ opacity: 0 }}
			key={currentQuestionIndex}
		>
			<motion.div
				className={`text-3xl text-black py-3 px-5  text-center mx-10 md:mx-24  bg-white ${roboto.className}`}
				initial={{ boxShadow: "0px 0px 0px 0px #000000" }}
				animate={{
					boxShadow: "6px 6px 0px 0px #000000",
					transition: {
						delay: 0.5,
						duration: 0.3,
					},
				}}
			>
				{mode == "answer"
					? `${index}. ${questionState.question}`
					: submitAnswer &&
					  questionState.answeredIndex &&
					  questionState.specificFeedback
					? questionState.specificFeedback[questionState.answeredIndex]
					: questionState.question}
			</motion.div>
			<div className="grid grid-flow-row my-5 gap-2 mx-16 md:mx-32">
				{questionState.answers.map((answer: any, index: any) => (
					<motion.div
						className={generate_classnames(index)}
						style={roboto.style}
						key={index}
						onClick={() => {
							if (
								mode == "question" &&
								currentQuestionIndex != undefined &&
								!submitAnswer
							) {
								setQuestionnaire((prevState: Questionnaire) => {
									const newQuestionnaire = { ...prevState };
									newQuestionnaire.questions[
										currentQuestionIndex
									].answeredIndex = index;
									return newQuestionnaire;
								});
							}
						}}
					>
						{answer}
					</motion.div>
				))}
			</div>
		</motion.div>
	);
}
