# GoogleAnalyticsTracker
[![Appveyor Build status](https://ci.appveyor.com/api/projects/status/udlspav2sr7fu3dc/branch/master?svg=true)](https://ci.appveyor.com/project/RandomlyKnighted/googleanalyticstracker/branch/master)
[![Travis CI Build Status](https://travis-ci.org/RandomlyKnighted/GoogleAnalyticsTracker.svg?branch=master)](https://travis-ci.org/RandomlyKnighted/GoogleAnalyticsTracker)
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

**Note:** The information on the methods below continue to be a work in progress and may not be completely accurate. 

#### **Social Interactions**
```csharp
Tracker tracker = new Tracker( "UA-XXXXXXXX-XX", "555" );
var url = tracker.GetSocialTrackingUrl("Category", "Action", "Small Description");
tracker.Track(url);
```
#### **Exception Tracking**

```csharp
Tracker tracker = new Tracker("UA-XXXXXXXX-XX", "555");
var url = tracker.GetExceptionTrackingUrl("http://mysite.com", "/home", "Home Page");
tracker.Track(url);
```

#### **User Timing Tracking**
```csharp
Tracker tracker = new Tracker( "UA-XXXXXXXX-XX", "555" );
var url = tracker.GetUserTimingTrackingUrl("Category", "Action", "Small Description");
tracker.Track(url);
```
#### **App/Screen Tracking**

```csharp
Tracker tracker = new Tracker("UA-XXXXXXXX-XX", "555");
var url = tracker.GetScreenviewTrackingUrl("http://mysite.com", "/home", "Home Page");
tracker.Track(url);
```

### **Ecommerce Tracking**

#### **Transaction**
```csharp
Tracker tracker = new Tracker( "UA-XXXXXXXX-XX", "555" );
var url = tracker.GetTransactionTrackingUrl("Category", "Action", "Small Description");
tracker.Track(url);
```
#### **Transaction Item**

```csharp
Tracker tracker = new Tracker("UA-XXXXXXXX-XX", "555");
var url = tracker.GetTransactionItemTrackingUrl("http://mysite.com", "/home", "Home Page");
tracker.Track(url);
```

### **Note:**
It may take 2+ hours for the data to be reflect in your Google Analytics dashboard.
