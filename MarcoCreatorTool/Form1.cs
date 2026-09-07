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
            RecordedAction action1 = new RecordedAction
            {
                Type = ActionType.MouseClick,
                X = 100,
                Y = 100,
                Delay = 500
            };

            RecordedAction action2 = new RecordedAction
            {
                Type = ActionType.KeyPress,
                Key = Keys.K,
                Delay = 500
            };

            // Add the action to the list
            recordedActions.Add(action1);
            recordedActions.Add(action2);
            MessageBox.Show($"Added: {action1.Type} at ({action1.X}, {action1.Y})");
            MessageBox.Show($"Added: {action2.Type} for key {action2.Key}");
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
                    await Task.Delay(action.Delay);
                    InputSimulator.Click(action.X, action.Y);
                }
                else if (action.Type == ActionType.KeyPress)
                {
                    // Key press action
                    await Task.Delay(action.Delay);
                    InputSimulator.KeyPress(action.Key);

                }
            }
        }
    }
}
