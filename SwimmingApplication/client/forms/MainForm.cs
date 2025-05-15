using model;
using services;

namespace SwimmingApplication.forms;

public partial class MainForm : Form, ISwimmingObserver
{
    private readonly SwimmingClientController _swimmingClientController;
   

    public MainForm(SwimmingClientController swimmingClientController)
    {
        _swimmingClientController = swimmingClientController;
        InitializeComponent();

        LoadRaces();
        LoadParticipants();
        LoadMainTable();

        racesTable.SelectionChanged += RacesTable_SelectionChanged;
        participantsTable.SelectionChanged += ParticipantsTable_SelectionChanged;
    }

    private void LoadMainTable()
    {
        var registrations = _swimmingClientController.getRegistrations();
        mainTable.Rows.Clear();
        foreach (var item in registrations)
        {
            var partName = item.ParticipantId.Name;
            var raceStyle = item.RaceId.Style;
            var raceDist = item.RaceId.Distance;
            mainTable.Rows.Add(partName, raceStyle, raceDist);
        }
    }

    private void LoadRaces()
    {
        var races = _swimmingClientController.getRaces();
        racesTable.Rows.Clear();
        foreach (var race in races)
        {
            racesTable.Rows.Add(race.ID, race.Style, race.Distance);
        }
    }

    private void LoadParticipants()
    {
        var participants = _swimmingClientController.getParticipants();
        participantsTable.Rows.Clear();
        foreach (var participant in participants)
        {
            participantsTable.Rows.Add(participant.ID, participant.Name, participant.Age);
        }
    }

    private void RacesTable_SelectionChanged(object sender, EventArgs e)
    {
        if (racesTable.SelectedRows.Count > 0)
        {
            var selectedRow = racesTable.SelectedRows[0];
            string style = selectedRow.Cells["styleCol"].Value?.ToString() ?? "";
            string distance = selectedRow.Cells["distanceCol"].Value?.ToString() ?? "";

            raceStyleTextBox.Text = style;
            raceDistTextBox.Text = distance;
        }
    }

    private void ParticipantsTable_SelectionChanged(object sender, EventArgs e)
    {
        if (participantsTable.SelectedRows.Count > 0)
        {
            var selectedRow = participantsTable.SelectedRows[0];
            string name = selectedRow.Cells["nameCol"].Value?.ToString() ?? "";
            partNameTextBox.Text = name;
        }
    }

    private void addBtn_Click(object sender, EventArgs e)
    {
        if (participantsTable.SelectedRows.Count == 0 || racesTable.SelectedRows.Count == 0)
        {
            MessageBox.Show("❗ Please select both a participant and a race.");
            return;
        }

        int participantId = Convert.ToInt32(participantsTable.SelectedRows[0].Cells["participantIDCol"].Value);
        int raceId = Convert.ToInt32(racesTable.SelectedRows[0].Cells["raceIDCol"].Value);

        var participant = _swimmingClientController.getParticipantById(participantId);
        var race = _swimmingClientController.getRaceById(raceId);

        var registration = new ParticipantRace(participant, race);
        _swimmingClientController.addRegistration(registration);

        MessageBox.Show("✅ Registration added successfully.");
        LoadMainTable();
    }

    private void logOutBtn_Click(object sender, EventArgs e)
    {
        try
        {
            _swimmingClientController.logout();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Logout failed: " + ex.Message);
        }
        
        var loginForm = new Login(_swimmingClientController);
        loginForm.Show();
        this.Hide();
        
    }

    // --- Observer callback for live updates from server ---
    public void RegistrationMade(ParticipantRace registration)
    {
        // This is called from a background thread (Task.Run), so marshal to UI thread
        BeginInvoke(() =>
        {
            MessageBox.Show("🔔 A new registration was made!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadMainTable();
        });
    }
}
