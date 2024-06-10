"use client";
import React, { useState, useEffect } from "react";
import { Unity, useUnityContext } from "react-unity-webgl";
export default function Home() {
	const { unityProvider } = useUnityContext({
		loaderUrl: "/unitybuild/unitybuild.loader.js",
		dataUrl: "/unitybuild/unitybuild.data",
		frameworkUrl: "/unitybuild/unitybuild.framework.js",
		codeUrl: "/unitybuild/unitybuild.wasm",
	});
	// const [devicePixelRatio, setDevicePixelRatio] = useState(() => {
	// 	return window ? (window.devicePixelRatio ? window.devicePixelRatio : 1) : 1;
	// });
	// useEffect(
	// 	function () {
	// 		const updateDevicePixelRatio = function () {
	// 			if (window) {
	// 				setDevicePixelRatio(window.devicePixelRatio);
	// 			} else {
	// 				setDevicePixelRatio(1);
	// 			}
	// 		};
	// 		if (window) {
	// 			const mediaMatcher = window.matchMedia(
	// 				`screen and (resolution: ${devicePixelRatio}dppx)`
	// 			);
	// 			mediaMatcher.addEventListener("change", updateDevicePixelRatio);
	// 			return function () {
	// 				mediaMatcher.removeEventListener("change", updateDevicePixelRatio);
	// 			};
	// 		}
	// 		return function () {};
	// 	},
	// 	[devicePixelRatio]
	// );
	return (
		<div>
			<Unity
				unityProvider={unityProvider}
				style={{ width:1280, height: 720}}
				// devicePixelRatio={devicePixelRatio}
			/>
		</div>
	);
}
