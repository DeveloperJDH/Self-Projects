using SharpDX.DirectInput;

    Console.WriteLine("It has started");
    // Initialize DirectInput
    var directInput = new DirectInput();

    // Find a Joystick Guid
    var joystickGuid = Guid.Empty;

    foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad,
                DeviceEnumerationFlags.AllDevices))
        joystickGuid = deviceInstance.InstanceGuid;

    // If Gamepad not found, look for a Joystick
    if (joystickGuid == Guid.Empty)
        foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick,
                DeviceEnumerationFlags.AllDevices))
            joystickGuid = deviceInstance.InstanceGuid;

    // If Joystick not found, throws an error
    if (joystickGuid == Guid.Empty)
    {
        Console.WriteLine("No joystick/Gamepad found.");
        Console.ReadKey();
        Environment.Exit(1);
    }

    // Instantiate the joystick
    var joystick = new Joystick(directInput, joystickGuid);

    Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", joystickGuid);

    // Query all suported ForceFeedback effects
    var allEffects = joystick.GetEffects();
    foreach (var effectInfo in allEffects)
        Console.WriteLine("Effect available {0}", effectInfo.Name);

    // Set BufferSize in order to use buffered data.
    joystick.Properties.BufferSize = 128;

    // Acquire the joystick
    joystick.Acquire();

    List<JoystickOffset> buttonInput = new List<JoystickOffset>();
    int i = 0;
// Poll events from joystick
    while (true)
    {
        joystick.Poll();
        var datas = joystick.GetBufferedData();

        foreach (var state in datas)
        {
        //Console.WriteLine("state: " + state);
        //buttonInput.Add(state.Offset.ToString());
        buttonInput.Add(state.Offset);
            i++;
            // Console.WriteLine("button: " + buttonInput.ElementAt(i - 1));
        }

        
            repeat(buttonInput, i, joystick);
    }


    static void repeat(List<JoystickOffset> buttonInput, int i, Joystick? joystick)
{
    if (buttonInput.Count > 0)
    {

        if (buttonInput.ElementAt(i - 1) == JoystickOffset.Buttons9)
        {

            Console.WriteLine("button 9 pressed.");
            int j = 0;
            foreach (var input in buttonInput)
            {
                //Console.WriteLine(buttonInput.ElementAt(j));
                j++;
            }
            Environment.Exit(0);
        }
            
    }
}