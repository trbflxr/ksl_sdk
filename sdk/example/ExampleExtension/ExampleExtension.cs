using System.Reflection;
using KSL.API;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Example {
	[KSLMeta("ExampleExt", "1.0.0", "trbflxr")]
	public class ExampleExtension : BaseExtension {
		public override void OnStart() {
			Kino.Log.Info("ExampleExtension started!");
		}

		public override void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
			Kino.Log.Info($"Scene loaded: {scene.name}, mode: {mode}");
		}

		public override void OnUIDraw() {
			Kino.UI.Label("Wellcome to ExampleExtension!");

			Kino.UI.Label("For more detailed example of using KSL API check out ExampleMod project.");

			if (Kino.UI.Button("Press me")) {
				Kino.Log.Info("Pressed");
			}
		}

		public override void OnAdditionalAboutUIDraw() {
			Kino.UI.Label("Additional about info...");

			Kino.UI.HyperlinkLabel("Visit our<link>", "discord", "https://discord.gg/kinomod");
		}

		public override Texture2D GetIcon() {
			var assembly = Assembly.GetExecutingAssembly();
			return Kino.Utils.LoadEmbeddedTexture(assembly, "Example.Resources.icon.png");
		}
	}
}