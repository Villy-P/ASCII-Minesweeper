// ! FINISHED
// TODO = get input for rows, cols, and bombs

class Program {
    public Grid grid = new Grid();

    private void play() {
        if (this.victory()) {
            Console.WriteLine("You win!");
            return;
        }
        // Get value
        int[] input = this.choose();
        int row = input[1];
        int col = input[0];
        Plot value = this.grid.plots[row][col];
        // If it is a mine then you lose
        if (value.content == "*" && !value.isFlagged) {
            this.grid.printBoard();
            Console.WriteLine("You lose!");
            return;
        }
        // If value is 0, not visible, and not flagged, set it to visible and 
        // do the same for all adjacent squares until all the 0's are visible
        if (!value.isVisible && !value.isFlagged)
            this.checkZeros(row, col, new HashSet<Plot>());
        this.grid.printBoard();
        this.play();
    }

    private bool victory() {
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

    private void checkZeros(int row, int col, HashSet<Plot> visited) {
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
        checkZeros(row - 1, col, visited);
        checkZeros(row + 1, col, visited);
        checkZeros(row - 1, col + 1, visited);
        checkZeros(row - 1, col - 1, visited);
        checkZeros(row + 1, col + 1, visited);
        checkZeros(row + 1, col - 1, visited);
        checkZeros(row, col + 1, visited);
        checkZeros(row, col - 1, visited);
    }

    private int[] choose() {
        List<char> letters = new List<char> {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i'};
        List<char> numbers = new List<char> {'0', '1', '2', '3', '4', '5', '6', '7', '8'};
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
                int row = Int32.Parse(choice[2].ToString());
                this.marker(row, col);
                this.play();
                // return new int[] {0, 0};
            } else if (
                choice.Length == 2 &&
                letters.Contains(choice[0]) &&
                numbers.Contains(choice[1])
            ) {
                return new int[] {
                    char.ToUpper(choice[0]) - 65,
                    Int32.Parse(choice[1].ToString())
                };
            } else {
                this.choose();
            }
        }
    }

    private void marker(int row, int col) {
        if (!this.grid.plots[row][col].isVisible)
            this.grid.plots[row][col].isFlagged = !this.grid.plots[row][col].isFlagged;
        this.grid.printBoard();
    }

    static void Main(string[] args) {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Minesweeper");
        Program program = new Program();
        program.grid.createGrid();
        program.grid.printBoard();
        program.play();
    }
}