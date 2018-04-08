using ClosedXML.Excel;
using GameCollection.Contrat.Dto;
using GameCollection.ElasticSearch.Services;
using GameCollection.ElasticSearch.Tool.TinyCsv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCsvParser;

namespace GameCollection.ElasticSearch.Tool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("START ElasticSearch Tool");

            //var esIndexSvc = new EsIndexService("gamecollection", "http://localhost:9205");
            //esIndexSvc.CreateIndex();
            //esIndexSvc.DeleteIndex();

            //List<GameDto> gameLst = LoadByExcel(@"G:\gihub\GameCollection\GameCollection\GameCollection.ElasticSearch.Tool\TinyCsv\Mes_jeux_test.csv");

            var esDocumentSvc = new EsDocumentService<GameDto>("gamecollection", "http://localhost:9205");//"https://testamaappproxy-mcrsoftfluent730.msappproxy.net");
            var games = esDocumentSvc.SearchAll().Results.OrderBy(a => a.Name);

            games.ForEach(a => Console.WriteLine($"{a.Identifier} - {a.Name} - {a.Console}"));
            //esDocumentSvc.BulkInsert(lst);

            ExportPivotTable(games.ToList());

            Console.ReadKey();
        }

        private static void ExportPivotTable(List<GameDto> games)
        {
            var workbook = new XLWorkbook();
            var sheet = workbook.Worksheets.Add("Data");
            //sheet.Hide();
            // Insert our list of pastry data into the "PastrySalesData" sheet at cell 1,1
            var source = sheet.Cell(1, 1).InsertTable(games, "GamesData", true);

            // Create a range that includes our table, including the header row
            var range = source.DataRange;
            var header = sheet.Range(1, 1, 1, 3);
            var dataRange = sheet.Range(header.FirstCell(), range.LastCell());

            // Add a new sheet for our pivot table
            var ptSheet = workbook.Worksheets.Add("PivotTable");

            //ptSheet.Columns().AdjustToContents();
            // Create the pivot table, using the data from the "PastrySalesData" table
            var pt = ptSheet.PivotTables.AddNew("PivotTable", ptSheet.Cell(1, 1), dataRange)
                .SetShowGrandTotalsColumns(true)
                .SetAutofitColumns(true)
                .SetShowGrandTotalsRows(true);


            ptSheet.ColumnWidth = 23;
            // The rows in our pivot table will be the names of the pastries
            pt.RowLabels.Add("Console").SetSort(XLPivotSortType.Ascending);

            pt.RowHeaderCaption = "Etiquettes de lignes";
            // The columns will be the months
            //pt.ColumnLabels.Add("Genre");

            // The values in our table will come from the "NumberOfOrders" field
            // The default calculation setting is a total of each row/column
            pt.Values.Add("Name", "Nombre de jeux").SummaryFormula = XLPivotSummary.Count;
            //    .NumberFormat.SetFormat("#,##0.00");
            //          ptSheet.AddConditionalFormat().ColorScale()
            //.LowestValue(XLColor.Red)
            //.Midpoint(XLCFContentType.Percent, 50, XLColor.Yellow)
            //.HighestValue(XLColor.Green);


            //pt.ClassicPivotTableLayout = true;
            //pt.AutofitColumns = true;
            //pt.SetAutofitColumns(true);
            //pt.SetTitle("test");
            workbook.SaveAs($"Mes_jeux_{DateTime.Now.ToShortDateString()}.xlsx");
        }

        private static List<GameDto> LoadByExcel(string pathFile)
        {
            List<GameDto> lst = new List<GameDto>();
            //CsvParserOptions csvParserOptions = new CsvParserOptions(true, ';');
            //CsvGameMapping csvMapper = new CsvGameMapping();
            //CsvParser<Game> csvParser = new CsvParser<Game>(csvParserOptions, csvMapper);

            //var result = csvParser
            //    .ReadFromFile(pathFile, Encoding.ASCII)
            //    .ToList();

            //for (int i = 1; i <= result.Count; i++)
            //{
            //    var gameDto = new GameDto();
            //    gameDto.Identifier = i.ToString();
            //    gameDto.Name = result[i - 1].Result.Name;
            //    gameDto.Developper = result[i - 1].Result.Developper;
            //    gameDto.Console = result[i - 1].Result.Console;
            //    gameDto.Genre = result[i - 1].Result.Genre;

            //    lst.Add(gameDto);
            //}

            return lst;
        }
    }
}
