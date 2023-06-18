namespace Predators_and_preys;

public partial class MainPage : ContentPage
{
    private readonly MapConfig _mapConfig = new MapConfig();

    public MainPage()
    {
        InitializeComponent();

        PreysEntry.Placeholder = _mapConfig.MaxOfPreys.ToString();
        PreysValidator.MaximumValue = _mapConfig.MaxOfPreys;

        PredatorsEntry.Placeholder = _mapConfig.MaxOfPredators.ToString();
        PredatorsValidator.MaximumValue = _mapConfig.MaxOfPredators;
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        if (TurnsValidator.IsValid && PreysValidator.IsValid && PredatorsValidator.IsValid)
            await Navigation.PushAsync(new SimulationPage(_mapConfig));
    }

    private void OnMapSizeCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var selectedRadioButton = ((RadioButton) sender);
        _mapConfig.MapSize = Convert.ToInt32(selectedRadioButton.Value);

        PreysEntry.Placeholder = _mapConfig.MaxOfPreys.ToString();
        PreysValidator.MaximumValue = _mapConfig.MaxOfPreys;
        
        PredatorsEntry.Placeholder = _mapConfig.MaxOfPredators.ToString();
        PredatorsValidator.MaximumValue = _mapConfig.MaxOfPredators;
    }

    private void PredatorsEntry_OnCompleted(object sender, EventArgs e)
    {
        if (int.TryParse(PredatorsEntry.Text, out var predators))
        {
            if (predators > 0 || predators <= _mapConfig.MaxOfPredators)
                _mapConfig.NumOfPredators = predators;
        }
    }

    private void PreysEntry_OnCompleted(object sender, EventArgs e)
    {
        if (int.TryParse(PreysEntry.Text, out var preys))
        {
            if (preys > 0 || preys <= _mapConfig.MaxOfPreys)
                _mapConfig.NumOfPreys = preys;
        }
    }

    private void TurnsEntry_OnCompleted(object sender, EventArgs e)
    {
        if (int.TryParse(TurnsEntry.Text, out var turns))
        {
            if (turns > 0)
                _mapConfig.NumOfTurns = turns;
        }
    }
}