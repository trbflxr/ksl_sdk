using System.Collections.Generic;
using KSL.API;
using UnityEngine;

namespace Example {
	internal class UIExample {
		private bool exampleToggle_;
		private float sliderValue1_;
		private float sliderValue2_;
		private string input1_ = "Input text";
		private string input2_;
		private Vector2 scrollPos1_ = Vector2.zero;
		private Vector2 scrollPos2_ = Vector2.zero;
		private readonly List<string> strings_ = new() {
			"Element 0",
			"Element 1",
			"Element 2",
			"Element 3",
			"Element 4",
			"Element 5",
			"Element 6",
			"Element 7"
		};

		internal void Draw() {
			Kino.UI.Label("Wellcome to ExampleMod!\nHere is an example UI.");

			if (Kino.UI.ContextButton("UI Elements")) {
				Kino.UI.PushContext(UIElementsExampleContext, "UI example");
			}

			if (Kino.UI.ContextButton("Layouts")) {
				Kino.UI.PushContext(LayoutsExampleContext, "Layouts example");
			}

			if (Kino.UI.ContextButton("Contexts")) {
				Kino.UI.PushContext(ContextsExampleContext, "Contexts example");
			}
		}

		#region UI elements
		private void UIElementsExampleContext() {
			if (Kino.UI.ContextButton("Buttons and toggles")) {
				Kino.UI.PushContext(ButtonsAndTogglesContext, "Buttons and toggles");
			}

			if (Kino.UI.ContextButton("Label and hyperlinks")) {
				Kino.UI.PushContext(LabelAndHyperlinksContext, "Label and hyperlinks");
			}

			if (Kino.UI.ContextButton("Sliders")) {
				Kino.UI.PushContext(SlidersContext, "Sliders");
			}

			if (Kino.UI.ContextButton("Input fields")) {
				Kino.UI.PushContext(InputContext, "Input fields");
			}

			if (Kino.UI.ContextButton("List view")) {
				Kino.UI.PushContext(ListViewContext, "List view");
			}

			if (Kino.UI.ContextButton("Spacers and lines")) {
				Kino.UI.PushContext(SpacersAndLinesContext, "Spacers and lines");
			}

			if (Kino.UI.ContextButton("Groups")) {
				Kino.UI.PushContext(GroupsContext, "Groups");
			}
		}

		private void ButtonsAndTogglesContext() {
			if (Kino.UI.Button("Button", tooltip: "Simple button")) {
				Kino.Log.Info("Button clicked");
			}

			if (Kino.UI.Button("Active button", active: true, tooltip: "This button is pressed down")) {
				Kino.Log.Info("Active button clicked");
			}

			if (Kino.UI.ContextButton("Context button")) {
				Kino.Log.Info("Context button clicked");
			}

			if (Kino.UI.Toggle("Toggle", ref exampleToggle_)) {
				Kino.Log.Info($"Toggle changed, value: {exampleToggle_}");
			}
		}

		private void LabelAndHyperlinksContext() {
			Kino.UI.Label("Label");
			Kino.UI.Label("Multiline label: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");

			Kino.UI.GroupLabel("Group label");
			Kino.UI.Button("First", Group.VFirst);
			Kino.UI.Button("Second", Group.VMiddle);
			Kino.UI.Button("Third", Group.VLast);

			Kino.UI.Hyperlink("Google", "https://google.com", new Tooltip { Text = "Open google.com", SingleLine = true });
			Kino.UI.Hyperlink("Hyperlink with callback (< Back)", () => { Kino.UI.PopContext(); }, new Tooltip { Text = "Go back", SingleLine = true });

			Kino.UI.HyperlinkLabel("Visit out<link>", "discord", "https://discord.gg/kinomod");
		}

		private void SlidersContext() {
			if (Kino.UI.Slider(ref sliderValue1_, 0.0f, 10.0f, $"Simple slider: {sliderValue1_:F2}")) {
				Kino.Log.Info($"Slider value changed: {sliderValue1_:F2}");
			}

			if (Kino.UI.SliderWithButtons(ref sliderValue2_, 0.0f, 100.0f, 1.0f, $"Slider with buttons: {sliderValue2_:F0}")) {
				Kino.Log.Info($"Slider with buttons value changed: {sliderValue2_:F0}");
			}
		}

		private void InputContext() {
			Kino.UI.Label("You can control max length and regex of input fields.");

			Kino.UI.GroupLabel("Simple text input");
			if (Kino.UI.Input(ref input1_, 32, tooltip: "Simple text input")) {
				Kino.Log.Info($"Input1 value changed: {input1_}");
			}

			Kino.UI.GroupLabel("Digits only input");
			if (Kino.UI.Input(ref input2_, 16, @"^\d+$", tooltip: "Digits only input")) {
				Kino.Log.Info($"Digits only input: {input2_}");
			}
		}

		private void ListViewContext() {
			Kino.UI.Label("You can iterate any IEnumerable collections with ListView and specify how to display elements from it using draw callback.");

			Kino.UI.GroupLabel("Simple string list");
			Kino.UI.ListView(ref scrollPos1_, 100.0f, strings_, (index, element) => {
				if (Kino.UI.Button(element)) {
					Kino.Log.Info($"Element selected: {element}, at index: {index}");
				}
			});

			Kino.UI.Spacer();

			Kino.UI.GroupLabel("Same list but draw callback changed");
			Kino.UI.ListView(ref scrollPos2_, 100.0f, strings_, (index, element) => {
				Kino.UI.BeginHorizontal();

				if (Kino.UI.Button($"{index}")) {
					Kino.Log.Info($"Selected index: {index}");
				}

				Kino.UI.Spacer();

				if (Kino.UI.Button(element)) {
					Kino.Log.Info($"Element selected: {element}, at index: {index}");
				}

				Kino.UI.EndHorizontal();

				Kino.UI.HorizontalLine();
			});
		}

		private void SpacersAndLinesContext() {
			Kino.UI.Label("You can draw lines and spacers to make UI more organized.");

			Kino.UI.HorizontalLine();

			Kino.UI.GroupLabel("Horizontal spacer");
			Kino.UI.BeginHorizontal();
			Kino.UI.Button("Left");
			Kino.UI.Spacer();
			Kino.UI.Button("Right");
			Kino.UI.EndHorizontal();

			Kino.UI.HorizontalLine();

			Kino.UI.GroupLabel("Vertical Spacer");
			Kino.UI.Button("First");
			Kino.UI.Spacer();
			Kino.UI.Button("Last");
		}

		private void GroupsContext() {
			Kino.UI.Label("Organize your mod UI using groups.");

			Kino.UI.GroupLabel("Vertical group");
			Kino.UI.Button("Button", Group.VFirst);
			Kino.UI.ContextButton("Context button", Group.VMiddle);
			Kino.UI.Toggle("Toggle", ref exampleToggle_, Group.VMiddle);
			Kino.UI.Slider(ref sliderValue1_, 0.0f, 10.0f, $"Slider: {sliderValue1_:F2}", Group.VMiddle);
			Kino.UI.SliderWithButtons(ref sliderValue1_, 0.0f, 10.0f, 0.01f, $"Slider with buttons: {sliderValue1_:F2}", Group.VLast);

			Kino.UI.Spacer();

			Kino.UI.GroupLabel("Horizontal group");
			Kino.UI.BeginHorizontal();
			Kino.UI.Button("Left", Group.HLeft);
			Kino.UI.Button("Middle1", Group.HMiddle);
			Kino.UI.Button("Middle2", Group.HMiddle);
			Kino.UI.Button("Right", Group.HRight);
			Kino.UI.EndHorizontal();
		}
		#endregion

		#region Layout
		private void LayoutsExampleContext() {
			Kino.UI.Label("By default KSL has vertical layout and you don't have to specify it explicitly.\nBut also you can use horizontal layout or even combination of both.\nHere is an example how to use it.");

			Kino.UI.GroupLabel("Vertical layout");
			Kino.UI.HorizontalLine();

			Kino.UI.Button("Vertical button 1");
			Kino.UI.Button("Vertical button 2");

			Kino.UI.HorizontalLine();


			Kino.UI.GroupLabel("Horizontal layout");
			Kino.UI.HorizontalLine();

			Kino.UI.BeginHorizontal();
			Kino.UI.Button("Left");
			Kino.UI.Button("Middle");
			Kino.UI.Button("Right");
			Kino.UI.EndHorizontal();

			Kino.UI.HorizontalLine();


			Kino.UI.GroupLabel("Combination");
			Kino.UI.HorizontalLine();

			Kino.UI.BeginHorizontal();

			Kino.UI.BeginVertical();
			Kino.UI.Button("Left top button");
			Kino.UI.Button("Left bottom button");
			Kino.UI.EndVertical();

			Kino.UI.BeginVertical();
			Kino.UI.Button("Right top button");
			Kino.UI.Button("Right bottom button");
			Kino.UI.EndVertical();

			Kino.UI.EndHorizontal();

			Kino.UI.HorizontalLine();
		}
		#endregion

		#region Contexts example
		private void ContextsExampleContext() {
			Kino.UI.Label("Here is an example how to use UI contexts in KSL.");

			if (Kino.UI.ContextButton("Settings (Config example)", tooltip: "Open settings menu")) {
				Kino.UI.PushContext(SettingsContext, "Settings");
				Kino.Log.Info("User opened settings context using button");
			}

			Kino.UI.HyperlinkLabel("Also you can open<link>using a hyperlink", "settings", () => {
				Kino.UI.PushContext(SettingsContext, "Settings");
				Kino.Log.Info("User opened settings context using hyperlink");
			});
		}

		private void SettingsContext() {
			if (Kino.UI.ContextButton("Graphics")) {
				Kino.UI.PushContext(GraphicsSettingsContext, "Graphics");
			}

			if (Kino.UI.ContextButton("Input")) {
				Kino.UI.PushContext(InputSettingsContext, "Input");
			}
		}

		private void GraphicsSettingsContext() {
			// In this example we changing value of a custom type (GraphicsSettings)
			// KSL will not trigger auto save for it so we have to do it manually.
			if (Kino.UI.Toggle("Use vsync", ref Config.Data.Graphics.Vsync)) {
				// You can save it like this
				Kino.Config.Save<IConfigData>();
				// Or create a wrapper method and call it like this
				Config.Save();

				// Also you can use this method to save config immediately on disc.
				// It will be fine to call it in this case
				// But it's not recommended to call it in sliders because it will cause fps drops
				Kino.Config.ForceSave<IConfigData>();
			}

			if (Kino.UI.Toggle("Draw shadows", ref Config.Data.Graphics.DrawShadows)) {
				Config.Save();
			}

			if (Kino.UI.Slider(ref Config.Data.Graphics.RenderDistance, 500.0f, 3000.0f, $"Render distance: {Config.Data.Graphics.RenderDistance:F0}")) {
				Config.Save();
			}
		}

		private void InputSettingsContext() {
			// Here we have to use local variables to pass it by ref to UI.Toggle.
			// Because you can't directly pass properties by ref since it returns temporary value.
			// Also there is no need to call force save for a config because we setting values directly to it nad not to any custom types.
			bool invertMouse = Config.Data.InvertMouse;
			if (Kino.UI.Toggle("Invert mouse", ref invertMouse, tooltip: "Who needs it anyway?")) {
				Config.Data.InvertMouse = invertMouse;
			}

			bool hideCrosshair = Config.Data.HideCrosshair;
			if (Kino.UI.Toggle("Hide crosshair", ref hideCrosshair, tooltip: "So are you a hardcore gamer?")) {
				Config.Data.HideCrosshair = hideCrosshair;
			}
		}
		#endregion
	}
}