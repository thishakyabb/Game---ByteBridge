export default function AnswerHolder({ answerlist }: { answerlist: string[] }) {
	return (
		<div className="h-screen w-screen flex flex-col justify-center">
			{answerlist.map((answer, index) => (
				<div key={index} className="flex justify-center items-center">
					<div className="text-2xl text-black">{answer}</div>
				</div>
			))}
		</div>
	);
}
