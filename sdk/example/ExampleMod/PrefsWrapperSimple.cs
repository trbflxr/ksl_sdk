using KSL.API;

namespace Example {
	public class PrefsWrapperSimple {
		private bool boolFlag_;
		public bool BoolFlag {
			get => boolFlag_;
			set {
				if (boolFlag_ != value) {
					Kino.Prefs.Set("bool_flag", value);
				}

				boolFlag_ = value;
			}
		}

		private string stringValue_;
		public string StringValue {
			get => stringValue_;
			set {
				if (stringValue_ != value) {
					Kino.Prefs.Set("string_value", value);
				}

				stringValue_ = value;
			}
		}

		public void Load() {
			boolFlag_ = Kino.Prefs.Get("bool_flag", true);
			stringValue_ = Kino.Prefs.Get("string_value", "never gonna give you up");
		}
	}
}