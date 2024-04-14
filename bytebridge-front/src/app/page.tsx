"use client";
import React, { useState, useEffect } from "react";
import { Unity, useUnityContext } from "react-unity-webgl";
export default function Home() {
	const { unityProvider } = useUnityContext({
		loaderUrl: "/unitybuild/webgl_build.loader.js",
		dataUrl: "/unitybuild/webgl_build.data",
		frameworkUrl: "/unitybuild/webgl_build.framework.js",
		codeUrl: "/unitybuild/webgl_build.wasm",
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
				style={{ width: 800, height: 600 }}
				// devicePixelRatio={devicePixelRatio}
			/>
		</div>
	);
}
