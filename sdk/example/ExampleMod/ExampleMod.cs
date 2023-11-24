using System.Reflection;
using KSL.API;
using UnityEngine;

namespace Example {
	[KSLMeta("ExampleMod", "1.0.0", "trbflxr")]
	public class ExampleMod : BaseMod {
		private readonly UIExample uiExample_ = new();
		private readonly PrefsStorageExample prefsExample_ = new();
		private readonly Synchronization syncExample_ = new();

		private void Start() {
			Kino.Log.Info("ExampleMod started!");

			// Simple prefs storage example
			prefsExample_.OnInit();

			// Advanced config usage
			bool cfgRes = Config.Init();
			Kino.Log.Info($"Config initialization result: {cfgRes}");

			RegisterHotkeys();

			syncExample_.OnInit();
		}

		public override void OnUIDraw() {
			uiExample_.Draw();

			Kino.UI.HorizontalLine();

			prefsExample_.Draw();

			Kino.UI.HorizontalLine();

			syncExample_.Draw();
		}

		public override void OnAdditionalAboutUIDraw() {
			Kino.UI.Label("Additional about info...");
			Kino.UI.Label("Here you can display more info about the mod, developers and etc.");

			Kino.UI.HyperlinkLabel("Visit our<link>", "discord", "https://discord.gg/kinomod");
		}

		public override Texture2D GetIcon() {
			var assembly = Assembly.GetExecutingAssembly();
			return Kino.Utils.LoadEmbeddedTexture(assembly, "Example.Resources.icon.png");
		}

		private void RegisterHotkeys() {
			Kino.Input.Bind(new[] { KeyCode.LeftControl, KeyCode.H }, PrintHello, "Hello action");
			Kino.Input.Bind(KeyCode.J, ContinuousAction, "Continuous action", ActionType.Hold);
			Kino.Input.Bind(KeyCode.K, syncExample_.TestSendToAll, "Test packet send");
		}

		private void PrintHello() {
			Kino.Log.Info("Hello");
		}

		private void ContinuousAction() {
			Kino.Log.Info("Printing info");
		}
	}
}