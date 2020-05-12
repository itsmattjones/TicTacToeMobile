namespace TicTacToe.Models
{
    public enum CellState
    {
        Available,
        TakenByPlayer1,
        TakenByPlayer2
    }

    public class BoardCell
    {
        public CellState State { get; set; }
        public IPlayer Owner { get; set; }
    }
}
