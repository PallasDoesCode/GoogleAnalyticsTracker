# GoogleAnalyticsTracker
[![Build status](https://ci.appveyor.com/api/projects/status/udlspav2sr7fu3dc/branch/master?svg=true)](https://ci.appveyor.com/project/RandomlyKnighted/googleanalyticstracker/branch/master)
[![Coverage Status](https://coveralls.io/repos/RandomlyKnighted/GoogleAnalyticsTracker/badge.svg?branch=master&service=github)](https://coveralls.io/github/RandomlyKnighted/GoogleAnalyticsTracker?branch=master)

GoogleAnalyticsTracker started as a simple C# wrapper developed by [Oliver Friedrich](https://gist.github.com/0liver/11229128). It was designed to help a [StackOverflow user with a problem](http://stackoverflow.com/a/23253778/1110819) they were having sending data to Google Analytics.

This project was created using the [Google Analytics Measurement Protocol documentation](https://developers.google.com/analytics/devguides/collection/protocol/v1/devguide) found on the Google Developers website.

#### **Event Tracking**
```csharp
Tracker tracker = new Tracker( "UA-XXXXXXXX-XX", "555" );
var url = tracker.GetEventTrackingUrl("Category", "Action", "Small Description");
tracker.Track(url);
```
#### **Page Tracking**

```csharp
Tracker tracker = new Tracker("UA-XXXXXXXX-XX", "555");
var url = tracker.GetPageviewTrackingUrl("http://mysite.com", "/home", "Home Page");
tracker.Track(url);
```

### **Note:**
It may take 2+ hours for the data to be reflect in your Google Analytics dashboard.
