
using HP.LFT.SDK;
using HP.LFT.SDK.UIAPro;
using HP.LFT.SDK.WinForms;
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
        public ITreeViewItem BatchAPI => _STD_Window.Describe<IPane>(new PaneDescription
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
                Name = @"BatchAPI",
                Path = @"Window;Pane;Window;Pane;Tree;TreeItem;TreeItem;TreeItem;TreeItem;TreeItem;TreeItem"
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
