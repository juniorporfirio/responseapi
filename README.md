# Using Response Api it's a great idea

A ResponseApi provides a standard, a reusable way to return multiple kinds of non-success responses from .NET services in a way that can easily be mapped to API responses types.

# Give a Star! ⭐
If you like or are using this project, please give it a star. Thanks!

# How can you use this ?
Imagine that you needs manipulate one method to return some kind of value. They might be creating something, persisting it and returning it.

Like this code :

```csharp

// Logic to get one people
public People Get(int Id)
{
  var people = _peopleRepository.One(Id);
  return people;
}
// Logic to create people
public People Create(string firstName, string lastName)
{
  var people = _peopleRepository.Create(firstName, lastName);
  return people;
}

```
What's happen if you needs validate something diferent of happy path:
* Validate if variable people is null
* Validate firstName, lastName with rules.
* Use FlueValidation
* Validate authorization
* Manipulate errors

You can use custom exceptions to manager this paths, but it's painful for the consumer.


# Fix using ResponseApi

Validate if people is not null return Success = 200 or NotFound = 404 to Api Response status code.
```csharp

// Logic to get one people
public ResponseApi<People> Get(int Id)
{
  var people = _peopleRepository.One(Id);
  return ResponseApi<People>.Against(people).IsNull();
}

```
Returning to Api Response status code Created = 201
```csharp

// Logic to create people

public ResponseApi<People> Create(string firstName, string lastName)
{
  var people = _peopleRepository.Create(firstName, lastName);
  return ResponseApi<People>.Created(people);
}

```
Validate if people is not exception return Success = 200 or Error = 500 to Api Response status code.
```csharp

// Logic to get one people
public ResponseApi<People> Get(int Id)
{
  var people = _peopleRepository.One(Id);
  return ResponseApi<People>.IsError(()=>people);
}
```
# Using package ResponseApi.FluentValidation

Validate if people is valid, return Success=200, or Invalid = 400 with messages of FluentValidation.

```csharp
public ResponseApi<People> Create(People people)
{
  var validator = new PeopleValidator();
  var validate = validator.Validate(people);
  return ResponseApi<People>.Against(people).IsInvalid(validate);
}
```

# Using package ResponseApi.AspNetCore
```csharp
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherForecastService _service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get() =>
            this.ToActionResult(_service.GetWeather());
    }
```
# Build Notes

Remember to update the PackageVersion in the .csproj file and then a build on master should automatically publish the new package to nuget.org.
Add a release with form 1.0.0 to GitHub Releases in order for the package to actually be published to Nuget. Otherwise it will claim to have been successful but it will be lying to you.

