﻿
using HP.LFT.SDK;
using HP.LFT.SDK.UIAPro;
using HP.LFT.SDK.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WD_UFT_Selenium_Auto.Library.UFTLibrary;

namespace WD_UFT_Selenium_Auto.Product.WD
{
    public class APEMAdmin_Window : STD_Window
    {


        //public Wizard_Window()
        //{
        //}

        public APEMAdmin_Window(string xpath) : base(xpath)
        {
        }

        public ExtractorProperty_Dialog ExtractorProperty => new ExtractorProperty_Dialog(_STD_Window, "//Dialog[@Text = 'Audit & Compliance Extractor - Properties']");


        public STD_TreeView TreeView => new STD_TreeView(_STD_Window, "//Treeview[@NativeClass = 'SysTreeView32']");

        public STD_ListView ListView => new STD_ListView(_STD_Window, "//Treeview[@NativeClass = 'SysListView32']");


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



    




    }

            


}
