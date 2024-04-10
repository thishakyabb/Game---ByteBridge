import type { Metadata } from "next";
export const metadata: Metadata = {
	title: "ByteBridge",
};
import { Major_Mono_Display } from "next/font/google";
import "./globals.css";
import { Providers } from "../providers";
const majormono = Major_Mono_Display({ weight: "400", subsets: ["latin"] });

export default function RootLayout({
	children,
}: Readonly<{
	children: React.ReactNode;
}>) {
	return (
		<html lang="en">
			<body
				className={`${majormono.className} overflow-auto min-h-screen  w-screen`}
				style={{ backgroundImage: "unset !important" }}
			>
				<Providers> {children}</Providers>
			</body>
		</html>
	);
}
