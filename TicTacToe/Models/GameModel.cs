using System;
using System.Collections.Generic;

namespace TicTacToe.Models
{
    public class GameModel
    {
        public List<CellModel> Board { get; set; }
        public int BoardSize { get; set; } = 9;
        public IPlayerModel PlayerOne { get; set; }
        public IPlayerModel PlayerTwo { get; set; }
    }
}
