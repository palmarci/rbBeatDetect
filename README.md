# rbBeatDetect

![image](https://github.com/palmarci/rbBeatDetect/assets/20556689/9a423138-2347-4d83-8c1d-770344c1b812)

## What is this?
This is a simple program to synchronize lights to rekordbox via OSC messages.

## How does this work?
The program scans the memory of the currently running rekordbox.exe and tracks the Master Deck's beat count.

## How to run this?

1. Make sure you have[ .NET 4.7.2 ](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net472 " .NET 4.7.2 ")installed
2. Simply download the latest release from [this](https://github.com/palmarci/rbBeatDetect/releases "this") page. 
3. Extract the contents to a directory.
4. Run rekordbox and load a track to **every** deck.
5. Set the Master to a deck (any).
6. Run the program, configure your settings.
7. Check the "Running" box.


Note: some OSC clients (like QLC+) needs an ON and an OFF signal to register a keypress. This is exactly what the "Mimic human keypress" switch does. You can specify a delay (in ms) between those messages.


**If you don't have internet access on your computer** 

The program won't be able to update the offsets on startup, so it will use the last cached version. I recommend trying this out for the first time with an internet connection. 

## How can i find the offsets?
[Guide](https://github.com/palmarci/rbBeatDetect/blob/main/RekordboxMemoryScanning.pdf)

[Offsets.json](https://raw.githubusercontent.com/palmarci/rbBeatDetect/main/offsets.json)

(DO NOT use the offsets.txt file, its for the older versions only!)
