using Hangman.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hangman.View
{
    /// <summary>
    /// Interaction logic for StatisticsView.xaml
    /// </summary>
    public partial class StatisticsView : Window
    {
        public StatisticsView()
        {
            InitializeComponent();
            CreateTable();
        }

        public void CreateTable()
        {
            for (int indexColumn = 0; indexColumn < 7; indexColumn++)
            {
                StatisticsTable.Columns.Add(new TableColumn());
            }

            StatisticsTable.RowGroups.Add(new TableRowGroup()); 
            StatisticsTable.RowGroups[0].Rows.Add(new TableRow()); 
            TableRow currentRow = StatisticsTable.RowGroups[0].Rows[0]; 

            currentRow.FontSize = 18;
            currentRow.FontFamily = new FontFamily("Algerian");
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Users Statistics"))));
            currentRow.Cells[0].ColumnSpan = 7;

            StatisticsTable.RowGroups[0].Rows.Add(new TableRow());
            currentRow = StatisticsTable.RowGroups[0].Rows[1];
            currentRow.FontSize = 16;

            Paragraph tableHeader = new Paragraph(new Run("Username"));
            tableHeader.TextAlignment = TextAlignment.Center;
            currentRow.Cells.Add(new TableCell(tableHeader));

            tableHeader = new Paragraph(new Run("Games played"));
            tableHeader.TextAlignment = TextAlignment.Center;
            currentRow.Cells.Add(new TableCell(tableHeader));

            tableHeader = new Paragraph(new Run("All categories wins"));
            tableHeader.TextAlignment = TextAlignment.Center;
            currentRow.Cells.Add(new TableCell(tableHeader));

            tableHeader = new Paragraph(new Run("Animals wins"));
            tableHeader.TextAlignment = TextAlignment.Center;
            currentRow.Cells.Add(new TableCell(tableHeader));

            tableHeader = new Paragraph(new Run("Cars wins"));
            tableHeader.TextAlignment = TextAlignment.Center;
            currentRow.Cells.Add(new TableCell(tableHeader));

            tableHeader = new Paragraph(new Run("Countries wins"));
            tableHeader.TextAlignment = TextAlignment.Center;
            currentRow.Cells.Add(new TableCell(tableHeader));

            tableHeader = new Paragraph(new Run("Movies wins"));
            tableHeader.TextAlignment = TextAlignment.Center;
            currentRow.Cells.Add(new TableCell(tableHeader));

            using (StreamReader streamReader = File.OpenText("..\\..\\..\\Resources\\Statistics.json"))
            {
                string line = null;
                int indexCurrentUser = 2;
                while ((line = streamReader.ReadLine()) != null)
                {
                    UserStatisticModel currentUser = JsonConvert.DeserializeObject<UserStatisticModel>(line);

                    StatisticsTable.RowGroups[0].Rows.Add(new TableRow());
                    currentRow = StatisticsTable.RowGroups[0].Rows[indexCurrentUser];
                    currentRow.FontSize = 14;

                    Paragraph tableUserEntry = new Paragraph(new Run(currentUser.Username));
                    tableUserEntry.TextAlignment = TextAlignment.Center;
                    currentRow.Cells.Add(new TableCell(tableUserEntry));

                    tableUserEntry = new Paragraph(new Run(currentUser.GamesPlayed.ToString()));
                    tableUserEntry.TextAlignment = TextAlignment.Center;
                    currentRow.Cells.Add(new TableCell(tableUserEntry));

                    tableUserEntry = new Paragraph(new Run(currentUser.AllCategoriesWins.ToString()));
                    tableUserEntry.TextAlignment = TextAlignment.Center;
                    currentRow.Cells.Add(new TableCell(tableUserEntry));

                    tableUserEntry = new Paragraph(new Run(currentUser.AnimalsWins.ToString()));
                    tableUserEntry.TextAlignment = TextAlignment.Center;
                    currentRow.Cells.Add(new TableCell(tableUserEntry));

                    tableUserEntry = new Paragraph(new Run(currentUser.CarsWins.ToString()));
                    tableUserEntry.TextAlignment = TextAlignment.Center;
                    currentRow.Cells.Add(new TableCell(tableUserEntry));

                    tableUserEntry = new Paragraph(new Run(currentUser.CountriesWins.ToString()));
                    tableUserEntry.TextAlignment = TextAlignment.Center;
                    currentRow.Cells.Add(new TableCell(tableUserEntry));

                    tableUserEntry = new Paragraph(new Run(currentUser.MoviesWins.ToString()));
                    tableUserEntry.TextAlignment = TextAlignment.Center;
                    currentRow.Cells.Add(new TableCell(tableUserEntry));

                    indexCurrentUser++;
                }
            }
        }
    }
}
