using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTableExt;

namespace PhoneBookWithEntiyFrameWork
{
    internal class TableVisualisation
    {

        //this method is used to display the table using the ConsoleTableExt package
        internal static void ShowTable<T>(List<T> tableData) where T : class
        {
            Console.WriteLine("\n\n");

            ConsoleTableBuilder
                .From(tableData)
                .WithTitle("Phone Book")
                .ExportAndWriteLine();
            Console.WriteLine("\n\n");



        }
    }
}