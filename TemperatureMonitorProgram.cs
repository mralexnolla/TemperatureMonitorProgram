using System;

// 1) Create a class to pass as an argument for the event handlers (EventArgs class)
public class TemperatureEventArgs : EventArgs
{
    public double CurrentTemperature { get; }

    public TemperatureEventArgs(double currentTemperature)
    {
        CurrentTemperature = currentTemperature;
    }
}

// 2) Set up the delegate for the event
public delegate void TemperatureExceededEventHandler(object sender, TemperatureEventArgs e);

// 3) Declare the code for the event
public class TemperatureSensor
{
    // Declare the event using the delegate
    public event TemperatureExceededEventHandler TemperatureExceeded;

    // Method to trigger the event when the temperature exceeds the limit
    protected virtual void OnTemperatureExceeded(double currentTemperature)
    {
        // If there are any subscribers to the event, raise the event
        if (TemperatureExceeded != null)
        {
            TemperatureExceeded(this, new TemperatureEventArgs(currentTemperature));
        }
    }

    // Method to check the temperature and raise the event if needed
    public void CheckTemperature(double temperature)
    {
        if (temperature > 30.0) // Temperature limit is 30.0 degrees
        {
            OnTemperatureExceeded(temperature);
        }
    }
}

// 4) Create code that will be run when the event occurs (Event Handler)
public class TemperatureMonitor
{
    // Event handler method that will be executed when the event is triggered
    public void HandleTemperatureExceeded(object sender, TemperatureEventArgs e)
    {
        Console.WriteLine($"Warning: Temperature exceeded! Current temperature is {e.CurrentTemperature} degrees.");
    }
}

// 5) Associate the event with the event handler
class Program
{
    static void Main(string[] args)
    {
        // Create instances of TemperatureSensor and TemperatureMonitor
        TemperatureSensor sensor = new TemperatureSensor();
        TemperatureMonitor monitor = new TemperatureMonitor();

        // Subscribe to the TemperatureExceeded event with the event handler
        sensor.TemperatureExceeded += monitor.HandleTemperatureExceeded;

        // Simulate checking the temperature - will trigger the event since 35.0 exceeds 30.0 degrees
        sensor.CheckTemperature(35.0);
    }
}