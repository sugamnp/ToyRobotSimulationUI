using System.Diagnostics;

namespace TRSimulation
{
    public partial class MainForm : Form
    {
        ToyRobot robot = new ToyRobot();
        Obstacle obstacle = new Obstacle(); 
        public MainForm()
        {
            InitializeComponent();
        }

        private void PlaceButton_Click(object sender, EventArgs e)
        {
            string input = placeInputTextBox.Text.Trim();
            string[] parts = input.Split(',');

            if (parts.Length == 3 && int.TryParse(parts[0], out int a) && int.TryParse(parts[1], out int b) && robot.IsValidDirection(parts[2].ToUpper()))
            {
                string direction = parts[2].ToUpper();

                if (robot.IsValidPosition(a, b))
                {
                    ClearGrid();
                    robot.Place(a,b, direction);
                    GiveRobotFace();
                    GiveRobotFace();
                    messageLabel.Text = $"Toy robot placed at {robot.x},{robot.y},{direction}";
                }
                else
                {
                    messageLabel.Text = "Invalid position";
                }
            }
            else
            {
                messageLabel.Text = "Invalid format: X,Y,DIRECTION";
            }
        }
         
        private void MoveButton_Click(object sender, EventArgs e)
        {
            if (!robot.placed)
            {
                messageLabel.Text = "Toy robot has not been placed yet.";
                return;
            }
            if (robot.Move()) //if robot can move
            {
                ClearGrid();
                GiveRobotFace(); 
                messageLabel.Text = $"Toy robot moved at {robot.x},{robot.y},{robot.direction}";
                if (robot.x == obstacle.x && robot.y == obstacle.y)
                {
                    ShowCustomMessageBox();
                }
            }
            else
            {
                messageLabel.Text = "Cant Move Further";

            }
        }

        private void ReportButton_Click(object sender, EventArgs e)
        {
           messageLabel.Text = $"Toy robot placed at {robot.x},{robot.y},{robot.direction}";
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            if (!robot.placed)
            {
                messageLabel.Text = "Toy robot has not been placed yet.";
                return;
            }
            robot.Left();
            ClearGrid();
            messageLabel.Text = $"Toy robot rotated to {robot.direction}";
            GiveRobotFace();
        }

        private void RightButton_Click(object sender, EventArgs e)
        {
            if (!robot.placed) 
            {
                messageLabel.Text = "Toy robot has not been placed yet.";
                return;
            }
            robot.Right();
            ClearGrid();
            messageLabel.Text = $"Toy robot rotated to {robot.direction}";
            GiveRobotFace();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AddObstacleButton_Click(object sender, EventArgs e) 
        {
            if (!robot.placed && !obstacle.placed) //can only add obstacle before robot is placed and can only be 1 obstacle
            { 
                obstacle.Place();
                gridDataGridView.Rows[obstacle.x].Cells[obstacle.y].Value = "௵";

            }   
            else
            {
                messageLabel.Text = "cant place obstacle";
            }
        }
  
        private void ClearGrid()
        {
            foreach (DataGridViewRow row in gridDataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Value = null;
                }
            }
            if (obstacle.placed)
            {
                gridDataGridView.Rows[obstacle.x].Cells[obstacle.y].Value = "௵";

            }
        }

        private void GiveRobotFace()
        {
            if (robot.direction == "EAST")
            {
                gridDataGridView.Rows[robot.x].Cells[robot.y].Value = "└[∴]┘";
            }
            else if (robot.direction == "WEST")
            {
                gridDataGridView.Rows[robot.x].Cells[robot.y].Value = "└[∵]┘";

            }
            else if (robot.direction == "NORTH")
            {
                gridDataGridView.Rows[robot.x].Cells[robot.y].Value = "[┐∵]┘";

            }
            else
            {
                gridDataGridView.Rows[robot.x].Cells[robot.y].Value = "└[∵┌]";
            }
        }

        private void ShowCustomMessageBox()
        {
            string message = "Sir, you have hit an obstacle.\n\n" +
                             "Don't click on OK.\n\n" +
                             "Otherwise, you will know the probability of getting hired today.\n\n";
            DialogResult result = MessageBox.Show(message, "WARNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = "https://sugamnp.github.io/trphotohtml/",
                    UseShellExecute = true
                });
            }
        }
    }
}
