using HP.LFT.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HP.LFT.SDK.UIAPro;
using stdWin = HP.LFT.SDK.StdWin;
using ButtonDescription = HP.LFT.SDK.UIAPro.ButtonDescription;
using CheckBoxDescription = HP.LFT.SDK.UIAPro.CheckBoxDescription;
using IButton = HP.LFT.SDK.UIAPro.IButton;
using ICheckBox = HP.LFT.SDK.UIAPro.ICheckBox;
using IWindow = HP.LFT.SDK.UIAPro.IWindow;
using WindowDescription = HP.LFT.SDK.UIAPro.WindowDescription;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;

namespace MES_APEM_UFT_Selenium_Auto.Product.APEM
{
    public class Install_Window
    {


		public static stdWin.IDialog aspenONEInstallerDialog = Desktop.Describe<stdWin.IDialog>(new stdWin.DialogDescription
		{
			NativeClass = @"#32770",
			Text = @"aspenONE Installer",
			IsOwnedWindow = false,
			IsChildWindow = false
		})
		.Describe<stdWin.IDialog>(new stdWin.DialogDescription
		{
			NativeClass = @"#32770",
			Text = @"aspenONE Installer",
			IsOwnedWindow = true,
			IsChildWindow = false
		});



		public static stdWin.IButton yesButton = Desktop.Describe<stdWin.IDialog>(new stdWin.DialogDescription
		{
			NativeClass = @"#32770",
			Text = @"aspenONE Installer",
			IsOwnedWindow = true,
			IsChildWindow = false
		})
		.Describe<stdWin.IButton>(new stdWin.ButtonDescription
		{
			NativeClass = @"Button",
			Text = @"&Yes"
		});
		public static void ChooseProduct()
		{
			stdWin.ITreeView selectTheProductGroupsYouWantToInstallToSeeProductsIncludedInEachGroupSimplyHoveTreeView = Desktop.Describe<stdWin.IDialog>(new stdWin.DialogDescription
			{
				NativeClass = @"#32770",
				Text = @"aspenONE Installer",
				IsOwnedWindow = false,
				IsChildWindow = false
			})
			.Describe<stdWin.ITreeView>(new stdWin.TreeViewDescription
			{
				NativeClass = @"SysTreeView32",
				AttachedText = @"Select the product groups you want to install. To see products included in each group, simply hove"
			});

			var ls = selectTheProductGroupsYouWantToInstallToSeeProductsIncludedInEachGroupSimplyHoveTreeView.VisibleNodes;
			foreach (var item in ls)
			{
				string a = item.Text;
				if (a.StartsWith("aspenONE Manufacturing Execution Systems"))
				{
					item.ClickState();
					item.Expand();
					break;
				}
			}
			var ls1 = selectTheProductGroupsYouWantToInstallToSeeProductsIncludedInEachGroupSimplyHoveTreeView.VisibleNodes;
			foreach (var item in ls1)
			{
				string a = item.Text;
				if (a.StartsWith("Aspen Cim-IO Interfaces"))
				{
					item.ClickState();
					break;
				}
			}
		}

		public static stdWin.IWindow welcomeWindow = Desktop.Describe<stdWin.IWindow>(new stdWin.WindowDescription
		{
			WindowTitleRegExp = $@"Welcome to aspenONE {Utility.version} Installer",
		});

		public static IButton installButton = Desktop.Describe<IWindow>(new WindowDescription
		{
			ProcessName = @"Setup",
			Name = $@"Welcome to aspenONE {Utility.version} Installer",
			Path = @"Window",
			SupportedPatterns = new string[] { @"LegacyIAccessible", @"SynchronizedInput", @"Transform", @"Window" },
			FrameworkId = @"WPF",
			ControlType = @"Window",
			AutomationId = string.Empty
		})
		.Describe<IPane>(new PaneDescription
		{
			ProcessName = @"Setup",
			Name = string.Empty,
			Path = @"Window;Pane",
			SupportedPatterns = new string[] { @"LegacyIAccessible", @"Scroll", @"SynchronizedInput" },
			FrameworkId = @"WPF",
			ControlType = @"Pane",
			AutomationId = string.Empty
		})
		.Describe<IButton>(new ButtonDescription
		{
			ProcessName = @"Setup",
			Name = string.Empty,
			Path = @"Window;Pane;Button",
			SupportedPatterns = new string[] { @"Invoke", @"LegacyIAccessible", @"SynchronizedInput" },
			FrameworkId = @"WPF",
			ControlType = @"Button",
			AutomationId = string.Empty,
			Index = 1
		});

		//     public static IButton installButton = Desktop.Describe<IWindow>(new WindowDescription
		//     {
		//         ProcessName = @"Setup",
		//         Name = $@"Welcome to aspenONE {Utility.version} Installer",
		//         Path = @"Window",
		//         SupportedPatterns = new string[] { @"LegacyIAccessible", @"SynchronizedInput", @"Transform", @"Window" },
		//         FrameworkId = @"WPF",
		//         ControlType = @"Window",
		//         AutomationId = string.Empty
		//     })
		//         .Describe<IPane>(new PaneDescription
		// {
		//	 ProcessName = @"Setup",
		//	 Name = string.Empty,
		//	 Path = @"Window;Pane",
		//	 SupportedPatterns = new string[] { @"LegacyIAccessible", @"Scroll", @"SynchronizedInput" },
		//	 FrameworkId = @"WPF",
		//	 ControlType = @"Pane",
		//	 AutomationId = string.Empty
		// })
		//.Describe<IButton>(new ButtonDescription
		//{
		//	ProcessName = @"Setup",
		//	Name = @"INSTALL NOW",
		//	Path = @"Window;Pane;Button",
		//	SupportedPatterns = new string[] { @"Invoke", @"LegacyIAccessible", @"SynchronizedInput" },
		//	FrameworkId = @"WPF",
		//	ControlType = @"Button",
		//	AutomationId = string.Empty
		//});


		//      public static IButton installButton = Desktop.Describe<IWindow>(new WindowDescription
		//{
		//	ProcessName = @"Setup",
		//	Name = @"Welcome to aspenONE V14.2 Installer",
		//	Path = @"Window",
		//	SupportedPatterns = new string[] { @"LegacyIAccessible", @"SynchronizedInput", @"Transform", @"Window" },
		//	FrameworkId = @"WPF",
		//	ControlType = @"Window",
		//	AutomationId = string.Empty
		//})
		//.Describe<IPane>(new PaneDescription
		//{
		//	ProcessName = @"Setup",
		//	Name = string.Empty,
		//	Path = @"Window;Pane",
		//	SupportedPatterns = new string[] { @"LegacyIAccessible", @"Scroll", @"SynchronizedInput" },
		//	FrameworkId = @"WPF",
		//	ControlType = @"Pane",
		//	AutomationId = string.Empty
		//})
		//.Describe<IButton>(new ButtonDescription
		//{
		//	ProcessName = @"Setup",
		//	Name = string.Empty,
		//	Path = @"Window;Pane;Button",
		//	SupportedPatterns = new string[] { @"Invoke", @"LegacyIAccessible", @"SynchronizedInput" },
		//	FrameworkId = @"WPF",
		//	ControlType = @"Button",
		//	AutomationId = string.Empty,
		//	Index = 1
		//});

		public static IButton InstallAspenOneButton = Desktop.Describe<IWindow>(new WindowDescription
		{
			ProcessName = @"msiexec",
			Name = @"aspenONE Installer",
			Path = @"Window",
			SupportedPatterns = new string[] { @"LegacyIAccessible", @"Transform", @"Window" },
			FrameworkId = @"Win32",
			ControlType = @"Window",
			AutomationId = string.Empty
		})
		.Describe<IPane>(new PaneDescription
		{
			ProcessName = @"msiexec",
			Name = string.Empty,
			Path = @"Window;Pane",
			SupportedPatterns = new string[] { @"LegacyIAccessible" },
			FrameworkId = @"Win32",
			ControlType = @"Pane",
			AutomationId = string.Empty,
			Index = 0
		})
		.Describe<IButton>(new ButtonDescription
		{
			ProcessName = @"msiexec",
			Name = @"Administrative Workflow:",
			Path = @"Window;Pane;Button",
			SupportedPatterns = new string[] { @"Invoke", @"LegacyIAccessible" },
			FrameworkId = @"Win32",
			ControlType = @"Button",
			AutomationId = @"1033"
		});

		public static ICheckBox iAcceptTheTermsOfThisAgreementCheckBox = Desktop.Describe<IWindow>(new WindowDescription
		{
			ProcessName = @"msiexec",
			Name = @"aspenONE Installer",
			Path = @"Window",
			SupportedPatterns = new string[] { @"LegacyIAccessible", @"Transform", @"Window" },
			FrameworkId = @"Win32",
			ControlType = @"Window",
			AutomationId = string.Empty
		})
		.Describe<IPane>(new PaneDescription
		{
			ProcessName = @"msiexec",
			Name = string.Empty,
			Path = @"Window;Pane",
			SupportedPatterns = new string[] { @"LegacyIAccessible" },
			FrameworkId = @"Win32",
			ControlType = @"Pane",
			AutomationId = string.Empty
		})
		.Describe<ICheckBox>(new CheckBoxDescription
		{
			ProcessName = @"msiexec",
			Name = @"I accept the terms of this agreement",
			Path = @"Window;Pane;CheckBox",
			SupportedPatterns = new string[] { @"Invoke", @"LegacyIAccessible", @"Toggle" },
			FrameworkId = @"Win32",
			ControlType = @"CheckBox",
			AutomationId = @"1050"
		});


		public static IButton nextButton = Desktop.Describe<IWindow>(new WindowDescription
		{
			ProcessName = @"msiexec",
			Name = @"aspenONE Installer",
			Path = @"Window",
			SupportedPatterns = new string[] { @"LegacyIAccessible", @"Transform", @"Window" },
			FrameworkId = @"Win32",
			ControlType = @"Window",
			AutomationId = string.Empty
		})
		.Describe<IPane>(new PaneDescription
		{
			ProcessName = @"msiexec",
			Name = string.Empty,
			Path = @"Window;Pane",
			SupportedPatterns = new string[] { @"LegacyIAccessible" },
			FrameworkId = @"Win32",
			ControlType = @"Pane",
			AutomationId = string.Empty
		})
		.Describe<IButton>(new ButtonDescription
		{
			ProcessName = @"msiexec",
			Name = @"Next >",
			Path = @"Window;Pane;Button",
			SupportedPatterns = new string[] { @"Invoke", @"LegacyIAccessible" },
			FrameworkId = @"Win32",
			ControlType = @"Button",
			AutomationId = @"1207"
		});


		public static stdWin.IEditField licenseServerInput = Desktop.Describe<stdWin.IDialog>(new stdWin.DialogDescription
		{
			NativeClass = @"#32770",
			Text = @"aspenONE Installer",
			IsOwnedWindow = false,
			IsChildWindow = false
		})
		.Describe<stdWin.IEditField>(new stdWin.EditFieldDescription
		{
			NativeClass = @"Edit",
			AttachedText = @"Please provide a list of license servers for AspenTech products. Install will keep the server name"
		});

		public static stdWin.IButton addServerButton = Desktop.Describe<stdWin.IDialog>(new stdWin.DialogDescription
		{
			NativeClass = @"#32770",
			Text = @"aspenONE Installer",
			IsOwnedWindow = false,
			IsChildWindow = false
		})
		.Describe<stdWin.IButton>(new stdWin.ButtonDescription
		{
			NativeClass = @"Button",
			Text = @"&Add Server"//As.RegExp("$Add Server*") //
		});


		public static stdWin.IEditField userNameInput = Desktop.Describe<stdWin.IDialog>(new stdWin.DialogDescription
		{
			NativeClass = @"#32770",
			Text = @"aspenONE Installer",
			IsOwnedWindow = false,
			IsChildWindow = false
		})
		.Describe<stdWin.IEditField>(new stdWin.EditFieldDescription
		{
			NativeClass = @"Edit",
			AttachedText = @"Please provide an Administrators group user account"
		});

		public static stdWin.IEditField passwordInput = Desktop.Describe<stdWin.IDialog>(new stdWin.DialogDescription
		{
			NativeClass = @"#32770",
			Text = @"aspenONE Installer",
			IsOwnedWindow = false,
			IsChildWindow = false
		})
		.Describe<stdWin.IEditField>(new stdWin.EditFieldDescription
		{
			NativeClass = @"Edit",
			AttachedText = @"Password:"
		});


		public static stdWin.IButton InstallNowButton = Desktop.Describe<stdWin.IDialog>(new stdWin.DialogDescription
		{
			NativeClass = @"#32770",
			Text = @"aspenONE Installer",
			IsOwnedWindow = false,
			IsChildWindow = false
		})
		.Describe<stdWin.IButton>(new stdWin.ButtonDescription
		{
			NativeClass = @"Button",
			Text = @"&Install Now"
		});

		public static IButton finishButton = Desktop.Describe<IWindow>(new WindowDescription
		{
			ProcessName = @"msiexec",
			Name = @"aspenONE Installer",
			Path = @"Window",
			SupportedPatterns = new string[] { @"LegacyIAccessible", @"Transform", @"Window" },
			FrameworkId = @"Win32",
			ControlType = @"Window",
			AutomationId = string.Empty
		})
		.Describe<IPane>(new PaneDescription
		{
			ProcessName = @"msiexec",
			Name = string.Empty,
			Path = @"Window;Pane",
			SupportedPatterns = new string[] { @"LegacyIAccessible" },
			FrameworkId = @"Win32",
			ControlType = @"Pane",
			AutomationId = string.Empty,
			Index = 1
		})
		.Describe<IButton>(new ButtonDescription
		{
			ProcessName = @"msiexec",
			Name = @"Finish",
			Path = @"Window;Pane;Button",
			SupportedPatterns = new string[] { @"Invoke", @"LegacyIAccessible" },
			FrameworkId = @"Win32",
			ControlType = @"Button",
			AutomationId = @"1120"
		});

		public static ICheckBox autoLaunchUpdateCheckBox = Desktop.Describe<IWindow>(new WindowDescription
		{
			ProcessName = @"msiexec",
			Name = @"aspenONE Installer",
			Path = @"Window",
			SupportedPatterns = new string[] { @"LegacyIAccessible", @"Transform", @"Window" },
			FrameworkId = @"Win32",
			ControlType = @"Window",
			AutomationId = string.Empty
		})
		.Describe<IPane>(new PaneDescription
		{
			ProcessName = @"msiexec",
			Name = string.Empty,
			Path = @"Window;Pane",
			SupportedPatterns = new string[] { @"LegacyIAccessible" },
			FrameworkId = @"Win32",
			ControlType = @"Pane",
			AutomationId = string.Empty,
			Index = 0
		})
		.Describe<ICheckBox>(new CheckBoxDescription
		{
			ProcessName = @"msiexec",
			Name = @"Automatically launch aspenONE Update Agent after rebooting the computer",
			Path = @"Window;Pane;CheckBox",
			SupportedPatterns = new string[] { @"Invoke", @"LegacyIAccessible", @"Toggle" },
			FrameworkId = @"Win32",
			ControlType = @"CheckBox",
			AutomationId = @"1302"
		});
	}

}
