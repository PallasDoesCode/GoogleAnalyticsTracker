# GoogleAnalyticsTracker

GoogleAnalyticsTracker started as a simple C# wrapper developed by [Oliver Friedrich](https://gist.github.com/0liver/11229128). It was designed to help a [StackOverflow user with a problem](http://stackoverflow.com/a/23253778/1110819) they were having sending data to Google Analytics.

### Examples

Tracker tracker = new Tracker("UA-XXXXXXXX-XX", "555");
tracker.TrackEvent("Category", "Action", "Small Description");

Tracker tracker = new Tracker("UA-XXXXXXXX-XX", "555");
tracker.TrackPageview("Category", "Action", "Small Description");


**Note:**
It may take 2+ hours for the data to be reflect in your Google Analytics dashboard.
