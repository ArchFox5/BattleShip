using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Battleship
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private int[,] grid = new int[,]
        {
            {1, 1, 0, 0, 1},
            {0, 0, 0, 0, 0},
            {0, 0, 1, 0, 1},
            {1, 0, 1, 0, 0},
            {1, 0, 1, 0, 1},
        };

        private bool[,] hits;

        private int nRows;
        private int nCols;

        private int row;
        private int col;

        private int score;

        private int time;

        [SerializeField] Transform gridRoot;

        [SerializeField] GameObject cellPrefab;
        [SerializeField] GameObject winLabel;
        [SerializeField] TextMeshProUGUI timeLabel;
        [SerializeField] TextMeshProUGUI scoreLabel;

        private void Awake()
        {
            nRows = grid.GetLength(0);
            nCols = grid.GetLength(1);

            hits = new bool[nRows, nCols];

            for (int i = 0; i < nRows * nCols; i++)
            {
                Instantiate(cellPrefab, gridRoot);
            }
        }
    }
}
