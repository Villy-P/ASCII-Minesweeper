﻿class Program {
    public Grid grid = new();

    private void Play() {
        if (this.Victory()) {
            Console.WriteLine("You win!");
            return;
        }
        // Get value
        int[] input = this.Choose();
        int row = input[1];
        int col = input[0];
        Plot value = this.grid.plots[row][col];
        // If it is a mine then you lose
        if (value.content == "*" && !value.isFlagged) {
            this.grid.PrintBoard();
            Console.WriteLine("You lose!");
            return;
        }
        // If value is 0, not visible, and not flagged, set it to visible and 
        // do the same for all adjacent squares until all the 0's are visible
        if (!value.isVisible && !value.isFlagged)
            this.CheckZeros(row, col, new HashSet<Plot>());
        this.grid.PrintBoard();
        this.Play();
    }

    private bool Victory() {
        int count = 0;
        foreach (List<Plot> row in this.grid.plots) {
            foreach (Plot col in row) {
                if (col.content != "*" && col.isVisible)
                    count++;
            }
        }
        if (count == 71)
            return true;
        return false;
    }

    private void CheckZeros(int row, int col, HashSet<Plot> visited) {
        // Check if row or column is outside of boundries
        if (row < 0 || row >= this.grid.rows || col < 0 || col >= this.grid.columns)
            return;
        if (visited.Contains(this.grid.plots[row][col]))
            return;
        // Set plot visible
        this.grid.plots[row][col].isVisible = true;
        // If the value is not 0 then return
        if (this.grid.plots[row][col].content != "0")
            return;

        visited.Add(this.grid.plots[row][col]);
        // Do the same for all 8 adjacent plots
        CheckZeros(row - 1, col, visited);
        CheckZeros(row + 1, col, visited);
        CheckZeros(row - 1, col + 1, visited);
        CheckZeros(row - 1, col - 1, visited);
        CheckZeros(row + 1, col + 1, visited);
        CheckZeros(row + 1, col - 1, visited);
        CheckZeros(row, col + 1, visited);
        CheckZeros(row, col - 1, visited);
    }

    private int[] Choose() {
        List<char> letters = new () {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i'};
        List<char> numbers = new() { '0', '1', '2', '3', '4', '5', '6', '7', '8'};
        while (true) {
            Console.Write("Choose a square (eg. E4) or place a marker (eg. mE4): ");
            string choice = Console.ReadLine()!.ToLower();
            if (
                choice.Length == 3 && 
                choice[0] == 'm' && 
                letters.Contains(choice[1]) && 
                numbers.Contains(choice[2])
            ) {
                int col = char.ToUpper(choice[1]) - 65;
                int row = int.Parse(choice[2].ToString());
                this.Marker(row, col);
                this.Play();
            } else if (
                choice.Length == 2 &&
                letters.Contains(choice[0]) &&
                numbers.Contains(choice[1])
            ) {
                return new int[] {
                    char.ToUpper(choice[0]) - 65,
                    int.Parse(choice[1].ToString())
                };
            } else {
                this.Choose();
            }
        }
    }

    private void Marker(int row, int col) {
        if (!this.grid.plots[row][col].isVisible)
            this.grid.plots[row][col].isFlagged = !this.grid.plots[row][col].isFlagged;
        this.grid.PrintBoard();
    }

    static void Main(string[] args) {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Minesweeper");
        Program program = new();
        program.grid.CreateGrid();
        program.grid.PrintBoard();
        program.Play();
    }
}