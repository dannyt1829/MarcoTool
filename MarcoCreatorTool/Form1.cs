using MarcoCreatorTool;
using System.Runtime.InteropServices;

namespace MarcoCreatorTool
{
    public partial class Form1 : Form
    {
        private bool _isRecording = false;
        private List<RecordedAction> recordedActions = new List<RecordedAction>();
        private InputRecorder inputRecorder = new InputRecorder();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void recordButton_Click(object sender, EventArgs e)
        {
            if (!_isRecording)
            {
                inputRecorder.Start();
                _isRecording = true;
                recordButton.Text = "Stop Recording";
            }
            else
            {
                inputRecorder.Stop();
                _isRecording = false;
                recordButton.Text = "Record";
                recordedActions = inputRecorder.GetRecordedActions();
            }
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
                await Task.Delay(action.Delay);
                switch (action.Type)
                {
                    case ActionType.MouseClick:
                        InputSimulator.Click(action.X, action.Y);
                        break;
                    case ActionType.KeyPress:
                        InputSimulator.KeyPress(action.Key);
                        break;
                    case ActionType.KeyDown:
                        InputSimulator.KeyDown(action.Key);
                        break;
                    case ActionType.KeyUp:
                        InputSimulator.KeyUp(action.Key);
                        break;
                    case ActionType.LMouseDown:
                        InputSimulator.LMouseDown(action.X, action.Y);
                        break;
                    case ActionType.LMouseUp:
                        InputSimulator.LMouseUp(action.X, action.Y);
                        break;
                    case ActionType.RMouseDown:
                        InputSimulator.RMouseDown(action.X, action.Y);
                        break;
                    case ActionType.RMouseUp:
                        InputSimulator.RMouseUp(action.X, action.Y);
                        break;
                }
            }
        }
    }
}
