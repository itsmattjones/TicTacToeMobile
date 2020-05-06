namespace TicTacToe.Models
{
    public class BoardCell : IBoardCell
    {
        /// <summary>
        /// Gets or sets the state of the cell.
        /// </summary>
        /// <value>The state of the cell.</value>
        public CellState State { get; set; }

    }
}
