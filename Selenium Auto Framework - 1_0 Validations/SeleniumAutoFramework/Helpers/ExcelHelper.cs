using ExcelDataReader;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;


namespace SeleniumAutoFramework.Helpers;

public class ExcelHelper
{

    private static object mylock = new object();
    public List<SheetDetails> SheetsName = new List<SheetDetails>();

    //STORING ALL EXCEL VALUES TO A MEMORY LOCATION
    public List<DataCollection> StoreExcelValuesToCollection(string fileName, string sheetName, DataTableCollection dataCollection)
    {
        List<DataCollection> _dataCol = new List<DataCollection>();
        _dataCol.Clear();
        try
        {
            DataTable resultTable = dataCollection[sheetName];

            if (resultTable != null && dataCollection[sheetName].Rows.Count > 0)
            {
                for (int row = 1; row <= resultTable.Rows.Count; row++)
                {
                    for (int col = 0; col < resultTable.Columns.Count; col++)
                    {
                        DataCollection dtTable = new DataCollection()
                        {
                            rowNumber = "Scenario" + row,
                            colName = resultTable.Columns[col].ColumnName,
                            colValue = resultTable.Rows[row - 1][col].ToString(),
                            sheetNm = resultTable.TableName

                        };
                        //Add all the details for each row
                        _dataCol.Add(dtTable);
                    }
                }

            }
            return _dataCol;
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            _dataCol.Clear();
            return _dataCol;
        }
    }

    //READING AND STORING ALL VALUES FROM EXCEL IN TO IN MEMORY COLLECTION
    public DataTableCollection LoadExcelToDataTable(string fileName)
    {
        List<DataCollection> _dataCol = new List<DataCollection>(); //Look in to this ****
        _dataCol.Clear();
        lock (mylock)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            //OPEN FILE AND RETURN AS STREAM
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                    //Get all the Tables
                    DataTableCollection tableCollection = result.Tables;

                    //Get Sheet Details
                    SheetDetails st = new SheetDetails();
                    for (int i = 0; i < tableCollection.Count; i++)
                    {
                        st.sheetName = tableCollection[i].TableName;
                        st.rowNo = i + 1;
                    }
                    stream.Dispose();
                    return tableCollection;
                }
            }
        }
    }

    //READING DATA FROM EXCEL COLLECTION
    public string ReadDataUsingRowNo(string columnName, List<DataCollection> datacol, string RowNo)
    {
        try
        {
            string data = null;
            // Retrieving data using LINQ to reduce iterations
            data = (from colData in datacol
                    where colData.colName == columnName && colData.rowNumber == RowNo
                    select colData.colValue).FirstOrDefault();

            Console.WriteLine("Col Name: " + columnName + "RowNo: " + RowNo + "RetrievedData " + data);
            return data.ToString();

        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            return null;
        }
    }


    //READING AND STORING ALL VALUES FROM REQUIRED ROW FROM A EXCEL FILE
    public string[] returnAllDataFromAnExcelRows(string fileName, string sheetName, DataTableCollection dataCollection, int requiredRowNum)
    {
        try
        {
            DataTable resultTable = dataCollection[sheetName];
            string[] rowValues = new string[100];
            string rowValue = null;
            if (dataCollection[sheetName].Rows.Count > 0)
            {
                rowValues = new string[resultTable.Columns.Count];
                for (int col = 0; col < resultTable.Columns.Count; col++)
                {
                    if (requiredRowNum == 1)
                        rowValue = resultTable.Columns[col].ColumnName;
                    else
                        rowValue = resultTable.Rows[requiredRowNum - 2][col].ToString();
                    //Add all the details for each row
                    rowValues[col] = rowValue.ToString();
                }

            }
            return rowValues;
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            return null;
        }
    }

    //THIS METHOD IS USED TO RETRIEVE EXCEL VALUE WITH RESPECTIVE COLUMN NAME
    public string ExcelValue(string columnName, List<DataCollection> datacol, string RowNo)
    {
        string value = ReadDataUsingRowNo(columnName, datacol, RowNo);
        Assert.IsTrue(value != null, $"Value Was Not Retrived From ExcelSheet For ColumnName : {columnName}");
        return value;
    }

}


public class DataCollection
{
    public string rowNumber { get; set; }
    public string colName { get; set; }
    public string colValue { get; set; }
    public string sheetNm { get; set; }
}


public class SheetDetails
{
    public int rowNo { get; set; }
    public string sheetName { get; set; }
    public string excelName { get; set; }
}

