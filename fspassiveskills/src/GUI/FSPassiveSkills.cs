using Vintagestory.API.Common;
using Vintagestory.API.Client;
using System;

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
			int titlebar = 31;
			ElementBounds window = ElementStdBounds.AutosizedMainDialog.WithAlignment(EnumDialogArea.CenterMiddle);
			ElementBounds dialogBounds = ElementBounds.Fixed(400, 820);
			ElementBounds dialog = ElementBounds.Fill.WithFixedPadding(0);

			ElementBounds borderLeft = ElementBounds.Fixed(10,10+titlebar, 10, 770);
			ElementBounds borderRight = ElementBounds.Fixed(380,10+titlebar, 10, 770);
			ElementBounds borderTop = ElementBounds.Fixed(10,10+titlebar, 380, 10);
			ElementBounds borderBottom = ElementBounds.Fixed(10,770+titlebar, 380, 10);

			ElementBounds diggingInset = ElementBounds.Fixed(30,30+titlebar, 80, 40);
			ElementBounds diggingTextBounds = ElementBounds.Fixed(37, 37+titlebar, 67, 23);

			ElementBounds textInset = ElementBounds.Fixed(133,30+titlebar, 125, 104);
			ElementBounds textBounds = ElementBounds.Fixed(135, 32+titlebar, 121, 100);

			dialog.BothSizing = ElementSizing.FitToChildren;
			dialog.WithChildren(new ElementBounds[]
			{
				dialogBounds,
				borderLeft,
				borderRight,
				borderTop,
				borderBottom,
				diggingInset,
				diggingTextBounds,
				textInset,
				textBounds
			});

			this.SingleComposer = capi.Gui.CreateCompo("testDialog", window)
				.AddShadedDialogBG(dialog, true, 5)
				.AddDialogTitleBar("FS Passive Skills", OnTitleBarCloseClicked, null, null)
				.BeginChildElements(dialog)
				
				.AddInset(borderLeft, 2, 0.85f)
				.AddInset(borderRight, 2, 0.85f)
				.AddInset(borderTop, 2, 0.85f)
				.AddInset(borderBottom, 2, 0.85f)

				.AddInset(diggingInset, -2, 0.85f)
				.AddStaticText("Digging", CairoFont.WhiteSmallishText(), diggingTextBounds)

				.AddInset(textInset, 2, 0.85f)
				.AddStaticText("Test Text", CairoFont.WhiteSmallishText(), textBounds)
				.EndChildElements()
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
