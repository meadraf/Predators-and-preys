﻿namespace Predators_and_preys;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute("Simulation", typeof(SimulationPage));
	}
}

