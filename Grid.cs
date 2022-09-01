public class Grid {
    public List<List<Plot>> plots = new List<List<Plot>>();
    public int rows = 9;
    public int columns = 9;
    public int bombs = 10;

    public void createGrid() {
        for (int i = 0; i < this.rows; i++) {
            this.plots.Add(new List<Plot>());
            for (int j = 0; j < this.columns; j++) {
                this.plots[i].Add(new Plot(i, j));
            }
        }
        for (int i = 0; i < this.bombs; i++)
            this.placeBomb();
        for (int i = 0; i < this.rows; i++) {
            for (int j = 0; j < this.columns; j++) {
                Plot value = this.plots[i][j];
                if (value.content == "*")
                    updateValues(i, j);
            }
        }
        
    }

    public void placeBomb() {
        int row = new Random().Next(0, this.rows);
        int col = new Random().Next(0, this.columns);
        Plot plot = this.plots[row][col];
        if (!(plot.content == "*")) 
            plot.content = "*";
        else
            this.placeBomb();
    }

    public string parseToValue(string value) {
        return Int32.Parse(value).ToString();
    }

    public void updateTopAndBottomRowValues(List<Plot> currentRow, int col) {
        if (col - 1 > -1)
            currentRow[col - 1].content = updateValue(currentRow[col - 1].content);
        currentRow[col].content = updateValue(currentRow[col].content);
        if (9 > col + 1)
            currentRow[col + 1].content = updateValue(currentRow[col + 1].content);
    }
    
    public string updateValue(string value) {
        if (!(value == "*"))
            return (Int32.Parse(value) + 1).ToString();
        return "*";
    }

    public void updateValues(int row, int col) {
        List<Plot> currentRow;
        if (row - 1 > -1) {
            currentRow = this.plots[row - 1];
            this.updateTopAndBottomRowValues(currentRow, col);
        }
        currentRow = this.plots[row];
        if (col - 1 > -1)
            currentRow[col - 1].content = updateValue(currentRow[col - 1].content);
        if (9 > col + 1)
            currentRow[col + 1].content = updateValue(currentRow[col + 1].content);
        if (9 > row + 1) {
            currentRow = this.plots[row + 1];
            this.updateTopAndBottomRowValues(currentRow, col);
        }
    }

    public void printBoard() {
        Console.Clear();
        string leadingSpaces = " ".PadRight(this.rows.ToString().Length + 1, ' ');
        Console.WriteLine(leadingSpaces + "  A   B   C   D   E   F   G   H   I");
        Console.WriteLine(leadingSpaces + "╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗");
        for (int i = 0; i < this.rows; i++) {
            Console.Write(i.ToString().PadRight(this.rows.ToString().Length + 1, ' ') + "║");
            for (int j = 0; j < this.columns; j++) {
                Console.Write(" " + this.plots[i][j].getValue() + " ║");
            }
            if (!(i == this.columns - 1))
                Console.Write("\n" + leadingSpaces + "╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣\n");
        }
        Console.WriteLine("\n" + leadingSpaces + "╚═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╝");
    }
}