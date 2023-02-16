// User class represents a user object
public class User
{
    // User's Id
    public int  Id { get; set; }
    // User's name (optional)
    public string ? Name { get; set; }
    // User's email address (optional)
    public string ? Email { get; set; }
}

// User Repository interface defines CRUD (create, read, update and delete) operations for users
public interface IUserRepository
{
    // Fetch all users
    IEnumerable<User> GetAll();
    // Gets the details of the specified user
    User Get(int id);
    // Adds a new user
    void Add(User user);
    // Updates an existing user's information
    void Update(User user);
    // Deletes the specified user
    void Delete(int id);
}

// The UserRepository class implements the IUserRepository interface and stores users in a List<User>
public class UserRepository : IUserRepository
{
    // Example of a List<User> to store users
    private readonly List<User> _users;

    // Constructor creates an empty user list
    public UserRepository()
    {
        _users = new List<User>();
    }

    // Fetching all users
    public IEnumerable<User> GetAll()
    {
        return _users;
    }

    // Returns the details of the specified user (user not found returns an empty User object instead of null)
    public User Get(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id) ?? new User();
    }

  // Adding a new user
    public void Add(User user)
    {
        // The new user's Id is assigned by adding 1 to the last user's Id in the user list
        user.Id = _users.Count + 1;
        _users.Add(user);
    }

   // Updates an existing user's information
    public void Update(User user)
    {
        // The user to be updated is found by Id using the Get method
        var existingUser = Get(user.Id);
        // Replace the existing user's name and email address with the user's name and email address to be updated
        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
    }

   // Deletes the specified user
    public void Delete(int id)
    {
        // The user to be deleted is found by ID using the Get method
        var user = Get(id);
        // Deleted from the user list
        _users.Remove(user);
    }
}

// The program class implements the main business logic of the code
class Program
{
    static void Main(string[] args)
    {
        // UserRepository example
        // First we create the UserRepository instance
        var repository = new UserRepository();
        
        // Adding the first user
        repository.Add(new User { Name = "Ahmet", Email = "ahmet@mail.com" });
        // Adding the second user
        repository.Add(new User { Name = "Mehmet", Email = "mehmet@mail.com" });
        
        // Getting and printing the information of the user with Id 1
        var ahmet = repository.Get(1);
        Console.WriteLine($"Name: {ahmet.Name}, Email: {ahmet.Email}");
        
        // Getting all users and printing their information
        var allUsers = repository.GetAll();
        foreach (var user in allUsers)
        {
            Console.WriteLine($"Name: {user.Name}, Email: {user.Email}");
        }
        // Updating the information of the user with Id 1
        repository.Update(new User { Id = 1, Name = "Ali", Email = "ali@mail.com" });
        
        // Getting and printing updated information of user with Id 1
        var ali = repository.Get(1);
        Console.WriteLine($"Name: {ali.Name}, Email: {ali.Email}");
    }
}
