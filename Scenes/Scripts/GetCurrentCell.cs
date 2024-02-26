using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Battleship
{
    public class GetCurrentCell : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private Transform gridRoot;
        [SerializeField] private GameObject winLabel;
        [SerializeField] private TextMeshProUGUI timeLabel;
        [SerializeField] private TextMeshProUGUI scoreLabel;

        private int row = 0;
        private int col = 0;

        private int[,] grid;
        private bool[,] hits;

        private int nRows = 5;
        private int nCols = 5;

        private int score;
        private int time;

        void Start()
        {
            hits = new bool[nRows, nCols];
            InitializeGrid();
            SelectCurrentCell();
            InvokeRepeating("IncrementTime", 1f, 1f);
        }

        void InitializeGrid()
        {
            grid = new int[nRows, nCols];

            for (int r = 0; r < nRows; r++)
            {
                for (int c = 0; c < nCols; c++)
                {
                    grid[r, c] = Random.Range(0, 2);
                }
            }
        }

        Transform RetrieveCurrentCell()
        {
            int index = (row * nCols) + col;
            return gridRoot.GetChild(index);
        }

        void SelectCurrentCell()
        {
            Transform cell = RetrieveCurrentCell();
            Transform cursor = cell.Find("Cursor");
            cursor.gameObject.SetActive(true);
        }

        void UnselectCurrentCell()
        {
            Transform cell = RetrieveCurrentCell();
            Transform cursor = cell.Find("Cursor");
            cursor.gameObject.SetActive(false);
        }

        void ShowHit()
        {
            Transform cell = RetrieveCurrentCell();
            Transform hit = cell.Find("Hit");
            hit.gameObject.SetActive(true);

            Transform cursor = cell.Find("Cursor");
            cursor.gameObject.SetActive(false);

            UnselectPreviousCell();

            Debug.Log("ShowHit() called");
        }

        void ShowMiss()
        {
            Transform cell = RetrieveCurrentCell();
            Transform miss = cell.Find("Miss");
            miss.gameObject.SetActive(true);

            Transform cursor = cell.Find("Cursor");
            cursor.gameObject.SetActive(false);

            UnselectPreviousCell();

            Debug.Log("ShowMiss() called");
        }

        void UnselectPreviousCell()
        {
            Transform previousCell = gridRoot.GetChild((row * nCols) + col);
            Transform previousCursor = previousCell.Find("Cursor");
            previousCursor.gameObject.SetActive(false);
        }

        void IncrementScore()
        {
            score++;
            scoreLabel.text = $"Score: {score}";
        }

        public void Fire()
        {
            if (hits[row, col]) return;
            hits[row, col] = true;
            if (grid[row, col] == 1)
            {
                ShowHit();
                IncrementScore();
            }
            else
            {
                ShowMiss();
            }

            TryEndGame();
        }

        void TryEndGame()
        {
            for (int r = 0; r < nRows; r++)
            {
                for (int c = 0; c < nCols; c++)
                {
                    if (grid[r, c] == 0) continue;
                    if (!hits[r, c]) return;
                }
            }

            winLabel.gameObject.SetActive(true);
            CancelInvoke("IncrementTime");
        }

        void IncrementTime()
        {
            time++;
            timeLabel.text = $"{time / 60}:{(time % 60).ToString("00")}";
        }

        public void OnLeftButtonClicked()
        {
            MoveHorizontal(-1);
        }

        public void OnRightButtonClicked()
        {
            MoveHorizontal(1);
        }

        public void OnUpButtonClicked()
        {
            MoveVertical(1);
        }

        public void OnDownButtonClicked()
        {
            MoveVertical(-1);
        }

        public void MoveHorizontal(int amt)
        {
            UnselectCurrentCell();
            col += amt;
            col = Mathf.Clamp(col, 0, nCols - 1);
            SelectCurrentCell();
        }

        public void MoveVertical(int amt)
        {
            UnselectCurrentCell();
            row += amt;
            row = Mathf.Clamp(row, 0, nRows - 1);
            SelectCurrentCell();
        }
    }
}
