
using HP.LFT.SDK;
using HP.LFT.SDK.UIAPro;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;

namespace MES_APEM_UFT_Selenium_Auto.Product.APRM
{
	public class APRMAdmin_Window : STD_Window
	{


		//public Wizard_Window()
		//{
		//}

		public APRMAdmin_Window(string xpath) : base(xpath)
		{
		}

		public Open_Dialog Open => new Open_Dialog(_STD_Window, "//Dialog[@Text = 'Open']");


		public STD_TreeView TreeView => new STD_TreeView(_STD_Window, "//Treeview[@NativeClass = 'SysTreeView32']");




		public IMenuItem actionMenuItem => _STD_Window.Describe<IMenuBar>(new MenuBarDescription
		{
			Path = @"Window;Pane;Pane;MenuBar"
		})
			.Describe<IMenuItem>(new MenuItemDescription
			{
				Name = @"Action"
			});

		public ITreeViewItem WeightDispense => _STD_Window.Describe<IPane>(new PaneDescription
		{
			Name = @"Workspace",
			Path = @"Window;Pane"
		})
			.Describe<ITreeViewItem>(new TreeViewItemDescription
			{
				Name = @"Areas",
				Path = @"Window;Pane;Window;Pane;Tree;TreeItem;TreeItem;TreeItem;TreeItem;TreeItem"
			})
			.Describe<ITreeViewItem>(new TreeViewItemDescription
			{
				Name = @"WeighDispense",
				Path = @"Window;Pane;Window;Pane;Tree;TreeItem;TreeItem;TreeItem;TreeItem;TreeItem;TreeItem"
			});

		public ITreeViewItem Batch => _STD_Window.Describe<IPane>(new PaneDescription
		{
			Name = @"Workspace",
			Path = @"Window;Pane"
		})
			.Describe<ITreeViewItem>(new TreeViewItemDescription
			{
				Name = @"Areas",
				Path = @"Window;Pane;Window;Pane;Tree;TreeItem;TreeItem;TreeItem;TreeItem;TreeItem"
			})
			.Describe<ITreeViewItem>(new TreeViewItemDescription
			{
				Name = @"Batch",
				Path = @"Window;Pane;Window;Pane;Tree;TreeItem;TreeItem;TreeItem;TreeItem;TreeItem;TreeItem"
			});

		public ITreeViewItem BatchRPL => _STD_Window.Describe<IPane>(new PaneDescription
		{
			Name = @"Workspace",
			Path = @"Window;Pane"
		})
			.Describe<ITreeViewItem>(new TreeViewItemDescription
			{
				Name = @"Areas",
				Path = @"Window;Pane;Window;Pane;Tree;TreeItem;TreeItem;TreeItem;TreeItem;TreeItem"
			})
			.Describe<ITreeViewItem>(new TreeViewItemDescription
			{
				Name = @"BatchRPL",
				Path = @"Window;Pane;Window;Pane;Tree;TreeItem;TreeItem;TreeItem;TreeItem;TreeItem;TreeItem"
			});
		public ITreeViewItem BatchAPI => Desktop.Describe<IWindow>(new WindowDescription
		{
			ProcessName = @"mmc",
			Name = @"Aspen Production Record Manager Administrator V14.2 - aspenONE",
			Path = @"Window",
			SupportedPatterns = new string[] { @"LegacyIAccessible", @"Transform", @"Window" },
			FrameworkId = @"Win32",
			ControlType = @"Window",
			AutomationId = string.Empty
		})
			.Describe<IPane>(new PaneDescription
			{
				ProcessName = @"mmc",
				Name = @"Workspace",
				Path = @"Window;Pane",
				SupportedPatterns = new string[] { @"LegacyIAccessible" },
				FrameworkId = @"Win32",
				ControlType = @"Pane",
				AutomationId = @"59648"
			})
			.Describe<IWindow>(new WindowDescription
			{
				ProcessName = @"mmc",
				Name = @"Console Root\Production Record Manager\Data Sources\ZIRU-2022-3\Areas\BatchAPI",
				Path = @"Window;Pane;Window",
				SupportedPatterns = new string[] { @"LegacyIAccessible", @"Transform", @"Window" },
				FrameworkId = @"Win32",
				ControlType = @"Window",
				AutomationId = @"65280"
			})
			.Describe<IPane>(new PaneDescription
			{
				ProcessName = @"mmc",
				Name = @"Console Embedded Window",
				Path = @"Window;Pane;Window;Pane",
				SupportedPatterns = new string[] { @"LegacyIAccessible" },
				FrameworkId = @"Win32",
				ControlType = @"Pane",
				AutomationId = @"59648"
			})
			.Describe<ITreeView>(new TreeViewDescription
			{
				ProcessName = @"mmc",
				Name = @"Console Embedded Window Tree",
				Path = @"Window;Pane;Window;Pane;Tree",
				SupportedPatterns = new string[] { @"LegacyIAccessible", @"Selection" },
				FrameworkId = @"Win32",
				ControlType = @"Tree",
				AutomationId = @"12785"
			})
			.Describe<ITreeViewItem>(new TreeViewItemDescription
			{
				ProcessName = @"mmc",
				Name = @"Console Root",
				Path = @"Window;Pane;Window;Pane;Tree;TreeItem",
				SupportedPatterns = new string[] { @"ExpandCollapse", @"LegacyIAccessible", @"SelectionItem" },
				FrameworkId = string.Empty,
				ControlType = @"TreeItem",
				AutomationId = string.Empty
			})
			.Describe<ITreeViewItem>(new TreeViewItemDescription
			{
				ProcessName = @"mmc",
				Name = @"Production Record Manager",
				Path = @"Window;Pane;Window;Pane;Tree;TreeItem;TreeItem",
				SupportedPatterns = new string[] { @"ExpandCollapse", @"LegacyIAccessible", @"SelectionItem" },
				FrameworkId = string.Empty,
				ControlType = @"TreeItem",
				AutomationId = string.Empty
			})
			.Describe<ITreeViewItem>(new TreeViewItemDescription
			{
				ProcessName = @"mmc",
				Name = @"Data Sources",
				Path = @"Window;Pane;Window;Pane;Tree;TreeItem;TreeItem;TreeItem",
				SupportedPatterns = new string[] { @"ExpandCollapse", @"LegacyIAccessible", @"SelectionItem" },
				FrameworkId = string.Empty,
				ControlType = @"TreeItem",
				AutomationId = string.Empty
			})
			.Describe<ITreeViewItem>(new TreeViewItemDescription
			{
				ProcessName = @"mmc",
				Name = @"ZIRU-2022-3",
				Path = @"Window;Pane;Window;Pane;Tree;TreeItem;TreeItem;TreeItem;TreeItem",
				SupportedPatterns = new string[] { @"ExpandCollapse", @"LegacyIAccessible", @"SelectionItem" },
				FrameworkId = string.Empty,
				ControlType = @"TreeItem",
				AutomationId = string.Empty
			})
			.Describe<ITreeViewItem>(new TreeViewItemDescription
			{
				ProcessName = @"mmc",
				Name = @"Areas",
				Path = @"Window;Pane;Window;Pane;Tree;TreeItem;TreeItem;TreeItem;TreeItem;TreeItem",
				SupportedPatterns = new string[] { @"ExpandCollapse", @"LegacyIAccessible", @"SelectionItem" },
				FrameworkId = string.Empty,
				ControlType = @"TreeItem",
				AutomationId = string.Empty
			})
			.Describe<ITreeViewItem>(new TreeViewItemDescription
			{
				ProcessName = @"mmc",
				Name = @"BatchAPI",
				Path = @"Window;Pane;Window;Pane;Tree;TreeItem;TreeItem;TreeItem;TreeItem;TreeItem;TreeItem",
				SupportedPatterns = new string[] { @"ExpandCollapse", @"LegacyIAccessible", @"SelectionItem" },
				FrameworkId = string.Empty,
				ControlType = @"TreeItem",
				AutomationId = string.Empty
			});


		public ITreeViewItem Equipment => _STD_Window.Describe<IPane>(new PaneDescription
		{
			Name = @"Workspace",
			Path = @"Window;Pane"
		})
			.Describe<ITreeViewItem>(new TreeViewItemDescription
			{
				Name = @"Areas",
				Path = @"Window;Pane;Window;Pane;Tree;TreeItem;TreeItem;TreeItem;TreeItem;TreeItem"
			})
			.Describe<ITreeViewItem>(new TreeViewItemDescription
			{
				Name = @"Equipment",
				Path = @"Window;Pane;Window;Pane;Tree;TreeItem;TreeItem;TreeItem;TreeItem;TreeItem;TreeItem"
			});





		//public IMenuItem importMenuItem => Desktop.Describe<IMenu>(new MenuDescription
		//{
		//    ControlType = @"Menu"
		//})
		//    .Describe<IMenuItem>(new MenuItemDescription
		//    {
		//        Name = @"Import..."
		//    });


	}




}
