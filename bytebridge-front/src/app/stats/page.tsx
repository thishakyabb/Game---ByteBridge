"use client";
import { Button } from "@nextui-org/react";
import { motion } from "framer-motion";
import { Roboto_Mono } from "next/font/google";
const roboto = Roboto_Mono({ weight: "400", subsets: ["latin"] });
export default function Stats() {
	const percentage = 69;
	return (
		<motion.div
			style={{
				background: "radial-gradient(circle, white 0%, #999999 100%)",
			}}
			className="overflow-auto  min-h-screen w-screen grid grid-rows-[1fr_2fr] relative"
		>
			<div className="text-4xl text-black pl-5 py-5 pb-10">Stats</div>
			<div className=" flex-col flex items-center">
				<motion.div className=" w-3/4 h-16 border-black border-1 relative flex m-10 items-center mb-16">
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
				<motion.div className="mb-5">
					<Button size="lg" color="primary" variant="shadow">
						Check answers
					</Button>
				</motion.div>
				<motion.div className="mb-5">
					<Button size="lg" color="primary" variant="shadow">
						Proceed to Game
					</Button>
				</motion.div>
			</div>
		</motion.div>
	);
}
