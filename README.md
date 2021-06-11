# ResponseApi
A package to reponse abstraction that mapped to HTTP response codes.

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
*  Validate if variable people is null
* Validate firstName, lastName with rules.
* Use FlueValidation
* Validate authorization
* Manipulate errors

One soluction to this approche, exceptions concerns to manager this paths, but it's painful for the consumer.

# Using Response Api it's a great ideia
A ResponseApi pattern provides a standard, a reusable way to return multiple kinds of non-success responses from .NET services in a way that can easily be mapped to API responses types.

# Show me code
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
Validate if people is not exception return Success = 200 or Invalid = 500 to Api Response status code.
```csharp

// Logic to get one people
public ResponseApi<People> Get(int Id)
{
  var people = _peopleRepository.One(Id);
  return ResponseApi<People>.IsError(()=>people);
}

```

