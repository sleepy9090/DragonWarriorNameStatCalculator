/**
 *  @file           DragonWarriorNameStatCalculator.cs
 *  @brief          Defines the logic for calculating the stats from the entered hero name.
 *  
 *  @copyright      2019 Shawn M. Crawford [sleepy]
 *  @date           10/17/2019
 *
 *  @remark Author  Shawn M. Crawford [sleepy]
 *
 *  @note           N/A
 *
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DragonWarriorNameStatCalculator
{
    public partial class FormDWNameStatCalc : Form
    {
        private static readonly string[] _EXPERIENCE_POINTS = { "0", "7", "23", "47", "110", "220", "450", "800", "1300", "2000",
            "2900", "4000", "5500", "7500", "10000", "13000", "17000", "21000", "25000", "29000",
            "33000", "37000", "41000", "45000", "49000", "53000", "57000", "61000", "65000", "65535" };

        private static readonly int[] _STRENGTH_GROWTH_STATS_1 = { 0, 1, 3, 3, 8, 12, 14, 18, 26, 31, 36, 44, 48, 56, 64, 68, 68, 81, 83, 88, 91, 93, 95, 99, 109, 113, 121, 126, 131, 136 };

        private static readonly int[] _STRENGTH_GROWTH_STATS_2 = { 0, 1, 3, 3, 7, 11, 13, 16, 24, 28, 33, 40, 43, 51, 58, 61, 61, 73, 75, 79, 82, 84, 86, 89, 98, 102, 109, 114, 118, 123 };

        private static readonly int[] _AGILITY_GROWTH_STATS_1 = { 0, 0, 2, 4, 6, 6, 13, 16, 18, 27, 31, 36, 44, 51, 60, 66, 74, 80, 82, 84, 86, 86, 90, 94, 96, 101, 103, 111, 116, 126 };

        private static readonly int[] _AGILITY_GROWTH_STATS_2 = { 0, 0, 2, 4, 6, 6, 12, 15, 16, 24, 28, 33, 40, 46, 54, 60, 67, 72, 74, 76, 78, 78, 81, 85, 87, 91, 93, 100, 105, 114 };

        private static readonly int[] _HP_GROWTH_STATS_1 = { 0, 7, 9, 16, 20, 23, 25, 31, 35, 39, 47, 48, 55, 63, 71, 77, 85, 100, 115, 123, 134, 143, 150, 155, 159, 165, 174, 180, 185, 195 };

        private static readonly int[] _HP_GROWTH_STATS_2 = { 0, 6, 8, 14, 18, 21, 23, 28, 32, 35, 42, 43, 50, 57, 64, 69, 77, 90, 104, 111, 121, 129, 135, 140, 143, 149, 157, 162, 167, 176 };

        private static readonly int[] _MP_GROWTH_STATS_1 = { 0, 0, 0, 11, 15, 19, 21, 24, 31, 35, 45, 53, 59, 65, 67, 90, 95, 103, 110, 123, 130, 141, 148, 156, 156, 163, 170, 175, 185, 195 };

        private static readonly int[] _MP_GROWTH_STATS_2 = { 0, 0, 0, 10, 14, 17, 19, 22, 28, 32, 41, 48, 53, 59, 60, 81, 86, 93, 99, 111, 117, 127, 133, 140, 140, 148, 153, 158, 167, 176 };

        private static readonly string[] _SPELLS_GROWTH = { "", "", "HEAL", "HURT", "", "", "SLEEP", "", "RADIANT", "STOPSPELL",
            "", "OUTSIDE", "RETURN", "", "REPEL", "", "HEALMORE", "", "HURTMORE", "", "", "", "", "", "", "", "", "", "", "", "" };

        private static readonly List<string> _COLUMN_NAMES = new List<string>
        {
            "Level",
            "Experience",
            "Strength",
            "Agility",
            "HP",
            "MP",
            "Spells"
        };

        public FormDWNameStatCalc()
        {
            InitializeComponent();

            textBoxName.MaxLength = 8;
            dataGridView.DataSource = ClearTable();
            SetColumnWidth();
        }

        public DataTable BuildStatTable(int initialStrengthStat, int initialAgilityStat, int initialHPStat, int initialMPStat, char growthType)
        {
            DataTable dataTable = new DataTable();
            int row = 0;
            
            foreach (string columnName in _COLUMN_NAMES)
            {
                dataTable.Columns.Add(columnName);
            }
            
            for (int x = 0; x < 30; x++)
            {
                dataTable.Rows.Add();
                dataTable.Rows[row][0] = (x + 1).ToString();
                dataTable.Rows[row][1] = _EXPERIENCE_POINTS[x];

                switch (growthType)
                {
                    case 'A':
                        dataTable.Rows[row][2] = initialStrengthStat + _STRENGTH_GROWTH_STATS_1[x];
                        dataTable.Rows[row][3] = initialAgilityStat + _AGILITY_GROWTH_STATS_2[x];
                        dataTable.Rows[row][4] = initialHPStat + _HP_GROWTH_STATS_1[x];
                        dataTable.Rows[row][5] = initialMPStat + _MP_GROWTH_STATS_2[x];
                        break;
                    case 'B':
                        dataTable.Rows[row][2] = initialStrengthStat + _STRENGTH_GROWTH_STATS_2[x];
                        dataTable.Rows[row][3] = initialAgilityStat + _AGILITY_GROWTH_STATS_1[x];
                        dataTable.Rows[row][4] = initialHPStat + _HP_GROWTH_STATS_2[x];
                        dataTable.Rows[row][5] = initialMPStat + _MP_GROWTH_STATS_1[x];
                        break;
                    case 'C':
                        dataTable.Rows[row][2] = initialStrengthStat + _STRENGTH_GROWTH_STATS_1[x];
                        dataTable.Rows[row][3] = initialAgilityStat + _AGILITY_GROWTH_STATS_1[x];
                        dataTable.Rows[row][4] = initialHPStat + _HP_GROWTH_STATS_2[x];
                        dataTable.Rows[row][5] = initialMPStat + _MP_GROWTH_STATS_2[x];
                        break;
                    case 'D':
                        dataTable.Rows[row][2] = initialStrengthStat + _STRENGTH_GROWTH_STATS_2[x];
                        dataTable.Rows[row][3] = initialAgilityStat + _AGILITY_GROWTH_STATS_2[x];
                        dataTable.Rows[row][4] = initialHPStat + _HP_GROWTH_STATS_1[x];
                        dataTable.Rows[row][5] = initialMPStat + _MP_GROWTH_STATS_1[x];
                        break;
                    default:
                        dataTable.Rows[row][2] = "Error";
                        dataTable.Rows[row][3] = "Error";
                        dataTable.Rows[row][4] = "Error";
                        dataTable.Rows[row][5] = "Error";
                        break;
                }

                dataTable.Rows[row][6] = _SPELLS_GROWTH[x];
                row++;
            }
            
            return dataTable;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            string heroName = textBoxName.Text;
            int totalValue;
            int remainder;
            int strengthStat;
            int agilityStat;
            int hpStat;
            int mpStat;
            char growthType;

            if (heroName.Length != 0)
            {
                totalValue = GetCharValue(heroName[0]);

                if (heroName.Length >= 2)
                {
                    totalValue += GetCharValue(heroName[1]);
                }

                if (heroName.Length >= 3)
                {
                    totalValue += GetCharValue(heroName[2]);
                }

                if (heroName.Length >= 4)
                {
                    totalValue += GetCharValue(heroName[3]);
                }

                remainder = totalValue % 16;
                strengthStat = GetInitialStrengthStat(remainder);
                agilityStat = GetInitialAgilityStat(remainder);
                hpStat = GetInitialHPStat(remainder);
                mpStat = GetInitialMPStat(remainder);
                growthType = GetGrowthType(remainder);

                if (strengthStat == -1 || agilityStat == -1 || hpStat == -1 || mpStat == -1 || growthType == '?')
                {
                    MessageBox.Show(@"Invalid character in name.", @"Name Stat Calc", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    dataGridView.DataSource = ClearTable();
                }
                else
                {
                    dataGridView.DataSource = BuildStatTable(strengthStat, agilityStat, hpStat, mpStat, growthType);
                }
            }
            else
            {
                MessageBox.Show(@"Please enter a name for the hero.", @"Name Stat Calc", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                dataGridView.DataSource = ClearTable();
            }

            SetColumnWidth();
        }

        private void SetColumnWidth()
        {
            DataGridViewColumn column = dataGridView.Columns[0];
            column.Width = 60;
            column = dataGridView.Columns[1];
            column.Width = 60;
            column = dataGridView.Columns[2];
            column.Width = 60;
            column = dataGridView.Columns[3];
            column.Width = 60;
            column = dataGridView.Columns[4];
            column.Width = 60;
            column = dataGridView.Columns[5];
            column.Width = 60;
        }

        private DataTable ClearTable()
        {
            DataTable dataTable = new DataTable();
            int row = 0;

            foreach (string columnName in _COLUMN_NAMES)
            {
                dataTable.Columns.Add(columnName);
            }

            for (int x = 0; x < 30; x++)
            {
                dataTable.Rows.Add();
                dataTable.Rows[row][0] = (x + 1).ToString();
                dataTable.Rows[row][1] = _EXPERIENCE_POINTS[x];
                dataTable.Rows[row][2] = "";
                dataTable.Rows[row][3] = "";
                dataTable.Rows[row][4] = "";
                dataTable.Rows[row][5] = "";
                dataTable.Rows[row][6] = "";

                row++;
            }

            return dataTable;
        }

        private int GetCharValue(char c)
        {
            int value;

            switch (c)
            {
                case ' ':
                case 'g':
                case 'w':
                case 'M':
                case '\'':
                    value = 0;
                    break;
                case 'h':
                case 'x':
                case 'N':
                    value = 1;
                    break;
                case 'i':
                case 'y':
                case 'O':
                    value = 2;
                    break;
                case 'j':
                case 'z':
                case 'P':
                    value = 3;
                    break;
                case 'k':
                case 'A':
                case 'Q':
                    value = 4;
                    break;
                case 'l':
                case 'B':
                case 'R':
                    value = 5;
                    break;
                case 'm':
                case 'C':
                case 'S':
                    value = 6;
                    break;
                case 'n':
                case 'D':
                case 'T':
                case '.':
                    value = 7;
                    break;
                case 'o':
                case 'E':
                case 'U':
                case ',':
                    value = 8;
                    break;
                case 'p':
                case 'F':
                case 'V':
                case '-':
                    value = 9;
                    break;
                case 'a':
                case 'q':
                case 'G':
                case 'W':
                    value = 10;
                    break;
                case 'b':
                case 'r':
                case 'H':
                case 'X':
                case '?':
                    value = 11;
                    break;
                case 'c':
                case 's':
                case 'I':
                case 'Y':
                case '!':
                    value = 12;
                    break;
                case 'd':
                case 't':
                case 'J':
                case 'Z':
                    value = 13;
                    break;
                case 'e':
                case 'u':
                case 'K':
                case ')':
                    value = 14;
                    break;
                case 'f':
                case 'v':
                case 'L':
                case '(':
                    value = 15;
                    break;
                default:
                    value = -1;
                    break;
            }

            return value;
        }

        private int GetInitialStrengthStat(int remainder)
        {
            int strengthStat;

            switch (remainder)
            {
                case 0:
                    strengthStat = 3;
                    break;
                case 1:
                    strengthStat = 4;
                    break;
                case 2:
                    strengthStat = 3;
                    break;
                case 3:
                    strengthStat = 4;
                    break;
                case 4:
                    strengthStat = 4;
                    break;
                case 5:
                    strengthStat = 4;
                    break;
                case 6:
                    strengthStat = 4;
                    break;
                case 7:
                    strengthStat = 4;
                    break;
                case 8:
                    strengthStat = 5;
                    break;
                case 9:
                    strengthStat = 4;
                    break;
                case 10:
                    strengthStat = 5;
                    break;
                case 11:
                    strengthStat = 4;
                    break;
                case 12:
                    strengthStat = 6;
                    break;
                case 13:
                    strengthStat = 4;
                    break;
                case 14:
                    strengthStat = 6;
                    break;
                case 15:
                    strengthStat = 4;
                    break;
                default:
                    strengthStat = -1;
                    break;
            }

            return strengthStat;
        }

        private int GetInitialAgilityStat(int remainder)
        {
            int agilityStat;

            switch (remainder)
            {
                case 0:
                    agilityStat = 3;
                    break;
                case 1:
                    agilityStat = 3;
                    break;
                case 2:
                    agilityStat = 4;
                    break;
                case 3:
                    agilityStat = 4;
                    break;
                case 4:
                    agilityStat = 4;
                    break;
                case 5:
                    agilityStat = 4;
                    break;
                case 6:
                    agilityStat = 4;
                    break;
                case 7:
                    agilityStat = 4;
                    break;
                case 8:
                    agilityStat = 5;
                    break;
                case 9:
                    agilityStat = 5;
                    break;
                case 10:
                    agilityStat = 4;
                    break;
                case 11:
                    agilityStat = 4;
                    break;
                case 12:
                    agilityStat = 6;
                    break;
                case 13:
                    agilityStat = 6;
                    break;
                case 14:
                    agilityStat = 4;
                    break;
                case 15:
                    agilityStat = 4;
                    break;
                default:
                    agilityStat = -1;
                    break;
            }

            return agilityStat;
        }

        private int GetInitialHPStat(int remainder)
        {
            int hpStat;

            switch (remainder)
            {
                case 0:
                    hpStat = 15;
                    break;
                case 1:
                    hpStat = 15;
                    break;
                case 2:
                    hpStat = 13;
                    break;
                case 3:
                    hpStat = 13;
                    break;
                case 4:
                    hpStat = 15;
                    break;
                case 5:
                    hpStat = 15;
                    break;
                case 6:
                    hpStat = 14;
                    break;
                case 7:
                    hpStat = 14;
                    break;
                case 8:
                    hpStat = 15;
                    break;
                case 9:
                    hpStat = 15;
                    break;
                case 10:
                    hpStat = 15;
                    break;
                case 11:
                    hpStat = 15;
                    break;
                case 12:
                    hpStat = 15;
                    break;
                case 13:
                    hpStat = 15;
                    break;
                case 14:
                    hpStat = 16;
                    break;
                case 15:
                    hpStat = 16;
                    break;
                default:
                    hpStat = -1;
                    break;
            }

            return hpStat;
        }

        private int GetInitialMPStat(int remainder)
        {
            int mpStat;

            switch (remainder)
            {
                case 0:
                    mpStat = 5;
                    break;
                case 1:
                    mpStat = 4;
                    break;
                case 2:
                    mpStat = 5;
                    break;
                case 3:
                    mpStat = 4;
                    break;
                case 4:
                    mpStat = 5;
                    break;
                case 5:
                    mpStat = 5;
                    break;
                case 6:
                    mpStat = 5;
                    break;
                case 7:
                    mpStat = 5;
                    break;
                case 8:
                    mpStat = 5;
                    break;
                case 9:
                    mpStat = 6;
                    break;
                case 10:
                    mpStat = 5;
                    break;
                case 11:
                    mpStat = 6;
                    break;
                case 12:
                    mpStat = 5;
                    break;
                case 13:
                    mpStat = 7;
                    break;
                case 14:
                    mpStat = 5;
                    break;
                case 15:
                    mpStat = 7;
                    break;
                default:
                    mpStat = -1;
                    break;
            }

            return mpStat;
        }

        private char GetGrowthType(int remainder)
        {
            char growthType;

            switch (remainder)
            {
                case 0:
                    growthType = 'A';
                    break;
                case 1:
                    growthType = 'B';
                    break;
                case 2:
                    growthType = 'C';
                    break;
                case 3:
                    growthType = 'D';
                    break;
                case 4:
                    growthType = 'A';
                    break;
                case 5:
                    growthType = 'B';
                    break;
                case 6:
                    growthType = 'C';
                    break;
                case 7:
                    growthType = 'D';
                    break;
                case 8:
                    growthType = 'A';
                    break;
                case 9:
                    growthType = 'B';
                    break;
                case 10:
                    growthType = 'C';
                    break;
                case 11:
                    growthType = 'D';
                    break;
                case 12:
                    growthType = 'A';
                    break;
                case 13:
                    growthType = 'B';
                    break;
                case 14:
                    growthType = 'C';
                    break;
                case 15:
                    growthType = 'D';
                    break;
                default:
                    growthType = '?';
                    break;
            }

            return growthType;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
