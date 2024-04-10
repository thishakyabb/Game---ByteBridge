import React from "react";
export default function FormHolder({
	label,
	formref,
}: {
	label: string;
	formref: React.RefObject<HTMLInputElement>;
}) {
	return (
		<div className="grid md:grid-flow-col grid-flow-row items-center">
			<div className="text-xl md:text-2xl text-black pl-5 py-5">{label} :</div>
			<input
				ref={formref}
				className="border-black border-1 min-w-56 bg-transparent ring-0 outline-none focus:border-2 p-3 "
			></input>
		</div>
	);
}
