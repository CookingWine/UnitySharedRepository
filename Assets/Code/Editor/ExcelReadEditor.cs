#if UNITY_EDITOR
using Excel;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEditor;
using UnityEngine;

public class IndividualData
{
    public string[] Values;
    public IndividualData( int Columns )
    {
        Values = new string[Columns];
    }
}

public class ExcelReadEditor
{
    public static Dictionary<string , IndividualData> LoadExcelAsDictionary( string path )
    {
        Dictionary<string , IndividualData> ItemDictionary = new( );//新建字典，用于存储以行为单位的各个操作单元

        FileStream fs = new( path , FileMode.Open , FileAccess.Read , FileShare.ReadWrite );//建立文件流fs

        IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader( fs );
        DataSet result = excelDataReader.AsDataSet( );

        // 配表中属性字段的数量
        int CountOfAttributes = result.Tables[0].Rows.Count;

        FileStream fsitem = new( path , FileMode.Open , FileAccess.Read , FileShare.ReadWrite );

        ExcelPackage excel = new( fsitem );

        ExcelWorksheets workSheets = excel.Workbook.Worksheets;//获取全部工作表

        ExcelWorksheet workSheet = workSheets[1];//只看第一个工作表，余者不看

        int colCount = workSheet.Dimension.End.Column;//工作表的列数
        int rowCount = workSheet.Dimension.End.Row;//工作表的行数

        for( int row = 2 ; row <= rowCount ; row++ )//从当前工作表的第二行遍历到最后一行(第一行是表头，所以不读取)
        {
            IndividualData item = new IndividualData( CountOfAttributes );//新建一个操作单元，开始接收本行数据

            for( int col = 1 ; col <= colCount ; col++ )//从第一列遍历到最后一列
            {
                //读取每个单元格中的数据
                item.Values[col - 1] = workSheet.Cells[row , col].Text;//将单元格中的数据写入操作单元
            }
            if( !ItemDictionary.ContainsKey( item.Values[0].ToString( ) ) )
            {
                ItemDictionary.Add( item.Values[0].ToString( ) , item );//将ID和操作单元写入字典
            }
            else
            {
                Debug.LogError( item.Values[0].ToString( ) );
            }
        }

        //Debug.Log( "complete" );
        return ItemDictionary;
    }
}

public class UpdateExcelEditor :Editor
{
    private readonly static string m_UIPrefabPath = Application.dataPath + "/AssetExcelData~/UIPrefabData.xlsx";

    private static Dictionary<string , IndividualData> m_ExcelDicData = new Dictionary<string , IndividualData>( );

    private readonly static UIPrefabData uIPrefabData = new UIPrefabData( );
    [MenuItem( "Tools/更新UI数据" , false , 10 )]
    public static void ManualOperationCheckLanguage( )
    {
        m_ExcelDicData.Clear( );
        m_ExcelDicData = ExcelReadEditor.LoadExcelAsDictionary( m_UIPrefabPath );

        foreach( var item in m_ExcelDicData )
        {
            UpdateUIPrefabData data = new UpdateUIPrefabData( item.Key , item.Value.Values[1] , item.Value.Values[2] );
            uIPrefabData.UIPrefabDatas.Add( data );
        }
        JsonExtend.WriterJson( uIPrefabData , Application.dataPath + "/Res/Confing/UIPrefabData.json" );
        AssetDatabase.Refresh( );
        Debug.Log( "数据更新完毕" );
    }
}
#endif