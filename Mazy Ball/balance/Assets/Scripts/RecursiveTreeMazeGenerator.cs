public class RecursiveTreeMazeGenerator : TreeMazeGenerator {
    public RecursiveTreeMazeGenerator(int row, int column) : base(row, column) { }
	protected override int GetCellInRange(int max)
	{
		return max;
	}
}
