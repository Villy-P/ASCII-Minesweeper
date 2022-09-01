public class Plot {
    public bool isVisible = true;
    public bool isFlagged = false;
    public int row, col;
    public string content = "0";

    public Plot(int row, int col) {
        this.row = row;
        this.col = col;
    }

    public string getValue() {
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