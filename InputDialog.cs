using System;
using System.Windows.Forms;

public class InputDialog : Form
{
    private TextBox inputTextBox;
    private Button okButton;
    private Button cancelButton;
    private Label promptLabel;
    private FlowLayoutPanel flowLayoutPanel1;

    public string InputValue { get; private set; }

    public InputDialog(string title, string prompt)
    {
        // Set form properties
        this.Text = title;
        this.Width = 1000;
        this.Height = 300;
        this.StartPosition = FormStartPosition.CenterParent;
        this.FormBorderStyle = FormBorderStyle.Sizable; 
        this.MinimumSize = new System.Drawing.Size(500, 250);

        this.flowLayoutPanel1 = new FlowLayoutPanel();
        this.promptLabel = new Label();
        this.inputTextBox = new TextBox();
        this.okButton = new Button();
        this.cancelButton = new Button();
        this.flowLayoutPanel1.SuspendLayout();
        this.SuspendLayout();
        // 
        // flowLayoutPanel1
        // 
        this.flowLayoutPanel1.Controls.Add(this.promptLabel);
        this.flowLayoutPanel1.Controls.Add(this.inputTextBox);
        this.flowLayoutPanel1.Controls.Add(this.okButton);
        this.flowLayoutPanel1.Controls.Add(this.cancelButton);
        this.flowLayoutPanel1.Location = new Point(0, 0);
        this.flowLayoutPanel1.Name = "flowLayoutPanel1";
        this.flowLayoutPanel1.Dock = DockStyle.Fill;
        this.flowLayoutPanel1.TabIndex = 0;
        // 
        // promptLabel
        // 
        this.promptLabel.AutoSize = true;
        this.flowLayoutPanel1.SetFlowBreak(this.promptLabel, true);
        this.promptLabel.Location = new Point(3, 0);
        this.promptLabel.Name = "promptLabel";
        this.promptLabel.Size = new Size(97, 41);
        this.promptLabel.TabIndex = 0;
        this.promptLabel.Text = prompt;
        // 
        // inputTextBox
        // 
        this.flowLayoutPanel1.SetFlowBreak(this.inputTextBox, true);
        this.inputTextBox.Location = new Point(3, 56);
        this.inputTextBox.Name = "inputTextBox";
        this.inputTextBox.Size = new Size(250, 47);
        this.inputTextBox.TabIndex = 1;
        // 
        // okButton
        // 
        this.okButton.Location = new Point(3, 120);
        this.okButton.Name = "okButton";
        this.okButton.Size = new Size(188, 58);
        this.okButton.TabIndex = 2;
        this.okButton.DialogResult = DialogResult.OK;
        this.okButton.Text = "ok";
        this.okButton.Click += OkButton_Click;
        this.okButton.UseVisualStyleBackColor = true;
        // 
        // cancelButton
        // 
        this.cancelButton.Location = new Point(197, 120);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new Size(188, 58);
        this.cancelButton.TabIndex = 2;
        this.cancelButton.Text = "cancel";
        this.cancelButton.DialogResult = DialogResult.Cancel;
        this.cancelButton.UseVisualStyleBackColor = true;

        this.Controls.Add(this.flowLayoutPanel1);
        this.Text = "Form2";
        this.flowLayoutPanel1.ResumeLayout(false);
        this.flowLayoutPanel1.PerformLayout();
        this.ResumeLayout(false);

        // Set form accept and cancel buttons
        this.AcceptButton = okButton;
        this.CancelButton = cancelButton;

        // Handle resizing event to reposition controls properly
        this.Resize += InputDialog_Resize;
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        InputValue = inputTextBox.Text;
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void InputDialog_Resize(object sender, EventArgs e)
    {
        // Adjust the width of inputTextBox and buttons dynamically
        inputTextBox.Width = this.ClientSize.Width - 20;
        okButton.Left = this.ClientSize.Width - 190;
        cancelButton.Left = this.ClientSize.Width - 100;
    }

    // Method to show the dialog and return the entered string
    public static string ShowDialog(string title, string prompt)
    {
        using (InputDialog dialog = new InputDialog(title, prompt))
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.InputValue;
            }
            else
            {
                return "";
            }
        }
    }
}
