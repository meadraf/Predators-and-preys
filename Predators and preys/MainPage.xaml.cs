namespace Predators_and_preys;

public partial class MainPage : ContentPage
{
	public int NumOfPredators { get; set; }
	public int MaxOfPredators { get; set; }
	public int NumOfPreys { get; set; }
	public int MaxOfPreys { get; set; }
	public int NumOfTurns { get; set; }
	public int MapSize { get; set; }
	
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnStartClicked(object sender, EventArgs e)
	{
        SemanticScreenReader.Announce(StartButton.Text);
    }

	private void OnMapSizeCheckedChanged(object sender, CheckedChangedEventArgs e)
	{
        var selectedRadioButton = ((RadioButton)sender);
		MapSize = Convert.ToInt32(selectedRadioButton.Value);
    }

    private void OnPredatorCheckedChanged(object sender, EventArgs e)
	{

	}

    private void OnPreyCountCheckedChanged(object sender, EventArgs e)
    {

    }

    void PredatorCountTextChanged(Object sender, TextChangedEventArgs e)
    {
		Entry entry = (Entry)sender;
    }
}