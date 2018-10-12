namespace TicTacToe.Models
{
    public class CellModel
    {
        /// <summary>
        /// Gets or sets the state of the cell.
        ///     0 = Unknown;
        ///     1 = Taken by player 1.
        ///     2 = Taken by player 2.
        /// Defaults to 0.
        /// </summary>
        /// <value>The state of the cell.</value>
        public int CellState { get; set; } = 0;

    }
}
