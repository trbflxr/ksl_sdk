using KSL.API;
using UnityEngine;

namespace Example {
	public class PrefsStorageExample {
		private float savedFloat_;
		private Vector2 savedVec2_;

		// Simple prefs wrapper example, good for beginning. See PrefsWrapperSimple.cs
		private readonly PrefsWrapperSimple prefsWrapperSimple_ = new();

		// Proper way to use IPrefsStorage takes a bit more time to set up but significantly reduces chance to mess up. See PrefsWrapperAdvanced.cs
		private readonly PrefsWrapperAdvanced prefsWrapperAdvanced_ = new();

		public void OnInit() {
			// Here is an example of using simple prefs storage
			// You can load and save values directly, or create a wrapper over Kino.Prefs
			if (!Kino.Prefs.TryGet("float_key", out savedFloat_, 150.0f)) {
				Kino.Log.Info("Created float value in prefs storage");
			}

			Kino.Log.Info($"Float value: {savedFloat_}");

			if (!Kino.Prefs.TryGet("vec2_key", out savedVec2_, Vector2.one)) {
				Kino.Log.Info("Created vec2 value in prefs storage");
			}

			Kino.Log.Info($"Vec2 value: {savedVec2_}");

			// Have to be called manually on mod / extension initialization step
			prefsWrapperSimple_.Load();
		}

		public void Draw() {
			Kino.UI.Label("Example Prefs usage");

			if (Kino.UI.ContextButton("Simple prefs wrapper")) {
				Kino.UI.PushContext(UISimplePrefsWrapper, "Simple prefs");
			}

			if (Kino.UI.ContextButton("Advanced prefs wrapper")) {
				Kino.UI.PushContext(UIAdvancedPrefsWrapper, "Advanced prefs");
			}
		}

		private void UISimplePrefsWrapper() {
			// We have to do it like this because C# do not allow to pass properties by ref
			bool boolFlag = prefsWrapperSimple_.BoolFlag;
			if (Kino.UI.Toggle("BoolFlag", ref boolFlag)) {
				prefsWrapperSimple_.BoolFlag = boolFlag;
			}

			Kino.UI.GroupLabel("StringValue");
			string stringValue = prefsWrapperSimple_.StringValue;
			if (Kino.UI.Input(ref stringValue)) {
				prefsWrapperSimple_.StringValue = stringValue;
			}
		}

		private void UIAdvancedPrefsWrapper() {
			bool myBoolParam = prefsWrapperAdvanced_.MyBoolParam;
			if (Kino.UI.Toggle("MyBoolParam", ref myBoolParam)) {
				prefsWrapperAdvanced_.MyBoolParam.Value = myBoolParam;
			}

			float myFloatParam = prefsWrapperAdvanced_.MyFloatParam;
			if (Kino.UI.Slider(ref myFloatParam, 0.0f, 1000.0f, $"MyFloatParam: {myFloatParam}")) {
				prefsWrapperAdvanced_.MyFloatParam.Value = myFloatParam;
			}

			float myIntParam = prefsWrapperAdvanced_.MyIntParam;
			if (Kino.UI.SliderWithButtons(ref myIntParam, 0.0f, 10.0f, 1.0f, $"MyIntParam: {myIntParam:F0}")) {
				prefsWrapperAdvanced_.MyIntParam.Value = (int)myIntParam;
			}
		}
	}
}