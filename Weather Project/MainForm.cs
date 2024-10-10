using System.Diagnostics.Eventing.Reader;

namespace Weather_Project;

public partial class MainForm : Form
{
    List<Label> TempLabels;
    List<Label> RainLabels;
    List<Label> UVLabels;
    List<Label> SpeedLabels;
    List<Label> DirectionLabels;
    List<PictureBox> PictureBoxes;
    public MainForm()
    {
        InitializeComponent();
        LocationHelper.CreateLists();
        FillLists();
        comboBoxCountry.Items.AddRange(LocationHelper.GetCountries());
    }

    private void comboBoxCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        comboBoxCity.Items.Clear();
        comboBoxCity.Items.AddRange(LocationHelper.GetCities(comboBoxCountry.SelectedIndex));
    }

    private async void buttonWeather_Click(object sender, EventArgs e)
    {
        buttonLocation.Enabled = false;
        await Weather.GetForeCast(LocationHelper.GetLatLon(comboBoxCountry.SelectedIndex, comboBoxCity.SelectedIndex));
        panelLocation.Visible = false;
        panelWeatherToday.Visible = true;
        FillTodayWeather();
    }

    private void FillTodayWeather()
    {
        labelWeatherToday.Text = DateTime.Today.ToLongDateString();
        textBoxDateLocation.Text = $"{comboBoxCountry.Text}, {comboBoxCity.Text}";

        List<List<string>> forecast = Weather.GetCSV();
        for (int i = 0; i < TempLabels.Count; i++) TempLabels[i].Text = forecast[1][i];
        for (int i = 0; i < RainLabels.Count; i++) RainLabels[i].Text = forecast[2][i];
        for (int i = 0; i < UVLabels.Count; i++) UVLabels[i].Text = forecast[3][i];
        for (int i = 0; i < SpeedLabels.Count; i++) SpeedLabels[i].Text = forecast[4][i];

        for (int i = 0; i < DirectionLabels.Count; i++)
        {
            double bearing = Convert.ToDouble(forecast[5][i]);
            if (bearing <= 22.5 || bearing >= 337.5) DirectionLabels[i].Text = "N";
            else if (bearing > 22.5 && bearing < 67.5) DirectionLabels[i].Text = "NE";
            else if (bearing >= 67.5 && bearing <= 112.5) DirectionLabels[i].Text = "E";
            else if (bearing > 112.5 && bearing < 157.5) DirectionLabels[i].Text = "SE";
            else if (bearing >= 157.5 && bearing <= 202.5) DirectionLabels[i].Text = "S";
            else if (bearing > 202.5 && bearing < 247.5) DirectionLabels[i].Text = "SW";
            else if (bearing >= 247.5 && bearing <= 292.5) DirectionLabels[i].Text = "W";
            else if (bearing > 292.5 && bearing < 337.5) DirectionLabels[i].Text = "NW";
        }

        for (int i = 0; i < PictureBoxes.Count; i++)
        {
            PictureBoxes[i].Image = new Bitmap($"Weather Icons/{forecast[0][i]}.png");
        }

        foreach (Label lbl in UVLabels)
        {
            int idx = Convert.ToInt16(lbl.Text);
            if (idx < 3) { lbl.BackColor = Color.Green; lbl.ForeColor = Color.White; }
            else if (idx < 6) { lbl.BackColor = Color.Yellow; lbl.ForeColor = Color.Black; }
            else if (idx < 8) { lbl.BackColor = Color.Orange; lbl.ForeColor = Color.Black; }
            else if (idx < 11) { lbl.BackColor = Color.Red; lbl.ForeColor = Color.White; }
            else if (idx >= 11) { lbl.BackColor = Color.Purple; lbl.ForeColor = Color.White; }
            else { lbl.BackColor = Color.Transparent; lbl.ForeColor = Color.Black; }
        }
    }

    private void FillLists()
    {
        TempLabels = new()
        {
            labelTemp00, labelTemp01, labelTemp02, labelTemp03, labelTemp04, labelTemp05,
            labelTemp06, labelTemp07, labelTemp08, labelTemp09, labelTemp10, labelTemp11,
            labelTemp12, labelTemp13, labelTemp14, labelTemp15, labelTemp16, labelTemp17,
            labelTemp18, labelTemp19, labelTemp20, labelTemp21, labelTemp22, labelTemp23
        };
        RainLabels = new()
        {
            labelRain00, labelRain01, labelRain02, labelRain03, labelRain04, labelRain05,
            labelRain06, labelRain07, labelRain08, labelRain09, labelRain10, labelRain11,
            labelRain12, labelRain13, labelRain14, labelRain15, labelRain16, labelRain17,
            labelRain18, labelRain19, labelRain20, labelRain21, labelRain22, labelRain23
        };
        UVLabels = new()
        {
            labelUV00, labelUV01, labelUV02, labelUV03, labelUV04, labelUV05,
            labelUV06, labelUV07, labelUV08, labelUV09, labelUV10, labelUV11,
            labelUV12, labelUV13, labelUV14, labelUV15, labelUV16, labelUV17,
            labelUV18, labelUV19, labelUV20, labelUV21, labelUV22, labelUV23
        };
        SpeedLabels = new()
        {
            labelSpeed00, labelSpeed01, labelSpeed02, labelSpeed03, labelSpeed04, labelSpeed05,
            labelSpeed06, labelSpeed07, labelSpeed08, labelSpeed09, labelSpeed10, labelSpeed11,
            labelSpeed12, labelSpeed13, labelSpeed14, labelSpeed15, labelSpeed16, labelSpeed17,
            labelSpeed18, labelSpeed19, labelSpeed20, labelSpeed21, labelSpeed22, labelSpeed23
        };
        DirectionLabels = new()
        {
            labelDir00, labelDir01, labelDir02, labelDir03, labelDir04, labelDir05,
            labelDir06, labelDir07, labelDir08, labelDir09, labelDir10, labelDir11,
            labelDir12, labelDir13, labelDir14, labelDir15, labelDir16, labelDir17,
            labelDir18, labelDir19, labelDir20, labelDir21, labelDir22, labelDir23
        };
        PictureBoxes = new()
        {
            pictureBox00, pictureBox01, pictureBox02, pictureBox03, pictureBox04, pictureBox05,
            pictureBox06, pictureBox07, pictureBox08, pictureBox09, pictureBox10, pictureBox11,
            pictureBox12, pictureBox13, pictureBox14, pictureBox15, pictureBox16, pictureBox17,
            pictureBox18, pictureBox19, pictureBox20, pictureBox21, pictureBox22, pictureBox23
        };
    }

    private void buttonBack_Click(object sender, EventArgs e)
    {
        panelWeatherToday.Visible = false;
        panelLocation.Visible = true;
        buttonLocation.Enabled = true;
    }
}