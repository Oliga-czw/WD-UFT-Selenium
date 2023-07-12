using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WD_UFT_Selenium_Auto.Library.UFTLibrary;

namespace WD_UFT_Selenium_Auto.Product.WD
{


    public class ClassMainInterFrame : UFT_InterFrame
    {

        public ClassMainInterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

    }


    public class Login_InterFrame : ClassMainInterFrame
    {
        public Login_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Editor userNameEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'User name:']");
        public UFT_Editor passwordEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Password:']");
        public UFT_Button loginbutton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'Button0']");
    }

    public class Home_InterFrame : ClassMainInterFrame
    {
        public Home_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Button OrderDispensing => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnOrderDispense']");
        public UFT_Button OpenWeigh => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnOpenWeigh']");
        //
        public UFT_Button BoothCleaning => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnBoothCleaning']");
        public UFT_Button ScaleChecking => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnScaleChecking']");
        public UFT_Button CampaignDispense => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnCampaignDispense']");
        public UFT_Button OrderKitting => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnOrderKitting']");
        public UFT_Button MaterialDispensing => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnMaterialDispense']");
        public UFT_Button LogOff => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnLogOff']");
        public UFT_Button Exit => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnExit']");
        public UFT_Label weightBooth => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'expOrderQty']");
        public UFT_Label operatorName => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'expOrderType']");
    }


    public class Dispensing_InterFrame : ClassMainInterFrame
    {
        public Dispensing_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        //public UFT_Editor userNameEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'User name:']");
        //public UFT_Editor passwordEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Password:']");
        public UFT_Table orderTable => new UFT_Table(_UFT_InterFrame, "//Table[@AttachedText = 'Orders:']");
        public UFT_Button next => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnNext']");
        public UFT_Button HomeButton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnHome']");
        public UFT_Editor Search => new UFT_Editor(_UFT_InterFrame, "//Editor[@ObjectName = 'txtSearch']");
        public UFT_Button SearchButton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnSearch']");
    }

    public class Material_InterFrame : ClassMainInterFrame
    {
        public Material_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Table materialTable => new UFT_Table(_UFT_InterFrame, "//Table[@AttachedText = 'Materials:']");
        public UFT_Button next => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'MaterialNext']");
        public UFT_Button cancel => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'CancelDispense']");
    }

    public class BoothClean_InterFrame : ClassMainInterFrame
    {
        public BoothClean_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button cleanComplete => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'Cleaning']");

        public UFT_Button HomeButton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'Button0']");

        //Current status
        public UFT_Label Status => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'DisplayLabel0']");
        public UFT_Label PreviousClean => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'DisplayLabel1']");
        //Last Dispense
        public UFT_Label Material => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'DisplayLabel2']");
        public UFT_Label Order => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'DisplayLabel3']");
        public UFT_Label Product => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'DisplayLabel4']");

    }
    public class Handle_Information_InterFrame : ClassMainInterFrame
    {
        public Handle_Information_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button Acknowledge => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnAcknowledge']");
    }
    public class ScaleWeight_InterFrame : ClassMainInterFrame
    {
        public ScaleWeight_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_List dispense_method => new UFT_List(_UFT_InterFrame, "//List[@TagName = 'Dispense method:']");
        public UFT_List scale => new UFT_List(_UFT_InterFrame, "//List[@TagName = 'Scale:']");
        public UFT_Editor barcode => new UFT_Editor(_UFT_InterFrame, "//Editor[@ObjectName = 'txtBarcode']");
        public UFT_Button zero => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'zeroBtn']");
        public UFT_Button tare => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'tareBtn']");
        public UFT_Button accept => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnAccept']");
        public UFT_Button cancel => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnCancel']");
        public UFT_Button reset => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnReset']");
        public UFT_Button NewSource => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnNewSource']");
        public UFT_Button Partial => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnPartial']");
        public UFT_Button HomeButton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'Button0']");
        public UFT_Editor tare_editor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Tare:']");
        public UFT_Label disploylMaeterial => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'dispLblMaeterial']");
        public UFT_Label Lot => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'dispLblLot']");
        public UFT_Label AvailQty => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'dispLblAvailQty']");
        public UFT_Label Status => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'dispLblStatus']");
        public UFT_Label Expiration => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'dispLblExpiration']");
        public UFT_Label Potency => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'dispLblPotency']");
        public UFT_Button comment => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'sbtnComment']");
        public UFT_UiObject weighBar => new UFT_UiObject(_UFT_InterFrame, "//UiObject[@ObjectName = 'WeighBar']");

        public UFT_Label WeightNumber => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'lblNumber']");
        //WeighBar
        public UFT_Editor net_editor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Net:']");


        public UFT_Button start => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'tareBtn']");
        //target container
        public UFT_Label tareLable => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'lblTare']");

        //Weighing Info --Source as target
        public UFT_Label InitailGross => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'lblTare']");
        public UFT_Label FinalGross => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'lblNet']");
        public UFT_Label Diffenence => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'lblGross']");
        public UFT_Editor SourceTare => new UFT_Editor(_UFT_InterFrame, "//Editor[@ObjectName = 'txtSourceTare']");

    }
    public class OpenWeight_InterFrame : ClassMainInterFrame
    {
        public OpenWeight_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_List Scale_select => new UFT_List(_UFT_InterFrame, "//List[@ObjectName = 'comboScale']");
        public UFT_Button zero => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnZero']");
        public UFT_Button tare => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnTare']");
        public UFT_Label ScaleReading => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'lblScaleReading']");
        public UFT_Label TarestLabel => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'displayTare']");
        public UFT_Label NetstLabel => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'displayNet']");
        public UFT_Label RangeMinLabel => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'lblMin']");
        public UFT_Label RangeMaxLabel => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'lblMax']");
        public UFT_Label ResolutionLabel => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'lblPrecision']");
        public UFT_Label GrossstLabel => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'displayGross']");
        public UFT_Button HomeButton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnCancel']");
        public UFT_Button PrintLabelButton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnPrintLabel']");

    }
    public class Material_Selection_InterFrame : ClassMainInterFrame
    {
        public Material_Selection_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Table materialTable => new UFT_Table(_UFT_InterFrame, "//Table[@TagName = 'Materials:']");
        public UFT_Button next => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnNext']");
        public UFT_Button HomeButton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnHome']");
        public UFT_Editor Search => new UFT_Editor(_UFT_InterFrame, "//Editor[@ObjectName = 'Search_Field']");
        public UFT_Button SearchButton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'Button0']");

    }
    public class ScaleCheck_InterFrame : ClassMainInterFrame
    {
        public ScaleCheck_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_List ScaleList => new UFT_List(_UFT_InterFrame, "//List[@ObjectName = 'cbxScales']");
        public UFT_Table Standardization_type => new UFT_Table(_UFT_InterFrame, "//Table[@TagName = 'Standardization Status']");
        public UFT_Button homeButton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName='Button0']");
        public UFT_List StandardizationList => new UFT_List(_UFT_InterFrame, "//List[@ObjectName = 'cbxStandzn']");
        public UFT_Button startcheck => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnStartCheck']");
        public UFT_Button testScale => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnTestScale']");
    }
    //TestScale_InterFrame
    public class TestScale_InterFrame : ClassMainInterFrame
    {
        public TestScale_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Editor RangeMin => new UFT_Editor(_UFT_InterFrame, "//Editor[@ObjectName = 'txtRangeMin']");
        public UFT_Button Apply => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnSaveScale']");
    }
    public class CheckWeight_InterFrame : ClassMainInterFrame
    {
        public CheckWeight_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Button cancelButton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnCancel']");
        // public UFT_List scale => new UFT_List(_UFT_InterFrame, "//List[@TagName = 'Scale:']");
        public UFT_Label CheckResult => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'lblInstruction']");
        public UFT_Label Standardization_label => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'DisplayLabel1']");
        public UFT_Label ExpirationPeriod_label => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'DisplayLabel3']");
        public UFT_Label LastCheckdate_label => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'DisplayLabel4']");
        public UFT_Label ExpirationDate_label => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'DisplayLabel5']");
        public UFT_Label ScaleResult_Label => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'ScaleResultLabel']");
        public UFT_Button zero => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnZero']");
        public UFT_Button readScale => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnReadScale']");
        public UFT_Button accept => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnAccept']");
        
        
        public UFT_Table checkTable => new UFT_Table(_UFT_InterFrame, "//Table[@AttachedText = 'Expiration date:']");
    }
    public class SelectAnOrderToKitting_InterFrame : ClassMainInterFrame
    {
        public SelectAnOrderToKitting_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Button HomeButton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnHome']");
        public UFT_Button StartKitButton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnNext']");
        public UFT_Button printButton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnReprint']");
        //public UFT_List ScaleList => new UFT_List(_UFT_InterFrame, "//List[@ObjectName = 'cbxScales']");
        public UFT_Table orderTable => new UFT_Table(_UFT_InterFrame, "//Table[@TagName = 'Orders:']");
        public UFT_Table KitTable => new UFT_Table(_UFT_InterFrame, "//Table[@TagName = 'Kit Assembly']");
        public UFT_Label selectedOrder => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'dispLblOrder']");
        public UFT_Editor barcodeEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@ObjectName = 'txtBarCode']");
        public UFT_Button accept => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnAccept']");
    }
    public class CampaignSelection_InterFrame : ClassMainInterFrame
    {
        public CampaignSelection_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Button homeButton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName='btnHome']");
        public UFT_Button nextButton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName='btnNext']");
        public UFT_Editor Search => new UFT_Editor(_UFT_InterFrame, "//Editor[@ObjectName = 'txtSearch']");
        public UFT_Button SearchButton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnSearch']");
        public UFT_Table CampaignsTable => new UFT_Table(_UFT_InterFrame, "//Table[@TagName = 'Campaigns:']");
        
    }
   
    //Handing_InterFrame
    public class Handing_InterFrame : ClassMainInterFrame
    {
        public Handing_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button AcknowledgeButton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'btnAcknowledge']");
    }
    //Comment_InterFrame
    public class Comment_InterFrame : ClassMainInterFrame
    {
        public Comment_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Editor commentEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@TagName = 'Comment:']");
        public UFT_Button OKButton => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'CommentOkBtn']");
    }
}

