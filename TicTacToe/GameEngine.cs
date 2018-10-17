﻿using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Models;

namespace TicTacToe
{
    public class GameEngine
    {
        /// <summary>
        /// Takes the current player's turn.
        /// </summary>
        /// <returns>The updated game model.</returns>
        /// <param name="gameModel">Game model.</param>
        /// <param name="cellId">Cell identifier.</param>
        /// <param name="gameIsWon">If set to true game is won.</param>
        /// <param name="gameIsDraw">If set to true game is draw.</param>
        public GameModel TakePlayerTurn(GameModel gameModel, string cellId, out bool gameIsWon, out bool gameIsDraw)
        {
            bool result = int.TryParse(cellId, out int CellId);
            gameIsWon = false;
            gameIsDraw = false;

            // If not a valid cellId abort.
            if (!result || cellId == string.Empty)
                return gameModel;

            // If the cell's already claimed abort.
            if (gameModel.Board[CellId].CellState != 0)
                return gameModel;
                
            IPlayerModel PlayerTurn = GetTurnPlayer(gameModel);

            // Set the cell to this player.
            gameModel.Board[CellId].CellState = PlayerTurn.PlayerAvatar;

            // Check if player has won, if has end game. Also check for draw.
            gameIsWon = GameIsWon(gameModel.Board, PlayerTurn);
            gameIsDraw = GameIsDraw(gameModel.Board);

            if (gameIsWon || gameIsDraw)
                return gameModel;

            // No Win or Draw? Rotate.
            gameModel.PlayerOne.IsPlayerTurn = !gameModel.PlayerOne.IsPlayerTurn;
            gameModel.PlayerTwo.IsPlayerTurn = !gameModel.PlayerTwo.IsPlayerTurn;

            return gameModel;
        }

        /// <summary>
        /// Takes the AI players turn.
        /// </summary>
        /// <returns>The updated model.</returns>
        /// <param name="gameModel">Game model.</param>
        /// <param name="gameIsWon">If set to true game is won.</param>
        /// <param name="gameIsDraw">If set to true game is draw.</param>
        public GameModel TakeAITurn(GameModel gameModel, out bool gameIsWon, out bool gameIsDraw)
        {
            gameIsWon = false;
            gameIsDraw = false;
            int cellId;

            AIDifficulty difficulty = gameModel.PlayerOne.PlayerType == PlayerType.AI ? 
                gameModel.PlayerOne.Difficulty : gameModel.PlayerTwo.Difficulty;

            // Make selection based on difficulty.
            switch(difficulty)
            {
                case AIDifficulty.Easy:
                    cellId = EasyAITurn(gameModel);
                    break;
                case AIDifficulty.Medium:
                    cellId = MediumAITurn(gameModel);
                    break;
                case AIDifficulty.Hard:
                    cellId = HardAITurn(gameModel);
                    break;
                default: // Default to medium
                    cellId = MediumAITurn(gameModel);
                    break;
            }

            // If not a valid cellId or if the cell is taken abort.
            if (cellId == -1 || gameModel.Board[cellId].CellState != 0)
                return gameModel;
                
            IPlayerModel PlayerTurn = GetTurnPlayer(gameModel);

            // Set the cell to this player.
            gameModel.Board[cellId].CellState = PlayerTurn.PlayerAvatar;

            // Check if player has won, if has game will end. Also check for draw.
            gameIsWon = GameIsWon(gameModel.Board, PlayerTurn);
            gameIsDraw = GameIsDraw(gameModel.Board);

            if (gameIsWon || gameIsDraw)
                return gameModel;

            // No Win or Draw? Rotate.
            gameModel.PlayerOne.IsPlayerTurn = !gameModel.PlayerOne.IsPlayerTurn;
            gameModel.PlayerTwo.IsPlayerTurn = !gameModel.PlayerTwo.IsPlayerTurn;

            return gameModel;
        }

        /// <summary>
        /// Choose a cell under Easy difficulty.
        /// </summary>
        /// <returns>The chosen cell to select.</returns>
        /// <param name="gameModel">Game model.</param>
        public int EasyAITurn(GameModel gameModel)
        {
            foreach (var cell in gameModel.Board)
            {
                if (cell.CellState == 0)
                    return gameModel.Board.IndexOf(cell);
            }
            return -1;
        }

        /// <summary>
        /// Choose a cell under Medium difficulty.
        /// </summary>
        /// <returns>The chosen cell to select.</returns>
        /// <param name="gameModel">Game model.</param>
        public int MediumAITurn(GameModel gameModel)
        {
            IPlayerModel otherPlayer = gameModel.PlayerOne.IsPlayerTurn ? gameModel.PlayerTwo : gameModel.PlayerOne;

            var solutions = GetPopulatedWinningCombinations(gameModel, otherPlayer.PlayerAvatar);

            // Attempt to block solutions with 2 cells taken by the other player.
            var possibleSolutions = solutions.OrderByDescending(x => x[1]);
            if (possibleSolutions.Any(x => x[1] > 1)) 
            {
                var solution = possibleSolutions.Where(x => x[1] > 1).OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                foreach (var cellid in solution.GetRange(2, 3))
                {
                    if (gameModel.Board[cellid].CellState == 0)
                        return cellid;
                }
            }

            // Attempt to block solutions with 1 cell taken by the other player.
            if(possibleSolutions.Any(x => x[1] > 0))
            {
                var solution = possibleSolutions.Where(x => x[1] > 0).OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                var untakenCellIds = new List<int>();

                foreach (var cellid in solution.GetRange(2, 3))
                {
                    if (gameModel.Board[cellid].CellState == 0)
                        untakenCellIds.Add(cellid);
                }

                if (untakenCellIds.Count > 0)
                    return untakenCellIds.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            }

            // If all fails, revert to previous difficulty.
            return EasyAITurn(gameModel);
        }

        /// <summary>
        /// Choose a cell under Hard difficulty.
        /// </summary>
        /// <returns>The chosen cell to select.</returns>
        /// <param name="gameModel">Game model.</param>
        public int HardAITurn(GameModel gameModel)
        {
            IPlayerModel AIPlayer = gameModel.PlayerOne.IsPlayerTurn ? gameModel.PlayerOne : gameModel.PlayerTwo;

            var solutionsAIPlayer = GetPopulatedWinningCombinations(gameModel, AIPlayer.PlayerAvatar);

            // Attempt to win the game.
            if (solutionsAIPlayer.Any(x => x[1] > 1))
            {
                foreach (var cellid in solutionsAIPlayer.FirstOrDefault(x => x[1] > 1).GetRange(2, 3))
                {
                    if (gameModel.Board[cellid].CellState == 0)
                        return cellid;
                }
            }

            // If central cell is not taken, take it.
            if (gameModel.Board[4].CellState == 0)
                return 4;

            // If it can't win, revert to using medium to block.
            return MediumAITurn(gameModel);
        }

        /// <summary>
        /// Checks whether the player has won the game.
        /// </summary>
        /// <returns>True if the game's won, otherwise false.</returns>
        /// <param name="board">The game board.</param>
        /// <param name="player">The player to check.</param>
        private bool GameIsWon(List<CellModel> board, IPlayerModel player)
        {
            return CheckForVictory(board, player) ? true : false;
        }

        /// <summary>
        /// Checks if the game is a draw
        /// </summary>
        /// <returns>True if the game's a draw, otherwise false.</returns>
        /// <param name="board">The game board.</param>
        private bool GameIsDraw(List<CellModel> board)
        {
            return CheckBoardFull(board) ? true : false;
        }

        #region Helper Methods

        /// <summary>
        /// Gets the current player whose turn it is.
        /// </summary>
        /// <returns>The player.</returns>
        /// <param name="gameModel">Game model.</param>
        public IPlayerModel GetTurnPlayer(GameModel gameModel)
        {
            return gameModel.PlayerOne.IsPlayerTurn ? gameModel.PlayerOne : gameModel.PlayerTwo;
        }

        /// <summary>
        /// Checks if the board is full.
        /// </summary>
        /// <returns>True if the board is full, otherwise false.</returns>
        /// <param name="board">The game board.</param>
        private bool CheckBoardFull(List<CellModel> board)
        {
            foreach (var cell in board)
            {
                if (cell.CellState == 0)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Checks for victory against solutions.
        /// </summary>
        /// <returns>True if the player has won, otherwise false.</returns>
        /// <param name="board">The game board.</param>
        /// <param name="player">Player to check.</param>
        private bool CheckForVictory(List<CellModel> board, IPlayerModel player)
        {
            var pAvatar = player.PlayerAvatar;

            // Check horizontal
            if (IsBoardMatch(pAvatar, board[0]) && IsBoardMatch(pAvatar, board[1]) && IsBoardMatch(pAvatar, board[2]))
                return true;

            if (IsBoardMatch(pAvatar, board[3]) && IsBoardMatch(pAvatar, board[4]) && IsBoardMatch(pAvatar, board[5]))
                return true;

            if (IsBoardMatch(pAvatar, board[6]) && IsBoardMatch(pAvatar, board[7]) && IsBoardMatch(pAvatar, board[8]))
                return true;


            // Check vertical
            if (IsBoardMatch(pAvatar, board[0]) && IsBoardMatch(pAvatar, board[3]) && IsBoardMatch(pAvatar, board[6]))
                return true;

            if (IsBoardMatch(pAvatar, board[1]) && IsBoardMatch(pAvatar, board[4]) && IsBoardMatch(pAvatar, board[7]))
                return true;

            if (IsBoardMatch(pAvatar, board[2]) && IsBoardMatch(pAvatar, board[5]) && IsBoardMatch(pAvatar, board[8]))
                return true;


            // Check diagonal
            if (IsBoardMatch(pAvatar, board[0]) && IsBoardMatch(pAvatar, board[4]) && IsBoardMatch(pAvatar, board[8]))
                return true;

            if (IsBoardMatch(pAvatar, board[2]) && IsBoardMatch(pAvatar, board[4]) && IsBoardMatch(pAvatar, board[6]))
                return true;


            return false;
        }

        /// <summary>
        /// Generates the winning combinations list.
        /// </summary>
        /// <returns>The winning combinations list.</returns>
        private List<List<int>> GenerateWinningCombinationsList()
        {
            return new List<List<int>>
            {
                new List<int> { 3, 0, 0, 3, 6 }, new List<int> { 3, 0, 1, 4, 7 }, new List<int> { 3, 0, 2, 5, 8 },
                new List<int> { 2, 0, 0, 1, 2 }, new List<int> { 2, 0, 3, 4, 5 }, new List<int> { 2, 0, 6, 7, 8 },
                new List<int> { 1, 0, 0, 4, 8 }, new List<int> { 1, 0, 2, 4, 6 }
            };
        }

        /// <summary>
        /// Populate the winning combinations list with all the cells
        /// the playerAvatar has taken and return the result.
        /// </summary>
        /// <returns>Populated winning combinations list.</returns>
        /// <param name="gameModel">Game model.</param>
        /// <param name="playerAvatar">Player avatar.</param>
        private List<List<int>> GetPopulatedWinningCombinations(GameModel gameModel, int playerAvatar)
        {
            var solutions = GenerateWinningCombinationsList();
            foreach (var solution in solutions)
            {
                var solutionCellIds = solution.GetRange(2, 3);
                foreach (var cellId in solutionCellIds)
                {
                    if (gameModel.Board[cellId].CellState == playerAvatar)
                        solutions[solutions.IndexOf(solution)][1]++;
                }
            }
            return solutions;
        }

        /// <summary>
        /// Checks if the cell on the board matches the players after.
        /// Which will determine whether the player has taken the cell.
        /// </summary>
        /// <returns>True if they've taken it, otherwise false.</returns>
        /// <param name="avatar">The player's avatar</param>
        /// <param name="boardCell">Board cell.</param>
        private bool IsBoardMatch(int avatar, CellModel boardCell)
        {
            var cellState = boardCell.CellState;
            return avatar == cellState ? true : false;
        }

        #endregion
    }
}
