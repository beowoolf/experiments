using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSC
{
    internal class Excel
    {
        private readonly XSSFWorkbook _wb;

        public Excel(string path)
        {
            using (var fs = File.OpenRead(path))
            {
                _wb = new XSSFWorkbook(fs);
            }
        }

        public List<string> LoadUrlsFromSheet(string sheetName)
        {
            var list = new List<string>();
            var sheet = _wb.GetSheet(sheetName);

            var non_empty_rows = new List<IRow>();
            var enumerator = sheet.GetRowEnumerator();

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (current == null)
                    continue;

                var cells = new List<ICell>();
                for (int i = 0; i < 46; i++)
                {
                    cells.Add((current as IRow).GetCell(i, MissingCellPolicy.CREATE_NULL_AS_BLANK));
                }

                if (!cells.All(a => string.IsNullOrEmpty(a.ToString())))
                {
                    non_empty_rows.Add(current as IRow);
                };
            }

            non_empty_rows = non_empty_rows
                             .Skip(1)
                             .ToList();

            int skipped = 0;
            var wiersze = non_empty_rows.Count;
            var wiersze_z_adresami = 0;

            for (int i = 0; i < non_empty_rows.Count; i++)
            {
                var row = non_empty_rows[i];
                var cells = new List<ICell>();

                for (int j = 0; j < 46; j++)
                {
                    cells.Add(row.GetCell(j, MissingCellPolicy.CREATE_NULL_AS_BLANK));
                }

                if (cells.All(x => string.IsNullOrEmpty(x.ToString())))
                {
                    //Logger.sbWarn.Append($"Wiersz {i} - Wiersz jest pusty");
                    skipped++; continue;
                }

                if (cells.Count != 46)
                {
                    Console.WriteLine($"Wiersz\t{i}\tBłąd, nieoczekiwana ilość kolumn ({cells.Count})");
                    skipped++;
                    continue;
                }

                try
                {
                    var parsed = ParseRow(cells);

                    if (list.Any(x => x == parsed))
                    {
                        Console.WriteLine($"Wiersz\t{i}\t z danymi: {parsed} wystąpił więcej niż jeden raz, więc wybrane zostało tylko pierwsze wystąpienie");
                        skipped++;
                    }
                    else
                    {
                        list.Add(parsed);
                        wiersze_z_adresami++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Problem w wierszu {i} - {ex.Message} / {ex.StackTrace}");
                    skipped++;
                }
            }
            return list;
        }

        private static string ParseRow(List<NPOI.SS.UserModel.ICell> cells)
        {
            return cells[0].ToString();
        }
    }
}
