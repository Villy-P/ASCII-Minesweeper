namespace ASCII_Minesweeper.Core {
    public class Plot {
        public bool isVisible = false;
        public bool isFlagged = false;
        public int row, col;
        public string content = "0";

        /// <summary>
        /// Initializes a new instance of the Plot class with specified row and column.
        /// </summary>
        /// <param name="row">The row index of the plot.</param>
        /// <param name="col">The column index of the plot.</param>
        public Plot(int row, int col) {
            this.row = row;
            this.col = col;
        }

        /// <summary>
        /// Sets the content of the plot to a mine.
        /// </summary>
        public string GetValue() {
            if (this.isFlagged)
                return "⚐";
            else if (!this.isVisible)
                return " ";
            else if (this.content == "0")
                return ".";
            else
                return this.content;
        }
    }
}