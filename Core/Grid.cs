public class Grid {
    public List<List<Plot>> plots = new();
    public int rows = 9;
    public int columns = 9;
    public int bombs = 10;

    public void CreateGrid() {
        for (int i = 0; i < this.rows; i++) {
            this.plots.Add(new List<Plot>());
            for (int j = 0; j < this.columns; j++) {
                this.plots[i].Add(new Plot(i, j));
            }
        }
        for (int i = 0; i < this.bombs; i++)
            this.PlaceBomb();
        for (int i = 0; i < this.rows; i++) {
            for (int j = 0; j < this.columns; j++) {
                Plot value = this.plots[i][j];
                if (value.content == "*")
                    UpdateValues(i, j);
            }
        }
        
    }

    public void PlaceBomb() {
        int row = new Random().Next(0, this.rows);
        int col = new Random().Next(0, this.columns);
        Plot plot = this.plots[row][col];
        if (!(plot.content == "*")) 
            plot.content = "*";
        else
            this.PlaceBomb();
    }

    public static string ParseToValue(string value) {
        return int.Parse(value).ToString();
    }

    public void UpdateTopAndBottomRowValues(List<Plot> currentRow, int col) {
        if (col - 1 > -1)
            currentRow[col - 1].content = UpdateValue(currentRow[col - 1].content);
        currentRow[col].content = UpdateValue(currentRow[col].content);
        if (9 > col + 1)
            currentRow[col + 1].content = UpdateValue(currentRow[col + 1].content);
    }
    
    public string UpdateValue(string value) {
        if (!(value == "*"))
            return (int.Parse(value) + 1).ToString();
        return "*";
    }

    public void UpdateValues(int row, int col) {
        List<Plot> currentRow;
        if (row - 1 > -1) {
            currentRow = this.plots[row - 1];
            this.UpdateTopAndBottomRowValues(currentRow, col);
        }
        currentRow = this.plots[row];
        if (col - 1 > -1)
            currentRow[col - 1].content = UpdateValue(currentRow[col - 1].content);
        if (9 > col + 1)
            currentRow[col + 1].content = UpdateValue(currentRow[col + 1].content);
        if (9 > row + 1) {
            currentRow = this.plots[row + 1];
            this.UpdateTopAndBottomRowValues(currentRow, col);
        }
    }

    public void PrintBoard() {
        Console.Clear();
        string leadingSpaces = " ".PadRight(this.rows.ToString().Length + 1, ' ');
        Console.WriteLine(leadingSpaces + "  A   B   C   D   E   F   G   H   I");
        Console.WriteLine(leadingSpaces + "╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗");
        for (int i = 0; i < this.rows; i++) {
            Console.Write(i.ToString().PadRight(this.rows.ToString().Length + 1, ' ') + "║");
            for (int j = 0; j < this.columns; j++) {
                Console.Write(" " + this.plots[i][j].GetValue() + " ║");
            }
            if (!(i == this.columns - 1))
                Console.Write("\n" + leadingSpaces + "╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣\n");
        }
        Console.WriteLine("\n" + leadingSpaces + "╚═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╝");
    }
}