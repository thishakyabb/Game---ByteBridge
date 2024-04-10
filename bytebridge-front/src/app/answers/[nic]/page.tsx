"use client";
import { get_local_profile } from "@/apis";
import Question from "@/components/Question";
import { Question as q } from "@/types";
import { motion } from "framer-motion";
import { Roboto_Mono } from "next/font/google";
import { useParams } from "next/navigation";
import { useEffect, useState } from "react";
const roboto = Roboto_Mono({ weight: "400", subsets: ["latin"] });

export default function Answers() {
	const params = useParams();
	const [questions, setQuestions] = useState<q[]>([]);
	const [percentage, setPercentage] = useState(0);
	useEffect(() => {
		const populate = async () => {
			//@ts-ignore
			const response = await get_local_profile(params.nic);
			setPercentage(
				(response.data.marks / response.data.questions.length) * 100
			);

			setQuestions(response.data.questions);
		};
		populate();
	}, []);

	return (
		<div
			style={{
				background: "radial-gradient(circle, white 0%, #999999 100%)",
			}}
			className="min-h-screen overflow-auto flex flex-col"
		>
			<div className="text-4xl text-black pl-5 py-5 mb-10">Answers</div>
			<motion.div className=" w-3/4 h-16 border-black border-1 relative flex m-10 items-center mb-16 self-center">
				<motion.div
					className="   mix-blend-difference z-20 margin-auto w-full text-center align-middle text-3xl"
					style={roboto.style}
				>
					{percentage}%
				</motion.div>
				<motion.div
					className="absolute h-full bg-black z-10"
					initial={{
						width: "0%",
					}}
					animate={{
						width: `${percentage}%`,
						transition: {
							delay: 0.5,
							type: "spring",
							stiffness: 100,
							damping: 30,
						},
					}}
				></motion.div>
			</motion.div>
			{questions.map((question, index) => (
				<div className="my-10">
					<Question
						mode="answer"
						questionState={question}
						key={index}
						index={index + 1}
					/>
				</div>
			))}
		</div>
	);
}
