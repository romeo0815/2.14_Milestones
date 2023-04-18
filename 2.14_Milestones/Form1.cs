using GameOfLifeV3;

namespace _2._14_Milestones
{
    public partial class Form1 : Form
    {
        private GameOfLife _game;
        private int _gridSize = 20; // Adjust the grid size as desired
        private int _cellSize = 20; // Adjust the cell size as desired
        private int _generation;

        public int Generation
        {
            get => _generation;
            set
            {
                _generation = value;
                generationStatusLabel.Text = $"Generation: {_generation}";
                livingCellsStatusLabel.Text = $"Living Cells: {LivingCells}";
            }
        }

        public int LivingCells
        {
            get
            {
                int count = 0;
                for (int x = 0; x < _gridSize; x++)
                {
                    for (int y = 0; y < _gridSize; y++)
                    {
                        if (_game.GetCell(x, y).IsAlive)
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
        }

        public Form1()
        {
            InitializeComponent();

            _game = new GameOfLife(_gridSize, _gridSize);
            Generation = 0;
            gameTimer.Interval = 500; // Set the interval to 500ms, or any desired value

            int formMargin = 10; // Adjust the margin around the panel as desired
            int panelWidth = _gridSize * _cellSize;
            int panelHeight = _gridSize * _cellSize;

            gridPanel.Size = new Size(panelWidth, panelHeight);
            this.ClientSize = new Size(panelWidth + formMargin, panelHeight + menuStrip1.Height + statusStrip1.Height + formMargin);


            gridPanel.Paint += GridPanel_Paint;
            gridPanel.MouseClick += GridPanel_MouseClick;
            gameTimer.Tick += GameTimer_Tick;

            startToolStripMenuItem.Click += StartToolStripMenuItem_Click;
            pauseToolStripMenuItem.Click += PauseToolStripMenuItem_Click;
            nextToolStripMenuItem.Click += NextToolStripMenuItem_Click;
            clearToolStripMenuItem.Click += ClearToolStripMenuItem_Click;
            gridPanel.Resize += GridPanel_Resize;
            this.Resize += MainForm_Resize;




        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            int horizontalMargin = this.ClientSize.Width - gridPanel.Width;
            int verticalMargin = this.ClientSize.Height - gridPanel.Height;

            _cellSize = Math.Min(
                (this.ClientSize.Width - horizontalMargin) / _gridSize,
                (this.ClientSize.Height - verticalMargin) / _gridSize
            );

            gridPanel.Invalidate();
        }


        private void GridPanel_Paint(object sender, PaintEventArgs e)
        {
            for (int x = 0; x < _gridSize; x++)
            {
                for (int y = 0; y < _gridSize; y++)
                {
                    Cell cell = _game.GetCell(x, y);
                    Rectangle rect = new Rectangle(x * _cellSize, y * _cellSize, _cellSize, _cellSize);
                    e.Graphics.FillRectangle(cell.IsAlive ? Brushes.Black : Brushes.White, rect);
                    e.Graphics.DrawRectangle(Pens.Gray, rect);
                }
            }
        }

        private void GridPanel_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / _cellSize;
            int y = e.Y / _cellSize;

            _game.ToggleCell(x, y);
            gridPanel.Invalidate();
            livingCellsStatusLabel.Text = $"Living Cells: {LivingCells}";
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            _game.NextGeneration();
            Generation++;
            livingCellsStatusLabel.Text = $"Living Cells: {LivingCells}";
            gridPanel.Invalidate();
        }

        private void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameTimer.Start();
        }

        private void PauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameTimer.Stop();
        }

        private void NextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!gameTimer.Enabled)
            {
                _game.NextGeneration();
                Generation++;
                livingCellsStatusLabel.Text = $"Living Cells: {LivingCells}";
                gridPanel.Invalidate();
            }
        }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameTimer.Stop();
            _game.Clear();
            Generation = 0;
            livingCellsStatusLabel.Text = $"Living Cells: {LivingCells}";
            gridPanel.Invalidate();
        }

        private void GridPanel_Resize(object sender, EventArgs e)
        {
            _cellSize = Math.Min(gridPanel.Width / _gridSize, gridPanel.Height / _gridSize);
            gridPanel.Invalidate();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}