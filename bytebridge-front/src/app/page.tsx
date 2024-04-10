"use client";
import { authenticate } from "../apis";
import { Avatar, Button, Spinner } from "@nextui-org/react";
import { AnimatePresence, motion, useAnimation } from "framer-motion";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";

export default function Home() {
	const controls = useAnimation();
	const [authenticated, setAuthenticated] = useState(false);
	const router = useRouter();
	useEffect(() => {
		const fetchData = async () => {
			const token = await authenticate();
			if (token) {
				controls.start({
					background: "radial-gradient(circle, white 0%, #999999 100%)",
					transition: { duration: 0.5 },
				});
				setAuthenticated(true);
			}
		};
		fetchData();
	}, []);
	return (
		<AnimatePresence>
			<motion.div
				className="h-screen w-screen flex justify-center items-center flex-col"
				initial={{
					background: "radial-gradient(circle, white 0%, #fffffff 100%)",
				}}
				animate={controls}
				layout
			>
				<motion.div className="text-7xl lg:text-9xl text-black mb-20" layout>
					Byte<span>//</span>
					<br />
					Bridge
				</motion.div>
				{!authenticated && (
					<>
						<motion.div
							initial={{ opacity: 0 }}
							animate={{ opacity: 1 }}
							exit={{ opacity: 0 }}
							layout
						>
							<Spinner size="lg" />
						</motion.div>
						<motion.div layout className="text-black py-3">
							Authenticating...
						</motion.div>
					</>
				)}
				{authenticated && (
					<>
						<motion.div layout className="py-2">
							<Button
								variant="shadow"
								size="lg"
								color="primary"
								onClick={() => router.push("/questionnaire")}
								className=""
							>
								Proceed
							</Button>
						</motion.div>
						<motion.div layout className="py-2">
							<Button
								variant="shadow"
								size="lg"
								color="default"
								startContent={<Avatar />}
								onClick={() => router.push("/profile")}
							>
								Profile
							</Button>
						</motion.div>
					</>
				)}
			</motion.div>
		</AnimatePresence>
	);
}
