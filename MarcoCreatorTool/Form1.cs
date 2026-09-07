using MarcoCreatorTool;

namespace MarcoCreatorTool
{
    public partial class Form1 : Form
    {
        private List<RecordedAction> recordedActions = new List<RecordedAction>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void recordButton_Click(object sender, EventArgs e)
        {
            RecordedAction action = new RecordedAction
            {
                Type = ActionType.MouseClick,
                X = 100,
                Y = 100,
                Delay = 500
            };

            recordedActions.Add(action);
            MessageBox.Show($"Added: {action.Type} at ({action.X}, {action.Y})");
        }

        private void play_Click(object sender, EventArgs e)
        {
            if (recordedActions.Count == 0)
            {
                MessageBox.Show("No actions recorded.");
                return;
            }

            foreach (var action in recordedActions)
            {
                if (action.Type == ActionType.MouseClick)
                {
                    MessageBox.Show($"Playing: {action.Type} at ({action.X}, {action.Y}) with delay {action.Delay}ms");
                    Cursor.Position = new Point(action.X, action.Y);
                }
            }
        }
    }
}
