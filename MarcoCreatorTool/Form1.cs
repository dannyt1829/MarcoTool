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
            await InputReplayer.ReplayActions(recordedActions);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            InputSaver.Save(recordedActions, "recorded_actions.json");
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            recordedActions = InputSaver.Load("recorded_actions.json");
        }
    }
}
