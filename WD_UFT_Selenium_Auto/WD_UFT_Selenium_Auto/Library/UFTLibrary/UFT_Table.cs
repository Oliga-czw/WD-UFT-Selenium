using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace WD_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class UFT_Table
    {
        public ITable _UFT_Table
        {
            get;
            private set;
        }
        public UFT_Table(ITable table)
        {
            _UFT_Table = table;
        }
        public UFT_Table(ITestObject parentObject, string xpath)
        {

            _UFT_Table = UFT_Xpath.GetChildObject<ITable>(parentObject, xpath);
        }
        public List<string> Columns
        {
            get
            {
                return _UFT_Table.Rows[0].Cells.Select(anyColumn => anyColumn.ColumnHeader.Trim()).ToList();
            }
        }
        public UFT_TableRow Row(string rowText, string ColumnHeader = null)
        {
            string actualColumn = ColumnHeader;
            ITableRow resultRow = null;
            var tableRows = _UFT_Table.Rows;

            if (ColumnHeader == null)
            {
                resultRow = _UFT_Table.Rows.Where(anyRow
                    => anyRow.Cells.Any(anyCell => rowText.Trim() == anyCell.Value.ToString().Trim()))
                    .FirstOrDefault();
            }
            else
            {
                resultRow = _UFT_Table.Rows.Where(anyRow
                    => anyRow.Cells.Any(anyCell => actualColumn == anyCell.ColumnHeader.Trim()
                   && rowText.Trim() == anyCell.Value.ToString().Trim())).FirstOrDefault();
            }

            if (resultRow != null)
            {
                Console.WriteLine($"---------- Row Found ----------");
                resultRow.Cells.ToList().ForEach(eachCell
                    => Console.WriteLine($"{eachCell.ColumnHeader} : {eachCell.Value.ToString()}"));
                Console.WriteLine($"-------------------------------");
            }

            return new UFT_TableRow(_UFT_Table, resultRow);
        }
        public void Click()
        {
            _UFT_Table.Click();
        }
        public void SelectCell(int row, int column)
        {
            _UFT_Table.SelectCell(row, column);
        }
        public void SelectRows(int row)
        {
            _UFT_Table.SelectRows(row);
        }

        public int Rowscount()
        {
            var rows = _UFT_Table.Rows;
            return rows.Count();
        }
        public void SelectCell(int row, string columnheader)
        {
            _UFT_Table.SelectCell(row, columnheader);
        }
        public ITableCell GetCell(int row, string columnheader)
        {
            return _UFT_Table.GetCell(row, columnheader);
        }

    }


public class UFT_TableRow
{
    private ITable _ITable
    {
        set;
        get;
    }
    private ITableRow _ITableRow
    {
        get;
        set;
    }
    public UFT_TableRow(ITable table, ITableRow tableRow)
    {
        _ITable = table;
        _ITableRow = tableRow;
    }


    public bool Existing
    {
        get
        {
            return _ITableRow != null;
        }
    }
    public void Click()
    {
        var resultCell = new UFT_TableCell(_ITable, _ITableRow.Cells[0]);
        resultCell.Click();
    }
    public string Value(string columnName)
    {
        return Cell(columnName).Value;
    }
    public UFT_TableCell Cell(string columnName)
    {
        var resultCell = _ITableRow.Cells.Where(anyCell => columnName.Trim() == anyCell.ColumnHeader.Trim()).FirstOrDefault();
        return new UFT_TableCell(_ITable, resultCell);
    }
}

public class UFT_TableCell
{
    private ITable _ITable
    {
        set;
        get;
    }
    private ITableCell _ITableCell
    {
        set;
        get;
    }
    public UFT_TableCell(ITable table, ITableCell tableCell)
    {
        _ITable = table;
        _ITableCell = tableCell;
    }
    public string Value
    {
        get
        {
            return _ITableCell.Value.ToString();
        }
    }
    public void Click()
    {

        _ITableCell.DragAndDropOn(_ITableCell);

        //int clickX = _ITableCell.X + _ITableCell.Width / 2;
        //int clickY = _ITableCell.Y + _ITableCell.Height / 2;

        //Console.WriteLine($"Click cell on point ({clickX}, {clickY})");

        //_ITable.Click(new ClickArgs
        //{
        //    Button = MouseButton.Left,
        //    Location = new Location(Position.TopLeft, new Point(clickX, clickY))
        //});
    }

}


    //public class UFT_WPFLabelRow
    //{
    //    public IUiObject _label
    //    {
    //        private set;
    //        get;
    //    }
    //    public UftDynamicProperty Parent
    //    {
    //        set;
    //        get;
    //    }
    //    public UFT_WPFLabelRow(IUiObject uiObjectLabel)
    //    {
    //        _label = uiObjectLabel;
    //        Parent = uiObjectLabel.NativeParent();
    //    }
    //    private List<string> _AllBrothers
    //    {
    //        get
    //        {
    //            return Parent.SubProperty("Children").ToList().Select(select => select.ActualValue).ToList();
    //        }
    //    }
    //    private int ValueIndex
    //    {
    //        get
    //        {
    //            int reusltIndex = _AllBrothers.IndexOf(_label.Text);
    //            Console.WriteLine($"Label index: {reusltIndex}, text: '{_label.Text}'");
    //            return reusltIndex + 1;
    //        }
    //    }
    //    public string Value
    //    {
    //        get
    //        {
    //            return _AllBrothers[ValueIndex];
    //        }
    //    }
    //}

    //public class UFT_WPFDataGridCell
    //{
    //    private IUiObject _DataGrid;
    //    public UftDynamicProperty _CellDynamic
    //    {
    //        get;
    //        private set;
    //    }
    //    public UFT_WPFDataGridCell(IUiObject objectDataGrid, UftDynamicProperty cellDynamic)
    //    {
    //        _DataGrid = objectDataGrid;
    //        _CellDynamic = cellDynamic;
    //    }
    //    public void Click()
    //    {
    //        var propertyRenderPosition = _CellDynamic.SubProperty("RenderPosition");
    //        int clickX = propertyRenderPosition.SubProperty("X").ToInt();
    //        int clickY = propertyRenderPosition.SubProperty("Y").ToInt();
    //        ClickArgs clickArgs = new ClickArgs
    //        {
    //            Button = MouseButton.Left,
    //            Location = new Location(Position.TopLeft, new Point(clickX + 5, clickY + 5)),
    //        };
    //        _DataGrid.Click(clickArgs);
    //    }
    //    public string Value
    //    {
    //        get
    //        {
    //            return _CellDynamic.SubProperty("Cell").SubProperty("DisplayValue").Value;
    //        }
    //    }

    //}
    //public class UFT_WPFDataGridRow
    //{

    //    private IUiObject _DataGrid;
    //    private string _RowText;
    //    private string _ColumnHeader;
    //    private UftDynamicProperty _ResultCell;
    //    public UftDynamicProperty _RowDynamic
    //    {
    //        private set;
    //        get;
    //    }
    //    public UFT_WPFDataGridRow(IUiObject objectDataGrid, UftDynamicProperty rowDynamic)
    //    {
    //        _DataGrid = objectDataGrid;
    //        _RowDynamic = rowDynamic;
    //    }
    //    public UftDynamicProperty CellDynamicProperty(int columnIndex)
    //    {
    //        return _RowDynamic.SubProperty("Children").ToList()[columnIndex];
    //    }
    //    public UftDynamicProperty CellDynamicProperty(string ColumnHeader)
    //    {
    //        var rowList = _RowDynamic.SubProperty("Children").ToList().Where(anyCell => anyCell.Existing("Cell"));
    //        Console.WriteLine(rowList.Count());

    //        return rowList.Where(anyCell => ColumnHeader.Trim() == anyCell.SubProperty("Cell").SubProperty("Column").SubProperty("Caption").Value.Trim()).FirstOrDefault();
    //    }
    //    public UFT_WPFDataGridCell Cell(int columnIndex)
    //    {
    //        return new UFT_WPFDataGridCell(_DataGrid, CellDynamicProperty(columnIndex));
    //    }
    //    public UFT_WPFDataGridCell Cell(string ColumnHeader)
    //    {
    //        return new UFT_WPFDataGridCell(_DataGrid, CellDynamicProperty(ColumnHeader));
    //    }

    //}

    //public class UFT_WPFDataGrid
    //{
    //    public IUiObject _DataGrid
    //    {
    //        private set;
    //        get;
    //    }
    //    private List<IUiObject> allRowIdentifyOrders;
    //    public UFT_WPFDataGrid(IUiObject dataGridObject)
    //    {
    //        _DataGrid = dataGridObject;
    //        allRowIdentifyOrders = _DataGrid.FindChildren<IUiObject>(
    //               new UiObjectDescription
    //               {
    //                           //NativeClass = "AspenTech.SCM.UI.DataGrid.Internal.RowHeaderPresenter",
    //                           Name = "RowHeaderPresenter",
    //               }).OrderBy(orderElement => orderElement.Location.Y).ToList();
    //    }
    //    public UFT_WPFDataGridRow Row(int rowIndex)
    //    {
    //        var rowIdentify = allRowIdentifyOrders[rowIndex];

    //        rowIdentify.Highlight();
    //        var rowDynamic = rowIdentify.NativeParent();

    //        return new UFT_WPFDataGridRow(_DataGrid, rowDynamic);
    //    }
    //    public UFT_WPFDataGridRow Row(string rowText, string ColumnHeader = null)
    //    {
    //        IUiObject rowIdentify = null;
    //        UftDynamicProperty rowDynamic = null;
    //        if (ColumnHeader == null)
    //        {
    //            rowIdentify = _DataGrid._WPFTextBlock(rowText);
    //            rowDynamic = rowIdentify.FindNativeParentViaProperty("Row");
    //        }
    //        else
    //        {
    //            rowIdentify = _DataGrid.FindChildrenWPFTextBlock(rowText).Where(anyTextBlock =>
    //            ColumnHeader.Trim() == anyTextBlock.FindNativeParentViaProperty("Cell").SubProperty("Cell").SubProperty("Column").SubProperty("Caption").Value.Trim()).FirstOrDefault();
    //            rowDynamic = rowIdentify.FindNativeParentViaProperty("Row");
    //        }

    //        if (rowIdentify != null)
    //        {
    //            rowIdentify.Highlight();
    //        }

    //        return new UFT_WPFDataGridRow(_DataGrid, rowDynamic);
    //    }
    //}


    //public class UFT_JavaTableColumStruct
    //{
    //    public int Index
    //    {
    //        get;
    //        private set;
    //    }
    //    public string Name
    //    {
    //        get;
    //        private set;
    //    }
    //    public Point Location
    //    {
    //        get;
    //        private set;
    //    }
    //    public UFT_JavaTableColumStruct(int columnIndex, string ColumnHeader, Point columnPoint)
    //    {
    //        Index = columnIndex;
    //        Name = ColumnHeader;
    //        Location = columnPoint;
    //    }
    //    public void Print()
    //    {
    //        Console.WriteLine($"Index: {Index}, Name: {Name}, Point: {Location}");
    //    }
    //}

    //public class UFT_JavaTableRowStruct
    //{
    //    /// <summary>
    //    /// First cell point
    //    /// </summary>
    //    public Point Location
    //    {
    //        get;
    //        private set;
    //    }
    //    private List<UFT_JavaTableColumStruct> _Columns;
    //    public UFT_JavaTableRowStruct(Point rowPoint, List<UFT_JavaTableColumStruct> tableColumns)
    //    {
    //        Location = rowPoint;
    //        _Columns = tableColumns;
    //    }
    //    public UFT_JavaTableCellStruct Cell(int columnIndex)
    //    {
    //        int cellY = Location.Y;
    //        int cellX = _Columns[columnIndex].Location.X;
    //        return new UFT_JavaTableCellStruct(new Point(cellX, cellY));
    //    }
    //    public UFT_JavaTableCellStruct Cell(string ColumnHeader)
    //    {
    //        int cellY = Location.Y;
    //        int cellX = _Columns.Where(anyColumn => ColumnHeader.Trim() == anyColumn.Name.Trim()).FirstOrDefault().Location.X;
    //        return new UFT_JavaTableCellStruct(new Point(cellX, cellY));
    //    }
    //    public UFT_JavaTableCellStruct Cell(UFT_JavaTableColumStruct column)
    //    {
    //        int cellY = Location.Y;
    //        int cellX = column.Location.X;
    //        return new UFT_JavaTableCellStruct(new Point(cellX, cellY));
    //    }
    //}

    //public class UFT_JavaTableCellStruct
    //{
    //    public Point Location
    //    {
    //        get;
    //        private set;
    //    }
    //    public UFT_JavaTableCellStruct(Point cellPoint)
    //    {
    //        Location = cellPoint;
    //    }
    //}

    //public class UFT_WPFTableStruct
    //{
    //    protected ITable _Table;
    //    public int ColumnHeight
    //    {
    //        get;
    //        protected set;
    //    }
    //    public int RowHeight
    //    {
    //        get;
    //        protected set;
    //    }
    //    public Size Size
    //    {
    //        get;
    //        protected set;
    //    }
    //    public List<UFT_WPFTableColumStruct> Columns
    //    {
    //        get;
    //        set;
    //    }
    //    private int _FirstColumnWidth;
    //    public UFT_WPFTableColumStruct Column(int columnIndex)
    //    {
    //        return Columns.Where(anyColumn => columnIndex == anyColumn.Index).FirstOrDefault();
    //    }
    //    public UFT_WPFTableColumStruct Column(string ColumnHeader)
    //    {
    //        return Columns.Where(anyColumn => ColumnHeader.Trim() == anyColumn.Name.Trim()).FirstOrDefault();
    //    }
    //    public UFT_WPFTableRowStruct Row(int rowIndex)
    //    {
    //        int rowY = ColumnHeight + RowHeight * rowIndex - Convert.ToInt32(RowHeight / 2);
    //        int rowX = Convert.ToInt32(_FirstColumnWidth / 2);
    //        return new UFT_WPFTableRowStruct(new Point(rowX, rowY), Columns);
    //    }
    //    public UFT_WPFTableStruct(ITable iTable)
    //    {
    //        Columns = new List<UFT_WPFTableColumStruct>();

    //        _Table = iTable;
    //        var columns = iTable.Property("Columns").ToList();

    //        RowHeight = _Table.Property("RowHeight").ToInt();
    //        string[] xy = iTable.Property("RenderSize").Value.Split(',');

    //        var tableWidth = Convert.ToInt32(xy.First().Trim());
    //        Console.WriteLine($"tableWidth:{tableWidth}");

    //        var tableHeight = Convert.ToInt32(xy.Last().Trim());
    //        Console.WriteLine($"tableHeight:{tableHeight}");

    //        Size = new Size(tableWidth, tableHeight);
    //        int rowsHeight = iTable.Rows.Count() * RowHeight;

    //        ColumnHeight = tableHeight - rowsHeight;

    //        int columnsHeaderWidth = 0;
    //        iTable.Property("Columns").ToList().ForEach(each => columnsHeaderWidth = columnsHeaderWidth + each.SubProperty("ActualWidth").ToInt());
    //        _FirstColumnWidth = tableWidth - columnsHeaderWidth;

    //        int columnPointY = Convert.ToInt32(ColumnHeight / 2);
    //        Columns.Add(new UFT_WPFTableColumStruct(
    //            //iTable,
    //            0, "", new Point(Convert.ToInt32(_FirstColumnWidth / 2), columnPointY)));

    //        int columnIndex = 1;
    //        int initialPointX = _FirstColumnWidth;
    //        foreach (var eachColumn in columns)
    //        {
    //            int currentColumnWidth = eachColumn.SubProperty("ActualWidth").ToInt();
    //            Point eachPoint = new Point(initialPointX + Convert.ToInt32(currentColumnWidth / 2), columnPointY);
    //            Columns.Add(new UFT_WPFTableColumStruct(
    //                //iTable, 
    //                columnIndex, eachColumn.SubProperty("Header").SubProperty("Header").Value, eachPoint));

    //            initialPointX = initialPointX + currentColumnWidth;
    //            columnIndex = columnIndex + 1;
    //        }
    //    }
    //    public void Click(UFT_WPFTableColumStruct column)
    //    {
    //        Click(column.Location);
    //    }
    //    public void Click(UFT_WPFTableRowStruct row)
    //    {
    //        Click(row.Location);
    //    }
    //    public void Click(UFT_WPFTableCellStruct cell)
    //    {
    //        Click(cell.Location);
    //    }
    //    public void Click(Point tableLocation)
    //    {
    //        var clickArgs = new ClickArgs();
    //        clickArgs.Button = MouseButton.Left;
    //        clickArgs.Location = new Location(Position.TopLeft, tableLocation);
    //        Console.WriteLine($"Click Table Location: {tableLocation}");
    //        _Table.Click(clickArgs);
    //        Thread.Sleep(1000);
    //    }
    //    public BaseTextTable TextTable
    //    {
    //        get
    //        {
    //            Click(new Point(5, 5));
    //            Thread.Sleep(1000);
    //            HP.LFT.SDK.Keyboard.PressKey(HP.LFT.SDK.Keyboard.Keys.Escape);
    //            HP.LFT.SDK.Keyboard.KeyDown(HP.LFT.SDK.Keyboard.Keys.Control);
    //            HP.LFT.SDK.Keyboard.PressKey(HP.LFT.SDK.Keyboard.Keys.A);
    //            Thread.Sleep(1000);
    //            HP.LFT.SDK.Keyboard.PressKey(HP.LFT.SDK.Keyboard.Keys.C);
    //            Thread.Sleep(1000);
    //            HP.LFT.SDK.Keyboard.KeyUp(HP.LFT.SDK.Keyboard.Keys.Control);

    //            return new BaseTextTable(Clipboard.GetText()).SetFirstRowAsColumns();
    //        }
    //    }

    //}
}


