using KSL.API;

namespace Example {
	// This is an example config data with custom type
	public interface IConfigData {
		// custom type example
		public GraphicsSettings Graphics { get; set; }

		// input settings
		public bool InvertMouse { get; set; }
		public bool HideCrosshair { get; set; }
	}

	// Config data wrapper. Used for initialization config data and easy access to it from anywhere in the code
	public static class Config {
		public static IConfigData Data { get; private set; }

		public static bool Init() {
			// First you have to register parsers for all custom types in the config. In this case there is only UISettings.
			// Then you can register config itself.
			Data = Kino.Config
				.RegisterParser<GraphicsSettingsParser>()
				.RegisterConfig<IConfigData>();

			if (Data == null) {
				return false;
			}

			EnsureDefaultValues();

			return true;
		}

		public static void Save() {
			Kino.Config.Save<IConfigData>();
		}

		// You can ensure that config has proper values set
		// In case if this is new config or you added new properties to already created
		private static void EnsureDefaultValues() {
			if (Data.Graphics.RenderDistance <= 0.0f) {
				Data.Graphics.RenderDistance = 1000.0f;
			}
		}
	}
}