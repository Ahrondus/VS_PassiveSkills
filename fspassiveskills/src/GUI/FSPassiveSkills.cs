using System;
using Vintagestory.API.Common;
using Vintagestory.API.Client;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;

namespace FSPassiveSkills
{
	public class FSPassiveSkillsGUI : GuiDialog
	{
		public override string ToggleKeyCombinationCode => "FSPassiveSkillsGUI";

		public FSPassiveSkillsGUI(ICoreClientAPI capi) : base(capi)
		{
			SetupDialog();
		}
		private void SetupDialog()
		{
			ElementBounds dialogBounds = ElementStdBounds.AutosizedMainDialog.WithAlignment(EnumDialogArea.CenterMiddle);

			ElementBounds textBounds = ElementBounds.Fixed(0,40,300,300);

			ElementBounds bgBounds = ElementBounds.Fill.WithFixedPadding(GuiStyle.ElementToDialogPadding);
			bgBounds.BothSizing = ElementSizing.FitToChildren;
			bgBounds.WithChildren(textBounds);

			SingleComposer = capi.Gui.CreateCompo("MyFirstDialogBox", dialogBounds)
				.AddShadedDialogBG(bgBounds)
				.AddDialogTitleBar("Title Bar, Nice!", OnTitleBarCloseClicked)
				.AddStaticText("This is a placeholder at the center of your screen.", CairoFont.WhiteDetailText(), textBounds)
				.Compose();
		}

		private void OnTitleBarCloseClicked()
		{
			TryClose();
		}
	}

	public class FSPassiveSkillsTextSystem : ModSystem
	{
		ICoreClientAPI capi;
		GuiDialog dialog;

		public override bool ShouldLoad(EnumAppSide forSide)
		{
			return forSide == EnumAppSide.Client;
		}

		public override void StartClientSide(ICoreClientAPI api)
		{
			base.StartClientSide(api);

			dialog = new FSPassiveSkillsGUI(api);

			capi = api;
			capi.Input.RegisterHotKey("FSPassiveSkillsGUI", "Test GUI with centered text.", GlKeys.P, HotkeyType.GUIOrOtherControls);
			capi.Input.SetHotKeyHandler("FSPassiveSkillsGUI", ToggleGui);
		}

		private bool ToggleGui(KeyCombination comb)
		{
			if (dialog.IsOpened()) 
				dialog.TryClose();
			else 
				dialog.TryOpen();
			
			return true;
		}
	}
}
