using KSL.API;

namespace Example {
	public class PrefsValue<T> {
		public string Key { get; }

		private T value_;
		public T Value {
			get => value_;
			set {
				if (!value_.Equals(value)) {
					Kino.Prefs.Set(Key, value);
				}

				value_ = value;
			}
		}

		public PrefsValue(string key, T defaultValue = default) {
			Key = key;
			value_ = Kino.Prefs.Get(Key, defaultValue);
		}

		// Override operator T so it can be used without need to call .Value 
		public static implicit operator T(PrefsValue<T> value) {
			return value.value_;
		}
	}

	// By using IPrefsStorage in this way you will have to specify key and default value only once
	// The chance to mess up with the key is minimal, because it specified only once per vale 
	public class PrefsWrapperAdvanced {
		public readonly PrefsValue<bool> MyBoolParam = new("my_bool_param", true);
		public readonly PrefsValue<float> MyFloatParam = new("my_float_param", 123.5f);
		public readonly PrefsValue<int> MyIntParam = new("my_int_param");
	}
}