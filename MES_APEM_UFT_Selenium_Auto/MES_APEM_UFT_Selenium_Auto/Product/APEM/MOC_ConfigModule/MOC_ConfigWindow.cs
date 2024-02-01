using HP.LFT.SDK;
using HP.LFT.SDK.Java;

using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;

namespace MES_APEM_UFT_Selenium_Auto.Product.APEM
{
    public class MOC_ConfigWindow:UFT_JavaWindow
    {
        public MOC_ConfigWindow()
        {
        }
        public MOC_ConfigWindow(string xpath) : base(xpath)
        {
        }
        public UFT_Button Workstations => new UFT_Button(_UFT_Window, "//Button[@Label = 'Workstations' and @IsWrapped = 'True']");
        public UFT_Button Import_ReplaceMerge => new UFT_Button(_UFT_Window, "//Button[@Label = 'Import replace/merge' and @IsWrapped = 'True']");
        public UFT_Button Export => new UFT_Button(_UFT_Window, "//Button[@Label = 'Export' and @IsWrapped = 'True']");
        public UFT_Button Edit => new UFT_Button(_UFT_Window, "//Button[@Label = 'Edit' and @IsWrapped = 'True']");
        public UFT_Button Table_Definition => new UFT_Button(_UFT_Window, "//Button[@Label = 'Table Definition' and @IsWrapped = 'True']");
        public ConfigImport_Dialog ConfigImportDialog => new ConfigImport_Dialog(_UFT_Window, "//Dialog[@Title = 'Import replace/merge from File']");
        public ConfigExport_Dialog ConfigExportDialog => new ConfigExport_Dialog(_UFT_Window, "//Dialog[@Title = 'Export to File']");
        public WorkstationInterFrame WorkstationInterFrame => new WorkstationInterFrame(_UFT_Window, "//InterFrame[@TagName = 'Workstations']");
        public WorkstationEditInterFrame WorkstationEditInterFrame => new WorkstationEditInterFrame(_UFT_Window, "//InterFrame[@TagName = 'Workstation:*']");
        public WorkstationInsertInterFrame WorkstationInsertInterFrame => new WorkstationInsertInterFrame(_UFT_Window, "//InterFrame[@TagName = 'Workstation']");

        public TableDefinitionInterFrame TableDefinitionInterFrame => new TableDefinitionInterFrame(_UFT_Window, "//InterFrame[@TagName = 'Table Definition*']");
        public TableDataInputInterFrame TableDataInputInterFrame => new TableDataInputInterFrame(_UFT_Window, "//InterFrame[@TagName = 'Data input:*']");
        public AddReason_Dialog AddReasonDialog => new AddReason_Dialog(_UFT_Window, "//Dialog[@Title = 'Audit Reason']");


    }

    public class ConfigImport_Dialog : UFT_Dialog
    {
        public ConfigImport_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Editor FileName => new UFT_Editor(_UFT_Dialog, "//Editor[@AttachedText = 'File Name:']");
    }
    
    public class ConfigExport_Dialog : UFT_Dialog
    {
        public ConfigExport_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Editor FileName => new UFT_Editor(_UFT_Dialog, "//Editor[@AttachedText = 'File Name:']");
        public UFT_Button ExportToFileButton => new UFT_Button(_UFT_Dialog, "//Button[@Label = 'Export to File']");
        public UFT_Button HomeButton => new UFT_Button(_UFT_Dialog, "//Button[@Label = 'Home']");

    }

    public class ConfigInterFrame : UFT_InterFrame
    {

        public ConfigInterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Button Insert => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Insert row' and @IsWrapped = 'True']");
        public UFT_Button Edit => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Edit values' and @IsWrapped = 'True']");
        public UFT_Button Confirm => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Confirm changes' and @IsWrapped = 'True']");

        public UFT_Button Close => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Close' and @IsWrapped = 'True']");
        public UFT_Button Cancel => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Cancel changes' and @IsWrapped = 'True']");

    }
    public class WorkstationInterFrame : ConfigInterFrame
    {

        public WorkstationInterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        private ITable Workstation => _UFT_InterFrame.Describe<ITable>(new TableDescription
        {
            TagName = @"Workstation  "
        });
        public UFT_Table WorkstationTable => new UFT_Table(Workstation);

    }
    public class WorkstationEditInterFrame : ConfigInterFrame
    {

        public WorkstationEditInterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Editor Workstation => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Workstation']");

        public UFT_List Role => new UFT_List(_UFT_InterFrame, "//List[@AttachedText = 'Role']");
        public UFT_List Site => new UFT_List(_UFT_InterFrame, "//List[@AttachedText = 'Site']");
        public UFT_List ProcessArea => new UFT_List(_UFT_InterFrame, "//List[@AttachedText = 'Process Area']");
        public UFT_List Workcenter => new UFT_List(_UFT_InterFrame, "//List[@AttachedText = 'Workcenter']");

    }
    public class WorkstationInsertInterFrame : ConfigInterFrame
    {

        public WorkstationInsertInterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Editor Workstation => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Workstation']");

        public UFT_List Role => new UFT_List(_UFT_InterFrame, "//List[@AttachedText = 'Role']");
        public UFT_List Site => new UFT_List(_UFT_InterFrame, "//List[@AttachedText = 'Site']");
        public UFT_List ProcessArea => new UFT_List(_UFT_InterFrame, "//List[@AttachedText = 'Process Area']");
        public UFT_List Workcenter => new UFT_List(_UFT_InterFrame, "//List[@AttachedText = 'Workcenter']");
    }
    public class TableDefinitionInterFrame : ConfigInterFrame
    {

        public TableDefinitionInterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {

        }
       
        private ITable Table => _UFT_InterFrame.Describe<ITable>(new TableDescription
        {
            TagName = @"Table  "
        });
        public UFT_Table Tables => new UFT_Table(Table);

        public UFT_Button DataView => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Data View']");
        public UFT_Editor Editor => new UFT_Editor(_UFT_InterFrame, "//Button[@AttachedText = 'Table*' and @IsWrapped = 'True']");
    }
    public class TableDataInputInterFrame : ConfigInterFrame
    {

        public TableDataInputInterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {

        }

        private ITable Table => _UFT_InterFrame.Describe<ITable>(new TableDescription
        {
            TagName = @"DOCUMENT_ID  "
        });
        public UFT_Table Tables => new UFT_Table(Table);

        public UFT_Editor DocumentEditor => new UFT_Editor(_UFT_InterFrame, "//Button[@AttachedText = 'DOCUMENT_ID*' and @IsWrapped = 'True']");
    }
}