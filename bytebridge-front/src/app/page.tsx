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
	const [devicePixelRatio, setDevicePixelRatio] = useState(
		window.devicePixelRatio
	);
	useEffect(
		function () {
			// A function which will update the device pixel ratio of the Unity
			// Application to match the device pixel ratio of the browser.
			const updateDevicePixelRatio = function () {
				setDevicePixelRatio(window.devicePixelRatio);
			};
			// A media matcher which watches for changes in the device pixel ratio.
			const mediaMatcher = window.matchMedia(
				`screen and (resolution: ${devicePixelRatio}dppx)`
			);
			// Adding an event listener to the media matcher which will update the
			// device pixel ratio of the Unity Application when the device pixel
			// ratio changes.
			mediaMatcher.addEventListener("change", updateDevicePixelRatio);
			return function () {
				// Removing the event listener when the component unmounts.
				mediaMatcher.removeEventListener("change", updateDevicePixelRatio);
			};
		},
		[devicePixelRatio]
	);
	return (
		<div>
			<Unity
				unityProvider={unityProvider}
				style={{ width: 800, height: 600 }}
				devicePixelRatio={devicePixelRatio}
			/>
		</div>
	);
}
