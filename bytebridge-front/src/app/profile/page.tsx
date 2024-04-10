"use client";
import FormHolder from "@/components/FormHolder";
import { AnimatePresence, motion } from "framer-motion";
import { useRef } from "react";
import { Roboto_Mono } from "next/font/google";
import { Button } from "@nextui-org/button";
import { useSearchParams } from "next/navigation";

const roboto = Roboto_Mono({ weight: "400", subsets: ["latin"] });
export default function Profile() {
	const nameref = useRef<HTMLInputElement>(null);
	const nicref = useRef<HTMLInputElement>(null);
	const usernameref = useRef<HTMLInputElement>(null);
	const mobileref = useRef<HTMLInputElement>(null);
	const emailref = useRef<HTMLInputElement>(null);
	const next = useSearchParams().get("next");
	return (
		<AnimatePresence>
			<motion.div
				className="grid grid-flow-row text-black min-h-screen overflow-auto"
				style={{
					background: "radial-gradient(circle, white 0%, #999999 100%)",
				}}
			>
				<div className="text-4xl text-black pl-5 py-5 pb-10">My Profile</div>
				<div className="grid grid-flow-row gap-3 justify-center md:justify-start md:ml-10">
					<FormHolder formref={nameref} label="Name" />
					<FormHolder formref={nicref} label="Nic no" />
					<FormHolder formref={usernameref} label="Username" />
					<FormHolder formref={mobileref} label="Mobile" />
					<FormHolder formref={emailref} label="Email" />
				</div>
				{next && (
					<div
						className="justify-center flex items-center pt-5 px-3 text-center"
						style={roboto.style}
					>
						You must complete your profile before proceeding to game
					</div>
				)}

				<div className="flex justify-center items-center pb-5">
					<Button
						variant="shadow"
						size="lg"
						onClick={() => {
							console.log("submit");
						}}
						color="primary"
					>
						{next ? "Save and Proceed" : "Save"}
					</Button>
				</div>
			</motion.div>
		</AnimatePresence>
	);
}
