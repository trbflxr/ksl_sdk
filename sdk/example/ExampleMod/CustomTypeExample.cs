using KSL.API;

namespace Example {
	public class GraphicsSettings {
		public bool Vsync;
		public bool DrawShadows;
		public float RenderDistance;
	}

	public class GraphicsSettingsParser : TypeParser<GraphicsSettings> {
		protected override string ToRawString(GraphicsSettings value) {
			return $"{value.Vsync},{value.DrawShadows},{value.RenderDistance:F2}";
		}

		protected override GraphicsSettings Parse(string rawData) {
			var result = new GraphicsSettings();

			if (string.IsNullOrWhiteSpace(rawData)) {
				return result;
			}

			var parts = rawData.Split(',');
			if (parts.Length != 3) {
				return result;
			}

			bool.TryParse(parts[0], out result.Vsync);
			bool.TryParse(parts[1], out result.DrawShadows);
			float.TryParse(parts[2], out result.RenderDistance);

			return result;
		}
	}
}