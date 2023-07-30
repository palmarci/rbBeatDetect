
# rbBeatDetect

![image](https://github.com/palmarci/rbBeatDetect/assets/20556689/9a423138-2347-4d83-8c1d-770344c1b812)

## What is this?
This is a simple program to synchronize lights to rekordbox via OSC messages. The program scans the memory of the currently running rekordbox.exe and tracks the Master Deck's beat values.

## How to use this?

1. Make sure you have [.NET 4.7.2 ](https://dotnet.microsoft.com/en-us/download/dotnetframework/net472 " .NET 4.7.2 ") or newer installed.
2. Download the latest release from [this](https://github.com/palmarci/rbBeatDetect/releases "this") page. 
3. Extract the contents to a directory and run the exe.
4. Run rekordbox and load a track to **every** deck.
5. Set the Master to a deck (any).
6. Run the program, configure your settings.
7. Check the "Running" box.

## FAQ
- **What does the Mimic human keypress button do?**

- Some OSC clients (like QLC+) needs an ON and an OFF signal to register a keypress. This is exactly what this switch does. You can specify a delay (in ms) between those messages.

 - **What happens if I have no internet access on my computer?**
   
The program won't be able to update the offsets on startup, so it will use the last cached version. I recommend trying this out for the first time with an internet connection. 
- **If I'm still running Windows 7?**
  
The offset download may fail due to the obsolete SSL/TLS settings on your system. You can easily fix those problems following [this](https://stackoverflow.com/a/70674920/8921786) guide. 

## How can i find the offsets?
- [Guide](https://github.com/palmarci/rbBeatDetect/blob/main/RekordboxMemoryScanning.pdf)

- [Offsets.json](https://raw.githubusercontent.com/palmarci/rbBeatDetect/main/offsets.json)

(do NOT use the offsets.txt file, its for the older versions only!)
