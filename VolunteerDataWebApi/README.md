# Volunteer Data Web API

This is a demo ASP.NET Web API implementation to provide backend data support for the Volunteer Bot. It provides a rudimentary data object model consisting of the following objects:

###Volunteer
The data associated with an individual registered to be a volunteer, such as name and contact information.
    
###Event
The data associated with an event that someone can volunteer themselves for, such as a donation drive or home visit.
    
###VolunteerIntent
Information from volunteers indicating which events they would like to help at.
    
###VolunteerActivity
Tracking information about the amount of time a volunteer actually contributed to an event.

More information about the Web API itself can be found through the automatically generated help page. To modify or update the help page generation in the project, refer to [Creating Help Pages for ASP.NET Web API](http://go.microsoft.com/fwlink/?LinkID=615543).

###NOTE: This Web API does not implement authentication or access controls on database methods. **Do NOT deploy this implementation as is.**

*To add authentication and security to the project:*
* [Creating web projects in Visual Studio with Authentication](http://go.microsoft.com/fwlink/?LinkID=320957)
* [Configure authentication](http://go.microsoft.com/fwlink/?LinkID=320962)
* [Secure your web API](http://go.microsoft.com/fwlink/?LinkID=615545)

*To deploy your own copy of this project:*
* [Ensure your app is ready for production](http://go.microsoft.com/fwlink/?LinkID=615534)
* [Using Microsoft Azure](http://go.microsoft.com/fwlink/?LinkID=615535)
* [Hosting providers](http://go.microsoft.com/fwlink/?LinkID=615536)

*To update and refine the data object model:*
* [Entity Framework (EF) documentation](https://msdn.microsoft.com/en-us/data/aa937723)

*Other resources for customizing this project:*
* [Get started with HTTP services using ASP.NET Web API](http://go.microsoft.com/fwlink/?LinkID=320959)
* [Change the site's theme](http://go.microsoft.com/fwlink/?LinkID=320960) 
* [Scaffold an ASP.NET Web API from a model](http://go.microsoft.com/fwlink/?LinkID=320963)
* [Access your web API on different devices](http://go.microsoft.com/fwlink/?LinkID=615544) 
* [Enable tracing for testing and debugging](http://go.microsoft.com/fwlink/?LinkID=615546)
* [Add real-time web with ASP.NET SignalR](http://go.microsoft.com/fwlink/?LinkID=615530)
* [Add components using Scaffolding](http://go.microsoft.com/fwlink/?LinkID=615531) 

This project depends on [NuGet](http://go.microsoft.com/fwlink/?LinkID=320961), which will restore the required dependency packages on build. 

