using MarcoCreatorTool;
using System.Runtime.InteropServices;

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
            // Create new recorded action
            RecordedAction action = new RecordedAction
            {
                Type = ActionType.MouseClick,
                X = 100,
                Y = 100,
                Delay = 500
            };

            // Add the action to the list
            recordedActions.Add(action);
            MessageBox.Show($"Added: {action.Type} at ({action.X}, {action.Y})");
        }

        private async void play_Click(object sender, EventArgs e)
        {
            // If no actions were recorded, return
            if (recordedActions.Count == 0)
            {
                MessageBox.Show("No actions recorded.");
                return;
            }

            // Play back the recorded actions
            foreach (var action in recordedActions)
            {
                if (action.Type == ActionType.MouseClick)
                {
                    // Mouse click action
                    MessageBox.Show($"Playing: {action.Type} at ({action.X}, {action.Y}) with delay {action.Delay}ms");
                    await Task.Delay(action.Delay);
                    InputSimulator.Click(action.X, action.Y);
                }
                else if (action.Type == ActionType.KeyPress)
                {
                    // Key press action
                    MessageBox.Show($"Playing: {action.Type} with key {action.Key} and delay {action.Delay}ms");
                    await Task.Delay(action.Delay);

                }
            }
        }
    }
}
