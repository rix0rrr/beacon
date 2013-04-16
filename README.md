# Beacon: automatic network discovery

*Beacon* is a small C# library that helps remove some of the user annoyances involved in writing client-server applications: finding the address of the server to connect to (because messing around putting in IP addresses is so much fun, eh?)

In that sense, it fills the same niche as other tools like Apple's *Bonjour*, but with minimal dependencies.

## How it works

UDP broadcasts, what else did you expect? :) It doesn't work outside your own network range without an external server (support for something like this could be added...!) but inside one network it works quite well.

The current Beacon implementation has support for local advertising based on application type (so different programs both using Beacon don't interfere with each other), and advertising server-specific data, which can be used to distinguish different servers from each other (for example, by presenting a display name to the user).

## Starting a Beacon server

Starting a beacon (to make a server discoverable) is simple:

    var beacon = new Beacon("myApp", 1234);
    beacon.BeaconData = "My Application Server on " + Dns.GetHostName();
    beacon.Start();

    // ...

    beacon.Stop();

`1234` is the port number that you want to advertise to clients. Typically, this is the port clients should connect to for your actual network service. You can fill in a bogus value if you don't need it.

## Scanning for servers using a Probe

Clients can find beacons using a `Probe`:

    var probe = new Probe("myApp");
    // Event is raised on separate thread so need synchronization
    probe.BeaconsUpdated += beacons => Dispatcher.BeginInvoke((Action)(() => {
        for (var beacon in beacons)
        {
            Console.WriteLine(beacon.Address + ": " + beacon.Data);
        }        
    }));

    probe.Start();
    
    // ...

    probe.Stop();

## Help wanted

This repository currently contains a simple C# implementation of the *Beacon* protocol. Implementations in other languages and for other platforms (Android, iOS, C++) are very welcome, as well as improvements to the protocol.